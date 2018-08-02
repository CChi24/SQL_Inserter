using System;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Form
{

    //Create a table first to insert personal information
    /*
    CREATE TABLE personal_info
    (  
      person_id int IDENTITY(1,1),
      first_name VARCHAR(50) NOT NULL,
      last_name VARCHAR(50) NOT NULL
    ); 
     */
    public partial class Default : System.Web.UI.Page
    {
        public void button1Clicked(object sender, EventArgs args)
        {
            //checks inputs to see if there are empty textboxes, numbers, or a combination of numbers and letters within the text boxes and throws an error. 
            Regex inputChecker = new Regex(@"^[A-Za-z]+$");
            if (!inputChecker.IsMatch(txtLastName.Text) || !inputChecker.IsMatch(txtFirstName.Text)  || txtLastName.Text == "" || txtFirstName.Text == "")
            {
                errorMsg.Visible = true;
                errorMsg.InnerHtml = "There is an issue with what you have entered. Please check to make sure your first and last name are ONLY letters.";
            }
            else
            {
                errorMsg.Visible = false;
                //Create a SqlConnection to MS SQL DB 
                //Connectionstrings are and should be modified in web config file based on your MS SQL database credentials.
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnector"].ConnectionString))
                {
                    //on the button click, it will insert data into the database table 
                    using (SqlCommand insertCommand = new SqlCommand("INSERT INTO personal_info (first_name, last_name) VALUES (@firstName, @lastName)", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                        insertCommand.Parameters.AddWithValue("@lastName", txtLastName.Text);

                        connection.Open();
                        int numberOfRowsAffected =insertCommand.ExecuteNonQuery();
                            
                        if(numberOfRowsAffected < 0){
                            errorMsg.InnerHtml = "Yikes! While inserting data, an issue occurred.";
                            errorMsg.Visible = true;
                        }    
                    }
                    connection.Close();
                }
                successMsg.Visible = true;
                successMsg.InnerHtml = "Successfully added to database!";
                txtLastName.Text = "";
                txtFirstName.Text = "";
            }
        }

        public void button2_Click(object sender, EventArgs args)
        {   
            //open a connection to the database
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnector"].ConnectionString))
            {
                //run a select command that only grabs the first row of the database.
                SqlCommand selectCommand = new SqlCommand("SELECT TOP 1 first_Name, last_Name FROM personal_info", connection);

                connection.Open(); 

                //reading through db table matching query...
                SqlDataReader selectReader = selectCommand.ExecuteReader();
                //checks to see if there are any rows
                if (selectReader.HasRows)
                {
                    while (selectReader.Read())
                    {
                        txtData.Visible = true;
                        //grabs the first_Name column and the last_Name column and formats them into a string to be input into a textbox. 
                        txtData.Text = "Hello " + selectReader["first_Name"].ToString() + " " + selectReader["last_Name"].ToString() +  "!";
                    }
                }
                else
                {
                    txtData.Visible = false;
                    errorMsg.Visible = true;
                    errorMsg.InnerHtml = "Woops! No data has been inserted yet! Please enter your first and last name and click \"Add To Database\"";
                }
            }
        }
    }
}
