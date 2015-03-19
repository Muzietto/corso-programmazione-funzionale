// ALBERI BINARI DI RICERCA

type 'a binTree =
    | Null    // empty tree
    | Node of 'a  * 'a binTree * 'a binTree ;;   // Node(root, left, right)

    (*

       Node(root,left,right) rappresenta l'albero binario btree tale che:
    
       -  root :  'a             e' la radice di btree
       -  left :  'a binTree     e' il sottoalbero sinistro di root 
       -  right : 'a binTree     e' il sottoalbero destro di troot

                 root
              /       \
         .. left..  .. right ..     

     *)


// A.1
// Inserisce l'elemento x nell'albero binario di ricerca btree
// insert :  'a * 'a binTree -> 'a binTree when 'a : comparison

let rec insert (x , btree ) =
    match btree with
        | Null -> Node(x, Null, Null)  // albero che contiene solo il nodo x 
        | Node(r, left, right) when x = r ->  btree 
        | Node(r, left, right) when x < r ->  Node(r,  insert( x, left) , right )
        | Node(r, left, right)  ->  Node(r , left, insert (x, right) ) ;;
        // nell'ultimo caso,  deve valere  r < x (tricotomia dell'ordinamento < )
   
// A.2
// Inserisce nell'albero di ricerca btree tutti gli elementi della lista list
// insertFromList : 'a list * 'a binTree -> 'a binTree when 'a : comparison

let rec insertFromList ( list, btree ) =
    match list with
        | [] -> btree
        | x :: xs ->
            let btree1 = insert (x, btree)
            insertFromList  (xs , btree1) ;;
          
// Esempi

let intList = [20 ; 10 ; 60 ; 15 ; 40 ; 100 ; 30 ; 50 ; 70 ; 35 ; 42 ; 58 ; 75 ; 32 ; 37] ;;

let intTree = insertFromList (intList, Null) ;;

(*   intTree

      ------------- 20 --------------
      |                             |
     10                  --------- 60 ----------
    /  \                 |                      |
   o   15           ----- 40 ------            100
      /  \          |             |            /  \ 
     o    o        30            50           70   o
                  /  \           / \         /  \
                 o   35        42   58      o   75
                    /  \       /\   /\          /\
                   32   37    o  o o  o        o  o 
                   /\   /\
                  o  o o  o
 
    o : Null

*)  


let strList1 = [ "pesca" ; "banana" ; "uva" ; "albicocca" ; "nocciola" ; "ribes" ] ;;

let strTree1 = insertFromList ( strList1, Null) ;;

(*   strTree1

                  -------------------- pesca --------------------
                  |                                             |
        ------ banana --------                                 uva
       |                      |                               /   \   
   albicocca               nocciola                       ribes    o
      /\                    /    \                         / \
     o  o                  o      o                       o   o
        

*)


let strList2 = [ "limone" ; "ciliegia" ; "mela" ; "pera" ; "noce"  ] ;; 

let strTree2 = insertFromList (strList2, strTree1) ;;

(*   strTree2

                  -------------------- pesca --------------------
                  |                                             |
        ------ banana --------                                 uva
       |                      |                               /   \   
   albicocca          -----nocciola-----                  ribes    o
      /\               |                 |                 / \
     o  o           limone             pera               o   o
                  /       \           /    \
            ciliegia      mela      noce    o   
              / \         / \       / \  
             o   o       o   o     o   o 

*)


// A.3
// Dato un albero binario di int, genera una albero di float con gli stessi valori 
// intToFloatTree : int binTree -> float binTree

let rec intToFloatTree btree =
    match btree with
        | Null -> Null
        | Node ( r, left, right ) ->
            Node ( float r , intToFloatTree left , intToFloatTree right ) ;;

// la funzione float : int -> float  trasforma un intero in un float

let floatTree = intToFloatTree intTree ;;

