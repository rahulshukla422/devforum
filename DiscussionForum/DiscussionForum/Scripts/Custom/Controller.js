/// <reference path="../angular.js" />

debugger;
app.controller('postCtrl', function ($scope, CustomSerivce) {

    $scope.AddComment = function () {
        var commment = {
            commentDesc: $scope.commentDesc,
            postId: $scope.postIds
        }
        //alert('Service called and with description= ' + commment.commentDesc + ' and post id =' + commment.postId);

        var result = CustomSerivce.addComment(commment)
                                    .then(function (data) {
                                        alert('Comment added successfully');
                                    }, function () {
                                        alert('Alert occurred while adding the comment');
                                    });


    }
});