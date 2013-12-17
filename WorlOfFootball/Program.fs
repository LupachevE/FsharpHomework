open System

type Football() = inherit obj ()

type Stadium (price : int, count : int, location : string, hd : bool, cover : bool) =
  class 
    inherit Football()
    let price = price
    member this.Price = price
    member this.Number = count
    member this.Location = location
    member this.HotDogs = hd
    member this.IsCovered = cover
  end

type Trainer (cash : int, name : string) =
  class
    inherit Football()
    let mutable cash = cash
    member this.SpendMoney(that : Stadium) = 
        cash <- cash - that.Price
        printfn "Curretn balance : %A"
    member this.ShowBalance = printfn "%A" cash
    member this.Name = name
  end

type FootballPlayers (pay : int, skill : int) = 
  class
    inherit Football()
    let pay = pay
    let mutable skill = skill
    member this.InNation = false
    member this.Skill = skill 

  end

type StamfordBridge private() =
  inherit Stadium (2000000, 50000000, "London", false, true)
  static let uniqueBridge = StamfordBridge()
  static member Instance = uniqueBridge

type FritzWalterStadion private() =
  inherit Stadium (500000, 2500000, "Kaiserslautern", true, false)
  static let uniqueFritz = FritzWalterStadion()
  static member Instance = uniqueFritz

type Advocaat private() =
  inherit Trainer (20000000, "Advocaat")
  static let uniqueAdvocaat = Advocaat()
  member this.BeChosen = false
  static member Instance = uniqueAdvocaat

type Hiddink private() = 
  inherit Trainer (30000000, "Hiddink")
  static let uniqueHiddink = Hiddink()
  member this.IncreseSkill(that : FootballPlayers) = if that.Skill + 10 <= 100 then that.Skill + 10 else 100  
  static member Instance = uniqueHiddink

type YoungLeague (pay : int, skill : int) = 
  class
    inherit FootballPlayers (pay, skill)
    member this.InPremier = false
  end

let Ronaldo = new FootballPlayers (3000000, 93)
let Zhirkov = new FootballPlayers (1000000, 87)
let Belyaev = new YoungLeague (30000, 56)

printfn "%A" ((Hiddink.Instance).IncreseSkill (Ronaldo))
printfn "%A" (StamfordBridge.Instance).Price
printfn "%A" (Advocaat.Instance).Name
(Advocaat.Instance).SpendMoney(FritzWalterStadion.Instance)