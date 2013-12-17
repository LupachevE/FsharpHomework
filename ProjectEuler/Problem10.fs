(*  Summation of primes
    Created by Lupachev Eugene
    171 group  *)
    
let sumPrime n =
  let rec isPrime x list =
    let sqrt_x = int64(sqrt(float x)) 
    match list with 
    |head :: tail when head <= sqrt_x -> if x % head <> 0L then isPrime x tail else false
    |_ -> true
  
  let rec findPrime list p =
    if p <= n then if isPrime p (List.rev list) then findPrime (p :: list) (p + 2L) 
                                       else findPrime list (p + 2L)
              else list

  List.sum(findPrime [2L] 3L)

let summation = sumPrime 20L
printfn "%A" summation