using System;
using AVFoundation;
using Foundation;


namespace EkklesiaiOS
{
    public class AudioService
	{
		int clicks = 0;
        string status = "";

		
		AVPlayer player;

        public AudioService() { 
        

        
        }

        //Testing
        //AudioStreamer audStream;

        public void playControl(string url)
        {
			this.player = new AVPlayer();
			this.player = AVPlayer.FromUrl(NSUrl.FromString(url));
			this.player.Play();
			status = "Pause";
        }

		public bool Play_Pause(string url)
		{
			if (clicks == 0)
			{
                playControl(url);
                //audStream = new AudioStreamer();
                //audStream.StartStreaming(url);

				clicks++;
			}
			else if (clicks % 2 != 0)
			{
                //audStream.stopStreaming();
				//this.player.Pause();
                this.player.Dispose();

				clicks++;
                status = "Play";

			}
			else
			{
                //this.player.Play();
                playControl(url);
                //audStream = new AudioStreamer();
                //audStream.StartStreaming(url);
                clicks++;
                status = "Pause";
			}
			return true;

		}
        public string getStatus()
        {
            return status;
        }

		public bool Stop(bool val)
		{
			if (player != null)
			{
				player.Dispose();
				clicks = 0;
			}
			return true;
		}
	}
}