module E

// split : 'a list -> 'a list * 'a list

let rec split list  =
    match list with
      |  [] -> ([],[])
      |  x :: [] -> ([x] , [])
      |  x0 :: (x1 :: xs) ->
          let (even, odd) = split xs
          (x0 :: even, x1 :: odd)


let psplit = List.partition(fun x -> x % 2 = 0);;

let splitn lst = 
    let rec remove x n =
        match x with 
        | [] -> []
        | x::xs -> if n%2 = 0 then x :: remove xs (n+1)
                    else remove xs (n+1)

    in (remove lst 0,remove lst 1)

// esempi

let s3 = split ["anna" ; "barbara" ]      
//  s3 = (["anna"], ["barbara"])
let s4 = split ["ape" ; "bue" ; "cane"]  
//  s4 = (["ape"; "cane"], ["bue"])
let s5 = split [ 0 .. 10 ]    
//  s5 = ([0; 2; 4; 6; 8; 10], [1; 3; 5; 7; 9])
let s6 = split [ 0 .. 11 ]    
//  s6 = ([0; 2; 4; 6; 8; 10], [1; 3; 5; 7; 9; 11])


//---------   ESERCIZIO 2
        
let rec takeWhile p = function
    |[] -> []
    | x :: xs when p x -> x :: takeWhile p xs
    | _ :: xs  ->  [];;

// esempi

let p1 = takeWhile (fun x -> x < 10) [ 1 .. 100]
// p1 = [1; 2; 3; 4; 5; 6; 7; 8; 9]

let p2 = takeWhile (fun x -> x < 0) [ 1 .. 100]
// p2 = []
    
let p3 =  takeWhile (fun x -> x % 2 = 0) [2;4;5;6];;
// p3 = [2; 4]

let p4 = takeWhile (fun x -> x % 2 = 1) [2;4;5;6];;
// p4 = []

(*
- takewhile non è filter, che seleziona *tutti* gli x s.t. p x, non i primi

- con acc:

let takeWhile p list=
    let rec takeWhile acc = function
    |h::tl when (p h) -> takeWhile (h::acc) tl
    |_ -> List.rev acc

    takeWhile [] list;;

efficiente come spazio, ma qui si paga in tempo con la chiamata a rev

*)

//---------   ESERCIZIO 3



let safeDiv x y =
    match (x,y) with
    | (Some xx, Some yy) when yy <> 0 -> Some (xx / yy)
       | _ -> None;;

let d1 = safeDiv (Some 3) (Some 4);;
// d1 = Some 0

let d2 = safeDiv (Some 3) (Some 0);;
// d2 = None

let d3= safeDiv (Some 3) None;;
// d3 = None


// optMapBinary   : ('a -> 'b -> 'c) -> 'a option -> 'b option -> 'c option  
let optMapBinary : ('a -> 'b -> 'c) -> ('a option -> 'b option -> 'c option) = fun f x y -> 
  match (x,y) with
   | (Some xx, Some yy) -> Some (f xx yy)
   | _ -> None;;

// esempi

let x1 =  optMapBinary (fun a -> fun b ->  2*(a + b) ) (Some 2) (Some 3)
// x1 = Some 10

let x2 =  optMapBinary (fun a -> fun b ->  2*(a + b) )  None (Some -2)
// x2 = None

let x3 =  optMapBinary (fun a -> fun b ->  2*(a + b) )  (Some 10) None
// x3 = None

// optPlus : (int option -> int option -> int option)
let optPlus = optMapBinary (+);;

// optTimes : (int option -> int option -> int option)
let optTimes = optMapBinary (*);;

// esempi

let y1 = optPlus (Some 3) (Some 1);;
// y1 = Some 4

let y2 = optPlus (Some 3) None
// y2  = None

let y3 = optTimes (Some 2) (Some -5)
// y3 = Some -10

let y4 =  optTimes  (safeDiv (Some 1) (Some 0)) (Some 1)
// y4 = None


//---------   ESERCIZIO 4

type Form =
    | P of int
    | Not of Form
    | And of Form * Form
    | Or of Form * Form;;

let rec nnfn f =
    match f with
        | P n -> P n
        | Not (P n) -> Not (P n)
        | Not (Not f) -> (nnfn f)
        | Not(And(f1, f2)) -> nnfn(Or(Not f1,Not f2))
        | Not(Or(f1, f2)) ->  nnfn(And(Not f1,Not f2))       
        | And(f1,f2) ->  And(nnfn f1,nnfn f2)
        | Or(f1,f2) ->   Or(nnfn f1,nnfn f2);;

// small opt in dm rules: avoid one rec call
let rec nnf f =
    match f with
        | P n -> P n
        | Not (P n) -> Not (P n)
        | Not (Not f) -> (nnf f)
        | Not(And(f1, f2)) -> Or(nnf (Not f1), nnf (Not f2))
        | Not(Or(f1, f2)) ->  And(nnf(Not f1),nnf( Not f2))        
        | And(f1,f2) ->  And(nnf f1,nnf f2)
        | Or(f1,f2) ->   Or(nnf f1,nnf f2);;

// Esempi


let f1 = nnf (And( P 1, Not( Not (P 2))))
// f1 = And (P 1,P 2)

let f2 = nnf (Not( And( P 1, Not( Not (P 2) ))))
// f2 =  Or (Not (P 1),Not (P 2))

let f3 = nnf (Not(And(P 1, Not(Not (P 2)))));;
// f3  = Or (Not (P 1), Not (P 2))

let f4 = nnf (Or(Not (Not (P 1 )), And (P 2, P 3 )))
// f4 =  Or (P 1, And (P 2,P 3))

let f5 = nnf (Not(And(P 1, Not(Or(P 2, P 3)))))
// f5 =  Or (Not (P 1),Or (P 2,P 3))

let f6 = nnf (And(P 1, Not (P 2)));;
// f6 = And (P 1,Not (P 2))

let f7= nnf  ( And ( P 1, Not ( Or ( P 2 ,Not (And  ( P 3 , Not ( P 4 ))))))) 
// f7 = And (P 1,And (Not (P 2),And (P 3,Not (P 4))))

