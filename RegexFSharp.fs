//  RegexFSharp.fsx

module RegexFSharp

open System 
open System.IO  
// open System.Text.RegularExpressions
open FSharp.Text.RegexProvider 

type PhoneRegex = Regex< @"(?<AreaCode>^\d{3})-(?<phoneNumber>\d{3}-\d{4}$)" >   
