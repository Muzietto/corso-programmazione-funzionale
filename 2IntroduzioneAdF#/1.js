//In javascript we ignore all the things regarding data type

/*
Scrivere una funzione costo che calcola il costo di un prodotto.

La funzione costo ha come argomento una tupla

   ( cod , prezzoDi, scontoDi )

dove:

-  cod : string

   codice del prodotto di cui si vuole calcolare il costo.

-  prezzoDi : string -> float

   funzione che, dato il codice di un prodotto (stringa), ne calcola il prezzo (float).
  
-  scontoDi : string -> int

   funzione che, dato il codice di un prodotto (stringa), ne calcola la percentuale di sconto da applicare
   (intero compreso fra 0 e 100).

Notare che il tipo della funzione costo e'


  string * (string -> float) * (string -> int)  -> float
*/

var costo = function(cod, prezzoDi, scontoDi){
	return prezzoDi(cod) - ((scontoDi(cod)/100)*prezzoDi(cod));
}

/*
Definire due funzioni prA e prB che assegnano a un prodotto il prezzo 
nel modo specificato  nella seguente tabella


  codice      prA        prB
---------------------------------
  "cod1"     20.0       40.0
  "cod2"     50.50     100.50 

  
e due funzioni scA e scB che assegnano gli sconti come nella seguente tabella


  codice      scA      scB
---------------------------------
  "cod1"      10       5
  "cod2"       0      25 

Notare che le funzioni hanno tipo:

  prA : string -> float
  prB : string -> float

  scA : string -> int
  scB : string -> int

*/

var prA = function(cod){
	if (cod === 'cod1') return 20.0;
	return 50.50;
}

var prB = function(cod){
	if (cod === 'cod1') return 40.0;
	return 100.50;
}

var scA = function(cod){
	if (cod === 'cod1') return 10;
	return 0;
}

var scB = function(cod){
	if (cod === 'cod1') return 5;
	return 25;
}

/*
Provare a calcolare i seguenti costi:

let c1AA = costo( "cod1", prA, scA ) ;;
// val c1AA : float = 18.0 

let c1BA = costo( "cod1" , prB, scA ) ;;
// val c1BA : float = 36.0 

let c2BB = costo( "cod2" , prB, scB ) ;;
// val c2BB : float =  75.375
*/

console.log(costo('cod1', prA, scA));
console.log(costo('cod1', prB, scA));
console.log(costo('cod2', prB, scB));