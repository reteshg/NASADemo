/*
Programming Language: C# 
Codebase to read text file with dates data
Function readDates: Read each line of dates from the text files
*/

using System;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
  public class readDatesFromFile{
    public string[] readDates(string folderName="dates"){   //all text file from the dates folder will be read
      
      DateTime parsedDate; //manipulate dates fetched from the dates text files
      List<string> dateList = new List<string>(); //store all the valid dates in this list
      DirectoryInfo datesDir = new DirectoryInfo(folderName); //get the directory name of the dates txt file
      FileInfo [] txtFiles = datesDir.GetFiles(); //get names of all text in specified folder

      foreach (FileInfo filename in txtFiles){ //loop through all the files in dates directory
        using (FileStream fs = File.Open(folderName+"/"+filename.Name, FileMode.Open, FileAccess.Read)) //open file stream to get file meta data
        using (BufferedStream bs = new BufferedStream(fs)) //create buffer stream object
        using (StreamReader sr = new StreamReader(bs)){ // use strea reader to read text files
          string line; //store each line of text
          while ((line = sr.ReadLine()) != null){ //loop through all the lines in the text file
            try{
              // this if statement handels all the dates whichare not in format MM/dd/yyyy
              if (!DateTime.TryParseExact(line, "MM/dd/yy", CultureInfo.InvariantCulture,DateTimeStyles.None, out parsedDate)){
                parsedDate=DateTime.Parse(line);
              }
                dateList.Add(string.Format("{0:yyyy-MM-dd}", parsedDate));
            }catch (FormatException){
              Console.WriteLine("Invalid date/format ("+line+") "+" in "+filename.Name); 
            }
          }
        }
      }
      string[] arr = dateList.ToArray(); //cast list to array
      return arr;
    }
  }


  
