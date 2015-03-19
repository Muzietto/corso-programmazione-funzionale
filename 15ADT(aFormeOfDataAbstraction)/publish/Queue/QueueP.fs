// Implementation file for a simple Queue

module Queue
exception EmptyQueue
type Queue<'a> = {front: 'a list; rear: 'a list}
let empty = {front = []; rear = []}

let put y {front = fs; rear = rs} = {front = fs; rear = y::rs}

let rec get = function
              | {front = x::fs; rear = rs} ->
                    (x,{front = fs; rear = rs})
              | {front = []; rear = []} -> raise EmptyQueue
              | {front = []; rear = rs} ->
                    get {front = List.rev rs; rear = []}

// why didn't I write empty -> raise EmptyQueue ???

let norm = function
 {front = []; rear = rs} ->
                    {front = List.rev rs; rear = []}
 | q -> q;;