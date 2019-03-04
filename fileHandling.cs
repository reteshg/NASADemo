/*
Programming Language: C# 
Codebase to read text file with dates data

Function readDates: Read each line of dates from the text files
Function checkFolders: Function deletes all the previously downloaded files in
Function renameImages: Renames all the downloaded images in each folder incrementaly

PS: Some code has been written only to manage data files (json) for Webpage functionality.  
*/

using System;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

  public class fileHandling{

    //funtion to read all text file inside the dates folder
    public string[] readDates(string folderName=Constants.datesFolderName){
      
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

            var dateString=string.Format("{0:yyyy-MM-dd}", parsedDate).ToString(); //parse dates to accepted format by NASA API
            dateList.Add(dateString); //add date to list
            Directory.CreateDirectory(Constants.imagesFolderName+"/"+dateString); //create directory named date to save images of the specified date
                
            }catch (FormatException){
              Console.WriteLine("Invalid date/format ("+line+") "+" in "+filename.Name); 
            }
          }
        }
      }
      


      string[] arr = dateList.ToArray(); //cast list to array
      string[] arrDistinct = arr.Distinct().ToArray();

      if (dateList.Count==0){
        Console.WriteLine("Error: No dates mentioned in the text file/s inside the 'dates' folder.");
      }else{

      //START CODE TO HANDLE WEBPAGE FUNCTIONALITY. This code writes json data with all the folder names containing images downloaded from NASA
      var jsonArr = arrDistinct.Select(x => new { dt = x}).ToArray(); //
      var json = JsonConvert.SerializeObject(jsonArr); //serialise array
      
      json="var j='"+json+"';"; 
      using (FileStream fs = File.Create(Constants.jsonFolderName+"/folder.js")){
        Byte[] info = new UTF8Encoding(true).GetBytes(json);
        fs.Write(info, 0, info.Length);
      }}
      //END CODE TO HANDLE WEBPAGE FUNCTIONALITY
      
      return arrDistinct;
    }

    //Function is manly handling data files and folders to show content on the webpage. 
    //Function deletes all the previous folders used to save images and json data
    //Function also creates folder required to run the code successfully
    public void checkFolders(){
      
      string path = Directory.GetCurrentDirectory(); //get current directory
      
      var images=path+"/"+Constants.imagesFolderName; //path to images folder
      var dates=path+"/"+Constants.datesFolderName;  //path to dates folder
      var json=path+"/"+Constants.jsonFolderName;  //path to json folder. This folder contain js file with list of image folders and number of images in each folder

      //Delete images folder with previous data
      DirectoryInfo di = new DirectoryInfo(images);
      if (Directory.Exists(images)){
        foreach (DirectoryInfo dir in di.GetDirectories()){
          dir.Delete(true); 
        }
      }

      //Delete js files with previous data
      DirectoryInfo dij = new DirectoryInfo(json);
      if (Directory.Exists(json)){  
      foreach (FileInfo filename in dij.GetFiles()){
        File.Delete(filename.ToString()); 
      }
      }
      
      
      
      //Create directories necessary for the code to run successfully
      if (!Directory.Exists(images)) {
        Directory.CreateDirectory(images);
      } 
      if (!Directory.Exists(dates)){
        Directory.CreateDirectory(dates);
      } 
      if (!Directory.Exists(json)){
        Directory.CreateDirectory(json);
      }
      
      string[] filePaths = Directory.GetFiles(dates);
      if (filePaths.Length==0){
        Console.WriteLine("Error: No text file available in 'dates' directory");
        return;
      }
        return;
      }
    

    //FUNCTION IS WRITTEN TO MANAGE FILE NAMES FOR WEBPAGE
    public void renameImages(){
      string path= Constants.imagesFolderName;
      int counter=0;
      
      foreach (string dirFile in Directory.GetDirectories(path)){    
        counter=0;      
        foreach (string fileName in Directory.GetFiles(dirFile )){
          counter++;
          //rename image files in serial order
          int lastindexofslash=fileName.LastIndexOf("/",fileName.Length);
          string cutpath=fileName.Substring(0,lastindexofslash);
          File.Move(fileName,cutpath+"/i_"+counter+".jpg");         
        }
      }
    } 
  }