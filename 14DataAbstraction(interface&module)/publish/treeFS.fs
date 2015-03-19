module  FSet
    
type 'a FSet = Null | Node of 'a * 'a FSet * 'a FSet


let emptyset =   Null;;
let singleton y =   (Node(y,Null,Null));;


let (.<) small big = 
      match (Unchecked.compare small big) with
      | -1 ->  true 
      | _ ->   false;;


(* why do we redefine comparison? In general this is *not* a good idea,
as it will allow to compare things that we should not
compare such as functions. However, here we would need 'a to be both 

'a : comparison and 
'a : equality 	for this implementation, and it gets complicated. 

So we suppress the static check using Uncheched.equals
*)

let rec insert  x  btree  =
    match btree with
        | Null -> Node(x, Null, Null)  
        | Node(r, left, right) when x = r ->  btree 
        | Node(r, left, right) when x .< r ->  Node(r,  (insert  x left) , right )
        | Node(r, left, right)  ->  Node(r , left, (insert x  right) ) 


let ofList list =
 let rec insertFromList ( list, tree) =
    match list with
        | [] -> tree
        | x :: xs ->
            let tree1 =  insert  x tree
            insertFromList( xs , tree1) 
 insertFromList(list,Null);;

let rec memberset  x  btree =
    match btree with
        |  Null -> false
        |  Node (r, left, right) ->
            ( x = r ) || 
            ( (x .< r) && memberset  x left ) ||
            ( not (x .< r) && memberset  x right );;  




let rec union ( s1) s2 = 
    match s1 with
     | Null -> s2
     | Node(x,ltr,rtr) -> let ts = insert x s2
                          let tsl = union ( ltr) ts
                          union ( rtr) tsl;;

// something useful to have

 let rec toList btree =
    match btree with
        | Null -> []
        | Node ( r , left, right ) ->
            toList left @ [r] @  toList right 
 


