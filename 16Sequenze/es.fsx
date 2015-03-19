///////////////////////////////////////
///  MAP

// map : ('a -> 'b) -> seq<'a> -> seq<'b>

(*

Data la sequenza

   sq = seq[ e0 ; e1 ; e2 ; ...   ]

la sequenza (map f sq) e'

    seq[ f(e0) ; f(e1) ; f(e2) ; ...   ]

*)

// Si assume che la sequenza sq sia infinita
let rec map f sq =
    // essendo sq infinita, sq contiene almeno un elemento, quindi le operazioni
    // (Seq.nth 0) e  (Seq.skip 1) su sq non falliscono
    seq{ let e0 = Seq.nth 0 sq 
         yield f e0
         let sq1 = Seq.skip 1 sq
         // notare che sq1 e' infinita,
         // quindi la chiamata ricorsiva fatta sotto rispetta l'assunzione  
         yield! map f sq1
    } ;;

// esempi

let nat =  Seq.initInfinite (fun x -> x) ;;

let squares = map (fun x -> x * x) nat ;;
// seq [0; 1; 4; 9; ...] (sequenza dei quadrati dei  numeri naturali)

// lista dei primi 15 quadrati
let listSq = squares |> Seq.take 15 |> Seq.toList ;;
// [0; 1; 4; 9; 16; 25; 36; 49; 64; 81; 100; 121; 144; 169; 196]

// versione che funziona anche su sequenze finite

// map : ('a -> 'b) -> seq<'a> -> seq<'b>
//  sq e' una qualunque sequenza (finita o infinita)
let rec map1 f sq =
    seq{
        // occorre distinguere il caso in cui sq e' vuota
        if Seq.isEmpty sq then
           yield! Seq.empty
        else
       // sq contiene almeno un elemento quindi le operazioni
       // (Seq.nth 0) e  (Seq.skip 1) su sq non falliscono
           let e0 = Seq.nth 0 sq
           yield f e0
           let sq1 = Seq.skip 1 sq
           yield! map1 f sq1
    } ;;


// sequenza  degli elementi   5^2, 6^2,  ... ,  10^2
map1 (fun x -> x * x) ( seq[ 5 .. 10 ] ) ;;


// OPPURE

//  sq e' una qualunque sequenza (finita o infinita)
let rec map2 f sq =
    seq{
        if not ( Seq.isEmpty sq ) then
          // sq contiene almeno un elemento quindi le operazioni
          // (Seq.nth 0) e  (Seq.skip 1) su sq non falliscono
          let e0 = Seq.nth 0 sq
          yield f e0
          let sq1 = Seq.skip 1 sq
          yield! map2 f sq1
    } ;;

// se la sequenza sq e' vuota,  (map f sq) definisce la sequenza vuota 

// sequenza  degli elementi   5^2, 6^2,  ... ,  10^2
map2 (fun x -> x * x) ( seq[ 5 .. 10 ] ) ;;


/////////////////////////////////////////////////////
///  FILTER

// filter : ('a -> bool) -> seq<'a> -> seq<'a>
// Si assume che la sequenza sq sia infinita
let rec filter pred sq =
    seq{
       let e0 = Seq.nth 0 sq // e0 e' definito in quanto sq e' infinita
       if pred e0 then yield e0
       let sq1 = Seq.skip 1 sq // sq1 e' definita in quanto sq e' infinita
       // essendo sq1  infinita, si puo' applicare ricorsivamente filter a sq1
       yield! filter pred sq1
       };;

// primi 20 multipli di 3
let l3 =  filter (fun x -> x%3 = 0 ) nat |> Seq.take 20 |> Seq.toList ;;



////////////////////////////////////////////
///  SEQUENZA  FIBONACCI

// fibFrom : int -> int -> seq<int>

(*

La sequenza di Fibonacci con valori iniziali a e b e':

   a ,  b , a+b , b + (a+b) , (a+b) + (b+(a+b)) , ...

 Notare che la sottosequenza che parte da b
   
        b , a+b , b + (a+b) , (a+b) + (b+(a+b)) , ...
  
corrisponde alla sequenza di Fibonacci con valori iniziali b e a+b.


*)
   

// sequenza di Fibonacci con valori iniziali a e b
let rec fibFrom a b  = seq{
                           yield a
                           yield! fibFrom b (a+b)
                      } ;;

// esempio

fibFrom 1 1  |> Seq.take 10 |> Seq.toList ;; // [1; 1; 2; 3; 5; 8; 13; 21; 34; 55]

// versione con uint64 

let rec fibFromUL ( a : uint64) b  =
    // e' sufficiente specificare il tipo di uno dei due parametri
    seq{yield a
        yield! fibFromUL b (a+b)
        } ;;

// sequenza di Fibonacci standard
let fibSeqUL = fibFromUL 1UL 1UL ;; // 1UL e' una costante di tipo  uint64 

// fib : int ->  int 
// calcola n-esimo numero della sequenza (n >= 0)

let fib n = Seq.nth n fibSeqUL ;;

fib 60 ;; // 60^o numero di Fibonacci
// val it : uint64 = 2504730781961UL



////////////////////////////////////////////
/// SEQUENZA SOMME

// sumSeq : seq<int> -> seq<int>

(*

Data la sequenza sq composta dagli interi

      n0 , n1 , n2 ; n3 , ..

la sequenza  (sumSeq sq) delle somme di sq contiene gli interi

     n0 , n0+n1 , n0+n1+n2 , n0+n1+n2+n3 , ...       

Notare che la sottosequenza

          n0+n1 , n0+n1+n2 , n0+n1+n2+n3 , ...   

corrisponde alla sequenza (sumSeq sq1), dove sq1 e' la sequenza degli elementi 

          n0+n1 , n2 , n3 , ...   



*)  

// Si assume che sq sia infinita
let rec sumSeq sq =
    seq{
        let n0 = Seq.nth 0 sq
        yield  n0 // primo elemento
        let n1 = Seq.nth 1 sq
        let sq1 = Seq.append (seq [ n0 + n1 ]) (Seq.skip 2 sq)
        //  sq1 = seq [ n0+n1 ; n2 ; n3 ;  ... ]
        // essendo sq1 infinita, si puo' applicare ricorsivamente sumSeq a sq1
        yield! sumSeq sq1  
    } ;;

// lista primi 15 elementi di (sumSeq nat) 

sumSeq nat  |>  Seq.take 15 |> Seq.toList ;;
// [0; 1; 3; 6; 10; 15; 21; 28; 36; 45; 55; 66; 78; 91; 105]


////////////////////////////////////////////
///   FILE SYSTEM

open System.IO ;;

//  allFiles : string -> seq<string>
let rec allFiles dir =
    seq { yield! Directory.GetFiles dir  // sequenza dei file in dir
          (*  per ogni directory d in dir calcola la sequenza (allFiles d)
              e concatena tutte le sequenza ottenute
          *)
          yield!  Seq.collect allFiles ( Directory.GetDirectories dir )
          } ;;

// esempio

let courseFPDir = "/home/fiorenti/Dropbox/FP" ;; // path di una dir.

let fileSeq = allFiles courseFPDir ;;

// calcolo numero dei file nella sequenza  fileSeq
let numFiles = fileSeq |> Seq.toList |> List.length ;; 

///////////////////////////////////////////////////////////
///  CRIVELLO DI ERATOSTENE 


//  sift : int -> seq<int> -> seq<int>
//  Elimina da sq tutti i multipli di a 

let sift a sq = Seq.filter (fun x ->  x % a <>  0 ) sq


// esempi

let sq1 = sift 2 nat ;;
// sq1 : seq<int> = [1; 3; 5; 7; ... ]  (sequenza infinita dei numeri dispari)

// lista dei primi 10 elementi di sq1
let l1 =  sq1 |>  Seq.take 10 |> Seq.toList ;;
// l1 : int list = [1; 3; 5; 7; 9; 11; 13; 15; 17; 19]

let sq2 = sift 3 nat;;
// sq2 : seq<int> = seq [1; 2; 4; 5; 7; 8; 10; ... ]

// lista dei primi 15 elementi di sq2
let l2 =  sq2 |>  Seq.take 15 |> Seq.toList ;;
// l2 : int list =  [1; 2; 4; 5; 7; 8; 10; 11; 13; 14; 16; 17; 19; 20; 22]


// Applica crivello di Eratostene a sq
// sieve : seq<int> -> seq<int>
let rec sieve sq =
    seq {
        let x0 = Seq.nth 0 sq
        yield x0
        let sq1 = Seq.skip 1 sq 
        yield! sieve (sift x0 sq1)
        } ;;
    
// nat2 e' la sequenza degli interi n >= 2
let nat2 = Seq.initInfinite ( fun x -> x + 2 ) ;;

// sequenza dei numeri primi
let primes = sieve nat2 ;;

// lista primi 10 numeri primi

let p10 = primes  |>  Seq.take 10 |> Seq.toList ;;
// p10 : int list = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29]


// VERSIONE CACHED (piu' efficiente)

// siftC : int -> seq<int> -> seq<int>
let siftC a sq = Seq.cache  ( sift a sq ) ;;

// sieveC : seq<int> -> seq<int>
let rec sieveC sq =
    seq {
        let x0 = Seq.nth 0 sq
        yield x0
        let sq1 = Seq.skip 1 sq 
        yield! sieveC (siftC x0 sq1)
        } ;;


// sequenza cached dei numeri primi 
let primesC = Seq.cache (sieveC nat2) ;;
// primesC : seq<int>





