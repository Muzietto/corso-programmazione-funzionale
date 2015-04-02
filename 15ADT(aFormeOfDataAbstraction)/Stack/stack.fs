module  Stack

type Stack<'a> = S of 'a list

exception EmptyStack;;
let empty = S [];;
let push x (S ss) = S (x :: ss);;
let pop ss = 
 match ss with
  S [] -> raise EmptyStack
 | S (x :: xs) -> (x, S xs);;

let isEmpty ss = ss = empty;;

// let st = push 4 empty |> push 3 |> push 5;;

//  [5; 3; 4]
let push_list xs  (S ss) = S (xs @ ss);;

let toList (S ss) = ss;;
let ofList ss = S ss;;
