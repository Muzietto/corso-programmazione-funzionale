

open System.Text.RegularExpressions ;;


//////////////////////////////
// ESERCIZIO 1  (NUMERI)

// getMatches : string -> Regex -> int -> seq<Match>
// costruisce la sequenza di tutti i match in una stringa partendo dalla posizione specificata

let rec getMatches  str (re:Regex)  pos =
    seq { let m0 = re.Match(str,pos)  // cerca primo match
          // se il match ha avuto successo, aggiungi m0 alla sequenza e continua la ricerca
          if m0.Success  then
              yield m0
              let pos1 = m0.Index +  m0.Length // posizione da cui continuare la ricerca
              yield! getMatches  str re pos1
          } ;;

// RE che riconosce un numero 
let  numRe = Regex  "\d+"  ;;
// \d  : una cifra 
// \d+ : sequanza di \d con almeno un elemento

// RE che riconosce un numero con segno
let numSigRe = Regex  "[\+\-]?\d+"  ;;
//    [\+\-]   :   uno fra i  caratteri + e -
//    [\+\-]?  :   uno fra i  caratteri + e - oppure nessun carattere
    
let s1 = "esempi di numeri: 12, -325 e 9876. Altri numeri: +11, 22, -44 , +15 e infine -321."  ;;
let s2 =  "stringa che non contiene numeri!"    ;;


// esempi

let seq1 =  getMatches s1  numRe 0 ;; // sequenza dei match
let seqv1 =  Seq.map ( fun (m : Match) -> m.Value ) seq1  ;; // sequenza dei valori
let l1 =  Seq.toList seqv1 ;; 
// l1 :  ["12"; "325"; "9876"; "11"; "22"; "44"; "15"; "321"]

let seq2 =  getMatches s2  numRe 0 ;; 
// seq2 e' la sequenza vuota

let seq3 =  getMatches s1  numSigRe 0 ;;
let seqv3 =  Seq.map ( fun (m : Match) -> m.Value ) seq3  ;;
let l3 =  Seq.toList  seqv3 ;;
// l3 :  ["12"; "-325"; "9876"; "+11"; "22"; "-44"; "+15"; "-321"]



/////////////////////////////////////////////////////////////////////
// ESERCIZIO 2 (DATE)
                  

// data formato  day.month.year  dove day a month hanno una o due cifre, year ha quattro cifre.
// giorno, mese e anno sono raggruppati
let dateGrpRe =  Regex "(\d?\d)\.(\d?\d)\.(\d\d\d\d)" ;;

(* Per denotare il carattere punto va scritto \. (quoting)
   Infatti, nel linguaggio delle RE il . fa il match con qualunqe carattere. 
*)

let dates =
        "0ggi e' il giorno 14.5.2014" +
        "Data non valida: 14.5.14" +
        "Esempi di altre date valide: 1.1.2014, 12.19.2013"+
        "Esempi di altre date non valide: 1/1/2014, 12\10\2013" ;;


type date =  { day:int ; month:int ; year:int } ;;

// getDates :  string -> seq<date>
// costruisce la sequenza delle date in una stringa
let getDates str =
   let seqDates =  getMatches str dateGrpRe  0 
   // dato  ma : Match ,  f ma costruisce il record che rappresenta la data  catturata da ma
   let f (ma : Match) =
       let d =  ma.Groups.Item(1).Value   |> int  // giorno
       //  ma.Groups.Item(1).Value e' una stringa 
       let m =  ma.Groups.Item(2).Value   |> int  // mese 
       let y =  ma.Groups.Item(3).Value   |> int  // anno 
       { day=d ; month=m ; year=y } 
   Seq.map f  seqDates ;;

  
// esempio

getDates dates |> Seq.toList ;;

(*
val it : date list =
    [
      {day = 14; month = 5; year = 2014} ;
      {day = 1; month = 1; year = 2014} ;
      {day = 12; month = 19; year = 2013}
    ]

*)

