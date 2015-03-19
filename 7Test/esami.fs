module Es

#light

//  Dato uno studente e un voto, crea la valutazione (studente, giudizio),


let valuta (studente,  voto) =
    match voto with
        | voto when voto < 18 -> (studente, "insufficiente")
        | _ when voto >= 18 && voto <= 22 -> (studente, "sufficiente")
        | _ when voto >= 23 && voto <= 27 -> (studente, "buono")
        |  _ -> (studente, "ottimo");;


// Data una lista di valutazioni (studente,voto), costruisce la  lista di valutazioni (studente,giudizio)

let rec valutaList valList =
    match valList with
        | [] -> []
        | (st,v) :: vals ->
            valuta (st, v ) :: ( valutaList vals )


let valutaList2 vs = 
       List.map (fun (st, v) -> valuta (st, v)) vs;;

// eta contratta
let valutaList3 vs = List.map valuta  vs;;



let giudizi = valutaList [ ("Bianchi", 16) ; ("Rossi" , 20) ; ( "Verdi",  24 ) ; ( "Neri" , 29) ]

(*
 Data una lista di studenti e una lista di voti, crea la lista delle
valutazioni (studente,voto)  Se le liste non hanno la stessa
lunghezza, la parte in eccedenza non viene considerata.
*)

let rec creaValList (studenti, voti) =
    match (studenti, voti) with
        | ( [] , _ ) | (_ , [] ) -> []
        | ( st :: sts ,  v :: vs ) ->
            let valList = creaValList (sts, vs)
            (st,v) :: valList;;

// E' zip uncurried

let uncurry f (a,b)= f a b;;

let creaValListz=  List.zip;;


// alcuni test

let st1 = [ "Alpi" ; "Brambilla" ; "Ceri" ;  "Dusi" ;  "Elba" ;  "Ferrari" ;  "Gigli " ; "Ibis" ]

let st2 = [ "Verdi" ; "Rossi" ] 

let voti1 = [ 16 ; 24 ; 28 ; 18 ; 15 ;  23 ; 30  ;28 ]    

let voti2 =  [ 24 ; 18 ; 30 ; 28 ]


let valList1 = creaValList st1 voti1

let valList2 = creaValList st1 voti2

let valList3 = creaValList st2 voti1


// Data una lista di valutazioni con voti, calcola la media dei voti

let media valList =
    let rec sommaAndConta vList =
        match vList with
        | [] ->  (0 , 0)
        | ( _ , v ) :: vals  ->
            let (sum , n) = sommaAndConta vals
            (sum + v , n + 1)
    let (sommaVoti , count ) =  sommaAndConta valList
    if count = 0 then failwith "empty vote list"
    else  (float sommaVoti)  / (float count)
   

// con due passate, zip e average, non esiste una fold che opera su coppie di liste

let media_ho zs = 
   let (_,vs) = List.unzip zs 
   List.averageBy (fun elem -> float elem)  vs;;


// alcuni test

let media1 = media valList1

let media2 = media valList2

let media3 = media valList3


// Data una lista di valutazioni (studente,voto), crea due liste di valutazioni,
// la lista dei bocciati (voto < 18) e la lista dei promossi (voto>= 18)


let rec separa valList =
    match valList with
        | [] ->  ( []  , [] )
        |  (st,voto) :: vals ->
            let (bocciati, promossi) =  separa vals
            if voto < 18 then  ( (st,voto) :: bocciati , promossi)
            else   (bocciati, (st,voto) :: promossi )


// alcuni test


let  (bocciati,promossi )  = separa  valList1 
        
