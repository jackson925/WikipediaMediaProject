# WikipediaMediaProject

This is my first ASP.NET application project. This application utilizes Wikipedia, Bing Image Search API, HTMLAgilityPack library, and Newtonsoft JSON.NET library,  I'll give a little background to the inspiration behind this idea then list the known bugs and dependencies.

I am a huge fan of Wikipedia, albeit the occasional lack of factual references, there is typically tons of useful information on every individual page. Often, when on Wikipedia, it is easy for me to become sidetracked by the endless list of links leading to related subjects and end up knowing less about the core subject and more about four or five "sub" subjects if you will. I would love to say that this is a solution to that (whatever that means) however, this almost plays more into it than not. The original idea was to pull all of the internal Wikipedia links that exist on the root page, pick five random ones and return images based on those subjects. This was very interesting, especially for highly dense subjects with 1500+ links because you never really know what you're going to get back but know that it is in some way related to the original subject of interest. Not far into the project, I realized that the images produced were not as tied to the subject as I would've liked for particular searches. For this reason, I added the option to have the user decide if they wanted to pick the 
subjects directly from the links pulled off of the page along with the a number of subjects they might want to choose from. Although the number of subjects is limited to 100, there are still some very interesting things to choose from for any given page!

If I had to make a suggestion on some cool pages to pick from, highly historical Wikipedia pages usually return very interesting things. Also, the Wiki page on Military Helicopters returns some really cool things as well.

Known Bugs:
1. There is not currently validation for the wikipedia input textbox. Make sure you provide a full, valid Wikipedia URl.
2. There is not currently validation for the requested number of subjects to be displayed in the Listbox. Dont exceed 100.
3. Despite my filtering efforts, sometimes the returned subjects contains duplicates, and sometimes it will return subjects with odd characters attached to it for example "<i> Tomatoes <i>". However, if selected, these subjects will still return image results.
4. If you select "no", indicating that you don't want to select five subjects, you have to press the "Get Subjects" button to populate the slider with five random images, not the "Get Images!" button.
5. There is currently nothing to handle the a case in where the user selects more than 5 images. Its can be annoying to count them, but picking more will break it.
6. Sometimes the images returned wont fit into the container holding the slider, moving the indicator selectors about. But I can say definitively that the "Safe-Search" options for the Bing API is set to "Moderate", so although the images might not fit perfectly in the slider, they wont be THAT inappropriate. 
7. If you select "no", the images returned wont have captions, and you'll likely have no idea what exactly was searched. Partly a mystery, but mostly a design flaw.


If you find any other bugs or have any suggestions, please let me know! Im going to be working continiously to impove this starting with the above mentioned! Thank you! (:

Dependencies:
1. Wikipedia
2. Bing Image Search API (1,000 queries/month)

Required Nuget Packages inlcude: NewtonSoft JSON.NET, and HTMLAgilityPack. 

Have fun exploring!
