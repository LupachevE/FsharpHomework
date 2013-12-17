(*  Largest prime factor
    Created by Lupachev Eugene
    171 group  *)

let rec lPrime x curr = 
  if x <> 1L then
    if x % curr = 0L then lPrime (x / curr) curr else lPrime x (curr + 1L)
  else curr

let factor = lPrime 600851475143L 2L

printfn "%A" factor