(*Created by Lupachev Eugene
  271 groop
  SPBSU M-M*)

open System

type Stadium (price : int, count : int, hd : bool, cover : bool, name : string) =
    
    let mutable chosen = false

    member this.Name = name
    member this.Price = price
    member this.Number = count
    member this.HotDogs = hd
    member this.IsCovered = cover
    member this.ChosenForPremierLeague
      with get() = chosen
      and set vaule = chosen <- true

type Trainer (cash : int, name : string) =
    let mutable cash = cash
    member this.SpendMoney(that : Stadium) = 
        cash <- cash - that.Price
        printfn "Curretn balance : %A"
    member this.ShowBalance = printfn "%A" cash
    member this.Name = name

type FootballPlayer (pay : int, skill : int, age : int) = 
    let pay = pay
    let mutable skill = skill
    member this.InNation = false
    member this.Skill = skill 
    member this.Age = age

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

type YoungLeague (pay : int, skill : int, age : int) = 
    inherit FootballPlayer (pay, skill, age)
    member this.StudyAtSchool = not (age >= 18)

//Создание массива стадионов для жеребьёвки

let numberSt = 8
let mutable StadiumArray = [|for i in 1..numberSt -> new Stadium(i * 10, i * 20, false, false, ("Stadium number " + i.ToString()))|]

//Некоторое взаимодействие классов

let Ronaldo = new FootballPlayer (3000000, 93, 38)
let Zhirkov = new FootballPlayer (1000000, 87, 35)
let Belyaev = new YoungLeague (30000, 56, 16)

//Описание жеребьёвки

let Draw(array : Stadium array) = 

    let chosenStadium = (new Random()).Next(1, numberSt)

    for i in 1..numberSt do 
        if chosenStadium = i then StadiumArray.[i].ChosenForPremierLeague <- true
  (*match rand.Next(8) with
  | 0 -> StamfordBridge.Instance.ChosenForChampionLeague <- true
  | 1 -> FritzWalterStadion.Instance.ChosenForChampionLeague <- true
  | _ -> failwith "No more stadions"*)

//Некоторые взаимодействия классов, включая проведение жеребьёвки

Draw(StadiumArray)
(Hiddink.Instance).battleCry (Ronaldo) |> ignore
(Advocaat.Instance).SpendMoney(FritzWalterStadion.Instance) |> ignore

//Вывод результатов

printfn "%A" (StamfordBridge.Instance).Price
printfn "%A" (Advocaat.Instance).Name
printfn "Belyaev.StudyAtSchool is %A" Belyaev.StudyAtSchool
printfn "%A" <| (Array.Find(StadiumArray, fun x -> x.ChosenForPremierLeague = true)).Name + " is a winner!"