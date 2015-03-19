module Lez8

// records

(*

 plain tuples are useful in many cases. But they have some
 disadvantages too. Because all tuple types are pre-defined, you can't
 distinguish between a pair of floats used for geographic coordinates
 say, vs. a similar tuple used for complex numbers. And when tuples
 have more than a few elements, it is easy to get confused about which
 element is in which place.

In these situations, what you would like to do is label each slot in
the tuple, which will both document what each element is for and force
a distinction between tuples made from the same types.

A record type is exactly that, a tuple where each element is labeled.
*)

type valv = {stud : string; voto : int};;


(* To create a record value, use a similar format to the type
definition, but using equals signs after the labels. This is called a
"record expression."  *)

let rossirec = {stud = "Rossi" ; voto = 20};;

(* order does not count, they get printed according to order on labels *)

let bianchirec = {voto = 16; stud = "Bianchi" };;

//  And to "deconstruct" a record, use pattern matching

let {stud = s; voto = v} = rossirec;;

// if you do not care about one value, use underscore

let {stud = _; voto = vt} = rossirec;;

// or use the dot notation

let v1  = rossirec.voto;;

(*
 however, use different labels: the following is accepted
 type valg = {stud : string; giudizio : string};;

but creates ambiguity. Morale: use different labels
*)

type valg = {id : string; giudizio : string};;


// records are immutable, so we do not assign: we create **new ones**
// "change" Rossi's vote to 25 

let newrossirec = {stud = "Rossi" ; voto = 25};;

// or using the "with" notation

let newrossirecs = {rossirec with voto = 25};;

// back to our example

let studss = [ {stud = "Bianchi"; voto = 16} ; 
               {stud = "Rossi" ; voto = 20} ; 
               {stud = "Verdi"; voto = 24 } ; 
               {stud = "Neri" ; voto = 29} ];;

// valuta valv -> valg

let valuta (record) =
    let {stud = studente ; voto = v} = record
    match v with
        | v when v < 18 -> {id = studente;  giudizio = "insufficiente"}
        | v when v <= 22 -> {id = studente; giudizio = "sufficiente"}
        | v when v <= 27 -> {id = studente; giudizio =  "buono"}
        |  _ -> {id = studente; giudizio = "ottimo"};;

let tr = valuta rossirec;;

let rec valutaListr valList =
    match valList with
        | [] -> []
        | c :: vals ->
            valuta c :: ( valutaListr vals );;

let tss = valutaListr studss;;

let valutaListr2 vs = List.map valuta  vs;;


// can't do with zip, as we build a list of records
let rec creaValListR (studenti : string list, voti : int list) =
    match (studenti, voti) with
        | ( [] , _ ) | (_ , [] ) -> []
        | ( st :: sts ,  v :: vs ) ->
            {stud = st; voto = v} :: creaValListR (sts, vs);;


// ITERATION

let rec fact = function
    | 0 -> 1
    | n -> n * fact(n-1);;


let  ifact n = 
 let rec factA = function
    | (0,m) -> m
    | (n,m) -> factA(n-1,n*m)
 factA(n,1);;

// theorem: forall n m, factA(n,m) = m * !n
// proof: induction on n
// corollary: ifact n = !n

let rec suml = function
    | [] -> 0
    | x :: xs -> x + suml xs;;

let isuml xs =
  let rec sumlA = function
    | ([],m) -> m
    | (x::xs,m) -> sumlA(xs,x+m)
  sumlA(xs,0);;

// theorem: forall xs : int list, m : int, sumlA(xs,m) = m + suml(xs)
 
(*
isuml [1..1000000];;
Real: 00:00:00.201, CPU: 00:00:00.296, GC gen0: 2
val it : int = 1784293664
> suml [1..1000000];;
Stack overflow:

*)
