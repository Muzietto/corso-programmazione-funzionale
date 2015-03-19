// extending the implementation of set.fs as tree with count and map

module  FSet

type 'a binTree =
    | Null    // empty tree
    | Node of 'a  * 'a binTree * 'a binTree ;; 

type FSet<'a> = T of 'a binTree;;

let emptyset =  T Null;;

let singleton y =  T (Node(y,Null,Null));;

let (.<) small big = 
      match (Unchecked.compare small big) with
      | 1 ->  false 
      | _ ->   true;;


let rec inst (x , btree ) =
    match btree with
        | Null -> Node(x, Null, Null)  
        | Node(r, left, right) when x = r ->  btree 
        | Node(r, left, right) when x .< r ->  Node(r,  inst( x, left) , right )
        | Node(r, left, right)  ->  Node(r , left, inst (x, right) ) 

let insert x (T btree) =  T (inst(x,btree));;


let ofList list =
 let rec insertFromList ( list, tree) =
    match list with
        | [] -> tree
        | x :: xs ->
            let tree1 = inst ( x, tree)
            insertFromList( xs , tree1) 
 T (insertFromList(list,Null));;



let memberset x (T btree) = 
 let rec search  (x, btree) =
    match btree with
        |  Null -> false
        |  Node (r, left, right) ->
            ( x = r ) || 
            ( (x .< r) && search  (x,left) ) ||
            ( not (x .< r) && search  (x,right) )

 search(x,btree);;


// alternative direct definition with "deep" pattern matching 
let rec memberset2 x btree =
    match btree with
        | T Null -> false
        | T (Node (r, left, right)) ->
            ( x = r ) || 
            ( (x .< r) && (memberset2 x ( T left)) ) ||
            ( not (x .< r) && (memberset2 x (T right)) );;

let rec union (T s1) s2 = 
    match s1 with
     | Null -> s2
     | Node(x,ltr,rtr) -> let ts = insert x s2
                          union (T rtr) (union (T ltr) ts);;


let toList (T bt) = 
 let rec inorderToList btree =
    match btree with
        | Null -> []
        | Node ( r , left, right ) ->
            inorderToList left @ [r] @  inorderToList right 
 
 inorderToList bt;;

let rec count (T btree) =
    match btree with
        | Null -> 0
        | Node ( r, left, right) ->
           let nodesL = count (T left)
           let nodesR = count (T right)
           nodesL + nodesR + 1 ;;

(*
This is wrong:

let map f ( T btree) =
 let rec mapTree btree =
    match btree with
        | Null ->  Null
        | Node ( r, left, right ) ->
            Node ( f r, mapTree  left, mapTree  right)
 T (mapTree btree);;

Why? because it does not remove duplications: try

let tm = map (fun x -> 100 ) t2;;

and see
*)


let rec map f  ss = 
    match ss with
     | T Null -> T Null
     | T (Node(x,ltr,rtr)) -> let nltr = map f (T ltr) 
                              let nrtr = map f (T rtr) 
                              insert (f x) (union nltr nrtr);;



// tests

let intList = [20 ; 10 ; 60 ; 15 ; 40 ; 100 ; 30 ; 50 ; 70 ; 35 ; 42 ; 58 ; 75 ; 32 ; 37] ;;

let t1 = ofList intList;;


let m = memberset 35 t1;;

let n = count t1;;

let t2 = ofList [1..5];;

let tu = union t1 t2;;

let tm = map (fun x -> x + x ) t2;;

