/*
 1. remove even numbers from int list
    rmEven : (int list -> int list)
    rmEven [2;5;5;87;6;100;2] = [5; 5; 87]

 2. remove all elements in odd position from a list of floats
    considering the first element an even position.
    rmOddPos   [20.4; 21.4; 22.4; 23.4; 24.4; 25.4; 26.4; 27.4] =
            [20.4;       22.4;       24.4;       26.4] 

3. generate a list of all int from n to 0 via s steps.
   You can assume that step is not 0.


down (10,2);;
val it : int list = [10; 8; 6; 4; 2; 0]

	down : (int * int -> int list)

down(10,3);;
val it : int list = [10; 7; 4; 1]	

Note: down(n,step) must be equal to the range expression [n.. -s .. 0],
so,  for example

[10 .. -29 .. 0];;
val it : int list = [10]
*/

//1
var rmEven = function(coll){return _.filter(coll, function(val){return val % 2 != 0;});};

console.log(rmEven([2, 5, 5, 87, 6, 100, 2]));

//2
var rmOddPos = function(coll){return _.filter(coll, function(val){return ((coll.indexOf(val) % 2) === 0);});};

console.log(rmOddPos([20.4, 21.4, 22.4, 23.4, 24.4, 25.4, 26.4, 27.4]));

//3
var down = function(n, step){return (step < 0) ? _.range(n, 0, step) : _.range(n, 0, -step);};

console.log(down(10,3));
console.log(down(10,-29));

/*
generate a list of all int  0 to n

	upto : (int -> int list)
	upto 8 = [0; 1; 2; 3; 4; 5; 6; 7; 8]
	
(hint: use an accumulator, so define a auxiliary function like in fast reverse)
no tail recursion no accumulator, baby this is javascript
*/
var upto = function(n){return _.range(0, n + 1);};

console.log(upto(8));