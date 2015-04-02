module DFS

// put the right path here
#r "stack.dll";;

open Stack;;

let atree = Map.ofList [ (1,[2;8])  ; (2,[6])  ; 
                      (8,[5; 4; 12]) ; (6,[]) ; (4,[]) ; (5,[]) ; (12,[])];;


let depf_st (tree : Map<'a,'a list>) pred root = 
 let rec df ss = 
     if isEmpty ss then empty
      else 
        let (x,xs) =   pop ss
        let succ_x = Map.find x tree
        let newss = push_list succ_x xs
        if pred x then push x (df newss) else  (df newss)
 df (push root empty) |> toList;;

let t1 = depf_st atree (fun x -> true) 1;;

let t2 = depf_st atree (fun x -> x % 2 = 0) 1;;
