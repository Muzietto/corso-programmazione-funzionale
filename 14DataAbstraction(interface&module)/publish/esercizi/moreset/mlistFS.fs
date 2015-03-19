// a first naive encodings: finiste sets as lists w/o repetitions

// same name of sig file
module FSet

// the type implementation: **must** be a tagged type or a record

type FSet<'a> = S of 'a list;;


let emptyset =  S [];;

let singleton y  = S [y];;

let memberset x (S ss) = List.exists (fun y ->   x =  y) ss;;

let insert x (S ss) = 
        S (if List.exists (fun y ->  x = y) ss then  ss else x :: ss) ;;

let rec union (S s1) s2 = 
    match s1 with
    [] -> s2
    | (x :: xs) -> let ts = insert x s2
                   union (S xs) ts

// let rec unionf (S s1) s2 = List.fold s2 s1;;

let toList ( S ss) = ss;;

let ofList ss = S ss;;

let isEmpty ss = ss = S [];;


let rec map f (S ss) = 
  match ss with
  | [] -> emptyset
  | x :: xs -> insert (f x) (map f (S xs));;

// Set.ofList [1..10] |> Set.map (fun x -> 0);;

// ofList [1..10] |> map (fun x -> 0);;

let count (S ss) = List.length ss;;
