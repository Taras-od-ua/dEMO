
angular.module("todoApp", ["kendo.directives"]).controller("dashboardController", function ($scope, $templateCache) {
 
  
  $scope.todos = [
    { text: 'first', done: true },
    { text: 'second', done: false }
  ];
  $scope.listOptions = listOptions;

  $scope.categories = [
    {
      id:1,
      name: 'default',
      todos: [
        {
          name: 'shop',
          list: [{ text: 's1', done: true }, { text: 's2', done: false }]
        },
        {
          name: 'visit',
          list: [{ text: 'v1', done: true }, { text: 'v2', done: false }]
        },
        {
          name: 'do',
          list: [{ text: 'd1', done: true }, { text: 'd2', done: false }, { text: 'd3', done: false }]
        }

      ]
    },
    {
      id: 2,
      name: 'urgent',
      todos: [
        {
          name: 'pharmacy',
          list: [{ text: 'f1', done: true }, { text: 'f2', done: false }]
        },
        {
          name: 'school',
          list: [{ text: 's1', done: true }, { text: 's2', done: false }]
        }
      ]
    }
  ];

  $scope.addTodo = function () {
    $scope.todos.push({ text: $scope.todoText, done: false });
    $scope.todoText = '';
  };

  $scope.remaining = function () {
    var count = 0;
    angular.forEach($scope.todos, function (todo) {
      count += todo.done ? 0 : 1;
    });
    return count;
  };

  $scope.archive = function () {
    var oldTodos = $scope.todos;
    $scope.todos = [];
    $scope.forEach(oldTodos, function (todo) {
      if (!todo.done)
        $scope.todos.push(todo);
    });
  };
/*Options*/

  function listOptions(todos) {
    var arr = $scope.categories.map(function (a) { return "#list"+a.id; });
    var connect = arr.join(", ");

    var datasource = new kendo.data.DataSource({
        transport: {
          read: function (options) {

            /*DashboardsService.loadDashboardWidgets(dashboardId, areaNo).then(function (data) {
              $.each(data.data, function (key, dash) {
                dash.dashboardType = type;
              });
              options.success(data);
            });*/

           
            options.success(todos);
          },
          /*parameterMap: function (data, operation) {
            return JSON.stringify(data);
          }*/
        },
        //sort: { field: "posY", dir: "asc" },
        //schema: {
          //data: "data", /*<-- this is the field from the response which contains the actual data*/
        //},
        /*requestStart: function (e) {
          kendo.ui.progress($("#ListView_" + areaNo + "_" + dashboardId), true);
        },
        requestEnd: function (e) {
          kendo.ui.progress($("#ListView_" + areaNo + "_" + dashboardId), false);

          (e.response && e.response.data.length < 1) ? $("#addWidgetButtron" + areaNo + "_" + dashboardId).show()
            : $("#addWidgetButtron" + areaNo + "_" + dashboardId).hide();
          var columns = parseInt($("#mainPanel").attr("data-total-columns"));
          if (!isNaN(columns)) totalLayoutColumns = columns;
        }*/
      });

    
    return {
      dataSource: datasource,
      /*template: function (e) {
        var t = "";

        t = $templateCache.get('todo.html');

        return t;
      },*/
      template: kendo.template($("#template").html()),


     /* start: function (e) {
        if (!allowDashboardEdit()) {
          e.preventDefault();
        }
      },
      change: function (e) {
        $scope.widgetAreaChanged(e);
      },*/
      
      
      placeholder: function (element) {
        return element.clone().css("opacity", 0.1);
      },
      hint: function (element) {
        return element.clone().removeClass("k-state-selected");
      },
      change: function (e) {
        /*var skip = this.dataSource.skip(),
          oldIndex = e.oldIndex + skip,
          newIndex = e.newIndex + skip,
          data = dataSource.data(),
          dataItem = dataSource.getByUid(e.item.data("uid"));

        dataSource.remove(dataItem);
        dataSource.insert(newIndex, dataItem);*/
      },
      filter: ".k-listview-content > div.product",
      ignore: 'input',
      cursor: "move",
      autoScroll: "true",
      connectWith: connect

    };
  }
  
});




  
