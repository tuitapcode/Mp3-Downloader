using IDManLib;

namespace IDMLib
{
    public class IDM
    {
        public static int LOI_URL = -1;
        public static int LOI_LOCAL_PATH = -2;
        public static int LOI_LOCAL_FILE_NAME = -3;
        public static int DOWNLOAD_OK = 1;

        public int sendLink(string Url, string LocalPath, string LocalFileName)
        {
            if (Url.Equals(string.Empty))
            {
                return LOI_URL;
            }

            if (LocalPath.Equals(string.Empty))
            {
                return LOI_LOCAL_PATH;
            }

            if (LocalFileName.Equals(string.Empty))
            {
                return LOI_LOCAL_FILE_NAME;
            }

            string bstrUrl = Url; //Link cần download
            string bstrReferer = "";
            string bstrCookies = "";
            string bstrData = "";
            string bstrUser = "";
            string bstrPassword = "";

            string bstrLocalPath = LocalPath; //Thư mục lưu file

            string bstrLocalFileName = LocalFileName; //Tên file (unicode)

            //Flags, can be zero or a combination of the following values: 
            //  1 - do not show any confirmations dialogs;
            //  2 - add to queue only, do not start downloading.
            int lFlags = 2;

            CIDMLinkTransmitter idm = new CIDMLinkTransmitter();
            idm.SendLinkToIDM(bstrUrl, bstrReferer, bstrCookies, bstrData, bstrUser, bstrPassword, bstrLocalPath, bstrLocalFileName, lFlags);
            return DOWNLOAD_OK;
        }
    }
}
