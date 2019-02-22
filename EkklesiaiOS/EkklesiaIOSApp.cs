using Foundation;
using Plugin.MediaManager;
using UIKit;

namespace EkklesiaiOS
{
    [Register("EkklesiaIOSApp")]
    public class EkklesiaIOSApp: UIApplication
    {
        private MediaManagerImplementation MediaManager => (MediaManagerImplementation)CrossMediaManager.Current;

        public override void RemoteControlReceived(UIEvent theEvent)
        {
            MediaManager.MediaRemoteControl.RemoteControlReceived(theEvent);
        }
    }
}
