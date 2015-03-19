#light

(*
 exercise: write two functions such that 
 1. remove even numbers from int list
    rmEven : (int list -> int list)
    rmEven [2;5;5;87;6;100;2] = [5; 5; 87]

 2. remove all elements in odd position from a list of floats
    considering the first elemnt an even position.
    rmOdd : rmOdd : float list -> float list
    rmOdd   [20.4; 21.4; 22.4; 23.4; 24.4; 25.4; 26.4; 27.4] =
            [20.4;       22.4;       24.4;       26.4] 
*)


let isEven n = (n%2 = 0)

let rec rmEven xs =
    match xs with
    |[] -> []
    |x:: xs when (isEven x) -> rmEven xs
    |x:: xs  -> x:: (rmEven xs)

let es = rmEven [2;5;5;87;6;100;2] 

let rmOdd xs =
    let rec rm_aux (i, xs) = 
        match (i,xs) with
        |(_,[]) -> []
        |(i,x:: xs) when (isEven i) -> x:: (rm_aux ((i + 1), xs))
        |(i,x:: xs)  -> (rm_aux ((i + 1), xs))
    rm_aux (0, xs)

let rec rmOdd2 xs =
    match xs with
        |[] -> []
        |x:: y:: xs  -> x:: rmOdd2  (xs)
        |x:: xs -> x:: rmOdd2 (xs)


let os = [20.4..28.0] |> rmOdd2



// generating lists -- the "hard" way
// lists of numbers from n to 0 


let main(n,step) = 
   let rec down n = 
       match n with
        | 0 -> [0]
        | m when m < 0 -> []
        | _ -> n :: down (n-step) 
   if (n < step) then [n] else down n;;


let  downto0 n = main(n,1)
let ns = (downto0 6) = [0..-1..6]
