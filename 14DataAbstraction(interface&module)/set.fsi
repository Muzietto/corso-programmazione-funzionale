// interface for finite  set of any type 'a, whose equality is reasonable

module FSet

// the type specification:

type FSet<'a when 'a : equality> 

(*
-  no implementation: type is kept abstract

- why the constraint 'a when 'a : equality?

  -> the type specified by the signature cannot be more general than
     its representation in the implementation module. Since we will
     use lists and characteristic functions as implementation, which use '=', we
     need to put this in the spec.
*)

// here the spec of the function (the interface)

val emptyset : FSet<'a> 

val singleton : 'a -> FSet<'a> 

val memberset  : 'a ->  FSet<'a> -> bool 

val insert : 'a -> FSet<'a> -> FSet<'a> 

val union : FSet<'a> -> FSet<'a> -> FSet<'a> 

val ofList :  'a list -> FSet<'a> 

val toList :  FSet<'a>  -> 'a list 

