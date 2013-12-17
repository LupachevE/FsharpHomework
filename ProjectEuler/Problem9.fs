(*  Special Pythagorean triplet
    Created by Lupachev Eugene
    171 group  *)

for a in 1..999 do
  for b in a + 1..999 do
    let c = 1000 - a - b
    if a * a + b * b = c * c then printfn "%A" (a * b * c)
