using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using OpenJobScheduler;
using System.Threading.Tasks;
using Firebase.Xamarin.Database.Query;
using System.Threading;
using CoreGraphics;
using SQLite;
using System.Drawing;
using Xamarin;

namespace EkklesiaiOS
{
    public class chatStored
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string chatMessage { get; set; }

    }


    public partial class ChatController : UIViewController
    {
        List<string> chatMsg;
        TableSource tblSource;
        public static int counter;
        LoadingOverlay loadingOverlay;


        //keyboardtap
        UITapGestureRecognizer _tap;
       


        public ChatController(IntPtr handle) : base(handle)
        {
            

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            //thread.Start();
            // This code dismisses the keyboard when the user touches anywhere
            // outside the keyboard.

            _tap = new UITapGestureRecognizer();
            _tap.AddTarget(() => {
                View.EndEditing(true);
            });
            _tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(_tap);

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);


            // Initialize table
            //tableChat.DataSource = new GrowRowTableDataSource(this);
            //TableView.Delegate = new GrowRowTableDelegate(this);
            tableChat.RowHeight = UITableView.AutomaticDimension;
            tableChat.EstimatedRowHeight = 40f;
            //tableChat.ReloadData();
            //if (chatMsg.Count>1)
            //{
            //    tableChat.ScrollToRow(NSIndexPath.FromRowSection(chatMsg.Count - 1, 0), UITableViewScrollPosition.None, false);  
            //}


           // chatBar.BadgeValue = "";

          

        }


        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            View.EndEditing(true);
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var bounds = UIScreen.MainScreen.Bounds;
            loadingOverlay = new LoadingOverlay(bounds);
            View.Add(loadingOverlay);
            await Task.Delay(1000);


            if (chatMsg == null)
            {
                chatMsg = new List<string>();
            }
            //txtChatMessage.BecomeFirstResponder();
            tblSource = new TableSource(chatMsg.ToArray());
            tableChat.Source = tblSource;
            tableChat.AllowsSelection = false;


            SelfQueryAsync();

            //thread = new Thread(SelfQueryAsync);

            //var firebase = new FirebaseClient(firebaseFunc.databaseUrl);
            //var observable = firebase
              //.Child("Ekklesia")
              //.AsObservable<string>()
                //.Subscribe(d => update(d.Object));

          
            //hatBar.BadgeValue = chatMsg.Count.ToString();
            updateFromMethod();

           
           await Task.Delay(10000);
            loadingOverlay.Hide();

            if (chatMsg.Count > 1)
            {

                tableChat.ScrollToRow(NSIndexPath.FromRowSection(chatMsg.Count - 1, 0), UITableViewScrollPosition.None, false);


            }

            this.txtChatMessage.ShouldReturn += (textField) => {
                textField.ResignFirstResponder();
                return true;
            };


            IQKeyboardManager.SharedManager.ToolbarDoneBarButtonItemText = "Done";
           // IQKeyboardManager.SharedManager.EnableAutoToolbar = true;
            IQKeyboardManager.SharedManager.ShouldShowTextFieldPlaceholder = false;
            IQKeyboardManager.SharedManager.ShouldShowToolbarPlaceholder = false;
            Xamarin.IQKeyboardManager.SharedManager.ShouldPlayInputClicks = true;

            //var vc = this.View.ViewController();
            //var dh = this.View.DebugHierarchy();
            //var isAlertView = this.View.IsAlertViewTextField();
            //var isSearchView = this.View.IsSearchBarTextField();

        }


        private void update(string obj)
        {
         
            chatMsg.Add(obj);
            tblSource.TableItems = chatMsg.ToArray();
            tableChat.ReloadData();
            if (chatMsg.Count > 1)
            {
                tableChat.ScrollToRow(NSIndexPath.FromRowSection(chatMsg.Count - 1, 0), UITableViewScrollPosition.None, false);
            }
        }

        private async void updateFromMethod()
        {
            while(true)
            {
                await Task.Delay(11000);
                try
                {
                    SelfQueryAsync();
                    tableChat.FlashScrollIndicators();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }

        private async void SelfQueryAsync()
        {
            var x = counter;
            //COntinuously Update the Chat
            try{
               
                var firebase = new FirebaseClient(firebaseFunc.databaseUrl);
                var items = await firebase
                  .Child("Ekklesia")
                  //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                  .OrderByKey()
                  .OnceAsync<string>();
                chatMsg.Clear();
                foreach (var item in items)
                {
                    chatMsg.Add(item.Object);

                }


                tblSource.TableItems = chatMsg.ToArray();
                tableChat.ReloadData();
                //tableChat.ScrollToRow(NSIndexPath.FromRowSection(chatMsg.Count - 1, 0), UITableViewScrollPosition.None, false);
              

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

        }

        partial void BtnSendMsg_TouchUpInside(UIButton sender)
        {

            sendMsg();

        }

       
      
        public async void sendMsg()
        {
            if (!txtChatMessage.Text.Equals(string.Empty))
            {
                var cht = playerViewController.user + "^" + txtChatMessage.Text;

                chatMsg.Add(cht);

                txtChatMessage.Text = "";



                //tableChat.ReloadData();

                tblSource.TableItems = chatMsg.ToArray();

                tableChat.ReloadData();

                //tableChat.ScrollToRow(NSIndexPath.FromRowSection(chatMsg.Count - 1, 0), UITableViewScrollPosition.None, false);

                tableChat.ScrollToRow(NSIndexPath.FromRowSection(chatMsg.Count - 1, 0), UITableViewScrollPosition.None, false);

                await Task.Delay(1000);
                await firebaseFunc.writeDataVer2Async(cht);


            }
        }


    }
}