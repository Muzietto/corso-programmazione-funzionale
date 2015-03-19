
(****    SEQUENZE      ****)

(*

Una  sequenza e' una collezione  eventualmente infinita di elementi dello stesso tipo.

La notazione
            
    seq [ e0 ; e1 ; e2;  ... ]

rappresenta una sequenza i cui elementi sono e0, e1, e2, ...

Il tipo di una sequenza e' seq<'a>, dove 'a e' il tipo degli elementi.

Gli elementi di una sequenza vengono calcolati solamente quando richiesto
(on demand), e questo e' essenziale per poter rappresentare sequenze infinite.

Questa modalita' di valutazione, in cui la computazione effettiva degli elementi e' ritardata,
e' detta lazy evaluation.

Per i dettagli vedere il Cap. 11 del libro, in particolare le sezioni 11.1, 11.2, 11.6. 


*)  


(**  SEQUENCE EXPRESSIONS  **)

(*

Una sequence expression (caso particolare di computation expression)
definisce un processo che permette di generare uno o piu' elementi della sequenza.

L'espressione

  seq{ 
     seq_expr0   // sequence expression
     seq_expr1
     ...
     seq_exprn
     }

definisce la sequenza ottenuta  eseguendo in successione
le sequence expression seq_expr0, seq_expr1, ...  ,seq_exprn.

Si ribadisce che gli elementi vengono effettivamente generati quando richiesto.

Le sequence expression piu' semplici sono:

o   yield  e    

    Aggiunge alla sequenza l'elemento e.     
      
o   yield! sq 

    Aggiunge alla sequenza tutti gli elementi della sequenza sq  (concatenazione).

Altri esempi di sequence expression (vedere la Tabella 11.2 del libro):

o   let x = exp                  
    seqexpr

    Valuta seqexpr ponendo x = exp (local declaration).

o   if expr then seqexpr          
 
    Se expr e' vera, allora viene valutata  seqexpr (filter).

o   if expr then seqexpr_1  else  seqexpr_2 

    Se expr e' vera, allora viene valutata seqexpr_1,
    altrimenti viene seqexpr_2 (conditional)


Non confondere le  sequence expression con le espressioni F#.
Ad esempio, nelle sequence expression e' possibile usare il costrutto
'if-then' (senza else), che non ha senso nelle espressioni F#.


*)   

// Esempi


let seq1 = seq { yield 0  // yield genera un elemento
                 yield 1
                 yield 2
                 yield 3
             } ;;

// seq1 : seq<int> definisce la sequenza seq [ 0; 1; 2; 3 ]

let seq2 = seq{
        yield 100
        yield! seq1  // yield! concatena la sequenza seq1
        yield 200
        yield! seq1
    }

(* seq2 = seq[ 100 ;   0 ;  1  ;  2  ; 3  ; 200 ;   0  ; 1  ; 2  ;  3  ] 
                e0    e1   e2    e3   e4     e5    e6   e7   e8    e9
                      ^^^^^^^^^^^^^^^^^^           ^^^^^^^^^^^^^^^^^^
                           seq1                         seq1      

*)

(*

  La funzione

     Seq.nth : int -> seq<'a> -> 'a

  estrae da una sequenza l'elemento con indice specificato (gli indici partono da 0)
 
*)

Seq.nth 0 seq1 ;;  // 0   (primo elemento di seq1)
Seq.nth 2 seq1 ;;  // 2   (terzo elemento di seq1)
Seq.nth 4 seq2 ;;  // 3   (e4)
Seq.nth 8 seq2 ;;  // 2   (e8)


(*

  La funzione

     Seq.take : int -> seq<'a> -> seq<'a>

  estrae la sottosequenza formata dai primi n elementi di una sequenza
  (n>=0 e' il primo argomento)
  
*)   

let seq3 = Seq.take 2 seq1 ;;
// seq3 = seq [ 0 ; 1]


(*
  La funzione 
   
    Seq.skip : int -> seq<'a> -> seq<'a>

  estrae la sottosequenza ottenuta saltando i primi n elementi di una sequenza
  (n>=0 e' il primo argomento)
  
*)   

let seq4 = Seq.skip 2 seq2 ;; 
// seq4 = seq [1; 2; 3; 200; 0; 1; 2; 3]

 Seq.nth 0 seq4 ;; // 1
 Seq.nth 1 seq4 ;; // 2


//  definiamo la funzione   tail : seq<'a> -> seq<'a>   che estrae la 'coda' di una sequenza 
let tail sq = Seq.skip 1 sq ;;  // toglie il primo elemento di sq


(*

La funzione

  Seq.toList : seq<'a> -> 'a list ;;
 
trasforma una sequenza finita in una lista. 

*)

let l1 = Seq.toList seq1 ;;
// l1 : int list = [0; 1; 2; 3]

let l2 = Seq.toList seq2 ;;
// l2 : int list = [100; 0; 1; 2; 3; 200; 0; 1; 2; 3]



/////////////////////////////////////////////////////////////////////


(***  LAZY EVALUATION  VERSUS STRICT (O EAGER)  EVALUATION ***)

(*

La valutazione lazy (pigra)  ritarda la computazione di una espressione
finche' il risultato non e' richiesto effettivamente.

Le sequence expression sono valutate in modo lazy.
Questo permette  la definizione di  sequenze infinite; durante l'esecuzione,
e' costruita solamenta la porzione finita effettivamente usata.

*)   

// esempio di uso della lazy evaluation nella definizione di una sequenza

let seql = seq { yield 0
                 yield 1
                 yield 2/0  // la valutazione di 2/0 solleva una eccezione 
                 yield 3
            } ;;

// La definizione di seql non produce errori (la definizione non genera alcun elemento).

Seq.nth 0  seql ;; // 0
// nella valutazione, viene generato solamente il primo elemento di seql

Seq.nth 1  seql ;; // 1
// nella valutazione, vengono  generati solamente i primi due elementi di seql

(*

Verificare cosa succede calcolando

Seq.nth 2  seql ;; 
Seq.nth 3  seql ;;

In entrambi i casi, per la valutazione occorre generare l'elemento 2/0,
e questo solleva una eccezione.


*)   


(*

NOTA
====

Le espressioni F# viste finora, sono valutate in modalita' strict (eager),
che e' la modalita' usata nei linguaggi imperativi.

In una applicazione

   f t

il valore di t e' calcolato prima di applicare f,
anche nei casi in cui il valore di t non viene usato in f.

Ad esempio, consideriamo la funzione

  first : 'a -> 'b -> 'a

che restituisce il primo argomento:

  let first x y = x ;;  // il valore di y non e' usato in first
 
L'applicazione

  first 1 2/0 ;;

solleva una eccezione, in quanto l'argomento 2/0 viene valutato prima di applicare first.


Le liste sono costruite in modo strict (e non lazy).

Infatti, la lista

  [1;2] 

corrisponde al termine

 1 :: ( 2 :: [] );

L'operatore :: (cons) e' valutato in modo  strict, quindi
la valutazione della lista richiede che tutti gli elementi in essa contenuti
siano valutati.

Di conseguenza, la definizione

 let listErr = [ 0; 1; 2/0; 3]

solleva una eccezione, dovuta alla presenza del termine 2/0.


Il costruttore seq
^^^^^^^^^^^^^^^^^^

E' possibile definire una sequenza applicando il costruttore seq a una lista.

Ad esempio

 seq [ 0 .. 10 ] ;;

definisce la sequenza di interi contenente gli elementi 0, 1, ... , 10.

Notare che la definizione
 
 let sq = seq [ 0; 1; 2/0; 3] ;;

solleva una eccezione in quanto, come visto sopra,
la lista e' valutata in modo strict (e non lazy).


*)   


/////////////////////////////////////////////////////

(****  SEQUENZE INFINITE  ****)

(*

Per definire una sequenza infinita si puo' usare la funzione

   Seq.initInfinite : (int -> 'a) -> seq<'a>

Data una funzione  f: int -> a, 

   Seq.initInfinite f  

costruisce la sequenza infinita seq[ f(0) ; f(1) : f(2) ; ... ]

*)

let nat =   Seq.initInfinite (fun x -> x) ;;
// nat : seq<int> = seq [0; 1; 2; 3; 4; ... ] (sequenza infinita dei numeri naturali)

Seq.nth  5  nat ;; // 5


let pari10 =  Seq.initInfinite (fun x -> 10 + 2 *x) ;; 
// pari10 : seq<int> = seq[10; 12; 14; ... ]  (sequenza infinita dei numeri pari n >= 10)

let l10 = Seq.toList ( Seq.take 8 pari10 ) ;;
// l10  : int list = [10; 12; 14; 16; 18; 20; 22; 24] (lista dei primi 8 elementi di pari10)


(*

ESERCIZIO
-----------

Definire una sequenza infinita sqTrue : seq<bool>  composta solamente da true
usando  Seq.initInfinite.
Notare che, per ogni n>=0,  (Seq.nth n sqTrue) deve valere true.

*)


(***  DEFINIZIONE DI SEQUENZE INFINITE MEDIANTE RICORSIONE **)


(*

E' possibile definire una sequenza infinita mediante ricorsione,
sfruttando il fatto che le sequence expression sono valutate
in modo lazy.

*)   


// intFrom : int -> seq<int>
// Dato un intero n, intFrom n definisce la sequenza degli interi k >= n

let rec intFrom n = seq { yield n  // primo elemeno della sequenza 
                          yield! intFrom (n + 1)  // elementi successivi
                                // seq[ n+1; n+2; ... ] 
                         } ;;


let int10 = intFrom -10 ;;
// int10 : seq<int> = seq [-10; -9; -8; ... ] 

//  da int10  estraiamo la lista [-4; -3; -2; -1; 0; 1; 2; 3; 4]

Seq.toList ( Seq.take 9 ( Seq.skip 6 int10 ) ) ;;
// lista dei primi 9 elementi della sottosequenza ottenuta da int10 saltando i primi 6 elementi

//  E' piu' comodo scrivere l'ultima definizione usando la pipe (|>)

int10 |> Seq.skip 6 |> Seq.take 9 |> Seq.toList ;;


(*

Verificare cosa succede definendo

  let rec intFromList n  =  n :: ( intFromList (n + 1) ) ;;

  let nat =  intFromList 0  ;;

Ricordarsi che :: (cons) e' valutato un modo strict. 

*)   

(*

ESERCIZIO
--------

Definire la sequenza infinita sqFalse : seq<bool>  composta solamente da false usando  la ricorsione.

*)
