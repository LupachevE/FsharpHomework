(*  Square differences
    Created by Lupachev Eugene
    171 group*)

let sumDif n =
  let list = [1..n]
  List.sum (List.map (fun x -> x * x) list) - List.sum list * List.sum list

let differ = sumDif 100
printfn "%A" differ