'use strict';
function sumWithFoldl(array) {

  return foldl(array, function(acc,curr){ return acc + curr; }, 0);
}

function mulWithFoldl(array) {

  return foldl(array, function(acc,curr){ return acc * curr; }, 1);
}

function countWithFoldl(array) {

  return foldl(array, function(acc,curr){ return acc + 1; }, 0);
}

function maxWithFoldl(array) {

  return foldl(array, function(acc,curr){
        if (acc > curr) {
          return acc;
        }
        return curr;
      }, -Math.pow(2,32) - 1);
}

function minWithFoldl(array) {

  return foldl(array, function(acc,curr){
        if (acc < curr) {
          return acc;
        }
        return curr;
      }, Math.pow(2,32) - 1);
}

function revWithFoldl(array) {

  return foldl(array, function(acc,curr){
        return [curr].concat(acc);
      }, []);
}

function sumWithFold(array) {

  return fold(array, function(acc, curr){ return acc + curr; }, 0);
}

function revWithFold(array) {

  return fold(array, function(acc, curr){
        return acc.concat(curr);
      }, []);
}

function mulWithFold(array) {

  return fold(array, function(acc, curr){ return acc * curr; }, 1);
}

function countWithFold(array) {

  return fold(array, function(acc,curr){ return acc + 1; }, 0);
}

function maxWithFold(array) {

  return fold(array, function(acc,curr){
        if (acc > curr) {
          return acc;
        }
        return curr;
      }, -Math.pow(2,32) - 1);
}

function minWithFold(array) {

  return fold(array, function(acc,curr){
        if (acc < curr) {
          return acc;
        }
        return curr;
      }, Math.pow(2,32) - 1);
}

function mapWithFold(fun, array) {

  return fold(array, function(acc, curr) {
    return [fun(curr)].concat(acc);
  }, []);
}


function appendWithFold(arr1, arr2) {

  return fold(arr2.reverse(), function(acc, curr) {
    return acc.concat(curr);
  }, arr1);
}

function concatWithFold(stringList) {

  return fold(stringList, function(acc, curr) {
    return curr + acc;
  }, '');
}


function unzip(zippedList) {

  return fold(zippedList.reverse(), function(acc, curr) {
    acc[0] = acc[0].concat(curr[0]);
    acc[1] = acc[1].concat(curr[1]);
    return acc;
  }, [[], []]);
}
