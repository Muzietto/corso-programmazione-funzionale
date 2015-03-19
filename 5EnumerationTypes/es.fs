
// ***** ESERCIZIO 1 ***

type figura = 
   | Rettangolo of  int *  int     // base * altezza
   | Quadrato   of  int            // lato
   | Triangolo  of  int * int  ;;  // base * altezza

let area fig =
   match fig with
   | Rettangolo(b,h) -> float ( b * h )   // la funzione deve restituire un valore di tipo float
   | Quadrato lato  -> float (lato * lato )
   | Triangolo(b,h) ->  float ( b + h )  / 2.0 ;;

let areaOpt fig =
   match fig with
   | Rettangolo (b,h) | Triangolo(b,h)  ->
       if b < 0 || h < 0 then None
       else Some (area fig)
   | Quadrato lato ->
       if lato < 0 then None
       else Some (area fig) ;;

(*

Non confondere l'espressione if-then-else di F# con l'omonima struttura di controllo
usata nei linguaggi di programmazione (C, Java, ..)

L'espressione

    if b < 0 || h < 0 then None
    else Some (area fig)

equivale all'espressione

   match  b < 0 || h < 0  with
     | true  ->  None
     | false ->  Some (area fig)

Analogamente,

   if lato < 0 then None
   else Some (area fig) 

e' equivalente a:

  match lato < 0 with
     |  true  ->  None
     |  false ->  Some (area fig)


*)   


// calcola la somme delle aree di fig1 e fig2 se definita

let sommaArea (fig1, fig2) =
    match( areaOpt fig1, areaOpt fig2) with
        | (None, _) | (_, None)   ->  None // l'area di fig1 o di fig2 non e' definita
        | ( Some a1 , Some a2 )   -> Some (a1 + a2) ;;

// esempi

let sum1 = sommaArea ( Rettangolo(2,5) , (Quadrato 10)) ;;
//  val sum1 : float option = Some 110.0

let sum2 = sommaArea ( Rettangolo(2,-5),  (Quadrato 10)) ;;
// val sum2 : float option = None

let sum3 = sommaArea ( Rettangolo(2, 5), (Quadrato -10)) ;;
// val sum3 : float option = None

// ***** ESERCIZIO 2 ******

// 2.a
// testa e coda di una lista, se definite

let head_tailOpt = function 
  | x :: xs ->  (Some x, Some xs  )
  | [] -> (None,None) ;;

// esempi

let h1 = head_tailOpt [ "uno" ; "due" ; "tre" ]  ;;
let h2 = head_tailOpt ([] : int list) ;;

// 2.b
// ultimo elemento di una lista, se definito

let rec lastOpt = function 
  | [] ->  None
  | [x] -> Some x
  | _ :: xs -> lastOpt xs ;;  // la lista xs non e' vuota

(**

NOTA
====

Il pattern

  [x]

equivale a  

   x :: xs when xs = [] 

E' preferibile usare la prima forma, piu' compatta e meglio leggibile

**)    


// esempi

let l1 = lastOpt [ "uno" ; "due" ; "tre" ] ;;
// val l1 : string option = Some "tre"

let l2 = lastOpt ( [ ] : int list ) ;;
// val l2 : int option = None
    
// 2.c
// estrae elementi di una option list, scartando i None 
    
let rec catOpt = function 
  | [] ->  []
  | (Some h) :: tail -> h :: catOpt tail
  | None ::tail  ->   catOpt tail ;;

// esempi

let lc1 = catOpt ([Some 1 ; None ; Some 2 ; Some 3 ; None] ) ;;                          
// val lc1 : int list = [1; 2; 3]

let lc2 = catOpt ([ None ; Some "cane" ; None ; None ; Some "gatto" ; Some "topo"] ) ;;  
// val lc2 : string list = ["cane"; "gatto"; "topo"]


// 2.d
// restituisce, se definito, l'elemento della lista ls in posizione n

let rec mynth (ls, n) =
    match (ls,n)  with
    | ( [], _ ) -> None
    | ( x::_ , 0) ->  Some x   
    | ( _ ::xs , _ ) ->  mynth ( xs,  (n - 1) )   ;;  

// esempi

let y1 = mynth (['a'..'z'], 0) ;;
// val y1 : char option = Some 'a'

let y2 = mynth (['a'..'z'], 2) ;;
// val y2 : char option = Some 'c'

let y3 = mynth (['a'..'z'], 30) ;;
// val y3 : char option = None



// ******  ESERCIZIO 3 ****************

type tagged = Int of int | Bool of bool;;
// un elemento di tipo tagged e' un bool o un int

type tList = tagged list;;

let tl1  = [ Int 0 ] ;;
let tl2  = [ Int 0 ; Int 1 ; Bool true ; Int 4 ; Bool false] ;; 
let tl3  = [ Int 3 ;  Bool (4>6) ; Int (44-66) ; Bool ( 10 = 5 + 5 )  ];;

// stringa che descrive un elemento v di tipo tagged
let printVal v =
    match v with
        | Int x ->  (string x) + " : int" 
        | Bool true ->   "true : bool" 
        | Bool false ->  "false : bool" ;;  


// stringa che descrive una lista tl di tipo tList ( = tagged list)
let rec printTl (tl : tList ) = 
    match tl with 
    | [] -> ""    // stringa vuota
    | [x] ->   printVal x
    | x :: xs ->  (printVal x) + "; " + printTl xs ;; // xs non e' vuoto
    
// esempi

let s1 = printTl tl1 ;;
// val s1 : string =  "0 : int"

let s2 = printTl tl2 ;;
// val s2 : string = "0 : int; 1 : int; true : bool; 4 : int; false : bool"


let s3 = printTl tl3 ;;
//  val s3 : string =  "3 : int; false : bool; -22 : int; true : bool"
