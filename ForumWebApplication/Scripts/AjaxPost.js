$(document).ready(function () {
    $('.likeButton').click(function () {
        var but = $(this)
        $.ajax(
            {
                url: '/Post/Like',
                settings: { type: 'POST' },
                type: 'POST',
                data: { 'strId': $(this).attr('id'), }
            }).done(function (data) {
                but.val('Likes:  ' + data);
            });
        return false;
    });

    $('.dislikeButton').click(function () {
        var but = $(this)
        $.ajax(
            {
                url: '/Post/Dislike',
                settings: { type: 'POST' },
                type: 'POST',
                data: { 'strId': $(this).attr('id'), }
            }).done(function (data) {
                but.val('Dislikes:  ' + data);
            });
        return false;
    });

    $('.responseButton').click(function () {
        var divClass = $(this).parent().attr('class');
        var divId = $(this).parent().attr('id');
        $.ajax(
            {
                url: '/Post/CreateResponse',
                settings: { type: 'POST' },
                type: 'POST',
                data: { 'topicId': divClass, 'postId': divId }
            }).done(function (response) {
                divId = '#' + divId;
                $(divId).append(response);
            });
        return false;
    });

    $('.BlockButton').click(function () {
        var postId = $(this).attr('name');
        $.ajax(
            {
                url: '/Post/Block',
                settings: { type: 'POST' },
                type: 'POST',
                data: { 'strPostId': postId, }
            });
        var divClass = '.' + postId;
        $(divClass).empty();
        $(divClass).append("Сообщение заблокированно модераторами форума.");
        return false;
    });

});