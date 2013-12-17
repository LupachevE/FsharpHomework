(*
   Lupachev Eugene
   Binary tree
 *)

open System

type 'a tree =
  | Empty
  | Node of 'a tree * 'a * 'a tree

let rec add tree x =
    match tree with
    | Empty -> Node(Empty, x, Empty)
    | Node(left, value, right) when x < value -> Node(add left x, value, right)
    | Node(left, value, right) when x > value -> Node(left, value, add right x)
    | _ -> tree

let rec find tree x =
  match tree with
    | Empty -> false
    | Node(left, value, right) when x < value -> find left x
    | Node(left, value, right) when x > value -> find right x
    | Node(left, value, right) when x = value -> true
    | _ -> false

let rec findMin tree = 
    match tree with
    | Node(Empty, value, _) -> value
    | Node(left, value, _) -> findMin left
    | Empty -> 0

let rec delete tree x =
    match tree with
    | Empty -> Empty
    | Node(left, value, right) when x < value -> Node(delete left x, value, right)
    | Node(left, value, right) when x > value -> Node(left, value, delete right x)
    | Node(left, value, right) when x = value -> 
      if left = Empty && right = Empty then Empty
      else if left = Empty then right
      else if right = Empty then left
      else let min = findMin right
           Node(left, min, delete right min)
    | _-> tree

let printTree tree =
  let rec print tree disp =
    let string (l : bool list) =
     List.foldBack(fun x acc -> if x then acc + "|  " else acc + "   ") l "" 
    match disp with
      | [] -> printf "%s" ""
      | [_] -> printf "%s" "|__"
      | head::tail -> printf "%s" <| string tail
                      printf "%s" "|__"
                  
    match tree with
      | Empty -> printfn "%s" "Nill"
      | Node(left, value, right) -> 
                                    printfn "%A" value
                                    print left (true :: disp)
                                    print right (false :: disp) 
  print tree []

let mutable testTree = Empty
testTree <- add testTree 13
testTree <- add testTree 3
testTree <- add testTree 55
testTree <- add testTree 23
testTree <- add testTree 14
testTree <- add testTree 23
testTree <- add testTree 6
testTree <- add testTree 42
testTree <- add testTree 11
testTree <- add testTree 19

printTree testTree