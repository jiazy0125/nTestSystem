using System;
using System.IO;
using System.Drawing;

namespace nTestSystem.Framework.Extensions
{
	public static class ImageExtentions
	{

        /// <summary>
        /// Base64String转本地图片
        /// </summary>
        /// <param name="buffer">Base64String</param>
        /// <returns></returns>
        public static Image StringToImage(this Image im, string buffer)
         {
            
			MemoryStream ms = new MemoryStream(Convert.FromBase64String(buffer));
			Image image = Image.FromStream(ms);

			return image;
		}
        /// <summary>
        /// 本地图片文件转Base64字符串
        /// </summary>
        /// <param name="imagepath">本地文件路径</param>
        /// <returns>Base64String</returns>
        public static string ImageToString(this Image im, string imagepath)
         {
			FileStream fs = new FileStream(imagepath, FileMode.Open);
			byte[] byData = new byte[fs.Length];
			fs.Read(byData, 0, byData.Length);
			fs.Close();
			return Convert.ToBase64String(byData);
		} 

	}
}
