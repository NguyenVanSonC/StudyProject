(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox','$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCagories = getProductCagories;
        $scope.keyword = '';

        $scope.search = search;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.SelectAll = SelectAll;
        $scope.IsAll = false;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected,
                function(i, item) {
                    listId.push(item.ID);
                });
            var config = {
                params: {
                    listId: JSON.stringify(listId)
                }
            }
            apiService.del('/api/productcategory/deletemulti',
                config,
                function(result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                    search();
                }, function(error) {
                    notificationService.displayError('Xóa không thành công');
                });
        }

        //$watch xem su thay doi cua cac doi tuong productCategories
        //if co ng-model productCategories.item.checked == true 
        $scope.$watch("productCategories",
            function (n, o) {
                //Bien checked lay 1 list(productCategories) cac item co ng-model checked ==true
                var checked = $filter("filter")(n, { checked: true });
                if (checked.length) {
                    //$scope.selected chi lay id
                    $scope.selected = checked;
                    $('#btnDelete').removeAttr('disabled');
                } else {
                    //.attr attribute: value
                    $('#btnDelete').attr('disabled', 'disabled');
                }
            },
            true);

        function SelectAll() {
            if ($scope.IsAll === false) {
                angular.forEach($scope.productCategories,
                    function(item) {
                        item.checked = true;
                    });
                $scope.IsAll = true;
            } else {
                angular.forEach($scope.productCategories,
                    function (item) {
                        item.checked = false;
                    });
                $scope.IsAll = false;
            }    
        }

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa không?').then(function() {
                var config = {
                    params: {
                        id: id
                    }
                }

                apiService.del('/api/productcategory/delete', config, function() {
                    notificationService.displaySuccess('Xóa thành công!');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công!')
                })
            });
        }

        function search() {
            getProductCagories();
        }

        function getProductCagories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            apiService.get('/api/productcategory/getall', config, function (result) {
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }

        $scope.getProductCagories();
    }
})(angular.module('tedushop.product_categories'));