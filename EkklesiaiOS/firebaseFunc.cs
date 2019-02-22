using System;
using Firebase;
using Firebase.Auth;
using Firebase.Xamarin.Database;
using Foundation;

namespace EkklesiaiOS
{
    
    partial class ChatComponent
    {
        string chatBody;

        public ChatComponent(string chat)
        {
            chatBody = chat;
        }

    }



    public class firebaseFunc
    {
        public static Auth mAuth;
        public static User firebaseUser;
        public static string databaseUrl = "https://ekklesia-connect-27650.firebaseio.com/";




        public firebaseFunc()
        {
        }

        public static async System.Threading.Tasks.Task writeDataVer2Async(string myObject)
        {
            var sucess = true;

            while(sucess)
            {
                try{

                    var firebase = new FirebaseClient(databaseUrl);

                    // add new item to list of data 
                    var item = await firebase
                        .Child("Ekklesia")
                        //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                        .PostAsync(myObject);

                    // note that there is another overload for the PostAsync method which delegates the new key generation to the client

                    Console.WriteLine($"Key for the new item: {item.Key}");

                    // add new item directly to the specified location (this will overwrite whatever data already exists at that location)
                    //var item = await firebase
                    //.Child("yourentity")
                    //.Child("Ricardo")
                    ////.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                    //.PutAsync(new YourObject());

                    sucess = false;

                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
      

        }

        public static bool authAnonymousUser(string username)
        {
            mAuth = Firebase.Auth.Auth.DefaultInstance;

            firebaseUser = mAuth.CurrentUser;

            if (username != string.Empty)
            {
                //mAuth.CreateUser("thetjdas2007@gmail.com", "ekklesia", (user, error) => {
                //});

                mAuth.SignInAnonymously((User, error) => {

                    Console.WriteLine(error.LocalizedDescription);
                });

                return true;
            }
            else
            {
                return false;
            }

        }

        public static void writeData(string myObject)
        {
            //NSObject obj = NSObjectConverter.ToObject(myObject, myObject.GetType());

            NSString toStore = (NSString) myObject;

            firebaseUser = mAuth.CurrentUser;

            //var userid = firebaseUser.Uid;

            //database.GetReferenceFromUrl("https://ekklesia-connect-27650.firebaseio.com/").GetChild("Ekklesia").SetValue(toStore);

        }

       
    }
}
