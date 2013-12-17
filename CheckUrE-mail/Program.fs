open System
open NUnit.Framework
open FsUnit
open System.Text.RegularExpressions

let thirdlevel = "^[a-zA-Z_][a-zA-Z0-9]*[\.]?[_-]*[a-zA-Z0-9]*"
let secondlevel = "[@][a-zA-Z]{1,30}"
let firstlevel = "\.(aero|arpa|asia|coopinfo|domain|info|jobs|mobi|museum|name|travel|yandex|[a-z]{2,3})$"
let pattern = thirdlevel + secondlevel + firstlevel
let checkEmail string = Regex.IsMatch(string, pattern)

(*Old tests
checkEmail "a@b.cc"
checkEmail "victor.polozov@mail.ru"
checkEmail "my@domain.info"
checkEmail "_.1@mail.com"
checkEmail "coins_department@hermitage.museum"

checkEmail "a@b.c"
checkEmail "a..b@mail.ru"
checkEmail ".a@mail.ru"
checkEmail "yo@domain.somedomain"
checkEmail "1@mail.ru"
*)

//New tests
[<TestFixture>]
type Tests() = 
  
  [<Test>]
  member this.``All 3 levels are short`` () = checkEmail "a@b.c" |> should be True
  
  [<Test>]
  member this.``Standart e-mail`` () = checkEmail "victor.polozov@mail.ru" |> should be True
  
  [<Test>]
  member this.``1st level domain - info`` () = checkEmail "my@domain.info" |> should be True
  
  [<Test>]
  member this.``Starts with '_'`` () = checkEmail "_.1@mail.com" |> should be True
  
  [<Test>]
  member this.``Strange and long, but correct`` () = checkEmail "coins_department@hermitage.museum" |> should be True
  
  [<Test>]
  member this.``Too short 1st level domain`` () = checkEmail "a@b.c" |> should be False
  
  [<Test>]
  member this.``Double dot`` () = checkEmail "a..b@mail.ru" |> should be False
  
  [<Test>]
  member this.``Wrong 1st level domain`` () = checkEmail "yo@domain.somedomain" |> should be False
 
  [<Test>]
  member this.``Starts with dot`` () = checkEmail ".a@mail.ru" |> should be False
 
  [<Test>] 
  member this.``Only number`` () = checkEmail "1@mail.ru" |> should be False