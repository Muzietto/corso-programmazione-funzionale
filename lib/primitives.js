'use strict';
/////////// TUPLES ////////////
function couple(a,b){
  return function(w){
    return w(a,b);
  };
}

function tuple(){
  var args = arguments_array(arguments);
  return function(w){
    return w.apply(null,args);
  };
}

function first(tuple){
  return tuple(function(){
    return arguments_array(arguments)[0];
  });
}

function second(tuple){
  return tuple(function(){
    return arguments_array(arguments)[1];
  });
}

function n_th(tuple, pos){
  return tuple(function(){
    return arguments_array(arguments)[pos - 1];
  });
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
  if (item === null || item === NaN || item === Infinity || typeof item === 'undefined') {
    return none();
  }
  var result = function(w){
    return w(item);
  };
  result.match = function(s,n){
    return s(item);
  };
  return result;
}

function none(){
  var result = function(w){
    return w(null);
  };
  result.match = function(s,n){
    return n();
  };
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

function value(option){
  return option(function(x){ return x; });
}


//////// TAGGED VALUE ////////
function Int(val) {
  return function(w) {
    return w('int', val);
  };
}

function Float(val) {
  return function(w) {
    return w('float', val);
  };
}

function Bool(val) {
  return function(w) {
    return w('bool', val);
  };
}

//////// BINARY SEARCH TREE ////////
function node(label,left,right){
  return function(nodeFun,leafFun){
    return nodeFun(label,left,right);
  };
}

function empty(){
  return function(nodeFun,leafFun){
    return leafFun ? leafFun() : null;
  };
}

// sample outer operator
function isEmptyLeaf(tree){
  return tree(
    function(){ return false; },
    function(){ return true; }
  );
}

// sample inner operators
function label(lbl,lf,rt){
  return lbl;
}
function left(lbl,lf,rt){
  return lf;
}
function right(lbl,lf,rt){
  return rt;
}

// TODO - express this as foldl(tree,foldFun,start)
function printTree(tree){
  return tree(
    function(label,left,right){
      return 'node(' + label + ',' + printTree(left) + ',' + printTree(right) + ')';
    },
    function(){ return 'empty()'; }
  );
}

/////////////// FOLDS OVER ARRAYS /////////////
function foldl(array,foldFun,start) {
  return helper(0,start);
  function helper(pos,acc){
    if (pos === array.length) return acc;
    return helper(pos + 1,foldFun(acc,array[pos]));
  }
}

function fold(array, foldFun, end){
  return helper(array.length, end);
  function helper(pos, acc) {
    if (pos === 0) return acc;
    return helper(pos - 1, foldFun(acc, array[pos - 1]));
  }
}

//////// MAP ////////
var map = (function() {
  'use strict';

  function empty() {
  return {};
  }

  function ofList(listOfArray) {
    return listOfArray.reduce(function(acc, curr) {
      acc[curr[0]] = curr[1];
      return acc;
    }, empty());
  }

  function dom(map) {
    var key, dominio = [];
    for (key in map) {
     if (map.hasOwnProperty(key)) {
        dominio.push(key);
      }
    }
    return dominio;
  }

  function toList(map) {
    var result = [], key;
    for (key in map) {
     if (map.hasOwnProperty(key)) {
        result.push([key, map[key]]);
      }
    }
    return result;
  }

  function add(key, val, map) {
    map[key] = val;
    return map;
  }

  function containsKey(key, map) {
    return map.hasOwnProperty(key);
  }

  function find(key, map) {
    if (map.hasOwnProperty(key)) {
      return map[key];
    }
    throw new ReferenceError('Invalid Key');
  }

  function tryFind(key, map) {
    if (map.hasOwnProperty(key)) {
      return some(map[key]);
    }
    return none();
  }

  function map(fun, originalMap) {
    var key;
    for (key in originalMap) {
     if (originalMap.hasOwnProperty(key)) {
        originalMap[key] = fun(key, originalMap[key]);
      }
    }
    return originalMap;
  }

  function filter(fun, map) {
    var key, result = {};
    for (key in map) {
     if (map.hasOwnProperty(key)) {
       if(fun(key)) {
          result[key] = map[key];
        }
      }
    }
    return result;
  }

  function exists(fun, map) {
    var key;
    for (key in map) {
     if (map.hasOwnProperty(key)) {
       if(fun(key)) {
            return true;
        }
      }
    }
    return false;
  }

  function forall(fun, map) {
    var key;
    for (key in map) {
     if (map.hasOwnProperty(key)) {
       if(!fun(key)) {
            return false;
        }
      }
    }
    return true;
  }

  function fold(fun, init, map) {
    var result = init, key;
    for (key in map) {
     if (map.hasOwnProperty(key)) {
        result = fun(result, key, map[key]);
      }
    }
    return result;
  }

  return {
    ofList: ofList,
    empty: empty,
    dom: dom,
    toList: toList,
    add: add,
    containsKey: containsKey,
    find: find,
    tryFind: tryFind,
    map: map,
    filter: filter,
    exists: exists,
    forall: forall,
    fold: fold
  };
}());
