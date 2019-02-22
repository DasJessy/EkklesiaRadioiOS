using Foundation;
using System;
using UIKit;
using Plugin.MediaManager;
using System.Collections.Generic;
using System.Net;
using SQLite;
using System.IO;

namespace EkklesiaiOS
{

    public class EkklesiaUser
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }

    }

    public partial class playerViewController : UIViewController
    {
        bool isplaying = false;
        public static string user;
        //AudioService stream;
        //AudioStreamer streamV2;
        public static string streamAddr = "http://kofifi.fm:8000/EkklesiaApp";

        public void initDatabase()
        {

            if (File.Exists(SQLFunc.databasepath()))
            {
                //Welcome User
                var db = new SQLiteConnection(SQLFunc.databasepath());
                var userfromDB = db.Get<EkklesiaUser>(0);
                user = userfromDB.Username;
                UIAlertView alert = new UIAlertView();
                alert.Title = "Hello from Ekklesia";
                alert.AddButton("Ok");
                alert.Message = "Welcome back \n" + userfromDB.Username;
                alert.AlertViewStyle = UIAlertViewStyle.Default;
                alert.Clicked += (object sender, UIButtonEventArgs e) => {
                alert.Dispose();

                    //irebaseFunc.authAnonymousUser(user);

                };

                alert.Show();
                db.Close();
                db.Dispose();
              
            }else
            {
                try
                {

                    UIAlertView alert = new UIAlertView();
                    alert.Title = "Hello from Ekklesia";
                    alert.AddButton("Save");
                    alert.Message = "Please enter a username";
                    alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;

                    alert.Clicked += (object sender, UIButtonEventArgs e) => {

                        if (e.ButtonIndex == 0)
                        {
                            var result = alert.GetTextField(0).Text;
                            if (!(result == string.Empty))
                            {

                                // Get the input
                                user = result;

                            }
                            else{
                                
                                user = "Ekklesia User";


                            }
                            var connection = new SQLiteConnection(SQLFunc.databasepath());
                            connection.CreateTable<EkklesiaUser>();
                            connection.CreateTable<chatStored>();
                            connection.Close();
                            connection.Dispose();

                            //Insert user in database
                            var conn = new SQLiteConnection(SQLFunc.databasepath());
                            EkklesiaUser userTOInsert = new EkklesiaUser();
                            userTOInsert.Username = user;
                            userTOInsert.ID = 0;
                            conn.InsertOrReplace(userTOInsert);
                            conn.Close();
                            conn.Dispose();

                            //firebaseFunc.authAnonymousUser(user);

                            alert.Dispose();
                           

                        }

                    };

                    alert.Show();

                   


                }
                catch (Exception ex)
                {
                    string error = ex.Message;

                    UIAlertView alert = new UIAlertView();
                    alert.Title = "Oups";
                    alert.AddButton("Ok");
                    alert.Message = "An error occured while trying to initialise the database \nEnsure that you have accepted the required permissions.";
                    alert.AlertViewStyle = UIAlertViewStyle.Default;
                    alert.Clicked += (object sender, UIButtonEventArgs e) => {
                        alert.Dispose();

                    };

                    alert.Show();
                }


            }
        }

       public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Init Database
            try
            {
                initDatabase();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            //CrossMediaManager.Current.BufferingChanged += Current_BufferingChanged;

            //CrossMediaManager.Current.PlayingChanged += Current_PlayingChanged;

            activitySpinner.HidesWhenStopped = true;

            //activitySpinner2.HidesWhenStopped = true;

            btnPlayRadioIphone.TouchDown += BtnPlayRadioIphone_TouchDown;

            //btnPlayRadioIPad.TouchDown += BtnPlayRadioIPad_TouchDown;
           
                generateVerse();

          
           

        }


        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

          
                generateVerse();

      
        }


        public bool isStreamReachable(String strAddr)
        {
            try
            {
                if (!Reachability.IsHostReachable(strAddr))
                {
                    // Put alternative content/message here

                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Oups",

                        Message = "Please check your Internet Connection and try again"
                    };
                    alert.AddButton("OK");

                    alert.Show();

                    return false;

                }
                else
                {

                    return true;


                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                UIAlertView alert = new UIAlertView()
                {
                    Title = "Oups",

                    Message = "Try again later or check your internet connection"
                };
                alert.AddButton("OK");

                alert.Show();

                return false;
            }
        }

        //IPHONE
        async void BtnPlayRadioIphone_TouchDown(object sender, EventArgs e)
        {
            //radioPlay(btnPlayRadioIphone);

            if (isStreamReachable(streamAddr))
            {
                if (isplaying == false)
                {
                    isplaying = true;

                    activitySpinner.StartAnimating();

                    // btnPlayRadioIphone.SetTitle("Pause", UIControlState.Normal);

                    btnPlayRadioIphone.SetImage(UIImage.FromFile("icons8-pause-button-filled-50.png"), UIControlState.Normal);

                    await CrossMediaManager.Current.Play(streamAddr);

                    activitySpinner.StopAnimating();


                }
                else
                {
                    isplaying = false;

                    //btnPlayRadioIphone.SetTitle("Play", UIControlState.Normal);

                    btnPlayRadioIphone.SetImage(UIImage.FromFile("icons8-play-button-circled-filled-50.png"), UIControlState.Normal);

                    await CrossMediaManager.Current.Pause();

                    activitySpinner.StopAnimating();

                }
            }



        }



        //IPAD
        //async void BtnPlayRadioIPad_TouchDown(object sender, EventArgs e)
        //{
        //    if (isStreamReachable(streamAddr))
        //    {

        //        if (isplaying == false)
        //        {
        //            isplaying = true;

        //            activitySpinner2.StartAnimating();

        //            btnPlayRadioIPad.SetTitle("Pause", UIControlState.Normal);

        //            await CrossMediaManager.Current.Play(streamAddr);

        //            activitySpinner2.StopAnimating();


        //        }
        //        else
        //        {
        //            isplaying = false;

        //            btnPlayRadioIPad.SetTitle("Play", UIControlState.Normal);

        //            await CrossMediaManager.Current.Pause();

        //            activitySpinner2.StopAnimating();

        //        }

        //    }




        //}


       
        void Current_PlayingChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.PlayingChangedEventArgs e)
        {

        }

        public playerViewController(IntPtr handle) : base(handle)
        {



        }


        public void generateVerse()
        {
            Random r = new Random();
            r.Next(0, 1);

            List<string> verses = new List<string>();
            string url = "https://beta.ourmanna.com/api/v1/get/?format=text&order=random";
         

            verses.Add("“The LORD makes firm the steps of the one who delights in him; though he may stumble, he will not fall, for the LORD upholds him with his hand.” Psalm 37:23-24");
            verses.Add("“Fight the good fight of the faith. Take hold of the eternal life to which you were called when you made your good confession in the presence of many witnesses.”1 Timothy 6:12");

            if (isStreamReachable(url))
            {
                string fromUrl = "";
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url);
                    fromUrl = s;
                }
                txtVerse.Text = fromUrl;




            }
            else
            {
                txtVerse.Text = verses[r.Next()].ToString();
            }




        }
    }
    }
