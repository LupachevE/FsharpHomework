(*  Fibonacci sum
    Created by Lupachev Eugene
    171 group*)

let fibSumm n =
  
  let isNeed x =
    if x % 2 = 0 then true else false  
  let rec fib f1 f2 curr =
      if f1 < n then
        fib f2 (f1 + f2) (if isNeed(f1) then (curr + f1) else curr)
      else curr
  fib 1 2 0

let summ = fibSumm 4000000 
printfn "%A" summ