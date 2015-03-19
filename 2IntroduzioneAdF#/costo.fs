
module P

// costo :  string * (string -> float) * (string -> int) -> float

let costo (cod : string, prezzoDi, scontoDi ) =
    let pr = prezzoDi cod   // pr : float e' il prezzo di cod
    let sc = scontoDi cod   // sc : int e' lo sconto di cod
    pr - pr * ( float  sc ) / 100.0 ;;   // valore della funzione


(*

Senza l'annotazione cod:string il tipo della funzione  costo e'

   'a * ('a -> float) * ('a -> int) -> float

'a e' una variabile di tipo (analoga ai parametri di tipo nei tipi generici in Java).


ATTENZIONE: QUANTO SEGUE È VALIDO PER F# 2.0 CHE USIAMO IN LAB. LE
VERSIONI SUCCESSIVE > 3.0 FANNO CASTING AUTOMATICO


Gli operatori aritmetici richiedono che gli argomenti abbiano lo stesso tipo
Ad esempio, l'espressione

  5 + 7.3

non e' corretta (5 ha tipo int, 7.3 ha tipo float).
Occorre scrivere

  5.0 + 7.3

Nell'esempio occorre scrivere

    pr * (float  sc )

altrimenti i tipi di pr e sc (e di conseguenza i tipi delle funzioni  prezzoDie scontoDi)
non vengono interpretati nel modo corretto.

*)  


// funzioni prezzo di tipo string -> float

let prA = function
    | "cod1" -> 20.0
    | "cod2"->  50.50 ;;
  
let prB = function
    | "cod1" -> 40.0
    | "cod2"-> 100.50 ;;


// funzioni sconto di tipo string -> int

let scA = function
    | "cod1" -> 10
    | "cod2"-> 0 ;;


let scB = function
    | "cod1" -> 5 
    | "cod2" -> 25 ;;


// esempi di calcolo di costi

let c1AA = costo( "cod1" , prA, scA ) ;;
// val c1AA : float = 18.0

let c1BA = costo( "cod1" , prB, scA ) ;;
// val c1BA : float = 36.0

let c2BB = costo( "cod2" , prB, scB ) ;;
// val c2BB : float =  75.375
