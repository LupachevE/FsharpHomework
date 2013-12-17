(*  Smallest m19ultiple
    Created by Lupachev Eugene
    171 group  *)

let rec GCD x y = 
    if x = 0L then y 
    else GCD (y % x) x

let LCM n =
  let rec lcm i acc =
    if i <= n then lcm (i + 1L) (acc * i / GCD i acc) else acc  
  lcm 1L 1L

let lcm = LCM 20L

printfn "%A" lcm