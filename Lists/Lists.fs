(* Lupachev Eugene
   Lists actions*)

let rec add_elem_toList n list =
  match list with
  | [] -> n :: list
  | head :: tail -> head :: (add_elem_toList n tail)

if  (add_elem_toList 10 [2;4;7;3;8] = [2;4;7;3;8;10]) then printfn "Add test completed" else printfn "Add test failed"

let rec append list1 list2 =
  match list1 with
  | [] -> list2
  | head :: tail -> head :: (append tail list2)

if (append [2;5] [4;6] = [2;5;4;6]) then printfn "Append test completed" else printfn "Append test failed" 

let reverse list =
  let rec Ed_rev list [] = 
    match list with
    | [] -> []
    | head :: tail ->  Ed_rev tail (head :: [])
  Ed_rev list []

if (reverse [1;2;3] = [3;2;1]) then printfn "Reverse test completed" else printfn "Reverse test failed"

let isEmpty list = 
  if (list = []) then printfn "Список пуст" else printfn "Список непуст"
 
isEmpty([1;2;3;4])
isEmpty([])
isEmpty([2])

let map list make = List.foldBack (fun elem acc -> (make elem)::acc) list []

if map [2;3;5;6] (fun x -> x * x) = [4;9;25;36] then printfn "Map test completed" else printfn "Map test failed"

let rec find find_what list=
    match list with
    | [] -> None
    | head ::tail -> if find_what head then Some head else find find_what tail 

if find ((>)9) [1;4;7;37;5;0] = Some 1 then printfn "Find test completed" else printfn "Find test failed"