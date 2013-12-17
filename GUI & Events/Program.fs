open System
open System.Windows.Forms
open System.Drawing

type Cars(price : int, model : string, ern : int) =
  
  let price = price
  let model = model
//Функция draw пока с заглушками, на их месте должна быть отрисовка каждой модели по отдельности
  let draw model =
    match model with
    | "Audi"       -> printfn "1"
    | "Toyota"     -> printfn "1"
    | "Honda"      -> printfn "1"
    | "Volkswagen" -> printfn "1"
    | "Ford"       -> printfn "1"
    | "Mitsubishi" -> printfn "1"
    | "Mersedes"   -> printfn "1"
    | _            -> printfn "1" 
  member this.Price = price
  member this.Ern = ern
  member this.Model = model
  member this.IsEnough(balance : int) = balance >= price
  member this.Draw(model) = draw(this.Model)
  

let OnFeet = new Cars(0, "Foot", 10)
let Audi = new Cars(1000, "Audi", 50)
let Toyota = new Cars(5000, "Toyota", 100)
let Honda = new Cars(15000, "Honda", 300)
let Volkswagen = new Cars(30000, "VolksWagen", 1000)
let Ford = new Cars(100000, "Ford", 2500)
let Mitsubishi = new Cars(300000, "Mitsubishi", 10000)
let Mersedes = new Cars(1000000, "Mersedes", 50000)

let mutable balance = 0
let mutable (CurrentCar : Cars) = OnFeet 

let CarForm = 
    
  let form = new Form(Text = "Choosing the car")
  let buttonAudi = new Button(Text = "Buy Audi", Left = 10,
                              Top = 10, Width = 20, Enabled = (balance >= Audi.Price))
  let buttonToyota = new Button(Text = "Buy Toyota", Left = 10,
                                Top = 40, Width = 20, Enabled = (balance >= Toyota.Price))
  let buttonHonda = new Button(Text = "Buy Honda", Left = 10,
                               Top = 70, Width = 20, Enabled = (balance >= Honda.Price))
  let buttonVolkswagen = new Button(Text = "Buy Volkswagen", Left = 10,
                                    Top = 100, Width = 20, Enabled = (balance >= Volkswagen.Price))
  let buttonFord = new Button(Text = "Buy Ford", Left = 10,
                              Top = 130, Width = 80, Enabled = (balance >= Ford.Price))
  let buttonMitsubishi = new Button(Text = "Bu Mitsubishi", Left = 10,
                                    Top = 160, Width = 20, Enabled = (balance >= Mitsubishi.Price))
  let buttonMersedes = new Button(Text = "Buy Mersedes", Left = 10,
                                  Top = 190, Width = 20, Enabled = (balance >= Mersedes.Price))

  form

let LuckyForm = 
    
    let form = new Form(Text = "Lucky or Not")
    let button = new Button(Text = "Press it!", Left = 400,
                            Top = 600, Width = 80, Enabled = true)
    form

let mainForm = 

    //if not (this.IsEnough(balance)) then button.Enabled <- not button.Enabled

    let form = new Form(Text = "Cars&Money")

    let textBox = new TextBox(Text = balance.ToString(),
                            Top = 100, Left = 150)

    let firstbutton = new Button(Text = "ChooseYourCar", Left = 10,
                                 Top = 10, Width = 100, Enabled = true)
    let secondbutton = new Button(Text = "TestYourLuck", Left = 10,
                                  Top = 100, Width = 100, Enabled = true)
    let thirdbutton = new Button(Text = "Ern your money", Left = 150,
                                 Top = 10, Width = 100, Enabled = true)
                                                                                                
    let buttonPress1 _ _ = do Application.Run(CarForm)
    let buttonPress2 _ _ = do Application.Run(LuckyForm)
    let buttonPress3 _ _ = balance <- balance + CurrentCar.Ern
                           MessageBox.Show(textBox.Text) |> ignore

    let eventHandler1 = new EventHandler(buttonPress1)
    let eventHandler2 = new EventHandler(buttonPress2)
    let eventHandler3 = new EventHandler(buttonPress3)

    firstbutton.Click.AddHandler(eventHandler1)
    secondbutton.Click.AddHandler(eventHandler2) 
    thirdbutton.Click.AddHandler(eventHandler3)

    let dc c = (c :> Control)

    form.Controls.AddRange([| dc firstbutton; dc secondbutton; dc thirdbutton|])

    form

do Application.Run(mainForm)