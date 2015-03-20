
function arguments_array(arguments){
  return Array.prototype.slice.call(arguments);
}

String.prototype.reverse = function(){
  return this.split('').reverse().join('');
}
