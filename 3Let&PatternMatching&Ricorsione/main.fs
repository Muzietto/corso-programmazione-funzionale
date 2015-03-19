(*
LEZIONE 3: LET + RIC

/////////////////////////////////////////
RECAP: modello di computazione = valutazione di espressioni

- ogni expr:
  - ha o non ha un tipo
  - ha o non ha un valore (non-terminazione/run time error)
  - può generare un effetto (I/O, eccezioni etc)

- tipi = valori + operazioni

  - "exp : ty" (espressione "exp" ha tipo "ty") è una predizione (statica)
     della forma del valore di "exp", se converge

     - un asserzione exp : ty è valida se exp ha effettivamente tipi ty, es
          -- (3 + 4) : int
          -- (3 + true) : bool **non* è valida

          - validità è dimostrata da una "typing derivation"



- espressioni, valori: giudizio "exp >> val" dato da "regole di valutazione"

  es:
     3 + 2 >> 5
     
    square (3 + 2) >> 25

  anche qui giustificata da "evaluation derivations"

  * nozione di funzione anonima
 
   es: fun x -> x * x

- LET: lego ("bind") un valore "val" a una variabile "id", notazione "id |-> val" ("binding")

- Un insieme di binding si dice un "enviroment" (ambiente)

es: le dichiarazioni

  let x = 2;;
  let y = true;;
  let id = fun x -> x;;

  generano enviroment (snoc lista di binding)
     id |-> fun x -> x, y |->  true, x |- 2

- Vi è un enviroment iniziale (prelude) che da nomi a enti
  predefiniti, vedi  System.Math.PI

  

 -- le variabili non "variano", sono "named constants"

 -- variabili hanno uno "scope", un ambito in cui hanno senso. F# usa

         scoping statico (aka lessicale), cioè determinato dal testo del programma

         --- scoping globale

            let id = exp

         --- scoping locale
            let id = exp1 in exp2: binding persi dopo valutazione di exp2
  
  * F#: sintassi light vs verbose 

*)

// globale
let x = 3;;

let id = fun x -> x;;

// locale
let x1 =
    let y = 5 // y e' definita localmente (non visibile all'esterno)
    y * y;;  // valore associato a  x1

//  y;;
// The value or constructor 'y' is not defined

// in caso di ri-uso dello stesso nome, il vecchio legame è ricreato

x;;

let x = 22 in x * x;;

x;;

// verbose LET id = exp IN exp

let x2 = let y = 5 in  y * y ;;


// let multipli = dichiarazioni

let x3 =
    let y = sin 4.0
    let z =  cos (float x1)
    y + z;;

// più noioso in verbose    (nota indentazione per il lettore)

let x3 =
    let y = sin 4.0 in
       let z = cos (float x1) in
         y + z;;


// binding *non* è assegnamento: il binding non cambia ma può essere "shadowed"

let yy = 56;;         

let yy = true;;

// il vecchio valore è dimenticato, "ombreggiato" da quello più recente

(*

In presenza di variabili, dobbiamo generalizzare la nozione di
derivazione  di valutazione (e di tipo) attraverso la nozione di enviroment

 env |- exp >> val


 per esempio env |- id >> val se id |-> val appartiene ad env

*)





///////////////////////////////////////////////////////////////////////////
(*

- PATTERN MATCHING
  
  * su tipi primitivi (numeri/bool etc)

    (doppia sintassi match/function)

let f n = 
  match n with
  | 0 -> v1
  | n -> v2

equivalente a

let f  = fun n -> 
  match n with
  | 0 -> v1
  | n -> v2

equivalente a

let f  = function 
  | 0 -> v1
  | n -> v2

cioè let f  = function ... espande a
     let f n = match n with ...


==> pattern matching su espressioni complesse come tupe.

    es: 
let And (x,y)  =
    match (x,y) with
    | (true, true) -> true
    | _ -> false  ;;
 *)


//// ESERCIZIO

(*
 http://cooml.di.unimi.it/fp/Lez02/esercizio.txt

*)


// RICORSIONE

(*

Dato un intero n >= 0,  n! (fattoriale di n) puo' essere definito ricorsivamente
nel modo seguente:

n!   =  0                              se  n = 0

     =  1 * 2 * ... * (n-1) *n         se  n > 0

Questo genera una "formula di ricorsione"

n!   =  0                   se  n = 0

     =  n  * (n-1)!         se  n > 0


- caso base e caso passo
- in questo caso terminante (è ricorsione primitiva) , ma in generale indecidibile

La tradizione in codice è immediata:

   *)

let rec fact m =
    match m with
        | 0 -> 1
        | n -> n * fact ( n-1 );;

// si noti il let rec

(* si noti che l'ordine delle pattern (cronologico) conta: questo non va:

let rec fact m =
    match m with
        | n -> n * fact ( n-1 )
        | 0 -> 1

===> warning FS0026: This rule will never be matched
*)

// funzioni ricorsive "su più argomenti"

(*

   2)  Dato un intero x e un intero n >= 0,  la funzione esponenziale puo'
essere definita ricorsivamente come: 

 x^n  = 1   se n = 0 

      =  x * x^(n-1)  se n > 0

*)

let rec exp ( bse, m ) =
    match m with
        | 0  -> 1
        | n  ->  bse *  exp (bse, n-1);;

(*
Definire una funzione ricorsiva

  make_str : int -> string

che, dato un intero n>=0, restituisce la stringa "0 1 2 ... n"

   *)

let rec make_str = function
    |0 -> "0"
    | n -> make_str ( n - 1) + " " +  (string n);;

 make_str 5;;    
