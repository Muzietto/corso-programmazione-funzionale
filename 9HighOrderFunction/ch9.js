'use strict';
function map(fun, list) {

  var i, l = [];
  for(i = 0; i < list.length; i = i + 1) {
    l[i] = fun(list[i]);
  }
  return l;
}

function mapR(fun, list) {

  if (list[0] === undefined) {
    return [];
  }
  return [fun(list[0])].concat(mapR(fun, list.slice(1, list.length)));
}

function filter(fun, list) {

  var i, l = [];
  for(i = 0; i < list.length; i = i + 1) {
    if (fun(list[i])) {
      l = l.concat(list[i]);
    }
  }
  return l;
}

function filterR(fun, list) {

  if (list[0] === undefined) {
    return [];
  }
  if (fun(list[0])) {
    return [list[0]].concat(filterR(fun, list.slice(1, list.length)));
  }
  return [].concat(filterR(fun, list.slice(1, list.length)));
}

function filter1(fun, list) {

  var i, t = [], f = [];
  for(i = 0; i < list.length; i = i + 1) {
    if (fun(list[i])) {
      t = t.concat(list[i]);
    } else {
      f = f.concat(list[i]);
    }
  }
  return {correct: t, incorrect: f};
}

function divisori(val) {

  var candidate = [], app = val;
  while (app !== 0) {
    candidate = [app].concat(candidate);
    app = app - 1;
  }
  candidate = filter(function(x) {
    return val % x === 0;
  }, candidate);
  return candidate;
}

function isPrime(val) {

  if(divisori(val).length <= 2) {
    return true;
  }
  return false;
}
