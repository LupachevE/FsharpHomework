open System
open System.Windows.Forms
open System.Drawing

type Cars(price : int, model : string, earn : int) =
  
  let price = price
  let model = model

  member this.Price = price
  member this.Earn = earn
  member this.Model = model
  member this.IsEnough(balance : int) = balance >= this.Price

let OnFeet = new Cars(0, "Foot", 100)
let Audi = new Cars(1000, "Audi", 500)
let Toyota = new Cars(5000, "Toyota", 1000)
let Honda = new Cars(15000, "Honda", 3000)
let Volkswagen = new Cars(30000, "VolksWagen", 10000)
let Ford = new Cars(100000, "Ford", 25000)
let Mitsubishi = new Cars(300000, "Mitsubishi", 100000)
let Mersedes = new Cars(1000000, "Mersedes", 500000)

let mutable balance = 0
let mutable (CurrentCar : Cars) = OnFeet  



let buttonAudi = new Button(Text = "Buy Audi", Left = 10,
                              Top = 10, Width = 100, Enabled = false)
let buttonToyota = new Button(Text = "Buy Toyota", Left = 10,
                                Top = 40, Width = 100, Enabled = false)
let buttonHonda = new Button(Text = "Buy Honda", Left = 10,
                               Top = 70, Width = 100, Enabled = false)
let buttonVolkswagen = new Button(Text = "Buy Volkswagen", Left = 10,
                                    Top = 100, Width = 100, Enabled = false)
let buttonFord = new Button(Text = "Buy Ford", Left = 10,
                              Top = 130, Width = 100, Enabled = false)
let buttonMitsubishi = new Button(Text = "Bu Mitsubishi", Left = 10,
                                    Top = 160, Width = 100, Enabled = false)
let buttonMersedes = new Button(Text = "Buy Mersedes", Left = 10,
                                  Top = 190, Width = 100, Enabled = false)

let FactoryMethod(model : Cars) =
  CurrentCar <- model
  balance <- balance - model.Price
  if balance < Audi.Price && not (CurrentCar <> Audi) then buttonAudi.Enabled <- false
  if balance < Toyota.Price && not (CurrentCar <> Toyota) then buttonToyota.Enabled <- false
  if balance < Honda.Price && not (CurrentCar <> Honda) then buttonHonda.Enabled <- false
  if balance < Volkswagen.Price && not (CurrentCar <> Volkswagen) then buttonVolkswagen.Enabled <- false
  if balance < Ford.Price && not (CurrentCar <> Ford) then buttonFord.Enabled <- false
  if balance < Mitsubishi.Price && not (CurrentCar <> Mitsubishi) then buttonMitsubishi.Enabled <- false
  if balance < Mersedes.Price && not (CurrentCar <> Mersedes) then buttonMersedes.Enabled <- false

//Форма для покупки машины. Нажимаем на кнопку - покупаем машину, всё просто.

let CarForm = 
    
  let form = new Form(Text = "Choosing the car")

  let textBox = new TextBox(Text = "Congratulations! You win!", Top = 100, Left = 150)

  let buttonPressAudi _ _       = FactoryMethod (Audi)
  let buttonPressToyota _ _     = FactoryMethod (Toyota)
  let buttonPressHonda _ _      = FactoryMethod (Honda)
  let buttonPressVolkswagen _ _ = FactoryMethod (Volkswagen)
  let buttonPressFord _ _       = FactoryMethod (Ford)
  let buttonPressMitsubishi _ _ = FactoryMethod (Mitsubishi)
  let buttonPressMersedes _ _   = FactoryMethod (Mersedes)
                                  MessageBox.Show(textBox.Text) |> ignore
                                  failwith "The game ends"

  let eventHandlerAudi       = new EventHandler(buttonPressAudi)
  let eventHandlerToyota     = new EventHandler(buttonPressToyota)
  let eventHandlerHonda      = new EventHandler(buttonPressHonda)
  let eventHandlerVolkswagen = new EventHandler(buttonPressVolkswagen)
  let eventHandlerFord       = new EventHandler(buttonPressFord)
  let eventHandlerMitsubishi = new EventHandler(buttonPressMitsubishi)
  let eventHandlerMersedes   = new EventHandler(buttonPressMersedes)

  form.FormClosing |> Event.add (fun e -> form.Hide(); e.Cancel <- true)
  
  buttonAudi.Click.AddHandler(eventHandlerAudi)
  buttonToyota.Click.AddHandler(eventHandlerToyota) 
  buttonHonda.Click.AddHandler(eventHandlerHonda)
  buttonVolkswagen.Click.AddHandler(eventHandlerVolkswagen)
  buttonFord.Click.AddHandler(eventHandlerFord)
  buttonMitsubishi.Click.AddHandler(eventHandlerMitsubishi)
  buttonMersedes.Click.AddHandler(eventHandlerMersedes)

  let dc c = (c :> Control)

  form.Controls.AddRange([| dc buttonAudi; dc buttonToyota; dc buttonHonda; dc buttonVolkswagen; dc buttonFord; dc buttonMitsubishi; dc buttonMersedes|])

  form

//Форма-лохотрон(на везение). Нажимаем на кнопку, получаем машину от Audi до Ford, или же теряем текущую.

let LuckyForm = 
    
  let rand = new Random()
  let form = new Form(Text = "Lucky or Not")
  let button = new Button(Text = "Press it!", Left = 40,
                            Top = 20, Width = 80, Enabled = true)

  let MainbuttonPress _ _ = 
        match rand.Next(8) with
        | 0 -> CurrentCar <- Audi
        | 1 -> CurrentCar <- Toyota
        | 2 -> CurrentCar <- Honda
        | 3 -> CurrentCar <- Volkswagen
        | 4 -> CurrentCar <- Ford
        | 5 -> CurrentCar <- OnFeet
        | 6 -> CurrentCar <- OnFeet
        | 7 -> CurrentCar <- OnFeet
        | 8 -> CurrentCar <- OnFeet
        | _ -> failwith "WrongCar"
        MessageBox.Show(CurrentCar.Model) |> ignore

  form.FormClosing |> Event.add (fun e -> form.Hide(); e.Cancel <- true)

  let MaineventHandler = new EventHandler(MainbuttonPress)

  button.Click.AddHandler(MaineventHandler)

  let dc c = (c :> Control)

  form.Controls.AddRange([| dc button |])

  form

//Основная форма. 4 кнопки: Вызвать форму "Buy car", вызвать форму "Lucky or not", посмотреть текущую машину и получить деньги.
//Деньги получаем в зависимости от модели машины, Побеждаем, если покупаем Mersedes.

let mainForm = 

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
    let ChooseCarbuttonPress _ _ = CarForm.Show()
    let LuckyFormbuttonPress _ _ = LuckyForm.Show()
    let GetMoneybuttonPress _ _  = balance <- balance + CurrentCar.Earn
                                   textBox.Text <- balance.ToString()
                                   MessageBox.Show(textBox.Text) |> ignore
                                   if balance >= Audi.Price && CurrentCar <> Audi then buttonAudi.Enabled <- true
                                   if balance >= Toyota.Price && CurrentCar <> Toyota then buttonToyota.Enabled <- true
                                   if balance >= Honda.Price && CurrentCar <> Honda then buttonHonda.Enabled <- true
                                   if balance >= Volkswagen.Price && CurrentCar <> Volkswagen then buttonVolkswagen.Enabled <- true
                                   if balance >= Ford.Price && CurrentCar <> Ford then buttonFord.Enabled <- true
                                   if balance >= Mitsubishi.Price && CurrentCar <> Mitsubishi then buttonMitsubishi.Enabled <- true
                                   if balance >= Mersedes.Price && CurrentCar <> Mersedes then buttonMersedes.Enabled <- true
    let ShowCarbuttonPress _ _   = textBox2.Text <- CurrentCar.Model
                                   MessageBox.Show(textBox2.Text) |> ignore


    let ChooseCareventHandler = new EventHandler(ChooseCarbuttonPress)
    let LuckyFormeventHandler = new EventHandler(LuckyFormbuttonPress)
    let GetMoneyeventHandler  = new EventHandler(GetMoneybuttonPress)
    let ShowCareventHandler   = new EventHandler(ShowCarbuttonPress)

    firstbutton.Click.AddHandler(ChooseCareventHandler)
    secondbutton.Click.AddHandler(LuckyFormeventHandler) 
    thirdbutton.Click.AddHandler(GetMoneyeventHandler)
    oneMoreButton.Click.AddHandler(ShowCareventHandler)

    let dc c = (c :> Control)

    form.Controls.AddRange([| dc firstbutton; dc secondbutton; dc thirdbutton; dc oneMoreButton|])

    form

do Application.Run(mainForm)