
(* 
SEARCH

A directed graph is as set of nodes N with a binary relation R on N.

We see directed  graphs as finite maps from nodes to the list of
their immediate successors.

- we treat here only **acyclic** graphs for simplicity, but the
  representation would be the same in the cyclic case, while
  algorithms need to carry around a list of *visited* nodes to avoid
  getting lost in a cycle during search.

- note that trees are a degenerate case where every node has at most one parent

- there are billions of other representations from using just relations
  to semi-imperative ones such as in OCaml, see

http://caml.inria.fr/pub/docs/oreilly-book/html/book-ora125.html
*)

type graph<'a when 'a : comparison> = Map<'a,'a list> ;;


(* let's start with a tree:

                         1
                       /   \
                      3     8
                     /     /\ \
                    6        
                          5   4  12
*)

let atree = Map.ofList [ (1,[3;8])  ; (3,[6])  ; 
                      (8,[5; 4; 12]) ; (6,[]) ; (4,[]) ; (5,[]) ; (12,[])];;

// a couple of preds

let truef x = true;;

let is_even x = x % 2 = 0;;


// depth - first
// depf : gr:graph<'a,'a list> -> pred:('a -> bool) -> root:'a -> 'a list

let depf gr pred root = 
 let rec df = function
     | [] -> []
     | x :: xs -> let succ_x = Map.find x gr
                  if pred x then x :: (df (succ_x @ xs)) 
                     else  (df (succ_x @ xs))
 df [root];;


let dt = depf atree truef 1;;
let de = depf atree is_even 1;;

// breadth-first

let breads gr pred root = 
 let rec bfs = function
     | [] -> []
     | x :: xs -> let succ_x = Map.find x gr
                  if pred x then x :: (bfs (xs @ succ_x)) 
                     else  (bfs (xs @ succ_x)) 

 bfs [root];;


let bt = breads atree truef 1;;
let be = breads atree is_even 1;;


(*
                        1
                       /  \
                      3    8
                     /  \   \
                    6    \  /  
                           4
*)


let ag = Map.ofList [ (1,[3;8])  ; (3,[ 6 ; 4])  
                     ; (8,[4]) ; (6,[]) ; (4,[]) ];;



let testall = depf ag  truef 1;;
// val testall : int list = [1; 3; 6; 4; 8; 4]

let test = depf ag is_even 1;;

//  val test : int list = [6; 4; 8; 4]



let tab = breads ag truef 1;;
// val tab : int list = [1; 3; 8; 6; 4; 4]
let tb = breads ag is_even 1;;



// %%%%%%%%%%%%%%%%%%%%%%%%%%%%


// nested version w/o append, no pred


let itdf tree root = 
 let rec df  list  visited = 
  match list with
     | [] -> visited
     | x :: xs ->  let nvs = (df  (Map.find x atree) (x :: visited))          
//                   (printf "nvs=%A,%d, %A\n" nvs x xs); 
                   (df  xs nvs)                    
 df  [root] [] |> List.rev;;