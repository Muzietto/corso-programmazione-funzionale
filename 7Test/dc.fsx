module DC

type category = Daycare | Nursery | Recreation;;

type name = string;;

type childDes = C of name * category ;;

let ds = [C("a", Daycare); C("b", Daycare); C("a", Nursery);C("c", Nursery)];;

let rec number (c ,ds) = 
   match ds with
   | [] -> 0
   | C (n,cat) :: xs  when c = cat -> 1 + (number (c,xs ))
   | _ :: xs  ->  number (c,xs) ;;

let tn = number(Daycare,ds);;

let month_charge  = function
 | Daycare -> 225.
 | Nursery  -> 116.
 | Recreation -> 100. ;;

// remove first occ of x and return charge
let rec remove (x, xs:   childDes list ) = 
    match xs with 
    | [] -> ([],0.)
    |  C (n,cat)::ys when x=n ->  (ys,month_charge cat) 
    |  c :: cs -> let (rest,k) = remove(x,cs) in (c:: rest,k);;
 


// assume family occurs
let pay(family,ds) = 
   let rec payment (family, ds : childDes list,nodiscount) = 
    match ds with
    | [] -> nodiscount
    | C (n,cat) :: xs  when n = family -> ((month_charge cat) / 2.) + (payment (family ,xs,nodiscount ))
    | _ :: xs  ->  payment (family,xs,nodiscount) 
   let (rest, nodiscount) = remove(family, ds) in payment(family,rest,nodiscount);;

//let ds = [C("a", Daycare); C("b", Daycare); C("a", Nursery);C("c", Nursery)];;

let ta = pay("a",ds);;
let tb = pay("b",ds);;
let tc = pay("c",ds);;