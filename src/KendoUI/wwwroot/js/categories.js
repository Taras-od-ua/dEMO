
angular.module("todoApp", ["kendo.directives"]).controller("categories", function ($scope) {

  var crudServiceBaseUrl = "https://demos.telerik.com/kendo-ui/service";

  $scope.treelistOptions = {
    dataSource: {
      transport: {
        read: {
          url: crudServiceBaseUrl + "/EmployeeDirectory/All",
          dataType: "jsonp"
        },
        update: {
          url: crudServiceBaseUrl + "/EmployeeDirectory/Update",
          dataType: "jsonp"
        },
        destroy: {
          url: crudServiceBaseUrl + "/EmployeeDirectory/Destroy",
          dataType: "jsonp"
        },
        create: {
          url: crudServiceBaseUrl + "/EmployeeDirectory/Create",
          dataType: "jsonp"
        },
        parameterMap: function (options, operation) {
          if (operation !== "read" && options.models) {
            return { models: kendo.stringify(options.models) };
          }
        }
      },
      schema: {
        model: {
          id: "EmployeeId",
          parentId: "ReportsTo",
          fields: {
            EmployeeId: { type: "number", editable: false, nullable: false },
            ReportsTo: { nullable: true, type: "number" },
            FirstName: { validation: { required: true } },
            LastName: { validation: { required: true } },
            HireDate: { type: "date" },
            Phone: { type: "string" },
            HireDate: { type: "date" },
            BirthDate: { type: "date" },
            Extension: { type: "number", validation: { min: 0, required: true } },
            Position: { type: "string" }
          }
        }
      }
    },
    sortable: true,
    editable: true,
    toolbar: ["create", "save", "cancel"],
    
    columns: [
      { field: "Name", title: "Name", width: "150px" },
      { field: "Priority", title: "Priority", width: "150px" },
      { field: "Position" },
     /* {
        title: "Location",
        template: "{{ dataItem.City }}, {{ dataItem.Country }}"
      },*/
      { command: ["edit","destroy"] }
    ]
  };

});





  
