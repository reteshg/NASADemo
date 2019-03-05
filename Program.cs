/*
Programming Language: C# 
Codebase to connect NASA Rover API and download images on the local machine
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace NasaDemo
{
  class MainClass {
    public static void Main (string[] args) {
      
      //Stopwatch to evaluate code execution time
      Stopwatch stopWatch = new Stopwatch();
      stopWatch.Start();
      TimeSpan ts = stopWatch.Elapsed;
      Thread.Sleep(1000);
      
      // Read dates from the text file
      fileHandling Ob_filehandling=new fileHandling();
      
      Ob_filehandling.checkFolders(); //check if all necessary folders exists
      
      Console.WriteLine("Time elapsed in checking folder strcture and deleting old images: {0:hh\\:mm\\:ss}", stopWatch.Elapsed);
      //Constants Ob_constants=new Constants();
      
      //Make Async calls to read JSON and download images from NASA Site
      NasaConnect Ob_NASAConnect=new NasaConnect(); 
      Ob_NASAConnect.asyncCalls(Ob_filehandling.readDates(Constants.datesFolderName),"json"); //Call to extract IMAGE URL from NASA API 
      
      Console.WriteLine("Time elapsed in validating dates and fetching image URL's: {0:hh\\:mm\\:ss}", stopWatch.Elapsed);
      
      string[] stringArray = Ob_NASAConnect.images.ToArray(); //Convert LIST object to string ARRAY 
      Ob_NASAConnect.asyncCalls(stringArray,"images"); //Call to download images
      
      Console.WriteLine("Time elapsed in downloading images: {0:hh\\:mm\\:ss}", stopWatch.Elapsed);
      
      Ob_filehandling.renameImages();
      
      Console.WriteLine("Time elapsed in generating data files for HTML page: {0:hh\\:mm\\:ss}", stopWatch.Elapsed);
      
      stopWatch.Stop();
    } 
  }
}