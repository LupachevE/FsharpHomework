(*Created by Lupachev Eugene
  271 groop
  SPBSU M-M*)

open System

type Stadium (price : int, count : int, hd : bool, cover : bool, name : string) =
    
    let mutable chosen = false

    member this.Name = name
    member this.Price = if price < 0 then failwith "Price out of range" else price
    member this.Number = count
    member this.HotDogs = hd
    member this.IsCovered = cover
    member this.ChosenForPremierLeague
      with get() = chosen
      and set vaule = chosen <- true

type Trainer (cash : int, name : string) =
    let mutable cash = cash
    member this.SpendMoney(that : Stadium) = 
        if cash - that.Price < 0 then printf "Not enough money" else cash <- cash - that.Price
        printfn "Curretn balance : %A"
    member this.ShowBalance = printfn "%A" cash
    member this.Name = name

type League (name : string, coef : float) =
    member this.Name = name
    member this.Coef = coef

type FootballPlayer (pay : float, skill : int, age : int) = 
    let mutable skill = skill
    let mutable currleague = new League("", 0.0)
    member this.WhatLeague(league : League) = currleague <- league 
    member this.InNation = false
    member this.Skill = if skill < 0 || skill > 100 then failwith "Skill out of range" else skill 
    member this.Pay = pay * currleague.Coef
    member this.ShowSalary = printfn "Current salary : %A" this.Pay
    member this.Age = if age < 0 then failwith "Age out of range" else age

type StamfordBridge private() =
  inherit Stadium (2000000, 50000000, false, true, "StamfordBridge")
  static let uniqueBridge = StamfordBridge()
  member this.Action = "Get 2 free tickets, if you wear special costumes"
  static member Instance = uniqueBridge

type FritzWalterStadion private() =
  inherit Stadium (500000, 2500000, true, false, "FritzWalterStadion")
  static let uniqueFritz = FritzWalterStadion()
  static member Instance = uniqueFritz

type Advocaat private() =
  inherit Trainer (20000000, "Advocaat")
  static let uniqueAdvocaat = Advocaat()
  member this.BeChosenInRussia = false
  static member Instance = uniqueAdvocaat

type Hiddink private() = 
  inherit Trainer (30000000, "Hiddink")
  static let uniqueHiddink = Hiddink()
  member this.battleCry(that : FootballPlayer) = if that.Skill + 10 <= 100 then that.Skill + 10 else 100  
  static member Instance = uniqueHiddink

type YoungFootballers (pay : float, skill : int, age : int) = 
    inherit FootballPlayer (pay, skill, age)
    member this.StudyAtSchool = not (age >= 18)

let numberSt = 8
let mutable StadiumArray = [|for i in 1..numberSt -> new Stadium(i * 10, i * 20, false, false, ("Stadium number " + i.ToString()))|]

let Ronaldo = new FootballPlayer (3000000.0, 93, 38)
let Zhirkov = new FootballPlayer (1000000.0, 87, 35)
let Belyaev = new YoungFootballers (30000.0, 56, 16)
let league1 = new League("YoungLeague", 0.4)

let chosenStadium = (new Random()).Next(1, numberSt)

let Draw(array : Stadium array) =

  for i in 1..numberSt do
    if chosenStadium = i then StadiumArray.[i].ChosenForPremierLeague <- true

printfn "%A" ((Hiddink.Instance).battleCry (Ronaldo))
printfn "%A" (StamfordBridge.Instance).Price
printfn "%A" (Advocaat.Instance).Name
(Advocaat.Instance).SpendMoney(FritzWalterStadion.Instance) |> ignore
if Belyaev.StudyAtSchool then printfn "Беляев учится в школе" else printfn "Беляев не учится в школе"
Draw(StadiumArray)
printfn (if StadiumArray.[2].ChosenForPremierLeague then "true" else "false")
printfn "%A" <| (Array.Find(StadiumArray, fun x -> x.ChosenForPremierLeague = true)).Name + " is a winner!"
Belyaev.ShowSalary
Belyaev.WhatLeague(league1)
Belyaev.ShowSalary