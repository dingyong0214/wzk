using System.Collections.Generic;
using System.IO;
using System.Net;

namespace WZK.Common
{
    /// <summary>
    /// 阿里云储存.
    /// </summary>
    public class AliyunImageStore :AliyunStore
    {
        public AliyunImageStore(string rootDirectory)
            : base(rootDirectory)
        {

        }

        #region 图片上传并压缩到指定大小
        /// <summary>
        /// 图片上传并压缩到指定大小.
        /// </summary>
        /// <param name="fs">文件字节.</param>
        /// <param name="maxWidth">最大宽度.</param>
        /// <param name="maxHeght">最大高度.</param>
        /// <param name="extension">文件后缀名</param>
        /// <returns>String.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>add by ****,2015-05-22 14:13:24</remarks>
        public string Resize(byte[] fs, int maxWidth, int maxHeght, string extension)
        {
            fs = ImageProcessor.Resize(fs, maxWidth, maxHeght, extension);

            return base.Upload(fs, extension);
        } 
        #endregion

        //#region 图片剪切
        ///// <summary>
        ///// 图片剪切.
        ///// </summary>
        ///// <param name="imagePath">原来图片位置.</param>
        ///// <param name="x">The x.</param>
        ///// <param name="y">The y.</param>
        ///// <param name="w">The w.</param>
        ///// <param name="h">The h.</param>
        ///// <param name="imageSize">图像缩放比例.</param>
        ///// <returns>String.</returns>
        ///// <exception cref="System.NotImplementedException"></exception>
        ///// <remarks>add by ****,2015-05-22 14:13:20</remarks>
        //public string[] Crop(string imagePath, int x, int y, int w, int h, Dictionary<int, int> sizes = null)
        //{
        //    using (WebClient webClient = new WebClient())
        //    {
        //        string name = base.GetFileName(),
        //            extension = Path.GetExtension(imagePath);

        //        byte[] fs = webClient.DownloadData(imagePath);

        //        fs = ImageProcessor.Crop(fs, x, y, w, h);

        //        if (sizes != null)
        //        {
        //            List<string> imageUrls = new List<string>();
        //            string imgUrl = null;

        //            foreach (var size in sizes)
        //            {
        //                imgUrl = name + "_" + size.Key + "_" + size.Value + extension;
        //                byte[] image = ImageProcessor.Resize(fs, size.Key, size.Value, extension);

        //                base.UploadToPath(image, imgUrl, extension);

        //                imageUrls.Add(base.GetUrl(imgUrl));
        //            }
        //            return imageUrls.ToArray();
        //        }
        //        return new string[] { name + Path.GetExtension(imagePath) };
        //    }
        //} 
        //#endregion
    }
}
