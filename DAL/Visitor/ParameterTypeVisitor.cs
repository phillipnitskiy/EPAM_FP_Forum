using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Visitor
{
    public static class ExpressionVisitorHelpers
    {
        public static Expression<Func<TTo, bool>> Convert<TFrom, TTo>(this Expression<Func<TFrom, bool>> root)
        {
            var visitor = new ParameterTypeExpressionVisitor<TFrom, TTo>();
            return (Expression<Func<TTo, bool>>)visitor.Visit(root);
        }
    }

    public class ParameterTypeExpressionVisitor<TFrom, TTo> : ExpressionVisitor
    {
        private ReadOnlyCollection<ParameterExpression> _parameters;

        public static Func<TTo, bool> Convert(Expression<Func<TFrom, bool>> root)
        {
            var visitor = new ParameterTypeExpressionVisitor<TFrom, TTo>();
            var expression = (Expression<Func<TTo, bool>>)visitor.Visit(root);
            return expression.Compile();
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return (_parameters != null) ? _parameters.FirstOrDefault(p => p.Name == node.Name) :
            (node.Type == typeof(TFrom) ? Expression.Parameter(typeof(TTo), node.Name) : node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            _parameters = VisitAndConvert(node.Parameters, "VisitLambda");
            return Expression.Lambda(Visit(node.Body), _parameters);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TFrom))
            {
                return Expression.MakeMemberAccess(Visit(node.Expression), typeof(TTo).GetProperty(node.Member.Name));
            }
            return base.VisitMember(node);
        }
    }
}
