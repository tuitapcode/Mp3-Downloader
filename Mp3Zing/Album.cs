using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using IDMLib;

namespace Mp3Zing
{
    public class Album
    {
        public string DownloadAlbum(string strId, string path)
        {
            IDM IDM = new IDM();

            string lp = path;

            if (path.Equals(string.Empty))
            {
                lp = @"g:\Musics\Downloads\";
            }

            string url = "http://dev.mp3.zing.vn/api/mobile/playlist/getsonglist?requestdata={\"length\":200,\"id\":\"" + strId + "\",\"start\":0}";
            var json = new WebClient().DownloadString(url);
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<dynamic>(json);

            int i = 1;
            foreach (dynamic x in dict["docs"])
            {
                string fn = getNum(i) + ". " + x["artist"] + " - " + x["title"];
                string mp3_128 = x["source"]["128"];
                string mp3_320 = x["source"]["320"];
                //string mp3_ll = x["link_download"]["lossless"];
                string dl_url = "";

                dl_url = mp3_320.Equals(string.Empty) ? mp3_128 : mp3_320;
                fn = fn + (mp3_320.Equals(string.Empty) ? " [128].mp3" : " [320].mp3");

                IDM.sendLink(dl_url, lp, fn);
                Console.WriteLine("#"+ i + ": Send to IDM -> " + fn + "\nDone\n");
                i++;
            }
            return "";
        }

        private string getNum(int i)
        {
            if (i<10)
            {
                return "00" + i;
            }
            else if (i<100)
            {
                return "0" + i;
            }
            else return "" + i;
        }
    }
}
