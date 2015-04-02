// adding more features to basic Q 



module Queue
type Queue<'a when 'a : equality>
val empty : Queue<'a>
val put   : 'a -> Queue<'a> -> Queue<'a>
val get   : Queue<'a> -> 'a * Queue<'a>
val isEmpty : Queue<'a> -> bool
val size  :  Queue<'a> -> int
val put_list   : List<'a> -> Queue<'a> -> Queue<'a>
val ofList : 'a list -> Queue<'a>
val toList :  Queue<'a> -> 'a list


exception EmptyQueue

