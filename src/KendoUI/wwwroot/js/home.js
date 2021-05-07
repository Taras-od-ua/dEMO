
angular.module("todoApp", ["kendo.directives"]).controller("todoList", function ($scope) {
  $scope.onChange = function () {
    alert($scope.datePicker.value());
  };

  
  $scope.todos = [
    { text: 'first', done: true },
    { text: 'second', done: false }];

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

});




  
