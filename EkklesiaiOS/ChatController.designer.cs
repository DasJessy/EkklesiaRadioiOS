// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace EkklesiaiOS
{
    [Register ("ChatController")]
    partial class ChatController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnSendMsg { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem chatBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tableChat { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtChatMessage { get; set; }

        [Action ("BtnSendMsg_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSendMsg_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (BtnSendMsg != null) {
                BtnSendMsg.Dispose ();
                BtnSendMsg = null;
            }

            if (chatBar != null) {
                chatBar.Dispose ();
                chatBar = null;
            }

            if (tableChat != null) {
                tableChat.Dispose ();
                tableChat = null;
            }

            if (txtChatMessage != null) {
                txtChatMessage.Dispose ();
                txtChatMessage = null;
            }
        }
    }
}