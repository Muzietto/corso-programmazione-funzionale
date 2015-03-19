// BASI DEL LINGUAGGIO



//----   ESPRESSIONI ----

// NUMERI

2;;

3.14;;

// notare che le definizioni vanno terminate con ;;

// OPERAZIONI SU NUMERI

2 + 5;;

3.13 * 2.;;

// DICHIARAZIONI

let x = 2;;

(*
 
Introduco un'espressione, qui '2', e la *lego* al nome 'x'.

**NON**  e' un assegnamento, semplicemente sto creando
un ambiente (enviroment) in cui il nome x e' legato (binding) a 2.

*)

let y = 3 ;;

let a_sum = x + y ;; 

// ALTRI VALORI PRIMITIVI: BOOLEANI

let tv = 5 = a_sum ;;
// tv ha tipo bool e valore true

// VEDREMO ALTRI PRIMITIVI IN SEGUITO


//----   DEFINIZIONE DI FUNZIONI ----

// La funzione square calcola il quadrato di un intero

let square x = x * x ;;

(*

  La funzione square ha tipo int -> int e definisce la funzione  che a x associa x * x.

  Dato un valore v di tipo int 
 
      square v    // applicazione della funzione square al valore v 
  
  ha tipo int  e valore v * v 

  Quando si applica una funzione non e' necessario scrivere l'argomento fra parentesi tonde
  (come invece si fa in C, Java, ecc.)

*)   


// Esempi di applicazione della funzione square
// lo spazio tra funzione "square"   ed espressione "5" denota l'applicazione

let a = square 5 ;;   
let b = square ( square 2 ) ;;

(*
  Nel secondo esempio le parentesi sono necessarie in quanto l'applicazione
  di funzioni e' associativa a sinistra.

  Senza parentesi l'espressione

      square  square 2

  e' interpretata come

     ( square  square ) 2 

  che non ha senso, in quanto square puo' essere applicata solamente a un valore di tipo int

*)   


// LAMBDA EXPRESSIONS

// La dichiarazione 'let square x =  x * x'  e'  equivalente a

let square = fun x -> x * x;;

(* 

questa sintassi, detta lambda-lifted, e' quella primitiva.

Al nome  'square' e' legata l'espressione

   fun x -> x * x   // lambda expression
  
che rappresenta la funzione che a x (argomento della funzione)
associa  'x*x' (corpo della  funzione).

Notare che l'espressione

   fun x -> x * x 

ha tipo

  int -> int

*)



// La funzione squareFloat calcola il quadrato di un float

let squareFloat (x : float) = x * x ;;

(*

La funzione  squareFloat ha tipo  float -> float.

Notare che:

1) occorre annotare esplicitamente il tipo di x, altrimenti viene assunto che x abbia tipo int.

2) La funzione squareFloat puo' essere applicata solamente a un valore di tipo float.

Quindi:
  
  squareFloat 2.0    e' corretto      (2.0 e' una costante di tipo float)   
  squareFloat 2      non e' corretto  (2   e' una costante di tipo int)

La funzione

 float : int -> float

trasforma un valore di tipo int nel corrispondente valore di tipo float. 
L'espressione

   squareFloat ( float 2  )

e' corretta; cosa succede omettendo le parentesi tonde?

Notare la differenza con i linguaggi tipo C, Java, ... in cui espressioni
miste della forma

  5 + 2.3    10 - 3 * 2.5

sono accettate (vengono fatti implicitamente dei cast
in modo che gli argomenti abbiano lo stesso tipo).
  

*)  

//  doubleSqr : int -> int
//  doubleSqr x calcola 2 * x^2

let doubleSqr x =
    let y = square x  // definizione locale  (y non e' visibile all'esterno di doubleSqr)
    2 * y ;; // valore della funzione

//  circleArea : float -> float
//  calcola l'area del cerchio di raggio r

let circleArea r = System.Math.PI * squareFloat r ;;

//  System.Math.PI : float e' la costante pi greco


// isPari : int -> bool
// isPari n = true sse n e' pari


let isPari n  =   n % 2 = 0 ;;


//------ TUPLE -------

let x = 5 ;;
let y = 10 ;;

let t1 = ( x + 1 , x < y ) ;;  // le parentesi possono essere omesse
//  la tupla  t1 ha tipo  int * bool e valore ( 6, true )

(*

Il tipo int * bool e' il prodotto cartesiano fra  int e bool.

Un valore  v di tipo (int * bool)  e' una tupla della forma

  ( n , b )   

dove n ha tipo int e b ha tipo bool.

Esempi di termini di tipo (int * bool):

 ( 5, true )    ( 10, false)

*)   


let t2 = ( (fun x -> x + 1) , t1) ;;

(*
 La tupla t2 ha tipo  (int -> int) * (int * bool)

- La prima componente di t2 e' una funzione di tipo int -> int
  (la funzione successore di un intero);

- La seconda componente di t2 ha tipo int * bool e valore ( 6, true ).

*)


// attenzione all'uso delle parentesi !

let t3 = ( fun x -> x + 1 , t1) ;;

(*
   t3 e' una funzione di tipo  int -> int * (int * bool)
   t3 associa a x di tipo int  la tupla (x+1 ,t1) di tipo int * (int * bool)

*)

let a3 = t3 7  ;;
// a3 ha tipo  int * (int * bool) e valore (8, (6,true))


// add2 : int * int -> int
// add2(x,y) calcola  x+y


let add2 (x,y)  = x + y ;;

(*

 Notare che add2 ha un unico argomento,  ossia la tupla  (x,y) di tipo int * int. 
 Le parentesi non servono per racchiudono l'elenco degli argomenti della funzione add2
 (come invece avviene in C, Java ecc.),  ma indicano che  (x,y) e' una tupla

*)
   

let s1 = add2 (7, 8) ;;
// s1 : int vale 15



//----- INTRODUZIONE AL  PATTERN MATCHING -----


// f : int -> string

let f n =
    match n with
    | 1 -> "uno"                          // valore di f n quando n vale 1
    | 2 -> "due"                          // valore di f n quando n vale 2
    | _ -> "diverso da uno e due" ;;      // valore di f n quando n e' diverso da 1 e 2 


//  daysOfMonth : int -> int
// Calcola quanti giorni ha il mese specificato 
        
let daysOfMonth = function
    | 2        -> 28      // February
    | 4|6|9|11 -> 30      // April, June, September, November
    | _        -> 31 ;;   // All other months
  

// oppure

let daysOfMonth1 month =
    match month with
        | 2        -> 28      // February
        | 4|6|9|11 -> 30      // April, June, September, November
        | _        -> 31  ;;  // All other months


// Esempi di funzioni booleane

//  not : bool -> bool
//  not x e' la negazione di x
    
let not = function
    | true  ->  false
    | false ->  true  ;;

// oppure

let not1 x  =
    match x with
        | true  -> false
        | false -> true ;;


let b1 = not  ( 10 < 20 ) ;;
// b1 : bool vale false

let b2 = not1 ( not1  ( 10 < 20 ) ) ;;
// b2 : bool vale true

// And : bool * bool -> bool
// And (x,y)  = x AND y 

    
let And = function    
    | (true, true) -> true
    | _ -> false ;;

// oppure
    
let And1 (x,y)  =
    match (x,y) with
    | (true, true) -> true
    | _ -> false  ;;

// oppure

let And2 t  =
    match t with
    | (true, true) -> true
    | _ -> false  ;;


let b3 = And ( b1, b2 ) ;;
// b3 : bool vale false

(*
   Come esercizio, scrivere la funzione Or : bool * bool -> bool
   che calcola l'OR fra due valori di tipo bool
*)



// isPariString : int -> string
// Il valore di isPariString n e' "pari" se n e' pari, "dispari" altrimenti

let isPariString n =
    match n % 2 with
        | 0 -> "pari"
        | _ -> "dispari" ;;

let s = "5 e' un numero " + isPariString 5  ;;

(*
L'operatore binario + e' overloaded (il suo significato dipende dai tipi degli argomenti)

In questo caso, avendo i due argomenti tipo string, + rappresenta l'operatore
di concatenazione fra stringhe
Quindi  s ha tipo  string  e valore  "5 e' un numero dispari"


*)


