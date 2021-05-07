(function (angular) {
  "use strict";
  angular.module('todoApp.Service', []).service('Service',["$rootScope", "$q", "$http", function ($rootScope, $q, $http) {
    return {

      getCategories: getCategories,
    };

    function get(url, params) {
      var defer = $q.defer();
      //$rootScope.$broadcast('beginRequest');
      $http({
        method: "GET",
        url: url,
        params: params
      })
        .then(
          function (res) {
        //    $rootScope.$broadcast('endRequest');
            defer.resolve(res.data);
          },
          function (err) {
          //  $rootScope.$broadcast('endRequest');
            defer.reject(err);
          }
        )
        .finally(function () {
          /* Just by adding a finally clause on $http, the finally
              handler in the chain i.e. in the controller utilizing
              the restService, does get called.*/
        });

      return defer.promise;;
    }

    function post(url, params) {
      var defer = $q.defer();
      $rootScope.$broadcast('beginRequest');
      $http({
        method: "POST",
        url: url,
        data: params,
        headers: headers,
        withCredentials: withCredentials,
        params: (params && params.omitLoader) ? params : null
      })
        .then(
          function (res) {
            $rootScope.$broadcast('endRequest');
            if (!res.data) {
              if (res.data.errors) {
                alert(res.data.errors.join(', '));
                defer.reject(res.data.errors);
              } else {
                defer.resolve();
              }
            } else {
              defer.resolve(res.data);
            }
          },
          function (err) {
            $rootScope.$broadcast('endRequest');
            defer.reject(err.data);
          }
        )
        .finally(function () {
          /*
          Just by adding a finally clause on $http, the finally
          handler in the chain i.e. in the controller utilizing
          the restService, does get called.
          */
        });

      defer.promise.resolveRequest = function () {
        defer.resolve(true);
      };

      return defer.promise;
    }

    function del(url, params) {
      var defer = $q.defer();
      $rootScope.$broadcast('beginRequest');
      $http({
        method: 'DELETE',
        url: url,
        data: params
      })
        .then(
          function (res) {
            $rootScope.$broadcast('endRequest');
            defer.resolve(res.data.data || res.data);
          },
          function (err) {
            $rootScope.$broadcast('endRequest');
            defer.reject(err);
          }
        )
        .finally(function () {
          // Just by adding a finally clause on $http, the finally
          // handler in the chain i.e. in the controller utilizing
          // the restService, does get called.
        });

      return defer.promise;
    }

    function put(url, params) {
      var defer = $q.defer();
      $rootScope.$broadcast('beginRequest');
      $http({
        method: "PUT",
        url: url,
        data: params
      })
        .then(
          function (res) {
            $rootScope.$broadcast('endRequest');
            if (!res.data) {
              if (res.data.errors) {
                alert(res.data.errors.join(', '));
                defer.reject(res.data.errors);
              } else {
                defer.resolve();
              }
            } else {
              defer.resolve(res.data);
            }
          },
          function (err) {
            $rootScope.$broadcast('endRequest');
            defer.reject(err.data);
          }
        )
        .finally(function () {
          /* Just by adding a finally clause on $http, the finally
           handler in the chain i.e. in the controller utilizing
           the restService, does get called.*/
        });

      return defer.promise;
    }





    function getCategories() {
      return get(config.urls.getCategoriesAPI);
    }


    function updateCategory(params) {
      return post(config.urls.updatecategoryAPI, params);
    }

  }]);

})(angular);

