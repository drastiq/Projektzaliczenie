using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.IO;

namespace Projektzaliczenie
{
    public class ftpManager
    {
        public static string nazwaServer;
        public static string nazwaUzytkownika;
        public static string haslo;
       

        private FtpWebRequest ftpRequest=null;
        private FtpWebResponse ftpResponse=null;
        private Stream ftpStrteam=null;
        private int buffer = 2048;

        public ftpManager(string _nazwaServ, string _nazwaUzytkownika, string _haslo)
        {
            nazwaServer = _nazwaServ;
            nazwaUzytkownika = _nazwaUzytkownika;
            haslo = _haslo;
        }
        public void fileRename(string oldName,string newName)
        {
            try
            {

                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(nazwaServer + "/" + oldName);
                ftpRequest.Credentials = new NetworkCredential(nazwaUzytkownika, haslo);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                ftpRequest.RenameTo = newName;


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void downloadFile(string servFile, string localFile) {
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(nazwaServer + "/" + servFile);
                //ftpRequest.UseDefaultCredentials = true;
                ftpRequest.Credentials = new NetworkCredential(nazwaUzytkownika, haslo);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive=true;
                ftpRequest.KeepAlive = true;  
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpStrteam = ftpResponse.GetResponseStream();
                FileStream fs = new FileStream(localFile, FileMode.OpenOrCreate);
                byte[] byteBuffer = new byte[Convert.ToUInt32(getFileSize(servFile))];
                int bytesRead = ftpStrteam.Read(byteBuffer, 0, Convert.ToInt32(getFileSize(servFile)));
                try
                {
                    while (bytesRead > 0)
                    {
                        fs.Write(byteBuffer, 0, bytesRead);
                        bytesRead = ftpStrteam.Read(byteBuffer, 0, Convert.ToInt32(getFileSize(servFile)));
                    }
                }
                catch (Exception e)
                {
                    throw e;      
                }
                fs.Close();
                ftpStrteam.Close();
                ftpResponse.Close();
                ftpRequest = null;





            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void uploadFile(string localFile,string servFile)
        {
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(nazwaServer + "/" + servFile);
                ftpRequest.Credentials = new NetworkCredential(nazwaUzytkownika, haslo);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream localFileStream = new FileStream(localFile, FileMode.Open);
                byte[] byteBuffer = new byte[localFileStream.Length];
                int byteSend = localFileStream.Read(byteBuffer, 0, Convert.ToInt32( localFileStream.Length));

                try
                {
                    while (byteSend != -1)
                    {
                        ftpStrteam.Write(byteBuffer, 0, byteSend);
                        byteSend = localFileStream.Read(byteBuffer, 0, Convert.ToInt32(localFileStream.Length));
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
                ftpResponse.Close();
                ftpStrteam.Close();
                localFileStream.Close();
                ftpRequest = null;
            }

            catch (Exception e)
            {

                throw;
            }
        }



        public long getFileSize(string filename)
        {
            long size;
            FtpWebRequest sizeRequest = (FtpWebRequest)FtpWebRequest.Create(nazwaServer + "/" + filename);
            sizeRequest.Credentials = new NetworkCredential(nazwaUzytkownika, haslo);
            sizeRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            sizeRequest.UseBinary = true;

            FtpWebResponse servResponse = (FtpWebResponse)sizeRequest.GetResponse();
            FtpWebResponse respSize = (FtpWebResponse)sizeRequest.GetResponse();
            size = respSize.ContentLength;
            return size;
        } 
        
          



    }
}
