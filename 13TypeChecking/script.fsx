module LZ13

// PART 1: TREE RECURSION  H&R SEC 6.7 PAG 142-144

type expr =
    | Number of int 
    | Sum of expr * expr 
    | Pow2 of expr  ;;

(*
-  recall the functions:

let rec toString = function 
 | Number n -> string n
 | Sum(e1,e2) -> "(" + toString e1 + "+" + toString e2 + ")"
 | Pow2(bse) -> "2^" + toString bse;;

let rec eval = function 
 | Number n ->  n
 | Sum(e1,e2) -> (eval e1) +  (eval e2)
 | Pow2(bse) -> pown 2 (eval bse);;

- they have the same structure, don't they?
*)

// primrec_expr:  fn:(int -> 'a) * fs:('a -> 'a -> 'a) * fp:('a -> 'a) -> e:expr -> 'a

let rec primrec_expr (fn, fs, fp) = function
  | Number n -> fn n
  | Sum(e1,e2) -> fs (primrec_expr (fn, fs, fp) e1) (primrec_expr (fn, fs, fp) e2)
  | Pow2(bse) -> fp  (primrec_expr (fn, fs, fp) bse);;

let ex1 =   Pow2(Sum(Number 7, Number 3));;

let pmeval = primrec_expr ((fun x -> x), (+), (fun bs -> pown 2 bs));;

let toString = primrec_expr ((fun x -> string x), 
                             (fun e1 e2 ->  "(" + e1 + "+" +  e2 + ")"),
                             (fun bs -> "2^" +  bs));;


// A bit of theory

(*
PRIMITIVE RECURSION

The set of primitive recursive function is the smallest set of
functions of natural mumbers that contains:

- is_zero(x) = 0
- s(x) = x + 1
- proj (x1 .. xn) i = xi

and is closed under composition
    f(x) = g(h(x) for h,g prim ric

and the PR scheme (for binary functions, slightly less general that usually stated):
  - f(x,0) = g(x)
  - f(x, n+1) = h(x,f(x,n))

*)


type mnat = Z | S of mnat;; 

let rec plus (x,n)  =
 match n with
  | Z -> x
  | S n' -> S (plus(x,n'));;

(*
 and we could go on defining recursively  *, -, exp,  etcetera
     	
or we could notice that plus has a PR definition where
  
  g is the identity and    h is the succ 

*)

// val pr2 : g:('a -> 'b) * h:('a -> 'b -> 'b) -> x:'a * n:mnat -> 'b

let rec pr2 (g,h) (x,n) = 
 match n with
 | Z -> g x
 | S n ->  let r = pr2 (g,h) (x, n)
           h x r;;

// now define plus prim-recursively

let mplus = pr2 ((fun x -> x), (fun _ r -> S r));;


let m1 = mplus(S (S Z), S ( S ( Z)));; // 2 + 2

(* The same with multiplication:

times(x,Z) = Z
times(x,S n) = plus(x,times(x,n))
*)

let mtimes = pr2 ( (fun _ -> Z), (fun x r -> mplus(x,r)) );;

let t1 = mtimes(S (S Z), S ( S ( S ( Z))));; // 2 * 3

// or evaluation to integers

let rec neval (n)  =
 match n with
  | Z ->  0
  | S n' -> 1 + neval(n')  ;;

let meval n  = pr2  ( (fun _ -> 0), (fun (x : int) r -> 1 + r) ) (0,n);;


(*
 EXERCISE: DEFINE EXPONENTIATION

exp(x,0) = 1
exp(x, y +1) = exp(x,y) * x 
*)


(* Note: Most arithmetic functions are prim-rec, but there are some
that are not, example Ackermann's functions, which grows very, very fast:

 es: ack(4,3) = 2^2^65536  -3

*)

let rec ack(m,n) = 
 match (m,n) with
  | (0,n') -> n' + 1
  | (m',0) -> ack(m'-1,1)
  | _ -> ack(m-1,ack(m,n-1));;

(* All prim-rec functions are total, that is terminating. Think FOR
loops.  However, sometimes the PR schema is difficult to use and it's
simpler to use general recursion. Think WHILE loops.

****************************************************************************  *)


// PART 2: TYPING


// a type expressions containg integers and booleans

type exp =
  | I of int                 (* integers *)
  | V of string              (* vars *)
  | Sum of exp * exp           (* addition *)
  | Diff of exp * exp        (* difference *)
  | Prod of exp * exp        (* products *)
  | B of bool                    (* true/false         *)
  | Eq of exp * exp           (* equality     *)
  | Neg of  exp            (* negation     *)
  | Less of exp * exp;;    (* less *)


// infix notation
let (++) m n = Sum(m,n);;
let (%%) m n = Prod(m,n);;
let (--) m n = Diff(m,n);;
let (==) m n = Eq(m,n);;
        

// for enviroments we use the class Map

type enviroment = Map<string,exp>;;

let anenv = Map.add "x" (I 3) (Map.add "a" (I -7) Map.empty);;

// 2 * (3 + x + a)
let  et  = I 2 %% (I 3 ++ V "x" ++  V "a");;

(*

 evaluation: eval : e:exp -> env:Map<string,exp> -> exp Note that
differently from yestarday we do not return an integer/bool, but an
exp. We'll see why shortly

*)

let rec eval e env =
    match e with
    | I n      ->  I n
    | B b     ->  B b
    | V s      -> Map.find s env
    | Sum(e1,e2) -> let (I n1) = eval e1 env
                    let (I n2) = eval e2 env
                    I (n1 + n2)
    | Diff(e1,e2)  -> let (I n1) = eval e1 env
                      let (I n2) = eval e2 env
                      I (n1 - n2)                                          
    | Prod(e1,e2)  -> let (I n1) = eval e1 env
                      let (I n2) = eval e2 env
                      I (n1 * n2)
    | Eq(n,m) -> let v1 = eval n env
                 let v2 = eval m env
                 B (v1 = v2) 
    | Less(n,m) ->  let (I v1) = eval n env
                    let (I v2) = eval m env
                    B (v1 < v2) 
    | Neg b ->  let (B nb) = eval b env
                B (not nb);;   


let p1 = eval et anenv;;

(*

 | Sum(e1,e2) -> let (I n1) = eval e1 env
  ------------------------^^^^^^

 warning FS0025: Incomplete pattern matches on this expression. For
 example, the value 'B (_)' may indicate a case not covered by the
 pattern(s).

What's going on here?

Incomplete pattern match signals our wishful thinking: we "know" that
eval should return an integer, but nothing is stopping us from doing
something silly such as negating an integer or summing two bools *)

let stupid = eval (Neg (I 3)) Map.empty;;

(*
This raises a Microsoft.FSharp.Core.MatchFailureException, (because
the f# compiler implicitely adds raise MatchFailureException to non
exhaustive patterns)

Another small issue: env may not contain the value of a certain key:

***************************************** 
We rewrite the interpreter
to make it "defensive", so that it raises exceptions if called on the
wrong arguments (or the variable is undefined).

First we introduce some exceptions (we do not handle them, though yet)

*)

exception NotABool;;
exception NotAInt;;
exception NotSameType;;
exception UndefVar of string;;

///  defensive evaluation
let rec evald e env =
    match e with
    | I n      ->  I n
    | B b     ->  B b
    | V s      -> match (Map.tryFind s env) with 
                  None -> raise (UndefVar s) 
                  | Some t -> t
    | Sum(e1,e2) -> match(evald e1 env, evald e2 env) with
                        | (I n1,I n2) -> I (n1 + n2)
                        | (_,_) -> raise NotAInt 
    | Diff(e1,e2)  -> match(evald e1 env, evald e2 env) with
                        | (I n1,I n2) -> I (n1 - n2)
                        | (_,_) -> raise NotAInt                                         
    | Prod(e1,e2)  -> match(evald e1 env, evald e2 env) with
                        | (I n1,I n2) -> I (n1 * n2)
                        | (_,_) -> raise NotAInt 
    | Eq(n,m) -> match(evald n env, evald m env) with
                        | (B b1,B b2)  -> B (b1 = b2)
                        | (I n1,I n2)  -> B (n1 = n2)
                        | (_,_) -> raise NotSameType  
    | Less(e1,e2)  -> match(evald e1 env, evald e2 env) with
                        | (I n1,I n2) -> B (n1 < n2)
                        | (_,_) -> raise NotAInt 

    | Neg b ->  match(evald b env) with
                (B nb) -> B (not nb)
                | _ -> raise NotABool;; 
(*
***********************************************
OK, now we get no warnings

    Happy puppy?

Not quite

This is called "dynamic typing" and basically is what happens in
languages such as Pearl, Python, Prolog, Lisp etc.

This is bad because

1. It is inefficient: we keep checking (class) tags at RUN-TIME 
2. It makes the code complicated: here just two tags, but think about real languages
3. It's pointless: these checks can be done at COMPILE-TIME

Hence, we do this as TYPE-CHECKING

Aside: it's true that in this toy language we could avoid run time
checks separating boolen expressions from arithmetic ones, or we could
interpret booleans as ints, but this is a general point


Why bother with types? Because they prevent mistakes. They are a sim-
ple, automatic way to find obvious problems in programs before these
pro- grams are ever run.

There are 3 kinds of types.

The Good: Static types that guarantee absence of certain runtime faults.

The Bad: Static types that have mostly decorative value but do not guaran-
tee anything at runtime.

The Ugly: Dynamic types that detect errors only when it can be too late.

Examples of the first kind are Java, F# and Haskell. In Java for instance,
the type system enforces that there will be no memory access errors, which in
other languages manifest as segmentation faults. F# and Haskell have even
more powerful type systems that can be used to enforce basic higher-level
program properties by type alone, for instance strict information hiding in
modules or abstract data types.

Famous examples of the bad kind are C and C++. These languages have
static type systems, but they can be circumvented easily. The language spec-
ification may not even allow these circumventions, but there is no way for
compilers to guarantee their absence.


Examples for dynamic types are scripting languages such as Perl and
Python. These languages are typed, but typing violations are discovered and
reported at runtime only, which leads to runtime messages such as “TypeEr-
ror: . . . ” 

The ideal for a static type system is to be permissive enough not to
get into the programmer’s way while being strong enough to achieve
Robin Milner’s slogan Well-typed programs cannot go wrong 

 What could go wrong? Some examples of common runtime errors are cor-
ruption of data, null pointer exceptions, nontermination, running out
of mem- ory, and leaking secrets. There exist type systems for all of
these, and more, but in practise only the first is covered in
widely-used languages such as Java, C#, Haskell,


*)

type tp = INT | BOOL;;

type tenviroment = Map<string,tp>;;

(* we define a function that returns the type of an an exp if
 well-typed in a typing context, otherwise it return an error message
 (with failwithf, for a change)

 tpchk : exp -> tenviroment ->  tp

Judgment: tenv |- e : t

** to myself: write the rules!!

*)
let rec tpchk e (tenv : tenviroment) = 
    match e with
     V s -> match (Map.tryFind s tenv) with 
                    None -> failwithf "Variable %s NOT in %A" s tenv  
                  | Some t -> t
    | I(_) -> INT
    | B(_) -> BOOL
    | Sum(e1,e2) | Prod(e1,e2) | Diff(e1,e2) -> 
                    let (t1,t2) = (tpchk e1 tenv, tpchk e2 tenv)
                    if t1 = INT && t2 = INT 
                        then t1 
                            else failwithf "%A and/or %A not an int" t1 t2
                            // cambiare less
    | Eq(e1,e2)  -> let (t1,t2) = (tpchk e1 tenv, tpchk e2 tenv)
                    if t1 = t2 
                                   then BOOL
                                     else failwithf "%A : %A, but %A : %A" e1 t1 e2 t2
    | Less(e1,e2)  -> let (t1,t2) = (tpchk e1 tenv, tpchk e2 tenv)
                      if t1 = INT && t2 = INT 
                                   then BOOL
                                     else failwithf "%A : %A, but %A : %A" e1 t1 e2 t2
    | Neg b -> match (tpchk b tenv) with
                | BOOL -> BOOL
                | t -> failwithf "%A not a boolean" t;;  
 


let atenv = Map.add "x" INT (Map.add "a" INT Map.empty);;
let p3 = tpchk et atenv;; 

let et2 =    (V "x" ++  V "a") == (I 2);;
let p4 = tpchk et2 atenv;; 

let et3 =    (V "x" ++  V "a") == (B true);;


// error (tpchk et3 atenv)

// Let's put the two toghether: evaluating only well typed programs
// tc_ev : e:exp -> tenv:tenviroment -> env:Map<string,exp> -> unit

// This is awful coding, but just to give you the idea

let tc_ev e tenv env= 
    try
        let tp = tpchk e tenv
        let v = eval e env
        ignore ; printf "evaluating %A yields %A"  e v
    with 
        | :? System.Exception -> tpchk e tenv |> ignore;;




let ok = tc_ev  et2 atenv anenv;; 
let notok = tc_ev et3 atenv anenv;; 

// *********************************************************************

// PART 3. IMP

type cmd  =                    (* statements             *)
   | Ass of string * exp       (* assignment             *)
   | Skip                      (* empty st               *)
   | Seq  of cmd * cmd         (* sequential composition *)
   | ITE   of exp * cmd * cmd  (* if-then-else           *)
   | While of exp * cmd;;      (* while                  *)

// y:=1 ; while not(x=0) do (y:= y*x ; x:=x-1)
// factorial with x free (to be set in the env)
let fac = Seq(Ass("y", I 1),
              While(Neg(Eq(V "x", I 0)),
                    Seq(Ass("y", Prod(V "x", V "y")) ,
                        Ass("x", Diff(V "x", I 1)) )));;


// executing a command (not defensive)
let rec exec cmd env =
    match cmd with
    | Ass(x,a)         -> let v = eval a env
                          Map.add  x v env
    | Skip             -> env
    | Seq(cmd1, cmd2)  -> let env' = exec cmd1 env
                          exec cmd2 env'
    | ITE(b,cmd1,cmd2) -> let (B test) = eval  b env
                          if test then exec cmd1 env else exec cmd2 env
    | While(b, cmd)    -> let (B test) = eval   b env
                          if test then let env' = exec cmd env
                                       exec (While(b, cmd)) env'
                                   else env
                                     ;;

// IF 2 = 2 THEN SKIP else x := 4
let ifexp1 = ITE(Eq(I 2, I 2),Skip,Ass("x", I 4));;
let p5 = exec ifexp1 Map.empty;;

// IF 2 * (3 + 2) = 10 THEN SKIP else x := 4
let ifexp2 = ITE(I 2 %%  (I 3 ++ I 2) == I 1,Skip,Ass("x", I 4));;
let p6 = exec ifexp2 Map.empty;;

// factorial
let s0 = Map.ofList [("x",(I 4))];;

let ff = exec fac s0;;

// type checking IMP programs; impOK : cmd:cmd -> tenv:tenviroment -> bool


(*
sample rule: 

tenv |- x : t	tenv |- a : t
------------------------------
    tenv |- x := a OK

*)

let rec impOK cmd tenv =
    match cmd with
    | Ass(x,a) -> let ta = tpchk a tenv
                  match (Map.tryFind x tenv) with 
                   None -> failwithf "Variable %s NOT in %A" x tenv  
                  |Some t -> if t = ta then true else failwith "types %A and %A do not match" t ta
    | Skip             -> true
    | Seq(cmd1, cmd2)  -> (impOK cmd1 tenv) && (impOK cmd2 tenv)
    | ITE(b,cmd1,cmd2) -> if (tpchk b tenv) = BOOL 
                            then (impOK cmd1 tenv) && (impOK cmd2 tenv) 
                               else failwith "type error in test"
    | While(b, cmd)    ->  if (tpchk b tenv) = BOOL 
                            then (impOK cmd tenv) 
                              else failwith "type error in test"
                                     ;;


let t0 = Map.ofList [("x", INT);("y", INT)];;

let fff = impOK fac t0;;

let rec pp = function
    [] -> ()
    | (x,y) :: xs ->  printf "%A has value %A\n" x y ; pp xs


let main cmd tenv state =
 let rec pp = function
    [] -> ()
    | (x,y) :: xs ->  printf "%A has value %A\n" x y ; pp xs

 if impOK cmd tenv 
    then 
        let res = exec cmd state
        pp (Map.toList  res) 
    else failwith "does not type check";;

let res = main fac t0 s0;;

