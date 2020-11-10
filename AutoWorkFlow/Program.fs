open System



[<EntryPoint>]
let main argv =
    
    use stream_reader = new IO.StreamReader(IO.File.OpenRead "config.toml")
    let input = stream_reader.ReadToEnd()
    let config = 
        input.Trim().TrimStart('[').Split('[')
        |> Seq.map (
            fun item ->
                let input = item.Trim().Split(']')
                (
                    input.[0].Trim(), 
                    input.[1].Trim().Split("\r\n")
                    |> Seq.map (
                        fun item ->
                            let input = item.Trim().Split('=')
                            (input.[0].Trim(), input.[1].Trim())
                    )
                    |> Map.ofSeq
                )               
        )
        |> Map.ofSeq

    printfn "%A" config

    match argv.Length with
    | 1 ->
        match argv.[0] with
        | "--version" ->
            printfn "0.0.1"

        | "--help" ->
            printfn "Help"

        | _ ->
            eprintfn "Unknow Command %s." argv.[0]
    | 2 ->
        match argv.[0] with
        | "generate" ->
            printfn "%A" argv.[0]

            match argv.[1] with
            | "--java_web" ->
                use bandizip = new System.Diagnostics.Process()
                bandizip.StartInfo <- new System.Diagnostics.ProcessStartInfo("bz.exe", @"c D:\Code\FuckJava\target\zip\软工二班_顾天皓.zip D:\Code\FuckJava\JspLearning\")
                match bandizip.Start() with
                | true ->
                    bandizip.WaitForExit()

                    match bandizip.HasExited with
                    | true ->
                        printfn "true, %A, %A" bandizip.ExitCode bandizip.ExitTime
                    | false ->
                        printfn "false"
                | false ->
                    eprintfn "Start failed"
                

                
            | _ ->
                eprintfn "Unknow Command: %s." argv.[1]

        | _ ->
            eprintfn "Unknow Command: %s." argv.[0]

    | _ ->
        printfn "Help"
    0 
