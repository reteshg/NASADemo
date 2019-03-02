/*
Programming Language: C# 
Codebase to connect to NASA API to fetch meta dats and download images to a local folder
Function readNasaAPI: Read image URL provided in JSON format form NASA API based on date parameter
Function downloadImagesFromNASA: Download images using NASA API
Function asyncCalls: Makes asyn calls to NASA API based on data type (JSON or IMAGES)
 */
using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class NasaConnect{

            public List<string> images = new List<string>(); //add list of all images to be fetched
            private static int count = 1; //counter variable to manage file names on local system
            
            //function to read JASON data and parse IMAGE URL
            public async Task<string> readNasaApi(string date){
            
            using (var webClient = new WebClient())
            {
             
                 string url="https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date="+date+"&api_key=MS956pjwjdbPUWeWlxd2idUZHQpFpXOAkh3wPt8p"; 
                 Console.WriteLine("Reading data from NASA on "+ date);   
                 try{
                 var json= await webClient.DownloadStringTaskAsync(url); //Async call to JSON data
                 dynamic imgurl = JsonConvert.DeserializeObject(json); //deserialize JSON
                 for (int i=0;i<=imgurl.photos.Count-1;i++){ // loop to fetch image URL
                     string temp=imgurl.photos[i].img_src;
                     images.Add(temp);  // add image URL to list                      
                 }
                 }catch(Exception e){
                     Console.WriteLine(e);
                 }
                 
                 return "";
             }
                

    }

    // function to download images from NASA API
    public async Task<string> downloadImagesFromNASA(string imageurl){
        
        using (var webClient = new WebClient()){
            string url=imageurl;
            int nextIndex = Interlocked.Increment(ref count);
            try{
                await webClient.DownloadFileTaskAsync(url, "/Users/Preeti/Documents/Projects/Csharp/NASADemo/images/" + nextIndex + ".jpg");    // async call to download images
            }catch(Exception e){
                Console.WriteLine(e);                    
            }
            Console.WriteLine("Image downloaded from Mars "+ nextIndex + ".jpg");
        }
        return "";
    }

      //function to manage Async calls to NASA APIs  
      public void asyncCalls(string[] list,string type){  
         
        var tasks = new List<Task>();
        
        foreach (var url in list) // loop to add data in the list to make Async calls
        {
            if (type=="json"){
                tasks.Add(readNasaApi(url));
            }else if(type=="images"){
                tasks.Add(downloadImagesFromNASA(url));
            }
        }
        try{  
        Task.WaitAll(tasks.ToArray());
        }catch(Exception e){
            Console.WriteLine(e);
        }
    }
}

