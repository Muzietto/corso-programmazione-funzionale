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

  // return the sequence with the first ord element skipped
  function skip(ord, thunk){
    'use strict';
    return (ord === 0) ? thunk : skip(ord - 1,second(thunk()));
  }

  // take the first ord number of a sequence and make and array sequence with it
  function take(ord, thunk){
    'use strict';
    return aux(ord,[]);
    function aux(ord,acc){
      return (ord === 0) ? arraySeq(acc) : aux(ord - 1,[nth(ord - 1,thunk)].concat(acc));
    }
  }

  // take the first member of the sequence and apply fun to it,
  // than return the couple made with it and the map of the remaining sequence
  function map(fun, seq) {
    'use strict';
    return function() {
      return couple(
        fun(nth(0, seq)),
        map(fun, skip(1, seq))
      );     
    };
  }

  function filter(fun, seq) {
    'use strict';
    return function() {
      var fst = nth(0, seq);
      if(fun(fst)) {
        return couple(
          fst,
          filter(fun, skip(1, seq))
        );   
      }
      return filter(fun, skip(1, seq))();
    };    
  }

  function fib() {
    'use strict';
    function helper(now, next) {      
      return function(){
        if (arguments.length > 0) throw 'invalid operation';
        return couple(
          now,
          helper(next, now + next)
        );
      };
    }
    return helper(0, 1);
  }
  
  function erst() {
    'use strict';
    var oddNaturals = Seq.sequence(function(x){ return x + 1; },2);
    function helper(seq) {      
      // return a sequence made of all the number in the sequence seq that aren't multiple of the number a
      function sift(a, seq) {
        return filter(function(x) { return x % a !== 0; }, seq);
      }
      // return the first number of the sequence and a sequence without the multiple of the first number
      return function() {
        var fst = nth(0, seq);
        return couple(
          fst,
          helper(sift(fst, skip(1, seq)))
        );
      };
    }
    return helper(oddNaturals);
  }

  return {
    sequence : sequence,
    skip : skip,
    take : take,
    nth : nth,
    map: map,
    filter: filter,
    fibonacci: fib,
    eratosthenes: erst
  };
}());