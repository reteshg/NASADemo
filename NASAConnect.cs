using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class NasaConnect{

            public List<string> images = new List<string>();
            private static int count = 1;
            public async Task<string> readNasaApi(string date){
            
            using (var webClient = new WebClient())
            {
             
                 string url="https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date="+date+"&api_key=MS956pjwjdbPUWeWlxd2idUZHQpFpXOAkh3wPt8p";
                 Console.WriteLine("Reading data from NASA on "+ date);   
                 try{
                 var json= await webClient.DownloadStringTaskAsync(url);
                 dynamic imgurl = JsonConvert.DeserializeObject(json);
                 for (int i=0;i<=imgurl.photos.Count-1;i++){
                     string temp=imgurl.photos[i].img_src;
                     images.Add(temp);                       
                 }
                 }catch(Exception e){
                     Console.WriteLine(e);
                 }
                 
                 return "";
             }
                

    }

    
    public async Task<string> downloadImagesFromNASA(string imageurl){
        
        using (var webClient = new WebClient()){
            string url=imageurl;
            int nextIndex = Interlocked.Increment(ref count);
            try{
                await webClient.DownloadFileTaskAsync(url, "/Users/Preeti/Documents/Projects/Csharp/NASADemo/images/" + nextIndex + ".jpg");    
            }catch(Exception e){
                Console.WriteLine(e);                    
            }
            Console.WriteLine("Image downloaded from Mars "+ nextIndex + ".jpg");
        }
        return "";
    }

      public void asyncCalls(string[] list,string type){  
         return;
        var tasks = new List<Task>();
        
        foreach (var url in list)
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

