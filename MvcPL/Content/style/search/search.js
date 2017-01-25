$(function () {
    var autocompleteUrl = "/Forum/FindTopic/";
    $("input#topic_search").autocomplete({
        source: autocompleteUrl,
        minLength: 3,
        delay: 1000,
        select: function (event, ui) {
            window.location = "/Forum/Topic/" + ui.item.id;
        }
    });
});
