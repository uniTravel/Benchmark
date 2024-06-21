module Collections

open System
open System.Collections.Generic
open BenchmarkDotNet.Attributes


[<SimpleJob(1, 3, 10)>]
[<MemoryDiagnoser>]
type Dictionary() =

    [<Params(10000, 1000000)>]
    member val len = 0 with get, set

    [<Benchmark>]
    member me.Zero() =
        let d = Dictionary<Guid, int64> me.len
        ()

    [<Benchmark>]
    member me.Little() =
        let d = Dictionary<Guid, int64> me.len
        [ 1..100 ] |> List.iter (fun i -> d.Add(Guid.NewGuid(), i))

    [<Benchmark>]
    member me.More() =
        let d = Dictionary<Guid, int64> me.len
        [ 1..1000 ] |> List.iter (fun i -> d.Add(Guid.NewGuid(), i))

    [<Benchmark>]
    member me.Scale() =
        let d = Dictionary<Guid, int64> me.len
        [ 1..1000 ] |> List.iter (fun i -> d.Add(Guid.NewGuid(), i))
        d |> Seq.take 500 |> Seq.iter (fun (KeyValue(k, v)) -> d.Remove(k) |> ignore)


[<SimpleJob(1, 3, 10)>]
[<MemoryDiagnoser>]
type List() =

    [<Params(10000, 1000000)>]
    member val len = 0 with get, set

    member val l = List.Empty with get, set

    member val s = Seq.empty with get, set

    [<IterationSetup>]
    member me.IterationSetup() =
        me.l <- [ 1 .. me.len ]
        me.s <- List.toSeq me.l

    [<Benchmark>]
    member me.List() =
        // [ 1 .. me.len ] |> List.iter (fun i -> me.l <- i :: me.l)
        let l,r = List.splitAt 100 me.l
        ()

    // [<Benchmark>]
    // member me.Seq() =
    //     [ 1 .. me.len ] |> List.iter (fun i -> me.s <- Seq.insertAt 0 i me.s)

    // member me.Append() =
    //     [ 1 .. me.len ] |> List.iter (fun i -> me.s <- seq { i } |> Seq.append me.s)
