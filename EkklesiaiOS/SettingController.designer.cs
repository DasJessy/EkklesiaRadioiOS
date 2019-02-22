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
    [Register ("SettingController")]
    partial class SettingController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnChangeUsername { get; set; }

        [Action ("BtnChangeUsername_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnChangeUsername_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnChangeUsername != null) {
                btnChangeUsername.Dispose ();
                btnChangeUsername = null;
            }
        }
    }
}