module L

(* *** if-then-else expressions

 * Form: if b then e1 else e2

 * Evaluation rules:

   if true then e1 else e2 --> e1
   
   if false then e1 else e2 --> e2

   *)

if 1 < 2 then 3 else 245;;

// what about this ?

// if 1 < 2 then 3 else "ciao";;

(*
 * remember: both arms must have **same** type -- static vs dynamic


 * remember: in FP there is no such thing as if-then without "else"

   -- (actually there is in F#, but in the monadic fragment)

 * remember: do **no** write

   	     if e1 = true then e2 else e3

   write:
	     if e1 then e2 else e3

*)
// Alternative declarations: fact with if - then - else
let rec fact_if n =
    if n=0 then 1 else n * fact_if(n-1);

let rec fact m =
    match m with
        | 0 -> 1
        | n -> n * fact ( n-1 );;

let rec exp_if(bse,n) =
    if n=0 then 1.0 else bse * exp_if(bse,n-1);;

let rec exp (bse, m ) =
    match m with
        | 0  -> 1.0
        | n  ->  bse *  exp (bse, n-1);;

// Use of patterns usually gives more understandable programs



(* *** PATTERN MATCHING RECAP	

General form

 match exp with 
  | pat1 -> exp1
   ....
  | patn -> expn

The idea: analyse the possible shape of exp (according to its type)

* if m : int

let rec sum1 m =
    match m with
        | 0 -> 0
        | n -> n + sum1 (n-1)

* if m : bool

let mnot m =
    match m with
        | true -> false
        | false -> true 

if m : bool * bool

let And m  =
    match m with
    | (true, true) -> true
    | _ -> false  


etc...

*)


(* *** TYPE INFERENCE


let rec exp (bse, m ) =
    match m with
        | 0  -> 1.0                          (* 1 *)
        | n  ->  bse *  exp (bse, n-1);;   (* 2 *)

• The type of the function must have the form: τ1 * τ2 -> τ3 , because
argument is a pair.

• τ3 = float because 1.0 : float (Clause 1, function value)

• τ2 = int because 0:int.      (Clause 1, pattern matching on m)

• bse *  exp (bse, n-1) : float, because τ3 = float.

• multiplication can have

  		 int*int -> int or float*float -> float
  
  as types, but no “mixture” of int and float

• Therefore bse:float and τ1 =float.

The F# system determines the type float*int -> float


*** MORE LATER ****
*)

(* *** Partial functions
	- f on some input may not terminate 
	- it may be undefined (1/0)


factorial  loops with negative input: (fact -6) = -6 * (fact -7) etc ...

let rec fact m =
    match m with
        | 0 -> 1
        | n -> n * fact ( n-1 );;
            

  ==> Make function **total** 
      1. exceptions
      2. options
	
For now, we just use a string exception and we (for the tiem being) do not handle it.

failwith : (string -> 'a)
*)
let rec gfac m =
    match m with
    | 0 -> 1
    | n when  n > 0 -> n * gfac (n-1)
    | _ ->  failwith "Negative argument to fact";;


// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

(* ****LISTS***** *)

// A list is a finite sequence of elements having the same type:
// [v1 ; . . . ; vn ]  where [ ] is called the empty list

 
let list0 : int list = [] ;;
let bl = [true; false] ;;
let list1  = [1] ;;
let list2            = [2;3] ;;
 
let rlist1  = [3.1; 2.6; 7.8;1.56] 
 
// list of any other type, e.g.pairs 
 
let ps = [("a",3);("ggg",6)] 
 
// list of any other bools -- note eager evaluation 
  
let bs = [1 < 2; 2>1; true] 
 
// divide by zero 
// let ww = [1; (1 / 0)]  
 
// lists of functions;  (float -> float) list 
 
let fs = [abs;cos;sin;tan]  
  
// lists of lists 
 
let xss : (float list) list = [rlist1;rlist1] 
 
// list can be part of other types, such as tuples int * string * (float list) list  
 
let alisttr = (1,"qq",xss) 
 
// HOMOGENOUS DATATYPES: elements of a list must have the same type!! 

// this does not type check 
 // let ws = [1;true] 

// We will see how to do heterogenous list next lecture, if you really
// want to, but why should you.

  // For the time being, use tuples

let onet = (1,true);;


// or better (immutable)  records
type aRec = {value : int; flag : bool};;
let rc = {value = 1; flag = true};;
 
// More about records later on

 
///////////////////////////////////////////////////////


// Range expressions: useful to introduce lists on the fly

let ns1 = [3..31] ;;

(*
 a more general form: use n step
 [n .. k .. m] is the list [n;n+k;n+2k;...;m] if n <= m, otherwise []
*)

let ns11 = [3..1..31] ;;
let ns12 = [3..2..31] 

// going the other way
let s6t1 = [6 .. -1 .. 0] 
 
// not just ints 
let pis = [0.6 ..3.3.. System.Math.PI*10.0] 

let char_a_j = ['a' .. 'j'];;

(*

range expression are just an example of list *comprehension*, which are a form of *computational* expression, which we shall see at the end of the course.

To wet your appetite, here's a definition of a function that returns
the list of n and its square

*)
 
let squares n = [ for x in 1 .. n -> (x, x*x) ];;
squares 4;;


//////////////////////////////////////////////////////////////////// 

// constructors: [] and :: -- consing 
(*
A non-empty list [x1 , x2 , . . . , xn ], n ≥ 1, consists of
• a head x1 and
• a tail [x2 , . . . , xn ]

   An inductive definition: 
    - [] is a list 
    - is x is an element and xs a list, then x :: xs is a list 
    - nothing else is a list  

  [2;3]   as a tree:

                ::
               /   \
              /     \
             2       ::
                     / \
                    /   \
                   3     []


  '::' associates to the right 
*) 

let dt = [2;3];;

// just sugar fot

let dtu = 2 :: 3 :: [];;


list2;;
let list3 = 3 :: list2 
 
 
// pattern matching 
 
let x::xs = 2 :: 3 :: 1 ::[];;
let [x1;x2;x3] = [3;2;1];; 
 


(* *** RECURSION ON LISTS

- General form 

let rec f ... ys ... =
 match ys with 
 | [] -> v
 | x::xs -> .... f xs ...

using just two clauses.

*)


// a first function on lists 
// sumlist : int list -> int 

let rec sumlist ys =  
    match ys with 
        | [] -> 0 
        | x::xs -> x + sumlist xs ;;
 
let ss = sumlist [3..8] ;;
 
// returning the pair of the sum and product of an int list
let rec sumprod xs =  
    match xs with 
        | [] -> (0,1) 
        | x::xs ->  
            let (sm,pr) = sumprod xs 
            (x + sm,x* pr) ;;

let sp = sumprod [3..8] ;;

// note pattern matching on pairs:  let (sm,pr) = sumprod xs 

let (fst,snd) = (1+3,true);;
 

 
// pattern matching on pairs of lists 
// assume same length 
let rec mix (l1,l2) =  
    match (l1,l2) with 
    | ([],[]) -> [] 
    | (x::xs, y::ys) -> x::y:: (mix (xs, ys)) 
    | (_,_) -> failwith "lists not same length" ;;
 
let t =  mix ([1..3],[4..6]) ;;
 
 
(* ****  exercise:   **** *)

// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

(* *** POLYMORPHISM *) 
 
let rec ilen (xs : int list) =  
    match xs with 
    | [] -> 0 
    | x::xs -> 1 + ilen xs 
 
let ls =   ilen [3..7] 
 
let rec clen (xs : char list) =  
    match xs with 
    | [] -> 0 
    | x::xs -> 1 + clen xs 
 
let ls2 = clen ['a'..'z'] 
 
 
let rec len xs =  
    match xs with 
    | [] -> 0 
    | x::xs -> 1 + len xs 
 
let ls11 = len [3..7] 
let ls12 = len ['a'..'z'] 
 
(*  append: 'a list * 'a list -> 'a list 
       append(l1, l2) returns a list consisting of the elements of l1 
                      followed by the elements of l2. 
 
   NOTE:  This operation is (already) defined  via the right-associative infix operator "@". 
 appn ([] ,ys )      = ys 
 appn ([x1..xn], ys)  = x1 :: appn ([x2..xn], ys)  
*) 
 
let rec appn (xs, ys) = 
    match xs with 
    | [] -> ys 
    | z::zs -> z :: appn (zs, ys) 
 
 
let app12   = appn (rlist1, rlist1) 
let app12'' = List.append [1] [2] 
let app122 =  [1] @ [2] 


(*  naive reverse 
spec:
rev [x1..xn] = [xn .. x1]
rec eq:
rev [x1..xn] = (rev [x2..xn]) @ [x1]
*)
 
let rec srev xs =  
    match xs with 
    | [] -> [] 
    | y :: ys -> appn (srev ys, [y]) 
 
let bfrev xs = 
    let rec rev_aux xs acc = 
        match xs with  
        | [] -> acc 
        | y :: ys -> rev_aux ys (y :: acc) 
    rev_aux xs [] 
 
 
 
// invariant |xs| = |ys| 
//  ('a list * 'b list -> ('a * 'b) list) 
let rec zip (xs,ys) =  
    match (xs,ys) with 
    | ([],[]) -> [] 
    | (x::xs,y::ys) -> (x,y):: zip(xs,ys) 
 
let zz = zip ([1..2], ['a';'b']) 
 
 
//List.unzip;; 
// val it : (('a * 'b) list -> 'a list * 'b list) 
 
let rec unzip xs =  
    match xs with 
    | [] -> ([],[]) 
    | (x,y)::rs ->  
        let (xs,ys) = unzip rs 
        (x::xs,y::ys)  
 
let us = unzip [(1, 'b'); (2, 'a'); (3, 'x'); (4, 'w')] 
 
// they are inverse 
 
let idz = (zip ([1;2], ['a';'b']) |> unzip) = ([1;2], ['a';'b']) 
 
let udz = unzip [(1, 'b'); (2, 'a'); (3, 'x'); (4, 'w')] |> zip = [(1, 'b'); (2, 'a'); (3, 'x'); (4, 'w')] 
 
 
// an example: removing all duplicates 
 
// remove : ('a * 'a list) -> 'a list when 'a : equality 
// remove all occurrences of  element from a list 
 
let rec remove (x, xs) = 
    match xs with 
    | [] -> [] 
    | y::ys -> if x=y then remove (x, ys) else y :: remove (x ,ys) 
 
let rrr = remove (1, [1;2;1;2;3]) 
 
 
// removeDup : 'a list -> 'a list when 'a : equality 
let rec removeDup xs =  
    match xs with 
    | [] -> [] 
    | y::ys ->  
        let rms = (remove (y, ys)) 
        y::removeDup rms 
 
let rrd = removeDup [1;2;1;2;3] 
 
 
// equality types 
// mem : 'a * 'a list -> bool when 'a : equality 
 
let rec mem (x, xs) =  
    match xs with 
    | [] -> false 
    | y::ys -> x=y || mem (x, ys) 
     
 
let rec meml (x, xs) =  
    match xs with 
    | [] -> false 
    | y::ys -> x > y || meml (x, ys) 
 
 
let b1 = mem (2, list2) 
 
// but this will fail  
//mem((tan : float -> float),fs);; 
// The type '(float -> float)' does not support the 'equality' constraint because it is a function type
