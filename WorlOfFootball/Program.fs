(*Created by Lupachev Eugene
  271 groop
  SPBSU M-M*)

open System

type Stadium (price : int, count : int, location : string, hd : bool, cover : bool) =
    
    let mutable chosen = false

    member this.Price = price
    member this.Number = count
    member this.Location = location
    member this.HotDogs = hd
    member this.IsCovered = cover
    member this.ChosenForChampionLeague
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
  inherit Stadium (2000000, 50000000, "London", false, true)

  let mutable chosen = false

  static let uniqueBridge = StamfordBridge()
  member this.Action = "Get 2 free tickets, if you wear special costumes"
  static member Instance = uniqueBridge

type FritzWalterStadion private() =
  inherit Stadium (500000, 2500000, "Kaiserslautern", true, false)
  static let uniqueFritz = FritzWalterStadion()
  member this.ChosenForPremierLeague = true
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

let Ronaldo = new FootballPlayer (3000000, 93, 38)
let Zhirkov = new FootballPlayer (1000000, 87, 35)
let Belyaev = new YoungLeague (30000, 56, 16)

let Draw() =
  let rand = new Random()
  match rand.Next(1) with
  | 0 -> StamfordBridge.ChosenForChampionLeague <- true
  | 1 -> FritzWalterStadion.ChosenForChampionLeague <- true
  | _ -> failwith "No more stadions"

printfn "%A" ((Hiddink.Instance).battleCry (Ronaldo))
printfn "%A" (StamfordBridge.Instance).Price
printfn "%A" (Advocaat.Instance).Name
(Advocaat.Instance).SpendMoney(FritzWalterStadion.Instance) |> ignore
printfn "%A" Belyaev.StudyAtSchool