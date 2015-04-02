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
        acc.unshift(curr);
        return acc;
      }, []);
}

function sumWithFold(array) {
  return fold(array, function(acc, curr){ return acc + curr; }, 0);
}

function revWithFold(array) {
  return fold(array, function(acc, curr){ 
        acc.push(curr);
        return acc;
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
    acc.unshift(fun(curr));
    return acc;
  }, []);
}


function appendWithFold(arr1, arr2) {
  return fold(arr2.reverse(), function(acc, curr) {
    acc.push(curr);
    return acc;
  }, arr1);
}

function concatWithFold(stringList) {
  return fold(stringList, function(acc, curr) {
    return curr + acc;    
  }, '');
}


function unzip(zippedList) {
  return fold(zippedList.reverse(), function(acc, curr) {
    acc[0].push(curr[0]);
    acc[1].push(curr[1]);
    return acc;
  }, [[], []]);
}
