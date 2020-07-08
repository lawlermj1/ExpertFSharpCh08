//  ExpertFSharpCh08.fs 

open System 
open System.IO  
open RegexdotNet 
open RegexFSharp 

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

    let samplestring = "This is a string"  
    printfn "regextest = %A " (samplestring =~ "(is )+")  
    printfn "regex1 = %A " (regex(" ").Split("This is a string"))  
    printfn "regex1 = %A " (regex(@"\s+").Split("I'm a little     teapot"))  
    printfn "regex1 = %A " (regex(@"\s+").Split("I'm a little \t\t\n\t\n\t teapot")) 
    let m = regex("joe").Match("maryjoewashere") 
    if m.Success then printfn "Matched at position %d" m.Index   
    let text2 = "was a dark and stormy night"  
    let t2 = regex(@"\w+").Replace(text2, "WORD") 

    let entry = @"
Jolly Jethro
13 Kings Parade 
Cambridge, Cambs CB2 1TJ
"
    let r = AddrBlk.Match(entry) 
    printfn "city = %A " r.Groups.["city"].Value  
    printfn "county = %A " r.Groups.["county"].Value  
    printfn "pcode = %A " r.Groups.["pcode"].Value 

// uses active pattern in the match 
    printfn "pcode = %A " (
        match "This is a string" with 
        | IsMatch "(?i)HIS" -> "yes, it matched"
        | IsMatch "ABC" -> "this would not match"
        | _ -> "nothing matched") 

    printfn "firstAndSecondWord = %A " (firstAndSecondWord "This is a super string")  
    printfn "firstAndSecondWord2 = %A " (firstAndSecondWord2 "This is a super string") 

    let resP = PhoneRegex().Match("425-123-2345") 
//    printfn "AreaCode = %A" resP.AreaCode.Value 
    printfn "AreaCode = %A" resP.Groups.["AreaCode"].Value   

    printfn " "
    printfn "All finished from ExpertF#Ch08" 
    0 // return an integer exit code
