/*
In javascript we ignore all the things regarding data type.
Another bad idea in javascript is doing recursive function, 
we can do it but there is a limit on the call that a function can do and there is no tail recursion,
so is better to not use the recursive function in javascript, but we can do it, so let's do it.
*/


/*
Dati due interi m>=0 e n>= 0, la definizione ricorsiva del MCD fra m e n e':

MCD(m,n) =  n    se m=0

         =  MCD(n % m, m)   se m > 0 


Definire una funzione ricorsiva  mcd : int * int -> int
che calcola MCD(m,n) (si assume  m>=0 e n>= 0)

Usando la funzione mcd, definire una funzione (non ricorsiva)

   simplify : int * int -> int * int

che semplifica una frazione.

Piu' precisamente, dati due interi a >= 0 e  b>0, 
simplify (a,b) = (c,d) se e solo se c/d e' la frazione ottenuta semplificando a/b.

Esempi:

simplify (15,9) ;; 
val it : int * int = (5, 3)

simplify (7,5) ;; 
val it : int * int = (7, 5)

*/
function mcd (m, n){
	if (m === 0) return n;
	return mcd(n % m, m);	
}

var simplify = function(m, n){
	return [m / mcd(m,n), n / mcd(m,n)];
}

console.log(simplify(15,9));
console.log(simplify(7,5));

/*
Definire una funzione ricorsiva

 copy : string *  int -> string

che, data la copia (str,n), dove str e' una stringa e n>=1,
costruisce la stringa contenente n copie di str.

Ad esempio:

copy ("Ciao", 4) ;;
val it : string = "CiaoCiaoCiaoCiao"

*/
function copy(str, nmb){
	if (nmb <= 1) return str;
	return str + copy(str, nmb - 1);
}

console.log(copy('Ciao', 4));

/*
Definire le seguenti funzioni ricorsive:

   o  sum1 : int -> int

      Dato n >=0, sum1(n) calcola la somma dei numeri compresi fra 0 e n.

   o  sum2 : int * int -> int

      Dati m e n tali che n >= m >= 0, sum2(m,n)  calcola la somma dei numeri compresi fra m e n
   
Esempi:

sum1 4 ;;          
val it : int = 10   // 10 = 0 + 1 + 2 + 3 + 4

sum2 (2,5) ;;
val it : int = 14   // 14 = 2 + 3 + 4 + 5
*/
function sum1(nmb){
	if (nmb === 0) return 0;
	return nmb + sum1(nmb - 1);
}

function sum2(nmb0, nmb1){
	if (nmb0 === nmb1) return nmb1;
	return nmb0 + sum2(nmb0 + 1, nmb1);
}

console.log(sum1(4));
console.log(sum2(2,5));

/*
Definire una funzione

  fib : int -> int

che, dato un intero n >= 0, calcola il valore  F_n dell'n-esimo numero di Fibonacci.

Si ricordi che:

F_0 = 0
F_1 = 1
F _n = F_(n-2) + F_(n-1)  per n > 2
*/
function fib(n){
	if (n <= 1) return n;
	return fib(n - 2) + fib(n - 1);
}

console.log(fib(11));
console.log(fib(12));