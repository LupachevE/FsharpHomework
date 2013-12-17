open FsUnit
open NUnit.Framework
open System

type IWorkWether =
  abstract member IsShiny : unit -> bool
  abstract member Daylight : unit -> string
  abstract member Speed : unit -> int

type IMagic =
  abstract member CallStorck : unit
  abstract member CallDeamon : unit

type ILuminary =
  abstract member IsShiny : unit -> bool

type IDaylight =
  abstract member Daylight : unit -> string
  
type IWind =
  abstract member Speed : unit -> int

type Luminary() =
  let isShiny = (new Random()).Next(1)
  interface ILuminary with 
    member x.IsShiny() =
      match isShiny with
      | 0 -> false
      | 1 -> true
      | _ -> false

type Daylight() =
  let DayTime = (new Random()).Next(3)
  interface IDaylight with
    member x.Daylight() = 
      match DayTime with
        | 0 -> "Morning"
        | 1 -> "Noon"
        | 2 -> "Evening"
        | 3 -> "Night"
        | _ -> "Wrong daytime"

type Wind() =
  let speed = (new Random()).Next(11)
  interface IWind with
    member x.Speed() = speed

type WorkWetherFactory() =
  interface IWorkWether with

    member x.Daylight() = new Daylight()
    member x.IsShiny() = new Luminary()
    member x.Speed() = new Wind()

type TestWetherFactory(daylight : string, wind : int, luminary : bool) =
  interface IWorkWether with

    member x.Daylight() = daylight
    member x.IsShiny() = luminary
    member x.Speed() = wind


type Magic () = 
  interface IMagic with
    member x.CallStorck = printfn "We use Storck"
    member x.CallDeamon = printfn "We use Deamon"

type CreatureType = Puppy | Kitten | Hedgehog | Bearcub | Piglet | Bat | Balloon

type Cloud () =
  let daylight = factory.CreateDay
  let luminary = new Luminary ()
  let wind = new Wind ()
