(**  ESPRESSIONI FUNZIONALI **)

(*


Definizione di funzione
^^^^^^^^^^^^^^^^^^^^^^^

Una espressione funzionale e' un termine della forma
  
    fun x -> expr    // fExpr

Una espressione funzionale definisce una funzione.

A differenza dei linguaggi imperativi, e' possibile definire funzioni
mediante espressioni funzionali senza assegnare loro un nome;
in tal caso si parla di  'funzioni anonime'.

Il tipo di una espressione funzionale fExpr  e'

  fExpr :  T1 -> T2

dove  T1 e' il tipo dell'argomento  e T2 il tipo del valore calcolato. 
Il tipo di una funzione viene determinato dalle regole  di type inference.


Applicazione di una funzione
~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Data una espressione funzionale fExpr e un termine t aventi tipo

 fExpr : T1 -> T2      t : T1

l'espressione

   fExpr t

denota l'applicazione di fExpr al termine t e il valore calcolato ha tipo T2.

Notare che:

-  Le funzioni hanno un solo argomento.

-  A differenza dei linguaggi imperativi, non occorre scrivere l'argomento tra parentesi tonde.

-  Se si e' in presenza di tipi polimorfi (tipi che contengono variabili),
   per T1 e T2 si intendono  le istanze dei tipi che rendono l'applicazione sensata.

    Ad esempio se 

       f :  'a * 'b ->  'a list         t  :  int * string
   
    per applicare f a t occorre istanziare i tipi generici ponendo:  
     
      'a = int 
      'b = string.

    Quindi

       T1 = int * string    T2 = int list

L'applicazione di   

     fExpr =  fun x -> expr

a un termine t e' calcolata come segue:

 fExpr t =  espressione ottenuto sostituendo in expr ogni occorrenza di x con t
            e 'valutando' l'espressione ottenuta.

Esempio

 ( fun x -> x + 1 ) 5 = 5 + 1 // espressione ottenuta sostituendo in x + 1 la variabile x con 5
                      = 6     // valutazione dell'espressione  5 + 1


Sintassi alternativa
^^^^^^^^^^^^^^^^^^^^

Una espressione funzionale puo' essere legata a un identificatore (let-binding)

  let f = fun x -> expr  // (1)

Dopo questa definizione,  'f'  e' un nome per  'fun x -> expr'

Sintassi  equivalente:

 let f x = expr  // (2)

La definizione (2), in cui la x e' portata a sinisistra di =,
e'  zucchero sintattico per la (1) ed e' la notazione generalmente usata.

Questo procedimento  e' chiamato  'lambda lifting'
ed e' parte della fase di 'closure conversion' nella compilazione 
 
*)   

// Esempi

fun x -> x + 1 ;;  // funzione anonima 
//val it : int -> int 

// applicazione

(fun x -> x + 1) 5;;
// val it : int = 6

// let binding

let succ =  fun x -> x + 1 ;;
// val succ : int -> int
// nell'ambiente corrente, l'identificatore succ e' legato (binding) alla funzione successore

succ 4;;
// val it : int = 5

// Altra notazione per definire succ

let succ1 x =   x + 1 ;;


// funzione identita'

let id = fun x -> x ;;
// val id : 'a -> 'a

// oppure:

let id1 x = x ;;
// val id2 : 'a -> 'a

id "id" ;; 
// val it : string = "id"

// esempio di funzione costante

let zero = fun x -> 0 ;;
//val zero : 'a -> int

(*
   notare che l'espressione a destra di -> non contiene la x.
   quindi la funzione restituisce sempre 0 qualunque sia l'argomento

*)

let zero1 x = 0 ;;

zero ["uno" ; "due" ] ;;  // 0
zero zero ;;  // 0
zero ( fun x -> 2 * x ) ;;  // 0


(*
** ESERCIZIO **

Scrivere una espressione funzionale, usando 'fun', 
corrispondente alla funzione  di List.isEmpty 

** END EX **)

(**

 In una espressione funzionale

    fun x -> expr

l'espressione expr puo' a sua volta essere una espressione funzionale. 


Esempio
^^^^^^^

    fun x -> ( fun y ->  x + y ) ;;

e' una espressione funzionale corretta.

Chiamiamo f la funzione definita sopra:

   let f = fun x -> ( fun y ->  x + y ) ;;

Che tipo ha f ?

Anzitutto, l'operatore '+' viene interpretato come la somma intera,
si assume quindi che x e y abbiano tipo int.

Segue che:

    fun y -> x + y                     ha tipo    int -> int

    fun x -> ( fun y ->  x + y )       ha tipo    int -> ( int -> int ) 

Dato che f ha tipo int -> (int -> int) , possiamo applicare f a un qualunque
termine di tipo int e il risultato ha tipo int -> int (quindi e' una funzione).

   
Esempi di applicazione 
^^^^^^^^^^^^^^^^^^^^^^

Applichiamo f a 5. Il risultato deve essere una funzione di tipo int -> int.

Infatti:

  f 5  =   fun y -> 5 + y
         // termine ottenuto sostituendo in  'fun y ->  x + y' la  variabile x con 5


Poniamo

    let g =  f 5 ;;  // g = fun y -> 5 + y : int -> int

    Esempio di applicazione di g:

      g 4  =  ( fun y -> 5 + y ) 4
           =  5 + 4  // sostituzione di y con 4 in 5 + y 
           =  9

E' possibile eseguire le due applicazioni scrivendo un'unica espressione:

     (f 5) 4 ;;    //  val it : int = 9  
   
Convenzioni sulla associativita'
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

 *  L'operatore ->  e' associativo a destra

     T1  -> T2 -> T3                  equivale a     T1 -> ( T2 -> T3 )
     fun x ->  (fun y  ->  expr)      equivale a     fun x -> fun y  ->  expr 

 *  L'applicazione di funzione e' associativa a sinistra.

  
      fExpr t1 t2     equivale a      ( fExpr t1 ) t2
    

  In base a queste convenzioni, riferendoci agli esempi scritti:

     int -> int -> int            equivale a        int -> ( int -> int )

     fun x ->  fun y ->  x + y    equivale a        fun x -> ( fun y ->  x + y )

     f 5 4                        equivale a        (f 5) 4

Notare che 'f 5 4' assomiglia all'applicazione di una funzione con due argomenti.
In realta' corrisponde a due applicazioni in cascata:
prima si applica f a 5,  poi si applica la funzione ottenuta a 4 
 
  
Sintassi alternativa
^^^^^^^^^^^^^^^^^^^^
 
Quando si hanno piu' 'fun' annidati, si puo' usare una notazione piu' compatta:

     fun x1 ->  ( fun x2 -> .... -> ( fun xn expr) ... )

si puo' riscrivere come

     fun x1 x2 .... xn -> expr   // un solo fun con elenco variabili

Il 'lifting' si estende a espressioni funzionali con piu' variabili

   let fn =  fun x1 x2 .... xn -> expr

si puo' riscrivere come
  
    let fn x1 x2 ... xn = expr

Nell'esempio precedente

  fun x -> ( fun y ->  x + y )

puo' essere riscritto in

  fun x y -> x + y

La definizione  

 let f =   fun x -> ( fun y ->  x + y )   
    // =   fun x y -> x + y

puo' essere riscritta nella forma piu' compatta
    
   let f x y = x + y 

La definizione di f scritta sopra assomiglia alla definizione di
una funzione con due parametri x e y.

Quando viene applicata f istanziando solo il primo parametro,
si parla di 'applicazione parziale'

let succ = f 1
// applicazione parziale di f (solo la x viene istanziata) 
// succ = fun y -> 1 + y : int -> int

Questo meccanismo per cui si puo' simulare l'applicazione di n argomenti
mediante n chiamate a funzioni aventi un argomento si chiama "currying"
(Schoefinkel, Curry, 1930) 	       	  

Notare che nei linguaggi imperativi non e' possibile applicare parzialmente
una funzione (tutti i parametri della funzione vanno istanziati).


Analogia  con array bidimensionali
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Un meccanismo analogo alla applicazione parziale si ha con la rappresentazione
di matrici nei linguaggi imperativi (ad esempio in C).

Una matrice A di int e' implementata mediante un array di tipo  'array di array di int'.

Per ogni riga i della matrice,  A[i] e' un array di int,
contenente gli elementi della riga i della matrice
( A[i] e' analogo alla  applicazione parziale  f i )

L'elemento alla riga i e colonna j della matrice corrisponde all'elemento j dell'array A[i],
ossia all'elemento

         ( A[i] ) [j]      // analogo a  (f i) j    

che si puo' scrivere anche come

         A[i,j]      // analogo a  f i j
    
  
*)

// Esempi

let f =  fun x -> ( fun y ->  x + y ) ;;
// val f : int -> int -> int

let g = f 5 ;;  // applicazione parziale
// val g : (int -> int) = fun y -> 5 + y

g 4 ;; // 9

(f 5) 4 ;; // 9

f 5 4 ;;  // 9


// notazioni equivalenti per definire f

let f1 =  (fun x  y ->  x + y) ;;
let f2 x y = x + y ;;

f1 10 7 ;; // 17

let g1 = f1 4 ;; // applicazione parziale
// g1 : int -> int = fun y -> 4 + y

g1 5 ;; // 9



// Altri esempi

(*
Per nominare la funzione descritta da un operatore,
l'operatore va scritto fra parentesi tonde
*)

(=) ;; // funzione associata all'operatore =

(*
  (=)   e' la funzione  fun x ->  ( fun y -> ( x = y) )
  (=)   ha tipo         'a -> 'a -> bool   when 'a : equality 

*)

(=) 0;;  // applicazione parziale
// val it : (int -> bool) 

(*
  (=) 0     e' la funzione  fun y ->   ( 0 = y) : 'a -> bool 

*)   

let isZero = (=) 0;; 

isZero 0;;  // true
isZero -1;;  // false


// Definizione di isPositive : int -> bool mediante applicazione parziale 

let isPositive = (<) 0;;

(*

 (<)     e' la funzione  fun x y -> (x < y) : ('a -> 'a -> bool) 
                    
 (<) 0   e' la funzione   fun y -> (0 < y) : int -> bool 

*)   

isPositive 10;;   // true
isPositive -1;;  // false

// combinators

// if_then_else defined

(*

Date le espressioni cond, e1, e2 tali che

  cond : bool       e1, e2  : 'a     
  
la funzione

  if_then_else cond e1 e2 

calcola il valore dell'espressione

  if cond then e1 else e2

(da non confondere con l'omonima struttura di controllo dei linguaggi imperativi!).

Il tipo della funzione if_then_else e':

 if_then_else : bool -> 'a -> 'a -> 'a


*)   

let if_then_else cond e1 e2 =
    match cond with
        | true  ->  e1
        | _     ->  e2  ;; // expr_bool is false


let min a b   = if_then_else ( a < b ) a b ;;
// val min : 'a -> 'a -> 'a when 'a : comparison

min 3 4  ;; // 3
min 3 1  ;; // 1


let modulo n = if_then_else (n > 0) n -n ;;
// val modulo : int -> int

modulo 5 ;;  // 5
modulo -2 ;; // 2

(*

** ESERCIZIO **

Si considerino le seguenti funzioni definite per
applicazione parziale della funzione if_then_else

let p1 =  if_then_else true     :  int -> int -> int ;:
let p2 =  if_then_else false    :  int -> int -> int  ;;
let p3 =  if_then_else true  0  :  int ->int  ;;
let p4 =  if_then_else false 0  :  int ->int  ;;


Cosa calcolano ?


** END EX **)   



(***

Ancora sulla notazione delle espressioni funzionali
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Finora abbiamo visto espressioni funzionali della forma

    fun x -> expr

E' anche possibile scrivere a sinistra di -> un termine strutturato.

Esempio
^^^^^^^

   fun (x,y) ->  x + y

e' una espressione funzionale di tipo

   int * int -> int

che definisce la funzione che calcola la somma di una coppia di interi.

La definizione

 let add = fun (x,y) ->  x + y  

si puo' riscrivere come (lifting):

 let add (x,y) = x + y 

Notare la differenza fra

   let   add (x,y) =  x + y ;;   // add : int * int -> int
   let     f x y   =  x + y ;;   //   f : int -> int -> int

Per calcolare la somma m+n:

 add   : prende  come unico argomento la coppia (m,n)
          
   f   : il calcolo di m + n si ottiene con la chiamata

         ( f m) n   // applico f a m e poi applico la funzione ottenuta a n


*)

// esempio

type valutazione = {stud : string;  matr : int ; voto : int};;
// record che descrive la valutazione di uno studente

let getVoto = fun {stud = _ ; matr =_; voto = v} -> v ;; 
// val getVoto : valutazione -> int

// definizione equivalente:

let getVoto1 {stud = _ ; matr = _; voto = v} = v ;;
// val getVoto1 : valutazione -> int

let v = getVoto { stud = "Rossi" ; matr = 12345 ; voto = 28 } ;;
// val v : int = 28



(****    PIPE    ****

L'operatore |> (pipe) permette di scrivere l'argomento di una funzione prima della funzione stessa.

Quindi,  il valore di t |> f  e'  f t

Il tipo di (|>) e':

  (|>) :  'a -> ('a -> 'b) -> 'b


*)

// Esempi

let x1 = 4 |>  (fun x -> 2 * x  + 1) 
// val x1 : int = 9


(****   COMPOSIZIONE DI FUNZIONI  *****


L'operatore >> permette di comporre due funzioni.
Date due funzioni 

  f : 'a -> 'b      g : 'b -> 'c

la funzione  f >> g  e' definita su t  applicando prima f e poi g

  t   |--f-->   f t   |-- g-->   g ( f t ) // notare le parentesi!

Quindi:

    f >> g  =  fun x ->  g ( f x )

Il tipo della funzione (>>) e':

   (>>)  : ('a -> 'b) -> ('b -> 'c) -> 'a -> 'c

***)

// Esempi

let add10 = (+) 10;;

(*

  (+)         e' la funzione   fun x y -> x + y  :  int -> int -> int
  (+) 10      e' la funzione   fun y -> 10 + y   :  int -> int
       
*)

let  mult2 = ( * ) 2;;

(*

  (*)        e' la funzione   fun x y -> x * y  :  int -> int -> int
  (*) 2      e' la funzione   fun y -> 2 * y    :  int -> int
       
*)

let h1 = add10 >> mult2 ;;

(*

 h1 : int -> int

 h1 n = mult2 ( add10 n)
      = mult2 ( 10 + n )
      = 2 * (10 + n)

Quindi:      
          
 h1 = fun x ->  2 * ( 10 + x)

*)  


h1 100 ;; //  220

let h2 = mult2 >> add10 ;;

// che funzione rappresenta h2 ? e' uguale a h1 ?

h2 100 ;; //  210

// NOTARE

//  t |> f |> g  =  g ( f  t )  = (f >> g) t  

