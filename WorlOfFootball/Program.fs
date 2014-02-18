(*Created by Lupachev Eugene
  271 groop
  SPBSU M-M*)

open System

type stadium (price : int, count : int, hd : bool, cover : bool, name : string) =
    
    let mutable chosen = false

    member this.ShowName = name
    member this.Coasts = if price < 0 then failwith "Price out of range" else price
    member this.HaveNumber = count
    member this.HaveHotDogs = hd
    member this.IsCovered = cover
    member this.ChosenForPremierLeague
      with get() = chosen
      and set vaule = chosen <- true

type trainer (cash : int, name : string) =
    let mutable cash = cash
    member this.SpendMoney(that : stadium) = 
        if cash - that.Coasts < 0 then printf "Not enough money" else cash <- cash - that.Coasts
        printfn "Curretn balance : %A"
    member this.ShowBalance = printfn "%A" cash
    member this.ShowName = name

type league (name : string, coef : float) =
    member this.Name = name
    member this.Coef = coef

type footballPlayer (pay : float, skill : int, age : int) = 
    let mutable skill = skill
    let mutable currleague = new league("", 0.0)
    member this.WhatLeaguePlays(league : league) = currleague <- league 
    member this.chosenInNation = false
    member this.haveSkill = if skill < 0 || skill > 100 then failwith "Skill out of range" else skill 
    member this.Coasts = pay * currleague.Coef
    member this.ShowSalary = printfn "Current salary : %A" this.Coasts
    member this.haveAge = if age < 0 then failwith "Age out of range" else age

type stamfordBridge private() =
  inherit stadium (2000000, 50000000, false, true, "StamfordBridge")
  static let uniqueBridge = stamfordBridge()
  member this.Action = "Get 2 free tickets, if you wear special costumes"
  static member Instance = uniqueBridge

type fritzWalterStadion private() =
  inherit stadium (500000, 2500000, true, false, "FritzWalterStadion")
  static let uniqueFritz = fritzWalterStadion()
  static member Instance = uniqueFritz

type advocaat private() =
  inherit trainer (20000000, "Advocaat")
  static let uniqueAdvocaat = advocaat()
  member this.BeChosenInRussia = false
  static member Instance = uniqueAdvocaat

type hiddink private() = 
  inherit trainer (30000000, "Hiddink")
  static let uniqueHiddink = hiddink()
  member this.battleCry(that : footballPlayer) = if that.haveSkill + 10 <= 100 then that.haveSkill + 10 else 100  
  static member Instance = uniqueHiddink

type youngFootballers (pay : float, skill : int, age : int) = 
    inherit footballPlayer (pay, skill, age)
    member this.StudyAtSchool = not (age >= 18)

let numberSt = 8
let mutable StadiumArray = [|for i in 1..numberSt -> new stadium(i * 10, i * 20, false, false, ("Stadium number " + i.ToString()))|]

let Ronaldo = new footballPlayer (3000000.0, 93, 38)
let Zhirkov = new footballPlayer (1000000.0, 87, 35)
let Belyaev = new youngFootballers (30000.0, 56, 16)
let league1 = new league("YoungLeague", 0.4)

let chosenStadium = (new Random()).Next(1, numberSt)

let Draw(array : stadium array) =

  for i in 1..numberSt do
    if chosenStadium = i then StadiumArray.[i].ChosenForPremierLeague <- true

printfn "%A" ((hiddink.Instance).battleCry (Ronaldo))
printfn "%A" (stamfordBridge.Instance).Coasts
printfn "%A" (advocaat.Instance).ShowName
(advocaat.Instance).SpendMoney(fritzWalterStadion.Instance) |> ignore
if Belyaev.StudyAtSchool then printfn "Беляев учится в школе" else printfn "Беляев не учится в школе"
Draw(StadiumArray)
printfn (if StadiumArray.[2].ChosenForPremierLeague then "true" else "false")
printfn "%A" <| (Array.Find(StadiumArray, fun x -> x.ChosenForPremierLeague = true)).ShowName + " is a winner!"
Belyaev.ShowSalary
Belyaev.WhatLeaguePlays(league1)
Belyaev.ShowSalary