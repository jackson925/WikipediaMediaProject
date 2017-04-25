using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WikiAgilityClassLibrary;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace AgilityInterface_1
{
    public partial class WikiPhotoFinder : System.Web.UI.Page
    {
        //This is the Interfaced list of image content objects containing content Urls and thumbnail Urls.
        public static IList<ImageContent> imageInfoList = new List<ImageContent>();
        //This is a list of JToken objects which are essentially the nodes of a JSON Array
        public static List<JToken> results = new List<JToken>();
        //This is the code-behind variable the contains the users subject input
        public string sitePath { get; set; }
        //This is the code behind variable that contains the amount of subject returns the user wants displayed
        public int subjectNum { get; set; }
        //This is the array containing the listbox items selected by the user
        public static string[] selectedSubject = new string[5];
        //This is an array containing the image urls that will be bound to the slider
        public string[] panelImage = new string[5];

        
        //Load page event
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieval and assignment of user input. In this case a wikipedia page.
            sitePath = userSubjectBox.Text;
           
        }
        //Get Subjects Button Click Event
        protected void Button3_Click(object sender, EventArgs e)
        {
            
            //Checking to see if the "no" radio button is checked
            if (noRadionButton.Checked)
            {
                
                //string[] requestArr = new string[5];
                //Calling static 
                //Static method that returns results with no subjects requested
                MediaGetter.GetSiteContent(sitePath);
                //Method that gets five random subject photo contentURl and thumnbnailURLs
                string[] jsonC = MediaGetter.MakeRequest(MediaGetter.headerArray);

                imageInfoList = MediaGetter.GetRawUrls(jsonC);
                //Assignment of first image Url to first slider image
                panelImage[0] = imageInfoList[0].contentUrl;
                //Assignment of second image Url to second slider image
                panelImage[1] = imageInfoList[1].contentUrl;
                //Assignment of third image Url to third slider image
                panelImage[2] = imageInfoList[2].contentUrl;
                //Assignment of fourth image Url to fourth slider image
                panelImage[3] = imageInfoList[3].contentUrl;
                //Assignment of fifth image Url to fifth slider image
                panelImage[4] = imageInfoList[4].contentUrl;

            }
            //Checking to see if the "yes" radio button is checked
            else if(yesRadioButton.Checked)
            {
                //Conversion and assignment of total subjects user wants returned
                subjectNum = Convert.ToInt32(subjectNumberBox.Text);
                //call to overloaded method that returns number of subjects user specified
                string [] subjects = MediaGetter.GetSiteContent(sitePath, subjectNum);
                //Instatiation of datatable for subjects to be displayed in
                DataTable dataTable = new DataTable();
                //Creation of Columns for Datatable
                dataTable.Columns.Add("Subjects");
                //Call to AddListItem method
                AddListItem(dataTable, subjects);
                //Binding of the datatable as a data source for the listbox
                subjectListBox.DataSource = dataTable;
                //Binding the "Subjects" column to listbox field
                subjectListBox.DataTextField = "Subjects";
                //Final Bind
                subjectListBox.DataBind();
            }
        }
        //Method that evaluate if an Item is not null and adds it to the datatable
        public static void AddListItem(DataTable dataTable, string[] x)
        {
                //Iterations through number of subjects based on user input
                foreach(string st in x)
                {
                  //Evaluating if its not null
                  if(st != null)
                  //Adding it to the table if its not null
                  dataTable.Rows.Add(st);
                }
        }
        //Click event for Select Photobutton IF USER SELECTS 'YES'
        protected void photoButton_Click(object sender, EventArgs e)
        {
            //iterator for listbox
            int t = 0;
            //Iterator for selected subject
            int p = 0;
            //For loop to evaluate and add selected items
            for(int i = 0; i < subjectListBox.Items.Count ; i++)
            {
                //Evaluating if item is selected
                if (subjectListBox.Items[t].Selected)
                {   
                    //If selected, add item value to string string array
                    selectedSubject[p] = subjectListBox.Items[t].ToString();
                    t++;
                    p++;
                }
                //Continue going through Listbox if the item is not selected
                else if(!subjectListBox.Items[t].Selected)
                {
                    t++; 
                }
            }
            //Call to method that queries Bing Images API, and stores contentURL and thumbnailURl in string array
           string[] jsonC = MediaGetter.MakeRequest(selectedSubject);
            //call to method that parses JSON data and stores in a List of ImageContent
            imageInfoList = MediaGetter.GetRawUrls(jsonC);
            //Assignment of first image Url to first slider image
            panelImage[0] = imageInfoList[0].contentUrl;
            //Assignment of second image Url to second slider image
            panelImage[1] = imageInfoList[1].contentUrl;
            //Assignment of third image Url to third slider image
            panelImage[2] = imageInfoList[2].contentUrl;
            //Assignment of fourth image Url to fourth slider image
            panelImage[3] = imageInfoList[3].contentUrl;
            //Assignment of fifth image Url to fifth slider image
            panelImage[4] = imageInfoList[4].contentUrl;
        }
    }
}








