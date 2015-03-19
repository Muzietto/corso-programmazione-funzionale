
function rectangle(b, h){
  return function(w){
    return w('rectangle', b, h);
  }
}

function triangle(b, h){
  return function(w){
    return w('triangle', b, h);
  }
}

function square(l){
  return function(w){
    return w('square', l);
  }
}

function area(shape){
  return shape(function(type, a, b){
    switch (type) {
      case 'rectangle':
        return a * b;
      case 'square':
        return a * a;
      case 'triangle':
        return a * b / 2;
    }
  });
}


////////// MAYBE ////////////
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
    return w();
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

