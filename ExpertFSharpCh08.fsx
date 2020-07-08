//  ExpertFSharpCh08.fsx

open System 
open System.IO  
open System.Text.RegularExpressions
// open FSharp.Text.RegexProvider 

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

let parseHttpRequest line = 
    let result = Regex.Match(line, @"Get (.?) HTTP/1\.([01])$") 
    let file = result.Groups.[1].Value 
    let version = result.Groups.[2].Value 
    file, version 
let regex s = Regex(s) 
let (=~) s (re:Regex) = re.IsMatch(s) 
let (<>~) s (re:Regex) = not (s =~ re)
let samplestring = "This is a string" ;;
if samplestring =~ regex "his" then printfn "A Match! " ;; 
"This is a string" =~ regex "(is )+" ;;
regex(" ").Split("This is a string");;
regex(@"\s+").Split("I'm a little     teapot");;
regex(@"\s+").Split("I'm a little \t\t\n\t\n\t teapot");;
let m = regex("joe").Match("maryjoewashere");;
if m.Success then printfn "Matched at position %d" m.Index ;; 
let text2 = "was a dark and stormy night" ;;
let t2 = regex(@"\w+").Replace(text2, "WORD");;

let entry = @"
Jolly Jethro
13 Kings Parade 
Cambridge, Cambs CB2 1TJ
"
let re = 
    regex @"(?<=\n)\s*(?<city>[^\n]+)\s*,\s*(?<county>\w+)\s+(?<pcode>.{3}\s*.{3}).*$" 
let r = re.Match(entry);;
r.Groups.["city"].Value ;;
r.Groups.["county"].Value ;;
r.Groups.["pcode"].Value ;;

// creating an active pattern function  
let (|IsMatch|_|) (re:string) (inp:string) = 
    if Regex(re).IsMatch(inp) then Some() else None 

match "This is a string" with 
| IsMatch "(?i)HIS" -> "yes, it matched"
| IsMatch "ABC" -> "this would not match"
| _ -> "nothng matched"

let firstAndSecondWord (inp:string) =
    let re = regex "(?<word1>\w+)\s+(?<word2>\w+)" 
    let results = re.Match(inp)
    if results.Success then 
        Some (results.Groups.["word1"].Value, results.Groups.["word2"].Value) 
    else 
        None 
firstAndSecondWord "This is a super string" ;;

let (?) (results:Match) (name:string) = 
    results.Groups.[name].Value 

let firstAndSecondWord2 (inp:string) =
    let re = regex "(?<word1>\w+)\s+(?<word2>\w+)" 
    let results = re.Match(inp)
    if results.Success then 
        Some (results ? word1, results ? word2) 
    else 
        None 
firstAndSecondWord2 "This is a super string" ;; 


