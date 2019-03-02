/*
Programming Language: C# 
Codebase to read text file with dates data
*/

using System;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
  public class readDatesFromFile{
    public string[] readDates(string folderName="dates"){  
      
      DateTime parsedDate;
      List<string> dateList = new List<string>();
      DirectoryInfo datesDir = new DirectoryInfo(folderName);
      FileInfo [] txtFiles = datesDir.GetFiles();

      foreach (FileInfo filename in txtFiles){
        using (FileStream fs = File.Open(folderName+"/"+filename.Name, FileMode.Open, FileAccess.Read))
        using (BufferedStream bs = new BufferedStream(fs))
        using (StreamReader sr = new StreamReader(bs)){
          string line;
          while ((line = sr.ReadLine()) != null){
            try{
              if(!DateTime.TryParseExact(line, "MM/dd/yy", CultureInfo.InvariantCulture,DateTimeStyles.None, out parsedDate)){
                    parsedDate=DateTime.Parse(line);
              }
              dateList.Add(string.Format("{0:yyyy-MM-dd}", parsedDate));
            }catch (FormatException){
              Console.WriteLine("Invalid date/format ("+line+") "+" in "+filename.Name); 
            }
          }
        }
      }
      string[] arr = dateList.ToArray();
      return arr;
    }
  }


  
