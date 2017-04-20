using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Helpers;

namespace ORM.Initializers
{
    public class ForumDbInitializer : CreateDatabaseIfNotExists<EntityModel>
    {

        protected override void Seed(EntityModel context)
        {

            #region Roles
            var roles = new List<Role>
            {
                new Role { Name = "Administrator", Description = "Administrator" },
                new Role { Name = "Moderator", Description = "Moderator" },
                new Role { Name = "User", Description = "User" }
            };
            #endregion

            context.Roles.AddRange(roles);

            #region Users
            var users = new List<User>
            {
                new User
                {
                    Roles = roles,
                    Login = "admin",
                    Email = "admin@gmail.com",
                    Password = Crypto.HashPassword("111111"),
                    RegistrationDate = DateTime.Now
                },
                new User
                {
                    Roles = new List<Role> { roles[2] },
                    Login = "phil",
                    Email = "phil@gmail.com",
                    Password = Crypto.HashPassword("111111"),
                    RegistrationDate = DateTime.Now
                }
            };
            #endregion

            context.Users.AddRange(users);

            #region Boards
            var boards = new List<Board>
            {
                new Board
                {
                    Name = "Общество",
                    Description = "Человек среди людей"
                },
                new Board
                {
                    Name = "Экономика и бизнес",
                    Description = "Экономика, бизнес, работа"
                },
                new Board
                {
                    Name = "Культура и исскуство",
                    Description = "Кино и архитектура, музыка и живопись, музеи и театры"
                },
                new Board
                {
                    Name = "Образование и наука",
                    Description = "Вузы, школы, исследования"
                },
                new Board
                {
                    Name = "Строительство и недвижимость"
                },
                new Board
                {
                    Name = "Развлечения и отдых",
                    Description = "Досуг, хобби..."
                },
                new Board
                {
                    Name = "Форум"
                }
            };
            #endregion

            #region SubBoards Общество
            boards.Find(b => b.Name == "Общество")
                .SubBoards = new List<Board>
                {
                    new Board
                    {
                        Name = "Народ руководит",
                        Description = "Экономические, политические и социальные проблемы Беларуси. Не ругаем, а предлагаем."
                    },
                    new Board
                    {
                        Name = "Экология",
                        Description = "Место для обсуждения всех экологических факторов, влияющих на качество жизни человека."
                    },
                    new Board
                    {
                        Name = "Потребитель",
                        Description = "Обсуждаем проблемы сфер торговли и платных услуг, а также варианты их решения."
                    },
                    new Board
                    {
                        Name = "Законодательство и право",
                        Description = "Права, законы, советы юристов..."
                    },
                    new Board
                    {
                        Name = "Города и регионы",
                        Description = "О нашей Родине и не только..."
                    },
                    new Board
                    {
                        Name = "Сми",
                        Description = "телевидение, радио, газеты, журналы (бумажные и онлайновые) - все СМИ обсуждаем здесь."
                    }
                };
            #endregion

            #region SubBoards Экономика и бизнес
            boards.Find(b => b.Name == "Экономика и бизнес")
                .SubBoards = new List<Board>
                {
                    new Board
                    {
                        Name = "Экономика и маркетинг"
                    },
                    new Board
                    {
                        Name = "Личные финансы"
                    },
                    new Board
                    {
                        Name = "Трудоустройство"
                    },
                    new Board
                    {
                        Name = "Бухгалтерия"
                    }
                };
            #endregion

            #region SubBoards Культура и исскуство
            boards.Find(b => b.Name == "Культура и исскуство")
                .SubBoards = new List<Board>
                {
                    new Board
                    {
                        Name = "Кино"
                    },
                    new Board
                    {
                        Name = "Исскуство"
                    },
                    new Board
                    {
                        Name = "Театр"
                    },
                    new Board
                    {
                        Name = "Музыка"
                    },
                    new Board
                    {
                        Name = "Литература"
                    }
                };
            #endregion

            #region SubBoards Образование и наука
            boards.Find(b => b.Name == "Образование и наука")
                .SubBoards = new List<Board>
                {
                    new Board
                    {
                        Name = "Образование"
                    },
                    new Board
                    {
                        Name = "Наука"
                    }
                };
            #endregion

            #region SubBoards Строительство и недвижимость
            boards.Find(b => b.Name == "Строительство и недвижимость")
                .SubBoards = new List<Board>
                {
                    new Board
                    {
                        Name = "Недвижимость"
                    },
                    new Board
                    {
                        Name = "Строительство"
                    },
                    new Board
                    {
                        Name = "Дизайн"
                    }
                };
            #endregion

            #region SubBoards Развлечения и отдых
            boards.Find(b => b.Name == "Развлечения и отдых")
                .SubBoards = new List<Board>
                {
                    new Board
                    {
                        Name = "Спорт"
                    },
                    new Board
                    {
                        Name = "Хобби и досуг"
                    },
                    new Board
                    {
                        Name = "Путешествия и туризм"
                    },
                    new Board
                    {
                        Name = "Игры"
                    },
                    new Board
                    {
                        Name = "Фотография"
                    }
                };
            #endregion

            #region SubBoards Форум
            boards.Find(b => b.Name == "Форум")
                .SubBoards = new List<Board>
                {
                    new Board
                    {
                        Name = "F.A.Q."
                    },
                    new Board
                    {
                        Name = "Новости"
                    }
                };
            #endregion

            #region Topics
            boards.Find(b => b.Name == "Общество")
                .Topics = new List<Topic>
                {
                    new Topic
                    {
                        User = users[0],
                        Subject = "Мигранты",
                        CreationDate = DateTime.Now,
                        Posts = new List<Post>
                        {
                            new Post
                            {
                                User = users[0],
                                Text = "Мое мнение о Ждановичах и скандалах иже с ними в сми. Эмиграция наших проблем не решит по одной простой причине: Беларусь как самостоятельное государство у нас ассоциируется с нашим народом. Нужно, чтобы оно сохранилось для нашего народа. Некоторые демографы даже не включают миграцию в параметры демографических исследований, потому что это просто смена одного народа другим. Миграционные группы, как правило, не ассимилируются. Сохранение государства – это не только сохранение границ. Это сохранение народа, веры, традиций, обычаев и менталитета. Миграционные потоки, которые приходят на территорию другого государства, не воспринимают эти нормы. Они несут свои ценности. Как правило, представители других национальностей селятся диаспорами и через какое-то время их становится много, и они начинают требовать себе преимущества. Во-вторых, миграция порождает ряд проблем: рост преступности, наркомании, рост всевозможных этнических столкновений. С одной стороны у коренного населения зреет недовольство, сформированное СМИ, я бы сказала настороженность в отношении мигрантов. Тезис «понаехали тут» стал уже едва ли не крылатым выражением, которое отражает мировоззрение жителя любого большого города.",
                                CreationDate = DateTime.Now
                            }
                        }
                    }
                };
            #endregion

            context.Boards.AddRange(boards);

            context.SaveChanges();

            base.Seed(context);
        }

    }
}
