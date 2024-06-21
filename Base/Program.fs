open BenchmarkDotNet.Running
open Collections


[<EntryPoint>]
let main argv =
    let switcher = BenchmarkSwitcher [| typeof<Dictionary>; typeof<List> |]
    switcher.Run argv |> ignore
    0
