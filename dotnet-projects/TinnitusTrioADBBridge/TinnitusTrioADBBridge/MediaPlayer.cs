using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioADBBridge
{
   public class MediaPlayer
    {
       [DllImport("winmm.dll")]
       private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwndCallback);

       public void Open()
       {
          
       }

       public void Play(string file)
       {
           var command = "open \"" + file + "\" type MPEGVideo alias MyMp3";
           mciSendString(command, null, 0, 0);

          command = "play MyMp3";
           mciSendString(command, null, 0, 0);
       }

       public void Stop()
       {
           string command = "stop MyMp3";
           mciSendString(command, null, 0, 0);

           command = "close MyMp3";
           mciSendString(command, null, 0, 0);
       }
    }
}
