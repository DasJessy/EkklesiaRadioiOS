using Foundation;
using System;
using UIKit;
using SQLite;

namespace EkklesiaiOS
{
    public partial class SettingController : UIViewController
    {
        public SettingController (IntPtr handle) : base (handle)
        {
        }

        partial void BtnChangeUsername_TouchUpInside(UIButton sender)
        {
            UIAlertView alert = new UIAlertView();
            alert.Title = "Update your username";
            alert.AddButton("Save");
            alert.Message = "Please enter a username";
            alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            alert.Clicked += (object sender2, UIButtonEventArgs e) => {

                if (e.ButtonIndex == 0)
                {
                    // Get the input

                    var xText = alert.GetTextField(0).Text;

                    if(!(xText == string.Empty))
                    {
                        playerViewController.user = xText;
                        var connection = new SQLiteConnection(SQLFunc.databasepath());
                        connection.CreateTable<EkklesiaUser>();
                        connection.Close();
                        connection.Dispose();

                        //Insert user in database
                        var conn = new SQLiteConnection(SQLFunc.databasepath());
                        EkklesiaUser userTOInsert = new EkklesiaUser();
                        userTOInsert.Username = playerViewController.user;
                        userTOInsert.ID = 0;
                        conn.InsertOrReplace(userTOInsert);
                        conn.Close();
                        conn.Dispose();

                        //firebaseFunc.authAnonymousUser(user);

                        alert.Dispose();
                    }



                }



            };
           


            alert.Show();
        }

        void Alert_Clicked(object sender, UIButtonEventArgs e)
        {
           

        }
    }
}