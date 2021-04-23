using System;
using System.Drawing;
using System.IO;

namespace nTestSystem.Framework.Commons
{
	public class ImageHelper
	{
        public static string ImageToString(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] bt = new byte[fs.Length];
                fs.Read(bt, 0, bt.Length);
                fs.Close();
                return Convert.ToBase64String(bt);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool StringToImage(string pic, string targetPath)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(pic);
                //读入MemoryStream对象
                MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                memoryStream.Write(imageBytes, 0, imageBytes.Length);
                //转成图片
                Image image = Image.FromStream(memoryStream);
                memoryStream.Close();
                image.Save(targetPath);
                return true;
                
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
