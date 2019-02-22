using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace EkklesiaiOS
{
    public static class SQLFunc
    {
        public static string sqliteFilename()
        {
            return "ekklesiaUsers.db3";
        }

        public static string databasepath()
        {

            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename());

            return path;
        }

        public static bool addChatMsgToDB(chatStored chtmsg)
        {
            try
            {

                var conn = new SQLiteConnection(SQLFunc.databasepath());
                conn.InsertOrReplace(chtmsg);
                conn.Close();
                conn.Dispose();
                return true;


            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }


        }

        public static List<chatStored> retrieveChatfromDB()
        {
            var conn = new SQLiteConnection(SQLFunc.databasepath());

            var chatList = conn.Table<chatStored>();

            List<chatStored> ChatGen = new List<chatStored>();
            foreach(chatStored x in chatList)
            {
                chatStored temp = new chatStored();
                temp.ID = x.ID;
                temp.chatMessage = x.chatMessage;
                ChatGen.Add(temp);

            }

            conn.Close();
            conn.Dispose();

            return ChatGen;
        }



    }



}
