# NASADemo
NASADemo project connects NASA APIs to download images clicked by the ROVER on any specific date. The application is written in C# (C-Sharp). 

The app read dates from a text file and hits NASA Rover api to fetch images clicked on that particular date.
The application takes multi-threaded approach to read JSON files from NASA REST API and download the images.

## Prerequisites:

.NET Core SDK 2.2.104 

```
Instaltion link https://dotnet.microsoft.com/download
```

JSON.Net 
```
Installation link https://www.nuget.org/packages/Newtonsoft.Json/
```

## Note:
Latest code has already been run once and all files have been uploaded on git. Simply open "webview.html" file in your browser to see the images clicked by ROVER on 2nd June 2018 

## Setup
* Create directory NASADemo
* Open Terminal and change cursor to NASADemo directory
* Pull all project files in the NASADemo folder 
* Run command: dotnet run
* Add/modify dates.txt file in "dates" folder and run the project from console
* Subscribe to [NASA API](https://api.nasa.gov/api.html) and get your API key to connect with NASA Rest services

## Code Files
### Program.cs: File with "Main" function
    * Calls function to check if all necessary folders (IMAGES, JSON, DATES) are available
    * Calls function:asyncCalls to fetch URL of all the images
    * Calls function:asyncCalls to download images
    * Calls function:renameImages to rename all images with incremantal values. This is needed to render images on the webpage (webview.html)

### fileHandling.cs: Handle tasks related to file handling
    * Function:readDates 
        * read text files with dates and populates in an array in MM-dd-yyyy format
        * Creates subfolder/s in "IMAGES" folder named as "02-12-2018". All images clicked on 02-12-2018 will be saved in this folder.
        * Creats a folder.js file in "JSON" folder which contains list of all the folders creadted in JSON format
    
    * Function:checkFolders
        * Checks function to check if all necessary folders (IMAGES, JSON, DATES) are available
            * IMAGES folder contains all the images downloaded from NASA API
            * JSON folder contains JSON data used by webview.html page to render the imags on the browser
            * DATES folder contains text file, based on which images are downloaded

    * Function:renameImages
        * Rename names of all image files downloaded from NASA site incrementally. This is needed in the webview.html file to render images

### NASAConnect.cs: Handles API calls to NASA REST Services  
    * Function:readNasaApi
        * Calls NASA API to fetch image URLS for each date
        * Creates a js files with count of images available on each date
    
    * Function:downloadImagesFromNASA
        * Download images from NASA site

    * Function:asyncCalls
        * Initiates Async calls to fetch image URLs or to download images       
