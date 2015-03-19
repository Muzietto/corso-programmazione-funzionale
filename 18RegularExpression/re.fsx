(****     REGULAR EXPRESSIONS     ****)


open System.Text.RegularExpressions ;;

(*

   o Character Classes
     ^^^^^^^^^^^^^^^^^

     A character class matches any one of a set of characters.
     
     [character_group]  -->    Matches any single character in character_group. 
          
     [^character_group] -->    Negation: Matches any single character that is not in character_group. 

     [first - last]     -->    Character range: Matches any single character in the range from first to last. 
     
     .                 -->     Wildcard: Matches any single character except \n.
                               To match a literal period character (.)
                               you must precede it with the escape character (\.).

    \s                 -->     Matches any white-space character.
	
    \S                 -->     Matches any non-white-space character.
	
    \d                 -->     Matches any decimal digit.
	
    \D                 -->     Matches any character other than a decimal digit.

    
   o Quantifiers
     ^^^^^^^^^^^
   
    A quantifier specifies how many instances of the previous element
    must be present in the input string for a match to occur.

     *     -->   Matches the previous element zero or more times. 
    
     +     -->   Matches the previous element one or more times.

     ?     -->   Matches the previous element zero or one time.

   o Alternation constructs 
     ^^^^^^^^^^^^^^^^^^^^^^

     Alternation constructs modify a regular expression to enable either/or matching. 

      |   -->   Matches any one element separated by the vertical bar (|) character.

*)


(*

Regex  Class
^^^^^^^^^^^^

- Constructors
  
  o  Regex(String)
     Initializes a new instance of the Regex class for the specified regular expression.

*)


// RE che descrive un numero intero positivo (senza segno)

(*  Un numero e' una sequenza di cifre con almeno una cifra.
    Attenzione a non inserire nell'espressione degli spazi
   (lo spazio va messo solo se fa parte della RE)
*)

let  numRe = Regex  "\d+"  ;;

// oppure

let  numRe1 = Regex  "[0-9]+" ;;  
// [0-9] denota un carattere compreso fra 0 e 9



(*

Regex Class
^^^^^^^^^^^

- Methods

 o Match(String)
   Searches the specified input string for the first occurrence of the regular expression
   specified in the Regex constructor.

   Return Value  : Type System.Text.RegularExpressions.Match
   An object that contains information about the match.

 o Match(String, Int32)
   Searches the input string for the first occurrence of a regular expression,
   beginning at the specified starting position in the string.
     
   Return Value  : Type System.Text.RegularExpressions.Match
   An object that contains information about the match.


*)   

// esempi

let s1 = "esempi di numeri: 12, -325 e 9876. Altri numeri: +11, 22, -44 , +15 e infine -321."  ;;
let s2 =  "stringa che non contiene numeri!"    ;;

let m1 = numRe.Match(s1) ;;   // chiamata del metodo Match su m1
// val m1 : Match = 12 (descrive il match con la sottostringa "12" di s1)



(*

Match Class
^^^^^^^^^^^    

- Properties

  o Index
    The position in the original string where the first character of the captured substring 

  o Length
    Gets the length of the captured substring

  o Success
    Gets a value indicating whether the match is successful

  o Value
    Gets the captured substring from the input string

*)   



m1.Success ;;  // true se e solo se il  match ha avuto successo 
// val it : bool = true

m1.Index ;;  // posizione da cui parte il match trovato
// val it : int = 18  (la stringa "12" inizia al carattere in posizione 18 di s1)

m1.Length ;; // lunghezza della stringa in m1
// val it : int = 2   (la stringa "12" ha lunghezza 2)

let m2 = numRe.Match(s1,20) ;; // cerca un match in s1 a partire dalla posizione 20 (virgola dopo 12)
// val m2 : Match = 325 

let m3 = numRe.Match(s2) ;;   
// val m3 : Match =
// (in s2 non c'e' alcun match)

m3.Success ;;  // il  match ha avuto successo?
// val it : bool = false



// RE che descrive un numero con segno 

(*
  Un numero con segno e' un numero che puo' essere preceduto da + o -
  In una RE, per denotare il carattere '+' occorre scrivere '\+' (quoting),
  per denotare il carattere '-' occorre scrivere  '\-'
  I simboli + e - hanno nel linguaggio RE un significato speciale  

*)   


let numSigRe = Regex  "[\+\-]?\d+"  ;;

let m4 = numSigRe.Match (s1) ;;                                                                    
// val m4 : Match = 12  (come prima)

let m5 = numSigRe.Match (s1,20) ;;
// val m5 : Match = -325 (notare che il segno - fa parte della stringa nel match)




(*

o Grouping
  ^^^^^^^^

  Grouping constructs delineate subexpressions of a regular expression
  and typically capture substrings of an input string. 


  ( subexpression )  -->  Captures the matched subexpression and assigns it a natural number
 
*)


(* Espressione regolare che corrisponde a una data nel formato day.month.year
   dove day a month hanno una o due cifre, year ha quattro cifre. *)

let dateRe =  Regex "\d?\d\.\d?\d\.\d\d\d\d" ;;
// \. denota il carattere punto (quoting)
//  Nel linguaggio  RE, . denota un qualunque carattere.


let dates =
    "Esempio di data: 14.5.2014" +
    "Invece 14.5.14 non e' una data valida" ;;

let md1 = dateRe.Match(dates) ;;
// val md1 : Match = 14.5.2014

md1.Index ;; // 17
md1.Length ;; // 9

// prossimo match (prima data valida dopo  14.5.2014)
let md2 = dateRe.Match(dates,26) ;;
// val md2 : Match = 1.1.2014  


(*

E' possibile raggruppare fra parentesi tonde sottoparti di una RE
in modo da poter estrarre le corrispondenti sottostringhe.

Ad esempio, supponiamo che da dateRe vogliamo estrarre le stringhe
che descrivono giorno, mese e anno.

Allora ciascune delle sottoespressioni

   \d?\d     // giorno
   \d?\d     // mese
   \d\d\d\d  // anno

va racchiusa fra parentesi


*)   


// RE che descrive una data in cui sono raggruppati giorno, mese e anno   
let dateGrpRe =  Regex "(\d?\d)\.(\d?\d)\.(\d\d\d\d)" ;;


let mdg1 = dateGrpRe.Match(dates) ;;  // primo match
// val mdg1 : Match = 14.5.2014


(*

Match Class
^^^^^^^^^^^
Represents the results from a single regular expression match. 

- Properties

  o Groups
    Gets a collection of groups matched by the regular expression.

    Type: System.Text.RegularExpressions.GroupCollection
    The character groups matched by the pattern.


GroupCollection Class
^^^^^^^^^^^^^^^^^^^^^
Returns the set of captured groups in a single match. 

- Properties

  o  Item(Int32)
     Enables access to a member of the collection by integer index.

     Type: System.Text.RegularExpressions.Group
     The member of the collection specified by groupnum.

Group Class
^^^^^^^^^^^
Represents the results from a single capturing group.

- Properties  

  o  Value
     Gets the captured substring from the input string.
     Type: System.String

*)   

// il Match mdg1  descrive la data 14.5.2014

let day =   mdg1.Groups.Item(1).Value ;;  // primo gruppo 
// val day : string = "14"

let month = mdg1.Groups.Item(2).Value ;;  // secondo gruppo
// val month : string = "5"

let year =  mdg1.Groups.Item(3).Value ;;   // terzo gruppo 
// val year : string = "2014"

(*

Notare i tipi dei seguenti termini:

mdg1 ----------------------->   MatchClass      (System.Text.RegularExpressions.Match)
mdg1.Groups ---------------->   GroupCollection (System.Text.RegularExpressions.GroupCollection)
mdg1.Groups.Item(1) -------->   Group           (System.Text.RegularExpressions.Group)
mdg1.Groups.Item(1).Value -->   String          (System.String)


*)   



(*

o Anchors
  ^^^^^^

  Anchors cause a match to succeed or fail depending on the current position in the string,
  but they do not cause the engine to advance through the string or consume characters.

   \G  -->  The match must start at the beginning of the string or the specified substring
            (\G is not matched to any character).

   $  -->  The match must terminate at the beginning of the string
           ($ is not matched to any character).

*)   

// Esempio: RE con anchors che descrive una stringa corrispondente a un numero positivo

(*

Vogliamo definire una espressione regolare che riconosce una una stringa
che rappresenta un numero senza segno (sequenza non vuota di cifre).

Usiamo la RE  '\d+' vista prima,  preceduta e seguita da \G e $
per significare che il match deve  essere  soddisfatto sull'intera stringa
(e non su sottostringhe della stringa)

*)

let numAnRe = Regex  "\G\d+$" ;;

let ma1 = numAnRe.Match "123" ;; // match con "123"
// val ma1 : Match = 123

let ma2 = numAnRe.Match "123 " ;;   
// val ma2 : Match =
// la stringa non e' riconosciuta  quanto dopo 3 c'e' uno spazio


let ma3 = numAnRe.Match " 123" ;;  // nessun match
// val ma3 : Match = 
// la stringa non e' riconosciuta perche' inizia con uno spazio


