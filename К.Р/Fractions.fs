let rec GCD (a : int, b: int) = 
  if a = 0 then b else GCD (b % a , a)

type MyType (a, b) =
  member this.den : int = a
  member this.num : int = b
  
  member this1.Summ (this2 : MyType ) = 
    if this1.den = 0 || this2.den = 0 then failwith "Error, division by 0"
    if this1.den = this2.den 
    then
      let mutable fin2_num = 0 
      let fin_den = this1.den 
      let fin_num = this1.num + this2.num
      MyType (fin_den, fin2_num)
      else 
        let fin_den = this1.den * this2.den
        let mutable ed_num1 = this1.num * this2.den
        let mutable ed_num2 = this2.num * this1.den
        let fin_num = ed_num1 + ed_num2
        MyType(fin_den / GCD (fin_den, fin_num), fin_num / GCD (fin_den, fin_num))
  
  member this1.Sub (this2 : MyType ) =
      if this1.den = this2.den 
      then
        let fin_den = this1.den 
        let fin_num = this1.num - this2.num
        MyType (fin_den, fin_num)
      else 
        let fin_den = this1.den * this2.den
        let ed_num1 = this1.num * this2.den
        let ed_num2 = this2.num * this1.den
        let fin_num = ed_num1 - ed_num2
        MyType(fin_den / GCD (fin_den, fin_num), fin_num / GCD (fin_den, fin_num))
      
  member this1.Mult (this2 : MyType) = 
    let fin_den = this1.den * this2.den
    let fin_num = this1.num * this2.num
    MyType (fin_den / GCD (fin_den, fin_num), fin_num / GCD (fin_den, fin_num))    

let fraction_line (a : MyType) =
  printf "-"
  let mutable lol = 10
  while a.den / lol <> 0 || a.num / lol <> 0 do
    printf "-"
    lol <- lol * 10

let print_spaces (a : MyType) =
  let mutable lol = 1
  while a.den / lol <> 0 || a.num / lol <> 0 do
    lol <- lol * 10
    printf " "

let count_digits a =
  let mutable curr = 0
  let mutable lol = 1
  while a/ lol <> 0 do
    curr <- curr + 1
    lol <- lol * 10
  curr

let first = MyType (12, 21)
let second = MyType (13, 14)
let final_summ = first.Summ(second)
let final_sub = first.Sub(second)
let final_mult = first.Mult(second)

printf "%A" first.num
if String.length (string (first.num)) < String.length (string(first.den)) then 
  for i in String.length (string (first.num)) + 1..String.length (string(first.den)) do
    printf " "
  else
    printf ""
printf " "
printf "%A" second.num
if String.length (string (second.num)) < String.length (string(second.den)) then 
  for i in String.length (string (second.num)) + 1..String.length (string(second.den)) do
    printf " "
  else
    printf ""
printf " "
printfn "%A" final_summ.num

fraction_line(first)
printf "+" 
fraction_line(second)
printf "=" 
fraction_line(final_summ)

printfn ""

printf "%A" first.den
if String.length (string (first.num)) < String.length (string(first.den)) then
    printf ""
  else
    for i in String.length (string (first.num)) + 1..String.length (string(first.den)) do
    printf " "
printf " "
printf "%A" second.den
if String.length (string (second.num)) < String.length (string(second.den)) then 
    printf ""
  else
   for i in String.length (string (second.num)) + 1..String.length (string(second.den)) do
    printf " "
printf " "
printfn "%A" final_summ.den

printfn ""

printf "%A" first.num
if String.length (string (first.num)) < String.length (string(first.den)) then 
  for i in String.length (string (first.num)) + 1..String.length (string(first.den)) do
    printf " "
  else
    printf ""
printf " "
printf "%A" second.num
if String.length (string (second.num)) < String.length (string(second.den)) then 
  for i in String.length (string (second.num)) + 1..String.length (string(second.den)) do
    printf " "
  else
    printf ""
printf " "
if final_sub.num * final_sub.den < 0 then printf " %A" final_sub.num 
  else 
    printfn "%A" final_sub.num
fraction_line(first)
printf "-" 
fraction_line(second)
printf "="
if final_sub.num * final_sub.den < 0 then printf "-"
else 
  printf ""
fraction_line(final_sub)

printfn ""

printf "%A" first.den
if String.length (string (first.num)) < String.length (string(first.den)) then
    printf ""
  else
    for i in String.length (string (first.num)) + 1..String.length (string(first.den)) do
    printf " "
printf " "
printf "%A" second.den
if String.length (string (second.num)) < String.length (string(second.den)) then 
    printf ""
  else
   for i in String.length (string (second.num)) + 1..String.length (string(second.den)) do
    printf " "
printf " "
if final_sub.num * final_sub.den < 0 then printf " %A" final_sub.num 
  else 
    printfn "%A" final_sub.num

printfn ""

printf "%A" first.num
if String.length (string (first.num)) < String.length (string(first.den)) then 
  for i in String.length (string (first.num)) + 1..String.length (string(first.den)) do
    printf " "
  else
    printf ""
printf " "
printf "%A" second.num
if String.length (string (second.num)) < String.length (string(second.den)) then 
  for i in String.length (string (second.num)) + 1..String.length (string(second.den)) do
    printf " "
  else
    printf ""
printf " "
printfn "%A" final_mult.num

fraction_line(first)
printf "*" 
fraction_line(second)
printf "=" 
fraction_line(final_summ)

printfn ""

printf "%A" first.den
if String.length (string (first.num)) < String.length (string(first.den)) then
    printf ""
  else
    for i in String.length (string (first.num)) + 1..String.length (string(first.den)) do
    printf " "
printf " "
printf "%A" second.den
if String.length (string (second.num)) < String.length (string(second.den)) then 
    printf ""
  else
   for i in String.length (string (second.num)) + 1..String.length (string(second.den)) do
    printf " "
printf " "
printfn "%A" final_mult.den