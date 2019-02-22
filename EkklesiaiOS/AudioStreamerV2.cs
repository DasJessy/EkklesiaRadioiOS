using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using AudioToolbox;

namespace EkklesiaiOS
{
    class AudioStreamerV2
    {
    }

    class AudioStreamer 
    {
        bool outputStarted;

        AudioFileStream afs;
        OutputAudioQueue oaq;

        public void StartStreaming(string url)
        {
            afs = new AudioFileStream(AudioFileType.MP3);

            // event handlers, these are never triggered
            afs.PacketDecoded += OnPacketDecoded;
            afs.PropertyFound += OnPropertyFound;

            GetAudio(url);
        }

        public void stopStreaming()
        {
            afs = null;
        }

        void GetAudio(string url)
        {
            // HTTP
            NSUrlSession session = NSUrlSession.FromConfiguration(
                NSUrlSessionConfiguration.DefaultSessionConfiguration,
                new SessionDelegate(afs),
                NSOperationQueue.MainQueue);

            var dataTask = session.CreateDataTask(new NSUrl(url));
            dataTask.Resume();
        }

        // event handler - never executed
        void OnPropertyFound(object sender, PropertyFoundEventArgs e)
        {
            if (e.Property == AudioFileStreamProperty.ReadyToProducePackets)
            {
                oaq = new OutputAudioQueue(afs.StreamBasicDescription);
                oaq.BufferCompleted += OnBufferCompleted;
            }
        }

        // another event handler never executed
        void OnPacketDecoded(object sender, PacketReceivedEventArgs e)
        {
            IntPtr outBuffer;
            oaq.AllocateBuffer(e.Bytes, out outBuffer);
            AudioQueue.FillAudioData(outBuffer, 0, e.InputData, 0, e.Bytes);
            oaq.EnqueueBuffer(outBuffer, e.Bytes, e.PacketDescriptions);

            // start playing if not already
            if (!outputStarted)
            {
                var status = oaq.Start();
                if (status != AudioQueueStatus.Ok)
                    System.Diagnostics.Debug.WriteLine("Could not start audio queue");
                outputStarted = true;
            }
        }

        void OnBufferCompleted(object sender, BufferCompletedEventArgs e)
        {
            oaq.FreeBuffer(e.IntPtrBuffer);
        }
    }

    // instantiated in GetAudio()
    class SessionDelegate : NSUrlSessionDataDelegate
    {
        readonly AudioFileStream afs;

        public SessionDelegate(AudioFileStream afs)
        {
            this.afs = afs;
        }

        // this is, too, never executed
        public override void DidReceiveData(NSUrlSession session, NSUrlSessionDataTask dataTask, NSData data)
        {
            afs.ParseBytes((int)data.Length, data.Bytes, false);
        }
    }

}