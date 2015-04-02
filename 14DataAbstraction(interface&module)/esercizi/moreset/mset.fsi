// interface for finite  set of any type 'a, whose equality is reasonable

module FSet

// the type specification:

type FSet<'a when 'a : equality> 

// here the spec of the function (the interface)

val emptyset : FSet<'a> 

val singleton : 'a -> FSet<'a> 

val memberset  : 'a ->  FSet<'a> -> bool 

val insert : 'a -> FSet<'a> -> FSet<'a> 

val union : FSet<'a> -> FSet<'a> -> FSet<'a> 

val ofList :  'a list -> FSet<'a> 

val toList :   FSet<'a> -> 'a list

val map : ('a -> 'b) -> FSet<'a> -> FSet<'b> 

val count : FSet<'a> -> int

