//  ExpertFSharpCh08.fs 

open System 
open System.IO  

let date x = DateTime.Parse(x) 

let splitLine (line : string) = line.Split ',' |> Array.map (fun s -> s.Trim()) 

let parseEmployee (line : string) = 
    match splitLine line with 
    | [|last; first; startDate; title |] -> 
        last, first, System.DateTime.Parse(startDate), title
    | _ -> 
        failwithf "invalid employee format: '%s'" line 

let readEmployees (fileName : string) = 
    fileName |> File.ReadLines |> Seq.map parseEmployee 

[<EntryPoint>]
let main argv =

// buf is mutable! 
    let buf = System.Text.StringBuilder()   
    let buf = buf.Append("Humpty Dumpty")  
    let buf = buf.Append(" sat on the wall")  
    printfn "buf = %A" buf 

    printfn "ma = %A" "MAGIC"B  
    printfn "dir = %A" @"c:\Program Files"  
    printfn "text = %A" """I "like" you"""  
    printfn "Name: %s, Age %d" "Anna" 3  
// use %A rather than %O  
    printfn "It is now O %O" System.DateTime.Now  
    printfn "It is now A %A" System.DateTime.Now  

    printfn "The result is A %A\n" [1;2;3]  
    printfn "date = %A" (date "23 July 1968")  
    printfn "birth = %A" (date "18 March 2003, 6:21:01pm")  
    printfn "uri tuple = %A " (Uri.TryCreate("http://www.thebritishmuseum.ac.uk/", UriKind.Absolute))  
    printfn "uri tuple = %A " (Uri.TryCreate("a2p://www.xyzRubbish.nt/", UriKind.Absolute))  
// Returns true, not false ?!?!? 

    let line = "Smith, John, 20 January 1992, Software Developer"

    printfn "parseEmployee = %A " (parseEmployee line) 
    File.WriteAllLines("employee.txt", Array.create 10000 line) 
    let firstThree = readEmployees "employee.txt" |> Seq.truncate 3 |> Seq.toList
    
    printfn " "
    printfn "All finished from ExpertF#Ch08" 
    0 // return an integer exit code
