(function (angular) {
  "use strict";


  angular.module("todoApp", ["kendo.directives"]).controller("categories", function ($rootScope, $scope, $q, $http, $templateCache) {

  var baseUrl = "http://localhost:5000/api/";

  function get(url, params) {
    var defer = $q.defer();
    $rootScope.$broadcast('beginRequest');
    $http({
        method: "GET",
      url: url,
       
        params: params
      })
      .then(
        function (res) {
          $rootScope.$broadcast('endRequest');
          defer.resolve(res.data);
        },
        function (err) {
          $rootScope.$broadcast('endRequest');
          defer.reject(err);
        }
      )
      .finally(function () {
        /* Just by adding a finally clause on $http, the finally
            handler in the chain i.e. in the controller utilizing
            the restService, does get called.*/
      });

    return defer.promise;
  }

  $scope.addCategory = function(e) {

    $scope.listView.add();

    e.preventDefault();
    };

  function getCategories() {
    return get(baseUrl+"categories");
  }
    
  $scope.listOptions = {
    dataSource: new kendo.data.DataSource({
      transport: {
          read: function(options) {
            getCategories().then(function(res) {
              options.success(res);

            });
        },
        update: function (options) {
          var model = $.extend({}, options.data.models[0]);
          model.color = '#'+kendo.parseColor(model.color).toHex();
            $.ajax({
              url: baseUrl + "categories/" + model.id,
              type: 'PUT',
              data: JSON.stringify(model),
              dataType: 'json',
              contentType: 'application/json',
              success: function(result) {
                options.success(result);
              }
            })
        },

        destroy: function (options) {
          var model = $.extend({}, options.data.models[0]);
            $.ajax({
              url: baseUrl + "categories/" + model.id,
              type: 'DELETE',
              success: function (result) {
                options.success(result);
              }
            })
          },

        create: function (options) {
          var model = $.extend({}, options.data.models[0]);
          model.id = 0;
          model.color ='#'+ kendo.parseColor(model.color).toHex();
            $.ajax({
              url: baseUrl + "categories",
              type: 'POST',
              data: JSON.stringify(model),
              dataType: 'json',
              contentType: 'application/json',
              success: function(result) {
                options.success(result);
              }
            });
          },
          parameterMap: function (options, operation) {
            if (operation !== "read" && options.models) {
              return { models: kendo.stringify(options.models) };
            }
          }
      },
      batch: true,
      
      schema: {
        model: {
          id: "id",
          fields: {
            id: { editable: false, nullable: false },
            title: { editable: true, nullable: false },
            color: { editable: true, nullable: false },
           
          }
        }
      }
    }),
    template: kendo.template($("#template").html()),
    editTemplate: kendo.template($("#editTemplate").html())
  };

});

})(angular);



  
