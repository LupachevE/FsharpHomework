(*  Largest palindrome
    Created by Lupachev Eugene
    171 group  *)

let mPol =
  let rec polindrome i j max =
    if i >= 900 then
      let x = i * j
      let a = x / 10 - (x / 100000) * 10000
      let b = a / 10 - (a / 1000) * 100
      if (x % 10 = x / 100000) && (a % 10 = a / 1000) && (b % 10 = b / 10) && (x > max) then polindrome (i - 1) j x
                                                                                        else polindrome (i - 1) j max   
    else if j >= 900 then polindrome 999 (j - 1) max
                     else max

  polindrome 999 999 0


printfn "%A" mPol