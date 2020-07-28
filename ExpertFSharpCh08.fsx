//  ExpertFSharpCh08.fsx

open System 
open System.IO  


let buf = System.Text.StringBuilder() ;; 
buf.Append("Humpty Dumpty");; 
buf.Append(" sat on the wall");; 
buf.ToString();; 

"MAGIC"B;; 
let dir = @"c:\Program Files";; 
let text = """I "like" you""";; 

sprintf "Name: %s, Age %d" "Anna" 3 ;;
// sprintf "Name: %s, Age %d" 10 3 ;;
sprintf "It is now O %O" System.DateTime.Now;; 
sprintf "It is now A %A" System.DateTime.Now;; 

printf "The result is O %O\n" [1;2;3] ;;
printf "The result is A %A\n" [1;2;3] ;;

DateTime.Parse("23 July 1968");; 
let date x = DateTime.Parse(x);; 
printfn "date = %A" (date "23 July 1968");; 
printfn "birth = %A" (date "18 March 2003, 6:21:01pm");; 

Uri.TryCreate("http://www.thebritishmuseum.ac.uk/",UriKind.Absolute);; 
Uri.TryCreate("a2p://www.xyzRubbish.nt/",UriKind.Absolute);; 
// Returns true, not false ?!?!? 

let line = "Smith, John, 20 January 1992, Software Developer"
line.Split ',';; 
line.Split ',' |> Array.map (fun s -> s.Trim());; 

let splitLine (line : string) = line.Split ',' |> Array.map (fun s -> s.Trim());; 
let parseEmployee (line : string) = 
    match splitLine line with 
    | [|last; first; startDate; title |] -> 
        last, first, System.DateTime.Parse(startDate), title
    | _ -> 
        failwithf "invalid employee format: '%s'" line 
parseEmployee line;; 

File.WriteAllLines("employee.txt", Array.create 10000 line) 
let readEmployees (fileName : string) = 
    fileName |> File.ReadLines |> Seq.map parseEmployee 
let firstThree = readEmployees "employee.txt" |> Seq.truncate 3 |> Seq.toList
