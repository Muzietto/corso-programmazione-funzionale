// namespace Seq
'use strict';
var Seq = (function(){

  // stateless sequence
  function sequence(seqFun,seed){
    // handle finite sequences
    if (Array.isArray(seqFun)) return arraySeq(seqFun,0);
    return function(){
      if (arguments.length > 0) throw 'invalid operation';
      var nextSeed = seqFun(seed);
      if (typeof nextSeed === 'undefined') throw 'empty sequence';
      return couple(
        seed,
        sequence(seqFun,nextSeed)
      );
    }
  }

  function arraySeq(array,pos){
    pos = pos || 0;
    return function(){
      if (typeof array[pos] !== 'undefined'){
        return couple(
          array[pos],
          arraySeq(array,pos + 1)
        );
      } else {
        throw 'empty sequence';
      }
    };
  }

  function nth(ord,thunk){
    if (ord === 0) {
      return first(thunk());
    }
    return nth(ord - 1,second(thunk()));
    //return (ord === 0) ? first(thunk()) : nth(ord - 1,second(thunk()));
  }

  function skip(ord,thunk){
    return (ord === 0) ? thunk : skip(ord - 1,second(thunk()));
  }

  function take(ord,thunk){
    return aux(ord,[]);
    function aux(ord,acc){
      return (ord === 0) ? arraySeq(acc) : aux(ord - 1,[nth(ord - 1,thunk)].concat(acc));
    }
  }

  function map(fun, seq) {
    return sequence(fun, first(seq()));
  }

/*
  function filter(fun, seq) {
    return sequence(function (x) {
      var res = first(seq());
      while(!fun(res)) {
        seq = second(seq());
        res = first(seq());
      }
      seq = second(seq());
      return res;
    }, first(seq()));
  }
*/
return {
  sequence : sequence,
  skip : skip,
  take : take,
  nth : nth,
  map: map,
  //filter: filter
}
}());
