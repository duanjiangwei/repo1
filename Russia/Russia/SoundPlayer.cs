using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;  //引入命名空间

namespace Russia
{
    public class SoundPlayer
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        private string mciStatus = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        private string shortFileName = "";

        [DllImport("winmm.dll", EntryPoint = "mciSendString", SetLastError = true, CharSet = CharSet.Auto)]   //导出API
        private static extern long mciSendString(
            string lpstrCommand,
            string lpstrReturnString,
            long uReturnLength,
            long hwndCallback
            );

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
            string lpszLongPath,
            string shortFile,
            int cchBuffer
            );


        //字段和属性
        private string File;   //保存文件名
        private string Alias;
        private string status = "close";  //保存mci设备的状态

        public string FileName
        {
            get { return File; }
            set { File = LongNameToShortName(value); }
        }

        public string AliasName
        {
            get { return Alias; }
        }

        //Methods
        public SoundPlayer(string strFileName, string strAliasName)   //构造函数
        {
            this.File = LongNameToShortName(strFileName); //将文件名转换
            this.Alias = strAliasName;  //播放类型
            status = "close";  //关闭状态
        }

        private string LongNameToShortName(string strFileName)
        {
            shortFileName = shortFileName.PadLeft(260, Convert.ToChar(" "));  //把文件名填充至260字节,在左边填充空格
            GetShortPathName(strFileName, shortFileName, shortFileName.Length); //调用API
            return GetCurrPath(shortFileName);
        }

        private string GetCurrPath(string name)
        {
            if (name.Length < 1) return "";
            name = name.Trim();
            name = name.Substring(0, name.Length - 1);
            return name;
        }

        public bool PlaySound()  //播放
        {
            if (PlayFile() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool StopSound()  //停止播放
        {
            StopFile();
            return false;
        }

        private bool PlayFile()  //调用API播放文件
        {
            bool returnValue;
            long lRet;
            try
            {
                StopFile();  //停止正在播放的文件

                lRet = mciSendString("open " + this.File + " alias " + Alias, "", 0, 0); //打开文件
                lRet = mciSendString("play " + Alias, "", 0, 0);//播放
                returnValue = (lRet == 0);

                status = "play";
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void CloseAudio()
        {
            mciSendString("close all", "", 0, 0);
            status = "close";
        }

        private bool StopFile()
        {
            long lRet;
            try
            {
                lRet = mciSendString("stop " + Alias, "", 0, 0);
                lRet = mciSendString("close " + Alias, "", 0, 0);
                status = "stop";
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsPlaying
        {
            get {
                if (status != "play")
                    return false;
                mciStatus = "";
                mciStatus = mciStatus.PadLeft(128, Convert.ToChar(" "));
                mciSendString("status " + this.Alias + " mode", mciStatus, 128, 0);
                return mciStatus.Substring(0, 7).ToLower() == "playing".ToLower();
            }
        }
    }
}
