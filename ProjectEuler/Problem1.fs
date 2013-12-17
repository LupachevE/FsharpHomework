(*  Multiples of 3 and 5
    Created by Lupachev Eugene
    171 group  *)

printfn "%A" (List.foldBack (+) (List.filter (fun x -> x % 3 = 0 || x % 5 = 0) [1..999]) 0)