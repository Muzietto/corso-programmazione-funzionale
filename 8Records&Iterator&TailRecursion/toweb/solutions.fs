module Solutions

//  Dato uno studente e un voto, crea la valutazione (studente, giudizio),

let valuta (studente,  voto) =
    match voto with
        | v when v < 18 -> (studente, "insufficiente")
        | v when v <= 22 -> (studente, "sufficiente")
        | v when v <= 27 -> (studente, "buono")
        |  _ -> (studente, "ottimo");;


// Data una lista di valutazioni (studente,voto), costruisce la  lista di valutazioni (studente,giudizio)

let rec valutaList valList =
    match valList with
        | [] -> []
        | (st,v) :: vals ->
            valuta (st, v ) :: ( valutaList vals );;

// nota: non vi è bisogno di specificare che la funzione opera su coppue di liste

let rec valutaList1 valList =
    match valList with
        | [] -> []
        | c :: vals ->
            valuta c :: ( valutaList1 vals );;


// Vedi prossima lezione
let valutaList3 vs = List.map valuta  vs;;


(*
 Data una lista di studenti e una lista di voti, crea la lista delle
valutazioni (studente,voto)  Se le liste non hanno la stessa
lunghezza, la parte in eccedenza non viene considerata.
*)

let rec creaValList (studenti : string list, voti : int list) =
    match (studenti, voti) with
        | ( [] , _ ) | (_ , [] ) -> []
        | ( st :: sts ,  v :: vs ) ->
            (st,v) :: creaValList (sts, vs);;

// Vedi prossima lezione: List.zip

// Data una lista di valutazioni con voti, calcola la media dei voti

let media valList =
    let rec sommaAndConta vList =
        match vList with
        | [] ->  (0 , 0)
        | ( _ : string , v ) :: vals  ->
            let (sum , n) = sommaAndConta vals
            (sum + v , n + 1)
    let (sommaVoti , count ) =  sommaAndConta valList
    if count = 0 then failwith "empty vote list"
    else  (float sommaVoti)  / (float count);;
   

// Vedi prossima lezione: con due passate, zip e average, 
// non esiste una fold che opera su coppie di liste

let media_ho zs = 
   let (_,vs) = List.unzip zs 
   List.averageBy (float)  vs;;

// versione iterativa con accumulatori -- vedi seconda parte della lezione

let mediaA listaStudenti =
    let rec sommaAndConta (listaStudenti, total, count) =
        match listaStudenti with
        | [] -> float total / float count 
        | (_, voto):: rest ->         
            sommaAndConta (rest, (total + voto), (count + 1))
    sommaAndConta (listaStudenti, 0, 0);;

// Data una lista di valutazioni (studente,voto), crea due liste di valutazioni,
// la lista dei bocciati (voto < 18) e la lista dei promossi (voto>= 18)


let rec separa valList =
    match valList with
        | [] ->  ( []  , [] )
        |  (st,voto) :: vals ->
            let (bocciati, promossi) =  separa vals
            if voto < 18 then  ( (st,voto) :: bocciati , promossi)
            else   (bocciati, (st,voto) :: promossi );;

// generalizziamo a qualunque predicato "p" : 'a list -> bool

let rec filter2 (p, xs) =
 match xs with
  | [] -> ([],[])
  | (x::xs) ->  let (l,r) = filter2 (p, xs) 
                if (p x) then (x::l,r) else (l,x :: r) 
// la chiamata

let separa2 xs = filter2((fun voto ->  voto < 18 ),xs);;


// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

type category = Daycare | Nursery | Recreation;;

type name = string;;

type childDes = C of name * category ;;

let ds = [C("a", Daycare); C("b", Daycare); C("a", Nursery);C("c", Nursery)];;

let rec number (c ,ds) = 
   match ds with
   | [] -> 0
   | C (n,cat) :: xs  when c = cat -> 1 + (number (c,xs ))
   | _ :: xs  ->  number (c,xs) ;;

let tn = number(Daycare,ds);;

let month_charge  = function
 | Daycare -> 225.
 | Nursery  -> 116.
 | Recreation -> 100. ;;

// remove first occ of x and return charge
// remove : x:name * xs:childDes list -> childDes list * float
let rec remove (x, xs) = 
    match xs with 
    | [] -> ([],0.)
    |  C (n,cat)::ys when x=n ->  (ys,month_charge cat) 
    |  c :: cs -> let (rest,k) = remove(x,cs) in (c:: rest,k);;
 
// assume family occurs
// payment : family:name * ds:childDes list * nodiscount:float -> float
//  pay : family:name * ds:childDes list -> float

let pay(family,ds) = 
   let rec payment (family, ds : childDes list,nodiscount) = 
    match ds with
    | [] -> nodiscount
    | C (n,cat) :: xs  when n = family -> ((month_charge cat) / 2.) + (payment (family ,xs,nodiscount ))
    | _ :: xs  ->  payment (family,xs,nodiscount) 
   let (rest, nodiscount) = remove(family, ds) 
   payment(family,rest,nodiscount);;

// just one pass (Vavassori)

let cost = month_charge;;

let payV (who , chlis) =
    let rec pay_aux chlis bill=
        match chlis with
        |[] -> bill
        |C(n,c)::xs when n=who -> if bill = 0.00 then pay_aux xs (bill + cost c)
                                      else pay_aux xs (bill + (cost c)/2.00)
        |_::xs -> pay_aux xs bill
    pay_aux chlis 0.00;; 

// with a flag -- it works (I think) because it just moves from 0 to 1 and that's it
let payf = fun (name, child:childDes list) ->
   let rec pay (name, child) flag = 
        match child with
        | [] -> 0.0
        | C(n,c)::cs -> if n = name then
                                    if flag = 0 then 
                                      cost c + (pay(name,cs) 1)  
                                    else cost c / 2.0 + pay(name,cs) flag
                               else pay(name,cs) flag
    in pay (name, child) 0;;

