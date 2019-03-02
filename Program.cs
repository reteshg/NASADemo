/*
Programming Language: C# 
Codebase to connect NASA Rover API and download images on the local machine
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace NasaDemo
{
  class MainClass {
    public static void Main (string[] args) {
  
      // Read dates from the text file
      readDatesFromFile Ob_ReadDates=new readDatesFromFile();
      string[] datesArr=new string[Ob_ReadDates.readDates("dates").Length]; //Cast LIST object to string ARRAY
      datesArr=Ob_ReadDates.readDates("dates");
      
      //Make Async calls to read JSON and download images from NASA Site
      NasaConnect Ob_NASAConnect=new NasaConnect(); 
      Ob_NASAConnect.asyncCalls(datesArr,"json"); //Call to extract IMAGE URL from NASA API 
      string[] stringArray = Ob_NASAConnect.images.ToArray(); //Convert LIST object to string ARRAY 
      Ob_NASAConnect.asyncCalls(stringArray,"images"); //Call to download images
   
    }
  }
}
