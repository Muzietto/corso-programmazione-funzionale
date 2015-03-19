//#r "/home/alberto/fp/2013/Lz10Modules/Queue/QueueSimple.dll";;
#r "QueueP.dll";;

open Queue;;


let q = put 3 empty |> put 5 |> put 2 |> put 77;;

let (x1,nq) = get q;;

let (x2,nq2) = get nq;;

// queues are still there

let (a,_) = get q;;


