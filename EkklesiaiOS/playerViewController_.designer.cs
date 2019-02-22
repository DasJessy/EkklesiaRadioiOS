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
    [Register ("playerViewController")]
    partial class playerViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView activitySpinner { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPlayRadioIphone { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtVerse { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (activitySpinner != null) {
                activitySpinner.Dispose ();
                activitySpinner = null;
            }

            if (btnPlayRadioIphone != null) {
                btnPlayRadioIphone.Dispose ();
                btnPlayRadioIphone = null;
            }

            if (txtVerse != null) {
                txtVerse.Dispose ();
                txtVerse = null;
            }
        }
    }
}