//#r @"/home/alberto/fp/2014/Lz14Mod/Queue/listFS.dll";;
#r "listFS.dll";;

open FSet;;


let ss = insert 3 emptyset |> insert 5 |> insert 2 |> insert 77;;

let s_1 = singleton 1;;

let ss1 = (insert 1 s_1);;

let u = union s_1 ss;;



// after, try with other representation
