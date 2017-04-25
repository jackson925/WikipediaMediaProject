using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;
using System.Drawing;
using System.Drawing.Imaging;

namespace WikiAgilityClassLibrary
{
    public class MediaGetter
    {
    

            //String array returned after GetSiteContentMethod contain 5 random subjects
            public static string[] headerArray = new string[5];
            //String array returned after Overloaded GetSiteContent which contained the number of subjects user indicated
            public static string[] subArr { get; set; }
            //Html Document object to copy the site of every href for parsing and pulling of header
            public static HtmlDocument copySiteTwo;
            //Html node array to hold all of the h1 nodes pulled off of every site link found on the orginal document
            public static HtmlNode[] siteNodes = new HtmlNode[1000];
            //Htmlweb object to make a request for every node for passing to copySiteTwo htmlDocument
            public static HtmlWeb secondSite = new HtmlWeb();
            //List containing all of the JSON Token object returned during parsing in GetRawURls Method
            public static List<JToken> results = new List<JToken>();
            //Interfacable list of Image Content holding all ImageContent Objects which each have thumbnailURL and ContentURL
            public static IList<ImageContent> imageInfoList = new List<ImageContent>();



        //Overloaded GetSiteContent Method that is called when the user selects "yes", which returns the amount of subjects the user indicates
        public static string[] GetSiteContent(string c, int e)
        {
            //Instatiationg of subArr inside of method to limit amount of subjects user indicates
            string[] subArr = new string[e];
            //List of strings that will hold all of the site hrefs found on user provided wikipage
            List<string> hrefList = new List<string>();
            //Original HtmlWeb object which will make a request for the site
            HtmlWeb web = new HtmlWeb();
            //Copy of original site to HtmlDocument for parsing
            HtmlDocument document = web.Load(c);
            //Parsing the Html document for href nodes and adding them all to an HtmlNode array
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//a[@href]").ToArray();

            //For loop that iterates though all href nodes found on Html document
            for (int t = 0; t < nodes.Count() - 1; t++)
            {
                //Instatiation of Stringbuilder object
                StringBuilder wikiHttp = new StringBuilder();
                //Temporary HtmlNode holder to look at each individual node
                HtmlNode tempHolder = nodes[t];
                //While loop stating that while the node only has one attribute(non-link), move to the next node
                while (tempHolder.Attributes.Count() == 1)
                {
                    tempHolder = nodes[t++];
                }
                //Assignment of the temporary nodes value(link), to string.
                string y = tempHolder.Attributes["href"].Value;
                //Checking to see if not Wikipedia internal link
                if (y.Contains("http"))
                {
                    //If yes, check if it meets these conditions
                    if (!y.Contains(@".") &&
                        (y.Contains("/") &&
                        (!y.Contains(@"#") &&
                        (!y.Contains("Categories") &&
                        (!y.Contains(@":"))))))
                    {
                        //If it passes, add it to the list
                        hrefList.Add(y);
                    }
                }
                //Execute this code if the link is a Wikipedia internal link
                else
                {
                    //If so, check if it meets these conditions
                    if (!y.Contains(@".") &&
                        (y.Contains("/") &&
                        (!y.Contains(@"#") &&
                        (!y.Contains("Categories") &&
                        (!y.Contains(@":"))))))
                    {
                        //If it is a Wiki internal link and meets these conditions append this string to it
                        wikiHttp.Append("http://en.wikipedia.org" + y);
                        //Then add it to the list
                        hrefList.Add(wikiHttp.ToString());
                    }
                }




            }
            //Checking to see if the number of subjects requested is greater than the amount of links found on the page
            if (hrefList.Count < e)
                //If so, iterate through them all
                for (int v = 0; v < hrefList.Count; v++)
                {
                    //Some links dont go through, so try catch was necessary here
                    try
                    {
                        //Load the site using HtmlWeb object "secondsite" and assign it to Html Document "copysitetwo"
                        MediaGetter.copySiteTwo = MediaGetter.secondSite.Load(hrefList[v]);
                        //Parse the site for h1 element and assign it to an HtmlNode
                        HtmlNode holderNode = MediaGetter.copySiteTwo.DocumentNode.SelectSingleNode("//h1");
                        //Add it to the array at index of v
                        MediaGetter.siteNodes[v] = holderNode;
                    }
                    //If the link was bad, catch the exception thrown
                    catch (Exception ex)
                    {
                        //Write exception in output
                        Trace.WriteLine(ex);
                    }
                    //Take the inner html text from every h1 node and store it in a string
                    string siteHeader = siteNodes[v].InnerHtml;

                    //store the string in subArr
                    subArr[v] = siteHeader;
                }
            //Checking to see if the amount of links found is greater than subjects requested by the user
            else if (hrefList.Count > e)
            {
                //If so, iterate through them all
                for (int v = 0; v < e; v++)
                {
                    //Some links dont go through, so try catch was necessary here
                    try
                    {
                        //Load the site using HtmlWeb object "secondsite" and assign it to Html Document "copysitetwo"
                        MediaGetter.copySiteTwo = MediaGetter.secondSite.Load(hrefList[v]);
                        //Parse the site for h1 element and assign it to an HtmlNode
                        HtmlNode holderNode = MediaGetter.copySiteTwo.DocumentNode.SelectSingleNode("//h1");
                        //Add it to the array at index of v
                        MediaGetter.siteNodes[v] = holderNode;
                    }
                    //If the link was bad, catch the exception thrown
                    catch (Exception ex)
                    {
                        //Write exception in output
                        Trace.WriteLine(ex);
                    }
                    //Take the inner html text from every h1 node and store it in a string
                    string siteHeader = siteNodes[v].InnerHtml;

                    //store the string in subArr
                    subArr[v] = siteHeader;
                    
                }
            }
            //return subArr
            return subArr;
        }


            //This is the non-Overloaded GetSiteContent Method that is called if the
            //user selects not to have subjects. It returns 5 random subjects out of all the nodes on the page
            public static string[] GetSiteContent(string x)
            {
                //List of strings that will hold all of the site hrefs found on user provided wikipage
                List<string> hrefList = new List<string>();
                //Original HtmlWeb object which will make a request for the site
                HtmlWeb web = new HtmlWeb();
                //Copy of original site to HtmlDocument for parsing
                HtmlDocument document = web.Load(x);
                //Parsing the Html document for href nodes and adding them all to an HtmlNode array
                HtmlNode[] nodes = document.DocumentNode.SelectNodes("//a[@href]").ToArray();
                //For loop that iterates though all href nodes found on Html document
                for (int t = 0; t < nodes.Count() - 1; t++)
                {
                    //Instatiation of Stringbuilder object
                    StringBuilder wikiHttp = new StringBuilder();
                    //Temporary HtmlNode holder to look at each individual node
                    HtmlNode tempHolder = nodes[t];
                    //While loop stating that while the node only has one attribute(non-link), move to the next node
                    while (tempHolder.Attributes.Count() == 1)
                    {
                        
                        tempHolder = nodes[t++];
                    }
                    //Assignment of the temporary nodes value(link), to string.
                    string y = tempHolder.Attributes["href"].Value;
                    //Checking to see if not Wikipedia internal link
                    if (y.Contains("http"))
                    {
                        //Does it meet these conditions
                        if (!y.Contains(@".") &&
                            (y.Contains("/") &&
                            (!y.Contains(@"#") &&
                            (!y.Contains("Categories") &&
                            (!y.Contains(@":"))))))
                        {
                            hrefList.Add(y);
                        }
                    }
                    else
                    {   
                        //Does it meet these conditions
                        if (!y.Contains(@".") &&
                            (y.Contains("/") &&
                            (!y.Contains(@"#") &&
                            (!y.Contains("Categories") &&
                            (!y.Contains(@":"))))))
                        {
                            //If it is a Wiki internal link and meets these conditions append this string to it
                            wikiHttp.Append("http://en.wikipedia.org" + y);
                            //Then add it to the list
                            hrefList.Add(wikiHttp.ToString());
                        }
                    }
                }


                //Instatiation of an array that will hold the random subjects selected
                string[] randomHrefArray = new string[5];
                //ListCount will provide max range for the random object
                int listCount = hrefList.Count();
                //Instatiation of random object
                Random ranPathSelector = new Random();
                //Loop for random href selection
                for (int i = 0; i < 5; i++)
                {
                    //assignment of random number to holder variable
                    int refSelector = ranPathSelector.Next(1, listCount);
                    //Assignment of random href at index of "refselector" to the array
                    randomHrefArray[i] = hrefList[refSelector];
                }

                //Loop that gets headers for all href sites in randomHrefArray
                for (int v = 0; v < randomHrefArray.Length; v++)
                {
                    //Instatiation of HtmlWeb object that will get site
                    HtmlWeb secondSite = new HtmlWeb();
                    //Again, some dont go through and throw exceptions
                    try
                    {
                        //Assignment of every href site to HtmlDocument for parsing
                        MediaGetter.copySiteTwo = secondSite.Load(randomHrefArray[v]);

                    }
                    //Catching exception if site doesnt go through
                    catch (Exception ex)
                    {
                        //Printing exeception to output
                        Trace.WriteLine(ex);
                    }
                    //Selecting h1 node for every Html Document/site pulled
                    //and assigning it to holder HtmlNode object
                    HtmlNode holderNode = MediaGetter.copySiteTwo.DocumentNode.SelectSingleNode("//h1");
                    //Adding the node containing h1 node to siteNodes Array
                    MediaGetter.siteNodes[v] = holderNode;
                    //Pulling inner html text from node and storing in temporary string
                    string siteHeader = siteNodes[v].InnerHtml;
                    //Adding string with h1 inner html content to string array, headerArray
                    headerArray[v] = siteHeader;
                }
                //Returning headerArray
                return headerArray;

            }



            //MakeRequest is the method that queries the Bing Image Search API
            //and returns JSON responses
            public static string[] MakeRequest(string[] x)
            {
                //Parsing, Formatting and encoding of query fields and assignment to NameValueCollection Object
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                //Counter to iterate through "string[] x"
                int i = 0;
                //This string array will hold the JSON responses and is returned
                string[] jsonContent = new string[5];
                //Do While loop to iterate through String[x]
                do
                {
                /*These are Bing Image Search Provided fields*/
                    //Assigns the string at index of i in string[] x as the query subject
                    queryString["q"] = x[i];
                    //Tells API how many results to return
                    queryString["count"] = "1";
                    //Tells the API how far from index 0 into the results to go
                    queryString["offset"] = "0";
                    //Tells the API the landuage of the search
                    queryString["mkt"] = "en-us";
                    //Tells the API how explicit or not the returned content needs to be
                    queryString["safeSearch"] = "Moderate";
                    //Concatinating queryString to API request URL and assigning to variable uri
                    var uri = "https://api.cognitive.microsoft.com/bing/v5.0/images/search?" + queryString;
                    //Creation of webrequest object cast to httpwebrequest for custom instructions
                    //Passing variable uri
                    HttpWebRequest http = (HttpWebRequest)WebRequest.Create(uri);
                    //Declaring that this request is a GET method
                    http.Method = "GET";
                    //Declaring that we want JSON data in response
                    http.ContentType = "application/json";
                    //Declaring 'Key' and providing key value in a new header
                    http.Headers.Add("Ocp-Apim-Subscription-Key", "YOUR BING IMAGE API KEY HERE");
                    //Getting the response and storing in webresponse object
                    WebResponse response = http.GetResponse();


                    //Instatiating a streamreader to read the response stream provided by response
                    //In 'using' statement to dispose of object after finished reading stream
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        //Reading everything in stream and storing it in a string
                        string jsonTemp = sr.ReadToEnd();
                        //adding the string to a string array
                        jsonContent[i] = jsonTemp;
                    }
                    //Counting iterator to step to the next element in string[] x
                    i++;
                //Providing the condition that allows requests for all elements
                //in string[] x to be queried by Bing Image Search API
                } while (i < x.Length);
                //Returing string[] jsonContent
                return jsonContent;
            }
            //GetRawUrls is the method that will parse the JSON response
            //data and return the desired items in an interfacible List of ImageContent
            public static IList<ImageContent> GetRawUrls(string[] jsonArr)
            {
                //Loop to iterate through all itemd in jsonArr
                for (int t = 0; t < jsonArr.Length; t++)
                {
                    //Parsing JSON string at index of t, and storing it in a JSONObject, 'bingsearch'
                    JObject bingSearch = JObject.Parse(jsonArr[t]);
                    //Navigating through the JObject to target desired values(jtokens),
                    //return them and store them in a List of Jtoken
                    List<JToken> holder = bingSearch["value"].Children().ToList();
                    //Adding the results to another Jtoken List that is outside of the loop
                    //for the next step
                    results.Add(holder[0]);
                }
                //Iterating through each JToken in the List of JToken 'results'
                foreach (JToken jResult in results)
                {
                    //Creating holder ImageContent objects to hold the values
                    /*When the jResult is assigned to the holder object 'imagecontent' of type
                      ImageContent, it automatically throws out fields that arent defined in ImageContent */
                    ImageContent imageContent = jResult.ToObject<ImageContent>();
                    //Adding the value held in holder ImageContent Object to Interfacible list
                    //That is outside of the loop.
                    imageInfoList.Add(imageContent);
                }
                //Return Interfacible List of ImageContent, 'imageInfoList'
                return imageInfoList;
            }
        }
    }

