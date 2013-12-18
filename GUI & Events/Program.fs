open System
open System.Windows.Forms
open System.Drawing

type Cars(price : int, model : string, earn : int) =
  
  let price = price
  let model = model
//Функция draw пока с заглушками, на их месте должна быть отрисовка каждой модели по отдельности
  (*let draw model =
    match model with
    | "Audi"       -> printfn "1"//drawAudi()
    | "Toyota"     -> printfn "1"//drawToyota()
    | "Honda"      -> printfn "1"//drawHonda()
    | "Volkswagen" -> printfn "1"//drawVolkswagen()
    | "Ford"       -> printfn "1"//drawFord()
    | "Mitsubishi" -> printfn "1"//drawMitsubishi()
    | "Mersedes"   -> printfn "1"//drawMersedes()
    | _            -> failwith "Wrong car model"*)

  member this.Price = price
  member this.Earn = earn
  member this.Model = model
  member this.IsEnough(balance : int) = balance >= this.Price
  //member this.Draw(model) = draw(this.Model)
  

let OnFeet = new Cars(0, "Foot", 200)
let Audi = new Cars(1000, "Audi", 50)
let Toyota = new Cars(5000, "Toyota", 100)
let Honda = new Cars(15000, "Honda", 300)
let Volkswagen = new Cars(30000, "VolksWagen", 1000)
let Ford = new Cars(100000, "Ford", 2500)
let Mitsubishi = new Cars(300000, "Mitsubishi", 10000)
let Mersedes = new Cars(1000000, "Mersedes", 50000)

let mutable balance = 0
let mutable (CurrentCar : Cars) = OnFeet  

//let changeBalance(this : Cars) = balance <- balance + this.Earn

let CarForm = 
    
  let form = new Form(Text = "Choosing the car")

  let textBox = new TextBox(Text = "Congratulations! You win!", Top = 100, Left = 150)

  let buttonAudi = new Button(Text = "Buy Audi", Left = 10,
                              Top = 10, Width = 100, Enabled = true)
  let buttonToyota = new Button(Text = "Buy Toyota", Left = 10,
                                Top = 40, Width = 100, Enabled = true)
  let buttonHonda = new Button(Text = "Buy Honda", Left = 10,
                               Top = 70, Width = 100, Enabled = true)
  let buttonVolkswagen = new Button(Text = "Buy Volkswagen", Left = 10,
                                    Top = 100, Width = 100, Enabled = true)
  let buttonFord = new Button(Text = "Buy Ford", Left = 10,
                              Top = 130, Width = 100, Enabled = true)
  let buttonMitsubishi = new Button(Text = "Bu Mitsubishi", Left = 10,
                                    Top = 160, Width = 100, Enabled = true)
  let buttonMersedes = new Button(Text = "Buy Mersedes", Left = 10,
                                  Top = 190, Width = 100, Enabled = true)

  let buttonPress1 _ _ = CurrentCar <- Audi
                         balance <- balance - CurrentCar.Price
  let buttonPress2 _ _ = CurrentCar <- Toyota
                         balance <- balance - CurrentCar.Price
  let buttonPress7 _ _ = CurrentCar <- Honda
                         balance <- balance - CurrentCar.Price
  let buttonPress3 _ _ = CurrentCar <- Volkswagen
                         balance <- balance - CurrentCar.Price
  let buttonPress4 _ _ = CurrentCar <- Ford
                         balance <- balance - CurrentCar.Price
  let buttonPress5 _ _ = CurrentCar <- Mitsubishi
                         balance <- balance - CurrentCar.Price
  let buttonPress6 _ _ = CurrentCar <- Mersedes
                         balance <- balance - CurrentCar.Price
                         MessageBox.Show(textBox.Text) |> ignore

  let eventHandler1 = new EventHandler(buttonPress1)
  let eventHandler2 = new EventHandler(buttonPress2)
  let eventHandler3 = new EventHandler(buttonPress3)
  let eventHandler4 = new EventHandler(buttonPress4)
  let eventHandler5 = new EventHandler(buttonPress5)
  let eventHandler6 = new EventHandler(buttonPress6)
  let eventHandler7 = new EventHandler(buttonPress7)
  //let mainHandler = new EventHandler(form.FormClosing(mainHandler))
  form.FormClosing |> Event.add (fun e -> form.Hide(); e.Cancel <- true)
  
  buttonAudi.Click.AddHandler(eventHandler1)
  buttonToyota.Click.AddHandler(eventHandler2) 
  buttonHonda.Click.AddHandler(eventHandler3)
  buttonVolkswagen.Click.AddHandler(eventHandler4)
  buttonFord.Click.AddHandler(eventHandler5)
  buttonMitsubishi.Click.AddHandler(eventHandler6)
  buttonMersedes.Click.AddHandler(eventHandler7)

  let dc c = (c :> Control)

  form.Controls.AddRange([| dc buttonAudi; dc buttonToyota; dc buttonHonda; dc buttonVolkswagen; dc buttonFord; dc buttonMitsubishi; dc buttonMersedes|])

  form

let LuckyForm = 
    
  let rand = new Random()
  let form = new Form(Text = "Lucky or Not")
  let button = new Button(Text = "Press it!", Left = 40,
                            Top = 20, Width = 80, Enabled = true)

  let buttonPress1 _ _ = 
        match rand.Next(5) with
        | 0 -> CurrentCar <- Audi
        | 1 -> CurrentCar <- Toyota
        | 2 -> CurrentCar <- Honda
        | 3 -> CurrentCar <- Volkswagen
        | 4 -> CurrentCar <- Ford
        | 5 -> CurrentCar <- OnFeet
        | _ -> failwith "WrongCar"
        MessageBox.Show(CurrentCar.Model) |> ignore

  form.FormClosing |> Event.add (fun e -> form.Hide(); e.Cancel <- true)

  let eventHandler1 = new EventHandler(buttonPress1)

  button.Click.AddHandler(eventHandler1)

  let dc c = (c :> Control)

  form.Controls.AddRange([| dc button |])

  form

let mainForm = 

    //if not (this.IsEnough(balance)) then button.Enabled <- not button.Enabled

    let form = new Form(Text = "Cars&Money")

    let textBox = new TextBox(Text = balance.ToString(),
                            Top = 100, Left = 150)
    let textBox2 = new TextBox(Text = CurrentCar.Model,
                            Top = 100, Left = 150)

    let firstbutton = new Button(Text = "ChooseYourCar", Left = 10,
                                 Top = 10, Width = 100, Enabled = true)
    let secondbutton = new Button(Text = "TestYourLuck", Left = 10,
                                  Top = 100, Width = 100, Enabled = true)
    let thirdbutton = new Button(Text = "Earn your money", Left = 150,
                                 Top = 10, Width = 100, Enabled = true)
    let oneMoreButton = new Button(Text = "Your car", Left = 150,
                                 Top = 100, Width = 100, Enabled = true)                                                                                         
    let buttonPress1 _ _ = CarForm.Show()
    let buttonPress2 _ _ = LuckyForm.Show()
    let buttonPress3 _ _ = balance <- balance + CurrentCar.Earn
                           textBox.Text <- balance.ToString()
                           MessageBox.Show(textBox.Text) |> ignore
    let buttonPress4 _ _ = textBox2.Text <- CurrentCar.Model
                           MessageBox.Show(textBox2.Text) |> ignore


    let eventHandler1 = new EventHandler(buttonPress1)
    let eventHandler2 = new EventHandler(buttonPress2)
    let eventHandler3 = new EventHandler(buttonPress3)
    let eventHandler4 = new EventHandler(buttonPress4)

    firstbutton.Click.AddHandler(eventHandler1)
    secondbutton.Click.AddHandler(eventHandler2) 
    thirdbutton.Click.AddHandler(eventHandler3)
    oneMoreButton.Click.AddHandler(eventHandler4)

    let dc c = (c :> Control)

    form.Controls.AddRange([| dc firstbutton; dc secondbutton; dc thirdbutton; dc oneMoreButton|])

    form

do Application.Run(mainForm)