(*  100001st prime
    Created by Lupachev Eugene
    171 group*)

let findPrime n =
  let rec isPrime x list =
    let sqrt_x = int(sqrt(float x)) 
    match list with 
    |head :: tail when head <= sqrt_x -> if x % head <> 0 then isPrime x tail else false
    |_ -> true
  
  let rec findPrime1 list sizelist p =
    if sizelist < n then if isPrime p (List.rev list) then findPrime1 (p :: list) (sizelist + 1) (p + 2) 
                                       else findPrime1 list sizelist (p + 2)
                     else list.Head
  findPrime1 [2] 1 3 

let prime = findPrime 10001

printfn "%A" prime