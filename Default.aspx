<%@ Page Language="C#" Inherits="Form.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default</title>
</head>
<body style="background-color:green;">
    <!-- Here we create a simple form to the user inputs data into -->
    <div style="text-align:center;">       
        <h1> Simple Database Inserter </h1>
	    <form id="form1" runat="server">      
            <label for="txtFirstName">First Name: </label>
            <asp:TextBox id="txtFirstName" runat="server"/>

            <br/><br/>

            <label for="txtLastName">Last Name: </label>
            <asp:TextBox id="txtLastName" runat="server"/>
            
            <br/><br/>

            <p id="successMsg" runat="server" visible="false"></p>    
            <p id="errorMsg" runat="server" visible="false"></p>
                
		    <asp:Button id="button1" runat="server" Text="Add to the Database!" OnClick="button1Clicked" />
            &emsp;
            <asp:Button id="button2" runat="server" Text="Display Last Insert!" OnClick="button2_Click"/>
                
            <br/><br/>
                
            <asp:TextBox id="txtData" runat="server" Visible="false"/> 
                
	    </form>
   </div>
</body>
</html>
