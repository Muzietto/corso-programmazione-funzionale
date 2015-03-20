/////////// TUPLES ////////////
function couple(a,b){
  return function(w){
    return w(a,b);
  }
}

function tuple(){
  var args = arguments_array(arguments);
  return function(w){
    return w.apply(null,args);
  }
}

function first(tuple){
  return tuple(function(){
    return arguments_array(arguments)[0];
  })
}

function second(tuple){
  return tuple(function(){
    return arguments_array(arguments)[1];
  })
}

function n_th(tuple, pos){
  return tuple(function(){
    return arguments_array(arguments)[pos - 1];
  })
}

///////// LIST ////////////////
function list(){
  return arguments_array(arguments);
}

function head(list){
  if (!list.slice) throw new Error('not a list');
  if (list.length === 0) return null;
  return list[0];
}

function tail(list){
  if (!list.slice) throw new Error('not a list');
  if (list.length === 0) return null;
  return list.slice(1);
}

function last(list){
  if (!list.slice) throw new Error('not a list');
  return list[list.length - 1];
}

function isEmpty(list){
  return (typeof list !== "undefined" && list !== null && list.length === 0);
}

////////// OPTION (aka MAYBE) ////////////
function some(item){
  if (item === null
   || item === NaN
   || item === Infinity
   || typeof item === 'undefined') {
    return none();
  }
  var result = function(w){
    return w(item);
  }
  result.match = function(s,n){
    return s(item);
  }
  return result;
}

function none(){
  var result = function(w){
    return w(null);
  }
  result.match = function(s,n){
    return n();
  }
  return result;
}

function maybe_fmap(maybe,fab){
  return maybe.match(
    function(i){ 
      try {
        return some(fab(i));
      } catch (e) {
        return none();
      }
    },
    function(){ return maybe; }
  );
}

function isSome(maybe){
  return maybe.match(
    function(){ return true; },
    function(){ return false; }
  );
}
function isNone(maybe){
  return maybe.match(
    function(){ return false; },
    function(){ return true; }
  );
}

