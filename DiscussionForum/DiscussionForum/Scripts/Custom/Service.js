/// <reference path="../angular.js" />
app.service('CustomSerivce', function ($http) {

    debugger;

    this.addComment = function (commment) {

        var response = $http({
            method: 'post',
            url: '/Post/AddCommments',
            data: JSON.stringify(commment),
            dataType:'json'
        });
        return response;
    }

});