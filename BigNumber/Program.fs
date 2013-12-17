(*Created by Lupachev Evgeniy
  factorial v2.0
  SPBGU M-M *)

open System

let d = 1000000000

type BigNumber =
  val private Sign : bool
  val public Value : ResizeArray<int>

  new (str : string) =
    let sign' = str.StartsWith("-")
    let cellSide = d.ToString().Length - 1
    let numStr = (if sign' then str.Substring(1) else str).Length
    let rest = numStr % cellSide
    let newValue = new ResizeArray<int>()
    if rest <> 0 then newValue.Add(int str.[0 .. rest - 1])
    for i = 0 to numStr / cellSide - 1 do
      let j = i * cellSide + rest + (if sign' then 1 else 0)
      newValue.Add(int str.[j .. j + cellSide - 1])
      
    newValue.Reverse()
    {
      Sign = sign'
      Value = newValue
    }

  new (number : int) = 
    BigNumber(number.ToString())

  new (number : int64) = 
    BigNumber(number.ToString())

  private new (sign : bool, list : ResizeArray<int>) = {Sign = sign; Value = list}

  member this.Abs = new BigNumber(false, this.Value)

  member first.CompareTo(second : BigNumber) =
    let fstlen = first.Value.Count
    let sndlen = second.Value.Count
    if first.Sign <> second.Sign then (if first.Sign then -1 else 1)
    else if fstlen <> sndlen then (if (fstlen > sndlen) then 1 else -1) * (if first.Sign then -1 else 1)
    else
      
      let mutable i = fstlen
      let mutable res = 0
      
      while i > 0 && res = 0 do
        i <- i - 1
        if first.Value.[i] > second.Value.[i] then res <- (if first.Sign then -1 else 1)
        if first.Value.[i] < second.Value.[i] then res <- (if first.Sign then 1 else -1)
      res


  member private first.Add(second : BigNumber) =
    let fstlen = first.Value.Count
    let scndlen = second.Value.Count
    let fstIsMin = fstlen < scndlen
    let max = if fstIsMin then second else first
    
    let res = new ResizeArray<int>()
    let mutable carry = 0

    for i = 0 to (if fstIsMin then fstlen else scndlen) - 1 do
      res.Add((first.Value.[i] + second.Value.[i] + carry) % d)
      carry <- (first.Value.[i] + second.Value.[i] + carry) / d

    for i = (if fstIsMin then fstlen else scndlen) to (if not fstIsMin then fstlen else scndlen) - 1 do
      res.Add((max.Value.[i] + carry) % d)
      carry <- (max.Value.[i] + carry) / d

    if carry <> 0 then res.Add(carry)
    res   

  member private first.Sub(second : BigNumber) =
    let fstlen = first.Value.Count
    let scndlen = second.Value.Count

    let res = new ResizeArray<int>()
    let mutable carry = 0

    for i = 0 to  scndlen - 1 do
      res.Add((first.Value.[i] - second.Value.[i] - carry + d) % d)
      carry <- if first.Value.[i] - second.Value.[i] - carry < 0 then 1 else 0

    for i = scndlen to fstlen - 1 do
      res.Add((first.Value.[i] - carry + d) % d)
      carry <- if first.Value.[i] - carry < 0 then 1 else 0

    let mutable i = res.Count - 1
    while res.[i] = 0 && i <> 0 do
      res.RemoveAt(i)
      i <- i - 1
    res

  static member (*) (first : BigNumber, second : BigNumber) =
    let mutable res = new BigNumber(0)
    if first.Value <> new ResizeArray<int>(0) && second.Value <> new ResizeArray<int>(0) then
        let fstlen = first.Value.Count
        let scndlen = second.Value.Count
        for i = 0 to fstlen - 1 do
          for j = 0 to scndlen - 1 do
              let summand = new BigNumber(int64 first.Value.[i] * int64 second.Value.[j])
              for k = 1 to i + j do summand.Value.Insert(0, 0)
              res <- res + summand
    new BigNumber(first.Sign <> second.Sign, res.Value) 

                                             
   
 
  static member (~-) (this : BigNumber) = new BigNumber(not this.Sign, this.Value)

  static member (+) (first : BigNumber, second : BigNumber) =
    if first.Sign = second.Sign then new BigNumber(first.Sign, first.Add(second)) 
                                else if (first.Abs).CompareTo(second.Abs) <> -1 then new BigNumber(first.Sign, first.Sub(second)) 
                                                                       else new BigNumber(second.Sign, second.Sub(first))

  static member (-) (first : BigNumber, second : BigNumber) = first + (-second)

 
  member this.print : unit =
    let len = this.Value.Count
    if this.Value <> ResizeArray<int>(0) then printf "%s" (if this.Sign then "-" else "")
    printf "%d" (this.Value.[len - 1])
    for i = len - 2 downto 0 do 
      printf " %s%d" (String.replicate (d.ToString().Length - String.length (string this.Value.[i]) - 1) "0") (this.Value.[i])
    printfn ""
    
let fact n =
  let rec fact i x = if i > 0 then fact (i - 1) (x * new BigNumber(i)) else x
  fact n (new BigNumber(1))

let st1 = DateTime.Now
let c = fact(1000)
let a = fact(1000)
let b = fact(1000)
let x = fact(1000)
let e = fact(1000)
let st2 = DateTime.Now
printfn "%A" (st2 - st1)