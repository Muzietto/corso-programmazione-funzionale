
//*** ESERCIZIO 1

let f = fun x -> x + 1 ;;
let g = fun x -> x  +1 ;;
(*

  g ha tipo  (int -> 'a) -> 'a

  g puo' essere applicata a una qualunqe  funzione di tipo int -> 'a 

*)   


// esempi di applicazione di g

g f ;;
// val it : int = 2

(*
  g f = f +1        // sostituisco x con f in 'x +1'   (applicazione di g a f)
      = (+1) + 1    // sostituisco x con +1 in 'x + 1' (applicazione di f a +1)
      = 2           // valore espressione (+1) + 1   
*)   


g ( fun y ->  (y,y) );;
// val it : int * int = (1, 1)
(*
   g ( fun y ->  (y,y) ) = ( fun y ->  (y,y) ) +1
                         = (+1 , +1) 

*)    


let isPari x = x % 2 = 0 ;;
// val isPari : int -> bool

g isPari ;;
// val it : bool = false

(*

 g isPari =   isPari +1 
          =   (+1 % 2 ) = 0
          =   false 
*)   


//*** ESERCIZIO 2

// 2.1

let rec map f ls =
    match ls with
        | [] -> []
        | x :: xs -> f x :: map f xs   ;;

// val map :  ('a -> 'b) -> 'a list -> 'b list

// 2.2
let l1 = [ 1 .. 10]  ;;

let l2 = map ( fun n -> n*n) l1 ;;
// val l2 : int list = [1; 4; 9; 16; 25; 36; 49; 64; 81; 100]


let l3 = map  ( fun n -> if n%2=0 then (n,"pari") else (n,"dispari") ) l1 ;;
// val l3 : (int * string) list =
//   [(1, "dispari"); (2, "pari"); (3, "dispari"); (4, "pari"); (5, "dispari");
//    (6, "pari"); (7, "dispari"); (8, "pari"); (9, "dispari"); (10, "pari")]

// 2.3

let names = [ ("Mario", "Rossi") ; ("Anna Maria", "Verdi") ; ("Giuseppe", "Di Gennaro")] ;;

let names1 = map ( fun (name,surname) -> "Dott. " + name + " " + surname )   names ;;
// val names1 : string list =
//   ["Dott. Mario Rossi"; "Dott. Anna Maria Verdi"; "Dott. Giuseppe Di Gennaro"]


