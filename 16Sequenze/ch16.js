// namespace Seq
'use strict'
var Seq = (function(){

  function sequence(seqFun,seed){
    // handle finite sequences
    if (Array.isArray(seqFun)) return arraySeq(seqFun,0);
    return function(){
      if (arguments.length > 0) throw 'invalid operation';
      return couple(
        seed,
        sequence(seqFun,seqFun(seed))
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
    return (ord === 0) ? first(thunk()) : nth(ord - 1,second(thunk()));
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

return {
  sequence : sequence,
  skip : skip,
  take : take,
  nth : nth
}
}());