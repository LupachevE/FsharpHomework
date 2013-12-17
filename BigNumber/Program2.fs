(*Created by Lupachev Evgeniy
  factorial v2.0
  SPBGU M-M *)

open System

let d = 1000000000

type BigNumber =
  val private Sign : bool
  val public Value : int list

  new (str : string) =
    let ord c = int c - int '0'
    let sign' = str.StartsWith("-")
    let lst = List.ofArray ((if sign' then str.Substring(1) else str).ToCharArray()) |> List.map ord
    let cellSide = d.ToString().Length - 1
    let numLen = lst.Length
    let groupOnce n list = 
      let rec r i h rest = 
          if (i >= n) then h::rest else 
            match rest with 
            | [] -> h :: []
            | head :: tail -> r (i + 1) (h * 10 + head) tail
      match list with
      | [] -> []
      | head :: tail -> if n = 0 then list else r 1 head tail

    let groupIt list = 
      let mod' = numLen % cellSide
      let withFirstGroup = groupOnce mod' lst
      let ungrouped = if mod' = 0 then withFirstGroup else List.tail withFirstGroup
      let rec r ungrouped = 
          match ungrouped with
          | [] -> []
          | _ -> let t = groupOnce cellSide ungrouped in (List.head t) :: (r (List.tail t))
      let grouped = r ungrouped
      if mod' = 0 then grouped else (List.head withFirstGroup) :: grouped
    {
      Sign = sign'
      Value = List.rev(groupIt lst)
    }

  new (number : int) = 
    BigNumber(number.ToString())

  new (number : int64) = 
    BigNumber(number.ToString())

  private new (sign : bool, list : int list) = {Sign = sign; Value = list}

  
  member this.Abs = new BigNumber(false, this.Value)

  member first.CompareTo(second : BigNumber) =
    let rec cmp list1 list2  =
      match list1, list2 with
      |head1 :: tail1, head2 :: tail2 -> if head1 = head2 then cmp tail1 tail2 else if (head1 > head2) then 1 else -1
      |_ -> 0
    let fstlen = first.Value.Length
    let sndlen = second.Value.Length
    if first.Sign <> second.Sign then (if first.Sign then -1 else 1)
    else if fstlen <> sndlen then (if (fstlen > sndlen) then 1 else -1) * (if first.Sign then -1 else 1)
    else cmp (List.rev first.Value) (List.rev second.Value) * (if first.Sign then -1 else 1)


  member private first.Add(second : BigNumber) =
    let rec add list1 list2 acc carry =
      match list1, list2 with
      |head1 :: tail1, head2 :: tail2 -> add tail1 tail2 (((head1 + head2 + carry) % d) :: acc) ((head1 + head2 + carry) / d)
      |[], head :: tail -> add list2 [] acc carry
      |head :: tail, [] -> add tail [] (((head + carry) % d) :: acc) ((head + carry) / d)
      |[], [] -> List.append (if carry = 1 then [1] else []) acc
    List.rev (add first.Value second.Value [] 0)

  member private first.Sub(second : BigNumber) =
    let rec sub list1 list2 acc carry =
      match list1, list2 with
      |head1 :: tail1, head2 :: tail2 -> sub tail1 tail2 (((head1 - head2 - carry + d) % d) :: acc) (if head1 - head2 - carry < 0 then 1 else 0)
      |head :: tail, [] -> sub tail [] (((head - carry + d) % d) :: acc) (if head - carry < 0 then 1 else 0)
      |_ -> acc
    
    let rec dltZero list =
      match list with
      |head :: tail -> if head = 0 then dltZero tail else list
      | [] -> [0]
    
    List.rev (dltZero (sub first.Value second.Value [] 0))

  static member (*) (first : BigNumber, second : BigNumber) =
    let rec mul (list1 : int list) (list2 : int list) acc i j =
      match list1, list2 with
      |head1 :: tail1, head2 :: tail2 -> 
        mul tail1 list2 (acc + new BigNumber(false, (List.append [for k in [1 .. i + j] -> 0] (new BigNumber(int64 head1 * int64 head2)).Value))) (i + 1) j
      |[], head :: tail -> mul first.Value tail acc 0 (j + 1)
      |_ -> acc.Value
      
    if first.Value = [0] || second.Value = [0] then new BigNumber(0) 
                                                else new BigNumber(first.Sign <> second.Sign, mul first.Value second.Value (new BigNumber(0)) 0 0)  
   
 
  static member (~-) (this : BigNumber) = new BigNumber(not this.Sign, this.Value)

  static member (+) (first : BigNumber, second : BigNumber) =
    if first.Sign = second.Sign then new BigNumber(first.Sign, first.Add(second)) 
                                else if (first.Abs).CompareTo(second.Abs) <> -1 then new BigNumber(first.Sign, first.Sub(second)) 
                                                                       else new BigNumber(second.Sign, second.Sub(first))

  static member (-) (first : BigNumber, second : BigNumber) = first + (-second)

 
  
  member this.print =
    printf "%s" (if this.Sign then "-" else "")
    let t = List.rev this.Value
    printf "%d" (List.head t) 
    let rec printList list =
      match list with
      |head :: tail -> 
                       let rec printZero n =
                         if n > 0 then printf "0"; printZero (n - 1)
                       let countZero = let l i = i.ToString().Length in (l d) - (l head) - 1
                       printZero(countZero)  
                       printf "%d" head
                       printList tail
      | _ -> ()
    printList (List.tail t)
    printfn ""

let fact n =
  let rec fact i x = if i > 0 then fact (i - 1) (x * new BigNumber(i)) else x
  fact n (new BigNumber(1))

let st1 = DateTime.Now
let c = fact(1000)
let e = fact(1000)
let h = fact(1000)
let g = fact(1000)
let a = fact(1000)
let st2 = DateTime.Now
printfn "%A" (st2 - st1)
c.print