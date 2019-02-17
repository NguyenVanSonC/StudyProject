(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
    function productCategoryAddController($scope, apiService, notificationService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.parentCategories = [];

        $scope.AddProductCategory = AddProductCategory;
       
        function AddProductCategory() {
            apiService.post('api/productcategory/create',
                $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã được thêm');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công!');
                });
        }

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents',
                null,
                function(result) {
                    $scope.parentCategories = result.data;

                }, function() {
                    console.log('Can not get list parents');
                });
        }

        loadParentCategory();
    }
})(angular.module('tedushop.product_categories'));