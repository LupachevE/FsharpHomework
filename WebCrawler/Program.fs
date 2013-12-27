open System
open System.Windows.Forms
open System.Net
open System.Collections.Concurrent
open System.Text.RegularExpressions

type WebCrawler() =

    let link = "(?:href|src)=\"((?:https?://)?[a-zA-Z0-9_./-]+)\""
    let picEndsWith = "\.(?:jpeg|jpg|gif|png)$"
    let host = "^https?://[a-zA-Z0-9_.-]+/"


    let picturesLoaded = new ConcurrentDictionary<string, unit>()
    let attendedLinks = new ConcurrentDictionary<string, unit>()

    let filterLinks link = Array.filter (fun (s : string) -> s.StartsWith link && 
                                                             not ((new Regex(picEndsWith)).IsMatch(s)) &&
                                                             not (attendedLinks.ContainsKey(s))
                                        )

    let filterPicLinks = Array.filter (fun (s : string) -> (new Regex(picEndsWith)).IsMatch(s) && 
                                                           not (picturesLoaded.ContainsKey(s))
                                      )

    member this.getHtml link =
      async { 
        try
            let uri       = new System.Uri(link)
            let! html     = (new WebClient()).AsyncDownloadString(uri)
            return html
            with _ -> return ""
            }

    member this.downLoad link = 
      async {
            try picturesLoaded.GetOrAdd(link, ())
                let uri = new Uri(link)
                //let fileName = link.GetHashCode().ToString() + (new Regex(picEndsWith)).Match(link).Value
                (new WebClient()).DownloadFileAsync(uri, "C:\Pictures")
            with
            |error -> error.Message |> printf "%s"
            }
            
    member this.getLinks host html =
      async {
        try let matches = (new Regex(link)).Matches(html)
            let links = [|for i in matches -> i.Groups.[1].Value|]
            return Array.map (fun (s : string) -> if (s.StartsWith("http")) then s 
                                                  elif (s.StartsWith("/")) then host + s.Remove(0, 1)
                                                  else host + s
                             ) links
            with _ -> return [||]
            }

    member this.Start link = 
      let host = (new Regex(host)).Match(link).Value
      let rec crawler link =
        async {
              try attendedLinks.GetOrAdd(link, ())
                  let! html = this.getHtml link
                  let! linksArray = this.getLinks html host
                  let validLinksArray = filterLinks link linksArray
                  let validPicsArray = filterPicLinks linksArray

                  linksArray
                  |> Array.map this.downLoad
                  |> Async.Parallel
                  |> Async.RunSynchronously
                  |> ignore

                  linksArray
                  |> Array.map crawler
                  |> Async.Parallel
                  |> Async.RunSynchronously
                  |> ignore

              with _ -> ()
              }

      crawler link |> Async.RunSynchronously

let mainForm =
        
    let form = new Form(Text = "Press the button")

    let webcrawler = new WebCrawler()

    let button = new Button(Text = "Press the button" , Left = 27, Top = 10, Width = 100, Enabled = true)

    let link = "http://vk.com/mrjonik"

    let buttonPress _ _ = webcrawler.Start link

    let eventHandler = new EventHandler(buttonPress)

    button.Click.AddHandler(eventHandler)

    let dc c = (c :> Control)

    form.Controls.AddRange([|dc button|])

    form

do Application.Run(mainForm)