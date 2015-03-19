// a fragment of MinML, implemented
#light 

module ML

type tp = 
   Nat 
 | Arr of tp * tp;; 

let (-->) t1 t2 = Arr(t1,t2);;

type var = string;;

type decl = (var * tp);;

type exp =
    | V of var
    | Z
    | S of exp
    | Case of exp * exp * var * exp
    | Fun of decl  * exp
    | Ap of exp * exp 
    | Rec of decl  * exp;;

// some abbreviations
let (@@) t1 t2 = Ap(t1,t2);;
let vx = V "x";;
let vx' = V "x'";;
let vy = V "y";;

// some sample expression

let idn =  Fun (("x", Nat),V "x");;
let idfs =  Fun (("x", Nat --> Nat),V "x");;
let predF = Fun (("x", Nat),Case(V "x",Z,"y",V"y"));;
let plus = Rec(("add", Nat --> (Nat --> Nat)),
               Fun (("x", Nat),
                 Fun(("y", Nat),
                   Case(vx,vy,"x'", S ((V "add" @@ vx') @@ vy)))));;


// type enviroments
type tenviroment = Map<var,tp>;;

let tempty = Map.empty;;

// exceptions for ill-typed terms -- note they carry arguments

exception VarNotFound of string * tenviroment;;
exception NatExpected of exp * tp;;
exception ArrExpected of exp * tp;;
exception TypeMismatch of exp * tp * tp;;


// type checking  tpcheck: e:exp -> tenv:tenviroment -> tp
// corresponds to inversion rules

let rec tpchk e (tenv : tenviroment) = 
    match e with
     V s -> match (Map.tryFind s tenv) with 
                    None -> raise (VarNotFound(s,tenv))  
                  | Some t -> t
    | Z -> Nat 
    | S e -> match (tpchk e tenv) with
              | Nat -> Nat
              | t -> raise (NatExpected(e, t))
    | Case(e1,e2,x,e3) -> 
        let _ = 
            (match (tpchk e1 tenv) with
                | Nat -> Nat
                | t -> raise (NatExpected(e1, t)))
        let t2 = tpchk e2 tenv
        let tenvx = (Map.add x Nat) tenv
        let t3 = tpchk e3 tenvx
        if t2 = t3 
            then t3 
                else raise(TypeMismatch(e3,t2,t3))
                                                
    | Fun((x,tp),e) -> let tenvx = (Map.add x tp) tenv
                       let trg = tpchk e tenvx
                       (tp --> trg)
    | Rec((x,tp),e) -> let tenvx = (Map.add x tp) tenv
                       let tp' = tpchk e tenvx
                       if tp = tp' 
                        then tp 
                            else raise (TypeMismatch( e, tp, tp')) 
    | Ap(e1,e2) -> match (tpchk e1 tenv) with
                    | Arr(t,t') -> let t2 = (tpchk e2 tenv)
                                   if t2 = t 
                                    then t' 
                                        else raise (TypeMismatch( e2, t, t2)) 
                    | s -> raise (ArrExpected( e1, s))
                    ;;  


// some tests
let test1 = tpchk (S Z) tempty;;

let test = tpchk idn tempty;;

let test2 = tpchk idfs tempty;;

let test3 = tpchk idfs (Map.add "x" Nat tempty);;

let test4 = tpchk vx (Map.add "x" Nat tempty);;

let test5 = tpchk (idn @@ vx) (Map.add "x" Nat tempty);;

let test6 = tpchk (predF @@ (S Z)) tempty;;

let test7 = tpchk (plus @@ (S Z)) tempty;;

// must fail
let test8 = tpchk (idfs @@ vx) (Map.add "x" Nat tempty);;


// op semantics. We introduce "runtime" expressions, no type tags

type rexp =
    | RV of var
    | RZ
    | RS of rexp
    | RCase of rexp * rexp * var * rexp
    | RFun of var  * rexp
    | RAp of rexp * rexp 
    | RRec of var  * rexp;;

// type erasure: we can get rexp from exp by removing type tags from vars
// erase : _arg1:exp -> rexp

let rec erase = function
  V s -> RV s
 | Z -> RZ
 | S n -> RS (erase n);
 | Case(e1,e2,x,e3) -> RCase(erase e1, erase e2, x, erase e3)
 | Ap(e1,e2) -> RAp(erase e1, erase e2)
 | Fun((x,_),e) -> RFun(x,erase e)
 | Rec((x,_),e) -> RRec(x,erase e);;

// substitution subst : x:var -> b:rexp -> a:rexp -> rexp
// subst x b a = a where x := b

let rec subst x b a  = 
  match a with
  | RV y -> if  x = y then b else RV y
  | RZ -> RZ
  | RS n -> RS (subst x b n)
  | RCase (a1,a2,y,a3) -> RCase(subst x b a1, subst x b a2, y,  if x = y then a3 else subst x b a3)
  | RFun (y, a1) -> RFun (y, if x = y then a1 else subst x b a1)
  | RRec (y, a1) -> RRec (y, if x = y then a1 else subst x b a1)
  | RAp (a1, a2) -> RAp (subst x b a1, subst x b a2);;


// tests
let rvx = RV "x";;
let rvy = RV "y";;

let s1 = subst  "x" RZ (erase idn);;
let s2 = subst  "x" RZ (RFun("y", RAp(rvx,rvy)));;

// evaluation on **closed** espressions
let rec eval e = 
    match e with  
    | RZ -> RZ
    | RS n -> RS (eval n)
    | RCase(e1,e2,x,e3) -> match (eval e1) with
                          RZ -> eval e2
                         | RS v -> subst x v e3 |> eval
    | RAp(a,b) -> let vb = eval b
                  match (eval a) with
                   | RFun(x,c) -> subst x vb c |> eval
    | RFun(x,e) -> RFun(x,e)
    | RRec(x,e) -> subst x (RRec(x,e)) e |> eval;;
 


(*
Don't worry about the incomplete pattern matching: 

1. we only evaluate closed program so you never have to eval RV "x"

2. in the RAp case,  the uniqueness of the

 match (eval a) with
               | RFun(x,c) -> ...

is actually a meta-theoretic property that can be proved

*)

// tests
let t1 = eval (RS RZ);;
let t2 = erase (idn @@ Z) |> eval;;
let t3 = eval ( RAp (RFun ("x",RCase (RV "x",RZ,"y",RV "y")),RS RZ));;
let t4 = erase ((plus @@ (S Z)) @@ (S Z)) |> eval;;


(*
- lazy vs eager evaluation

 -- eval (RRec(x,x)) does not terminate, hence 

    eval (RAp(RFun("y",RZ), RRec("x",rvx)));;

diverges as well, although morally it should return RFun("y",RZ). This is because
F# (and MinML) have an **eager** evaluation strategy: arguments are
fully evaluted before being passed on to a function. There are very
good reasons for it, but it's not compulsory. In a **lazy** languege
such as Haskell, this does not happen and Z is actually returned.

There are degree in lazyness: here we stick to the only call-by-name
functions, but one can imagine lazy pairs, even lazy numbers such as succ ( rec x.x)*)


let rec lazyeval = function  
 | RZ -> RZ
 | RS n -> RS (lazyeval n)
 | RCase(e1,e2,x,e3) -> match (lazyeval e1) with
                          RZ -> lazyeval e2
                         | RS v -> subst x v e3 |> lazyeval
 | RAp(a,b) ->                                      // LAZY: b does not get evaluated
               match (lazyeval a) with
               | RFun(x,c) -> subst x b c |> lazyeval
 | RFun(x,e) -> RFun(x,e)
 | RRec(x,e) -> subst x (RRec(x,e)) e |> lazyeval;;

// example
let ll =     eval (RAp(RFun("y",RZ), RRec("x",rvx)));;
