$(document).ready(function () {

    $("[name='NewTopic']").click(function () {
        $.ajax({
            url: '/Home/CreateTopicForm',
            settings: { type: 'POST' },
            type: 'POST',
        }).done(function (response) {
            $('#ForNewTopic').empty();
            $('#ForNewTopic').append(response);
        });
        return false;
    });

});