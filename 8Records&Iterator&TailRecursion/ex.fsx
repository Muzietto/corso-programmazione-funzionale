module ItEx

let ipow2 n =
 let rec ipow2_aux(n,acc) = 
  match n with
   | 0 -> acc
   | n -> ipow2_aux(n-1,bigint 2 *acc)
 ipow2_aux(n,bigint 1);;

let fibonacci n =
  let rec f (a, b, n) =
    match n with
    | 0 -> a
    | 1 -> b
    | n -> f( b ,a + b, n - 1)
  f (bigint 0, bigint 1, n);;


// records

type valv = {stud : string; voto : int};;

type valg = {id : string; giudizio : string};;

let valuta (record) =
    let {stud = studente ; voto = v} = record
    match v with
        | v when v < 18 -> {id = studente;  giudizio = "insufficiente"}
        | v when v <= 22 -> {id = studente; giudizio = "sufficiente"}
        | v when v <= 27 -> {id = studente; giudizio =  "buono"}
        |  _ -> {id = studente; giudizio = "ottimo"};;

let valutaListr2 vs = List.map valuta  vs;;

// can't do with zip, as we build a list of records
let rec creaValListR (studenti : string list, voti : int list) =
    match (studenti, voti) with
        | ( [] , _ ) | (_ , [] ) -> []
        | ( st :: sts ,  v :: vs ) ->
            {stud = st; voto = v} :: creaValListR (sts, vs);;


let media valList =
    let rec sommaAndConta vList =
        match vList with
        | [] ->  (0 , 0)
        | ({stud = _; voto = v} : valv) :: vals  ->
            let (sum , n) = sommaAndConta vals
            (sum + v , n + 1)
    let (sommaVoti , count ) =  sommaAndConta valList
    if count = 0 then failwith "empty vote list"
    else  (float sommaVoti)  / (float count);;


let rec separaR2 valList =
    match valList with
        | [] ->  ( []  , [] )
        |  r :: vals ->
            let (bocciati, promossi) =  separaR2 vals
            if r.voto  < 18 then   (r :: bocciati , promossi)
            else   (bocciati, r :: promossi );;
