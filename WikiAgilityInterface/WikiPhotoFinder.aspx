<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WikiPhotoFinder.aspx.cs" Inherits="AgilityInterface_1.WikiPhotoFinder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

   
   <style type="text/css">
     #wrap{
         width:100%;
     }
     .left{
         width:40%;
         border-radius: 10px;
         -webkit-box-shadow: -7px 9px 30px -2px rgba(140,161,132,1);
         -moz-box-shadow: -7px 9px 30px -2px rgba(140,161,132,1);
          box-shadow: -7px 9px 30px -2px rgba(140,161,132,1);
         background-color:white;
         height:650px;
         float:left;
     }
     .right{
         width:55%;
         background-color: white;
         border-radius: 10px;
         -webkit-box-shadow: -7px 9px 30px -2px rgba(140,161,132,1);
         -moz-box-shadow: -7px 9px 30px -2px rgba(140,161,132,1);
         box-shadow: -7px 9px 30px -2px rgba(140,161,132,1);
         height:700px;
         float:right;
         margin-left: 5px;

     }
     #mainHeader{
         -webkit-box-shadow: -9px 20px 34px -19px rgba(0,0,0,1);
         -moz-box-shadow: -9px 20px 34px -19px rgba(0,0,0,1);
          box-shadow: -9px 20px 34px -19px rgba(0,0,0,1);

     }
       #subjectListBox {
           width: 80%;
           height: 140px;
           margin-left: 40px;
           margin-top: -70px;
       }
       #Button3 {
           margin-left: 100px;
       }
       #photoButton{
           margin-left: 50px;
       }
       #subjectNumberBox{
           width: 50px;
       }
       #myCarousel{
           margin-left: 4px;
       }
       .fixed-ratio-size{
           max-width:100%; 
           max-height:100%;
           object-fit: contain;
           margin:auto;
           display:block;
       }
       #userSubjectBox{
           width:200px;
       }
       #userSubjectBox,#subjectLbl{
           display:inline;
       }
       #subjectNumberBox,#subNum{
           display:inline;
       }
       ul{
           margin-left: -15px;
       }
      
   </style>




</head>
<body>
    <form id="form1" runat="server">

        <div class="container">

            <h1 id="mainHeader" class="jumbotron text-center well-lg">Wiki Media Finder </h1>
            
        </div>
        <div class="container" id="containter1">
            <div id="wrap">
                <div class="left ">
                    <h1 class="jumbotron text-center" style="margin-top: 0px;">Subject Section</h1>
                    <ul style="list-style-type: none">
                        <li>
                            <asp:Label ID="subjectLbl" runat="server">Please enter a Wikipedia Url:</asp:Label>
                            <asp:TextBox ID="userSubjectBox" class="form-control" runat="server"></asp:TextBox>
                        </li>
                    </ul>

                    <br />
                    <br />
                    <ul style="list-style-type: none">
                        <li>
                            <asp:Label ID="subjects" runat="server">Do you want to a list of subjects found on the page?</asp:Label>
                            <asp:RadioButton ID="yesRadioButton" GroupName="subGroup" Text="Yes" runat="server" />
                            <asp:RadioButton ID="noRadionButton" GroupName="subGroup" Text="No" runat="server" />
                        </li>
                    </ul>

                    <br />
                    <br />
                    <ul style="list-style-type: none">
                        <li>
                            <asp:Label ID="subNum" runat="server"> How many subjects do you want displayed? (100 max) </asp:Label>
                            <asp:TextBox class="form-control" ID="subjectNumberBox" runat="server"></asp:TextBox>
                        </li>
                    </ul>
                    <br />
                    <br />
                    
                    <br />

                    
                                    
                    <br />
                   
                    <asp:ListBox class="form-control" ID="subjectListBox" runat="server" SelectionMode="Multiple"></asp:ListBox>
                    <br />
                    
                    <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClick="Button3_Click" Text="Get Subjects" />
                    <asp:Button ID="photoButton" class="btn btn-success"  runat="server" Text="Get Photos!" OnClick="photoButton_Click" />
                    <br />
                    <br />

                    <br />
                </div>
                <div class="right">
                    <h1 class="jumbotron text-center" style="margin-top: 0px;">Content display Section </h1>

                    <div id="myCarousel" class="carousel slide" data-ride="carousel">
                        
                        <ol class="carousel-indicators">
                            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                            <li data-target="#myCarousel" data-slide-to="1"></li>
                            <li data-target="#myCarousel" data-slide-to="2"></li>
                            <li data-target="#myCarousel" data-slide-to="3"></li>
                            <li data-target="#myCarousel" data-slide-to="4"></li>                      
                       </ol>

                        
                        <div class="carousel-inner" role="listbox" />
                        <div class="item active">
                                                          <!--Binging of image URl to first Panel-->
                            <img class="fixed-ratio-size" src="<%=panelImage[0]%>" alt="" />
                            <div class="carousel-caption">
                                 <!--Binding of string used to query Bing Images API to caption-->
                                <p><%=selectedSubject[0]%></p>
                            </div>
                        </div>

                        <div class="item">                <!--Binging of image URl to second Panel-->
                            <img class="fixed-ratio-size" src="<%=panelImage[1]%>" alt="" />
                            <div class="carousel-caption">
                                <!--Binding of string used to query Bing Images API to caption-->
                                <p><%=selectedSubject[1]%></p>
                            </div>

                        </div>

                        <div class="item">                <!--Binging of image URl to third Panel-->
                            <img class="fixed-ratio-size" src="<%=panelImage[2]%>" alt="" />
                            <div class="carousel-caption">
                                <!--Binding of string used to query Bing Images API to caption-->
                                <p><%=selectedSubject[2]%></p>
                            </div>

                        </div>

                        <div class="item">                <!--Binging of image URl to fourth Panel-->
                            <img class="fixed-ratio-size" src="<%=panelImage[3]%>" alt="" />
                            <div class="carousel-caption">
                                <!--Binding of string used to query Bing Images API to caption-->
                                <p><%=selectedSubject[3]%></p>
                            </div>
                        </div>
                        <div class="item">                <!--Binging of image URl to fifth Panel-->
                            <img class="fixed-ratio-size" src="<%=panelImage[4]%>" alt="" />
                            <div class="carousel-caption">
                                <!--Binding of string used to query Bing Images API to caption-->
                                <p><%=selectedSubject[4]%></p>
                            </div>
                        </div>
                    </div>


                    <asp:Label ID="testDisplayLabel" runat="server"></asp:Label>
                </div>

            </div>
        </div>

        <p>
            &nbsp;
        </p>


    </form>
</body>
</html>
