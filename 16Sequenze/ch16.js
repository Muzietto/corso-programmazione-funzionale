// namespace Seq
var Seq = (function(){

  // stateless sequence
  function sequence(seqFun, seed){
    'use strict';
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
    };
  }

  function arraySeq(array,pos){
    'use strict';
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

  function nth(ord, thunk){
    'use strict';
    if (ord === 0) {
      return first(thunk());
    }
    return nth(ord - 1,second(thunk()));
    //return (ord === 0) ? first(thunk()) : nth(ord - 1,second(thunk()));
  }

  function skip(ord, thunk){
    'use strict';
    return (ord === 0) ? thunk : skip(ord - 1,second(thunk()));
  }

  function take(ord, thunk){
    'use strict';
    return aux(ord,[]);
    function aux(ord,acc){
      return (ord === 0) ? arraySeq(acc) : aux(ord - 1,[nth(ord - 1,thunk)].concat(acc));
    }
  }

  function map(fun, seq) {
    'use strict';
    return function() {
      return couple(
        fun(first(seq())),
        map(fun, second(seq()))
      );     
    };
  }

  function filter(fun, seq) {
    'use strict';
    return function() {
      var fst = first(seq());
      if(fun(fst)) {
        return couple(
          fst,
          filter(fun, second(seq()))
        );   
      }
      return filter(fun, second(seq()))();
    };    
  }

  function fib(now, next) {
    'use strict';
    return function(){
      if (arguments.length > 0) throw 'invalid operation';
      return couple(
        now,
        fib(next, now + next)
      );
    };
  }
  
  return {
    sequence : sequence,
    skip : skip,
    take : take,
    nth : nth,
    map: map,
    filter: filter,
    fibonacci: fib
  };
}());