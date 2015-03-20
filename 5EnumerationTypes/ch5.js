////////////////// ES. 1 ///////////////////////////
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
        return some(a > 0 && b > 0 ? a * b : null);
      case 'square':
        return some(a > 0 ? a * a : null);
      case 'triangle':
        return some(a > 0 && b > 0 ? a * b / 2: null);
    }
  });
}

function areaSum(){
  var shapes = Array.prototype.slice.apply(arguments);
  return shapes.reduce(function(acc, curr){ 
    return some(value(acc) + (isSome(area(curr)) ? value(area(curr)) : 0)); }, some(0));
}

function value(option){
  return option(function(x){ return x; });
}

///////////////// ES. 2 ///////////////////////
function head_tailOpt(list){
  return couple(
    some(head(list)),
    some(isEmpty(tail(list)) ? null : tail(list))
  );
}

function lastOpt(list){
  return some(isEmpty(last(list)) ? null : last(list))
}
