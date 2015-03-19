(****   EXPRESSION TREES   ****)

// Vedi Cap. 6 del libro

(*

Lo scopo e' di rappresentare espressioni.

Analizziamo tre esempi:

- expr1:  espressioni intere con operazioni +, -, *

- expr2:  expr1 con variabili

- expr3:  expr2 con let-definition



*)   

///////////////////////////////////////////////////

(**  expr1:  ESPRESSIONI  INTERE       **)

(*

Definizione di expr1
^^^^^^^^^^^^^^^^^^^^

Una espressione intera e di tipo expr1  e' definita induttivamente
dalla seguente grammatica BNF:

   e  ::=   n  |  e1 + e2  | e1 - e2  |   e1 * e2 

dove:
- n e' una costante intera;
- e1, e2 sono espressioni di tipo expr1.

Definizione equivalente:

(1) una costante intera n e' una espressione di tipo expr1

(2) se e1, e2 sono espressioni di tipo expr1 allora

       e1 + e2     e1 - e2       e1 * e2 

   sono espressioni di tipo expr1

(3) ogni espressione di tipo expr1 ha la struttura in (1) o (2).

-------

Esempi di espressioni di tipo expr1 (le parentesi evidenziano la struttura dell'espressione)

  5      3 + 7       5 * (3 + 7)         (2 + 7 ) * ( (4 * 3) + 2 ) 


Una espressione puo' essere vista come un albero (expression tree):

 - la costante n e' l'albero contenente solo il nodo n

 - l'espressione e1 + e2  e' l'albero avente radice +  e, come sottoalberi sinistro e destro,
   gli alberi corrispondenti alle espressioni e1 ed e2

              +
            /  \
           e1   e2

 - la definizione degli alberi e1 - e2 e e1 * e2 e' analoga.


Ad esempio, l'espressiono 3 + 7 corrisponde all'albero

    +
   /  \  
  3    7

Analogamente:

     *
   /   \
  5     +      rappresenta    5 * (3 + 7) 
       /  \
      3    7


     -- *--
    /       \
   +         +      
  / \       /  \    rappresenta   (2 + 7 ) * ( (4 * 3) + 2 ) 
 2   7     *    2
          / \
         4   3
  

*)

// tipo per rappresentare una expr1

type expr1 =
  | C of int    // costante intera
  | Sum  of expr1 * expr1    
  | Diff of expr1 * expr1
  | Prod of expr1 * expr1   ;;


// Esempi

// a1 = 5
let a1 =  C 5 ;;  
// val a1 : expr1 = C 5

// a2 = 7 * (1 + 2)
let a2 = Prod ( C 7, Sum ( C 1, C 2 )) ;;  
// val a2 : expr1 = Prod (C 7,Sum (C 1,C 2))

// a3 = a1 + a2 = 5 + ( 7 * (1 + 2) )
let a3 = Sum (a1,a2) ;; 
// val a3 : expr1 = Sum (C 5,Prod (C 7,Sum (C 1,C 2)))

(*  

Valutazione di expr1
^^^^^^^^^^^^^^^^^^^^

Una espressione e di tipo expr1 ha un valore v intero.

Introduciamo il giudizio

   e >> v

per indicare  che v (un intero) e' il valore dell'espressione e.

Ad esempio:

  3 + 5 >> 8      2 * ( 5 + 7 ) >> 24


Definiamo  'e >>  v' per induzione sulla struttura dell'espressione e.

1) Se e e' la costante intera n, allora   e >> n

  In modo piu' conciso scriviamo:

     n >> n,   per ogni costante intera n

2) Supponiamo che si abbia

     e = e1 + e2    e1 >> n1      e2 >> n2

   Allora  e >> n1 + n2

   In modo piu' conciso scriviamo:

     e1 >> n1   e   e2 >> n2    implica     e1 + e2  >>  n1 + n2

3)   e1 >> n1   e   e2 >> n2    implica     e1 - e2  >>  n1 - n2

4)   e1 >> n1   e   e2 >> n2    implica     e1 * e2  >>  n1 * n2


Esempi:

   3 + 5 >> 8

Infatti, per il punto 19 vale

  3  >> 3    5 >> 5

Per il punto 29, poiche' 3 + 5 = 8 segue 

  3 + 5  >>  8


*)   


// eval1  : expr1 -> int
// ( eval1 e = v )   sse   ( e >> v ) 

let rec eval1 e  =
    match e with
    | C n   -> n
    | Sum(e1,e2)   -> eval1 e1  + eval1 e2 
    | Diff(e1,e2)  -> eval1 e1  - eval1 e2 
    | Prod(e1,e2)  -> eval1 e1  * eval1 e2 ;;

// Esempi

// a1 = 5
eval1 a1 ;; // 5

// a2  =  7 * (1 + 2)
eval1 a2 ;; // 21

// a3 =  5 + ( 7 * (1 + 2) )
eval1 a3 ;; // 26

/////////////////////////////////////////////////////////////////////

(**  expr2: ESPRESSIONI  INTERE  CONTENENTI VARIABILI     **)

(*

Definizione di expr2
^^^^^^^^^^^^^^^^^^^^

Una espressione intera e di tipo expr2 e' definita da

   e  ::=  x | n |  e1 + e2 | e1 - e2 |   e1 * e2 

dove:

-  x e' una variabile;
-  n e' una costante intera;
-  e1, e2, sono espressioni di tipo expr2.

Valutazione di expr2
^^^^^^^^^^^^^^^^^^^^

La valutazione di una espressione di tipo expr2 ha senso solo
se alle variabili che compaiono in essa  e' assegnato un valore.

Chiamiamo ambiente (environment) una funzione finita (mappa) che assegna valori alle variabili.

Il giudizio

  env |- e >> v 

indica che l'espressione e (espressione di tipo expr2) ha valore v (un intero) nell'ambiente env.

NOTA
====

Per semplicita', nel seguito assumiamo che env assegni un valore
a tutte le variabili che compaiono in e.

----

Definiamo  'env |- e >>  v' per induzione sulla struttura dell'espressione e.

1) Se e e' la variabile x e env(x)=v, allora  env |- e >> v
   
   In modo piu' conciso scriviamo:

    env |- x >> env(x) ,   per ogni variabile x 

2)  env |- n >> n  ,  per ogni costante intera n

3)  env |- e1 >> n1  e   env |- e2 >> n2   implica   env |-  e1 + e2 >> n1 + n2

4)  env |- e1 >> n1  e   env |- e2 >> n2   implica   env |-  e1 - e2 >> n1 - n2

5)  env |- e1 >> n1  e   env |- e2 >> n2   implica   env |-  e1 * e2 >> n1 * n2


Esempio.

Consideriamo l'ambiente 

  envxyz = { (x,1) , (y,2) , (z,3) }

Allora:

  envxyz |-  x + 5  >>  6

  envxyz |-  z * ( x + 5)  >> 18

  envxyz |-  y + z  >>  5


*)

// tipo per rappresentare una expr2

type expr2 =
  | V of string          // variabile
  | C of int
  | Sum of expr2 * expr2
  | Diff of  expr2 * expr2
  | Prod of  expr2 * expr2  ;;


// tipo per rappresentare un ambiente

type envt = Map<string,int> ;;

// eval2  : expr2 -> envt -> int
// ( eval2 e env = v )   sse  (  env  |-  e >> v )

let rec eval2 e ( env : envt)  =
    match e with
    | V x ->  Map.find x env  // calcola env(x)  
    | C n -> n
    | Sum(e1,e2)   -> eval2 e1 env  + eval2 e2 env 
    | Diff(e1,e2)  -> eval2 e1 env  - eval2 e2 env
    | Prod(e1,e2)  -> eval2 e1 env  * eval2 e2 env ;;

// Esempi

// envxyz = { (x,1) , (y,2) , (z,3) }    
let envxyz = Map.ofList [ ("x",1) ; ("y",2) ; ("z", 3)] ;;         


//  b1 = x + 5
let b1 = Sum ( V "x" , C 5 ) ;; 
eval2 b1 envxyz ;; // 6

// b2 = z * b1 = z * ( x + 5)
let b2 = Prod ( V "z" , b1) ;; 
eval2 b2 envxyz ;; // 18


eval2  ( Sum ( V "y" , V "z" ) ) envxyz ;; // 5 
// il termine Sum(...) va racchiuso fra parentesi 


//////////////////////////////////////////////////////////////////////////////////

(**  expr3: ESPRESSIONI  INTERE  CON VARIABILI  E LET-DEFINITION    **)

(*

Definizione di expr3
^^^^^^^^^^^^^^^^^^^^

Una espressione intera e di tipo expr3 e' definita da

   e  ::=  x | n |  e1 + e2 | e1 - e2 |  e1 * e2  | let x = e1 in e2

dove:

-  x e' una variabile;
-  n e' una costante intera;
-  e1, e2  sono espressioni di tipo expr3.

Esempi:

 let y = 8 in (y + 10)

 y + ( let y = 8 in (y + 10)  )

 ( let x = (y+1) in (x + z) ) + ( let x = 100 in (x + z) ) 


Valutazione di expr3
^^^^^^^^^^^^^^^^^^^^

Il significato dell'espressione

     let x = e1 in e2     // (#)

e' che a ogni variabile x che compare in e2 va associato il valore di e1.

Dato un ambiente env, la notazione

   env[x=v]

indica l'ambiente ottenuto da env assegnando a x il valore v
(il vecchio valore di x non e' visibile).


Il valore di (#) nell'ambiente env va quindi calcolato come segue:

i)    calcolo il valore v1 di e1 nell'ambiente env.

ii)   definisco l'ambiente

          envx  =  env[x=v1]   // overriding di env con  x = v1

iii)  calcolo il valore dell'espressione e2 nell'ambiente envx



NOTA
=====

L'ambiente envx e' un 'ambiente locale' usato esclusivamente per valutare (#);
quindi, il valore x=v1 viene assegnato provvisoriamente durante la valutazione di (#).

---

Il giudizio 'env |- e >> v' e' definito come nel caso di expr2,
aggiungendo la clausola relativa alle let-definition:

 env |- e1 >> v1   e   env[x= v1] |- e2 >> v2  implica

     env |- ( let x = e1 in e2 ) >> v2


Esempi:

Consideriamo l'ambiente

 env1 = { (x,10) , (y,20) , (z,30) } 

Valutiamo in env1 le seguenti espressioni

1)  let y = 8 in (y + 10)

2)  y + ( let y = 8 in (y + 10)  )

3)  (let x = (y+1) in (x + z) ) + (let x = 100 in (x + z)) 

----

1) 

  env1[ y=8 ]  = { (x,10) , (y,8) , (z,30) }

quindi:

  env1 [y=8| |-  y + 10  >> 18

Possiamo concludere:

  env1 |- let y = 8 in (y+10) >> 18

2) 

  env1 |- y >> 20
  
Tenendo conto di 1), segue:

  env1 |-  y + ( let y = 8 in (y + 10) ) >>  38


3) Valutiamo (let x = (y+1) in (x + z) ) + (let x = 100 in (x + z)) 

Poiche'

 env1 |- y+1 >> 21

si ottiene

 env1 |-  let x = (y+1) in (x + z)  >> 51

Inoltre

 env1 |-  let x = 100 in (x + z)  >> 130

Segue che  

 env1 |-  (let x = (y+1) in (x + z) ) + (let x = 100 in (x + z)) >>  181
              

*)

// tipo per rappresentare una expr3

type expr3 =
  | V of string          
  | C of int
  | Sum of expr3 * expr3
  | Diff of  expr3 * expr3
  | Prod of  expr3 * expr3
  | Let of string * expr3 * expr3 ;;  // Let-definition


// eval3 : expr3 -> envt -> int
// ( eval3 e env = v )   sse  (  env  |-  e >> v )

let rec eval3 e ( env : envt)  =
    match e with
    | V x ->  Map.find x env  
    | C n -> n
    | Sum(e1,e2)   -> eval3 e1 env  +  eval3 e2 env 
    | Diff(e1,e2)  -> eval3 e1 env  -  eval3 e2 env
    | Prod(e1,e2)  -> eval3 e1 env  *  eval3 e2 env 
    | Let(x,e1,e2) -> let v1 = eval3 e1 env         // v1 e' il valore di e1 in env
                      let envx = Map.add x v1 env  // envx = env[x= v1]
                      eval3 e2 envx    ;;          // calcolo valore di e2 in envx 

// Esempi

// env1 = { (x,10) , (y,20) , (z,30) } 
let env1 = Map.ofList[("x",10); ("y",20) ; ("z",30) ]

// c1 =  y + ( let y = 8 in (y + 10)  )
let c1 = Sum( V "y" , Let("y", C 8, Sum(V "y", C 10))) ;; 

let v1 = eval3 c1 env1 ;; // 38

// a = ( let x = (y+1) in (x + z) )
let a = Let("x", Sum (V "y", C 1), Sum( V "x", V "z")) ;;

// b = ( let x = 100 in (x + z) )
let b = Let("x", C 100, Sum(V "x", V "z")) ;;

// c2 =  ( let x = (y+1) in (x + z) ) + ( let x = 100 in (x + z) ) 
//    =  a + b
let c2 = Sum(a,b)  ;;
let v2 = eval3 c2 env1 ;; // 181

(** CONSIDERAZIONI FINALI **)

(*

Tutte le espressioni viste possono essere valutate direttamente usando l'interprete di F#;
l'ambiente va definito mediante let-definition.

*)   


3 ;;  //  richiesta di valutazione di una espressione
// val it : int = 3  -- risultato della valutazione

4 + 2 * (3 + 4 ) ;;
// val it : int = 18

// definizione di un ambiente

let x = 10 ;;
let y = 20 ;;
let z = 30 ;;

// l'ambiente definito e' env1 = { (x,10) , (y,20) , (z,30) } 

2 * x  + y ;;  // l'espressione e' valutata in env1
// val it : int = 40

let y = 8 in (y + 10) ;;
// val it : int = 18
// NOTA: l'espressione sopra *non*  ridefinisce y !


y + ( let y = 8 in (y + 10)  ) ;;
// val it : int = 38

let x = (y+1) in (x + z) ;;
// val it : int = 51

let x = 100 in (x + z) ;;
// val it : int = 130

( let x = (y+1) in (x + z) ) + ( let x = 100 in (x + z)) ;;
// val it : int = 181
