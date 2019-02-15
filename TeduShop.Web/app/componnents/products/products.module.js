/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tedushop.products', ['tedushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/componnents/products/productListView.html",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            templateUrl: "/app/componnents/products/productAddView.html",
            controller: "productAddController"
        });
    }
})();