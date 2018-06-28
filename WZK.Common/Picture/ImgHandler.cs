using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Helpers;

namespace WZK.Common
{
    public class ImgHandler
    {
        private static ImgHandler _instance = new ImgHandler();
        public static ImgHandler Instance
        {
            get
            {
                return _instance;
            }
        }

        #region 参数
        /// <summary>
        /// 文件夹
        /// </summary>
        private string savePath = DateTime.Today.ToString("yyyy/MM/dd/");
        /// <summary>
        /// 文件名 - 不重复
        /// </summary>
        private string fileName = "";
        /// <summary>
        /// 文件后缀名
        /// </summary>
        private string extensions = ".jpg";
        #endregion

        #region 上传图片  
        /// <summary>
        /// 上传图片.
        /// </summary>
        /// <param name="picture">图片对象.</param>
        /// <param name="type">图片类型 1.用户头像 2.身份证图片 3.自动回复 4.朋友圈  5.图片分析，6.手机截频.</param>
        /// <returns>图片全路径</returns>
        /// <remarks>add by dingyong,2016-09-02 10:02:32 </remarks>
        public string UploadImg(WebImage picture, int type)
        {
            fileName = DateTime.Now.ToFileTime().ToString() + StrRandom(4, true);
            string typeName = "WZK/Common";
            switch (type)
            {
                case 1:
                    typeName = "WZK/Avatar";
                    break;
                case 2:
                    typeName = "WZK/Idcard";
                    break;
                case 3:
                    typeName = "WZK/AutoReply";
                    break;
                case 4:
                    typeName = "WZK/Moments"; 
                    break;
                case 5:
                    typeName = "WZK/Analysis"; 
                    break;
                case 6:
                    typeName = "WZK/ScreenCapture";
                    break;
            }
            string key = string.Format(@"" + typeName + "/{0}{1}", savePath + "original/original_", fileName);   //相对路径
            AliyunStore store = new AliyunStore();
            int y = 1;
            string sourcePictureUrl = store.UploadToPath(picture.GetBytes(), key + extensions);
            if (!string.IsNullOrWhiteSpace(sourcePictureUrl))   //图片有返回地址
            {
                var picSize = new List<dynamic>();
                switch (type)
                {
                    case 1:      //用户头像
                        picSize.Add(new { width = 100, height = 100 });
                        picSize.Add(new { width = 200, height = 200 });
                        break;
                    case 2:    //身份证认证图片
                        break;
                    case 3:  //自动回复
                        break;
                    case 4:   //朋友圈
                        break;
                    case 5:   //图片分析
                        break;
                    case 6:
                        break;
                }
                foreach (var item in picSize)//上传缩略图
                {
                    //压缩图片
                    var newimageByte = ImageProcessor.Resize(picture.GetBytes(), item.width, item.height, "");
                    string syPictureUrl = store.UploadToPath(newimageByte, key.Replace("original/original", item.width + "_" + item.height+"/"+ item.width + "_" + item.height+"") + extensions);
                    if (type == 6 && item.width == 800 && item.height == 800)   //添加水印（暂时去掉此功能）
                    {
                        if (picture != null)
                        {
                            //水印图片
                            using (WebClient MyWebClient = new WebClient())
                            {
                                Byte[] pageData = MyWebClient.DownloadData(@"http://thdingcan.oss-cn-shanghai.aliyuncs.com/Watermark/watermark.jpg");
                                WebImage mask = new WebImage(pageData);
                                picture.AddImageWatermark(
                                    mask,
                                    width: mask.Width,
                                    height: mask.Height,
                                    horizontalAlign: "Center",
                                    verticalAlign: "Middle",
                                    opacity: 100,   //透明度
                                    padding: 0
                                );
                                picture.Crop(0, 0, 1, 1);
                                store.UploadToPath(picture.GetBytes(), key.Replace("original/original", item.width + "_" + item.height + "/" + item.width + "_" + item.height + "") + extensions);
                            }
                        }
                    }
                }
            }
            return sourcePictureUrl;
        }
        #endregion

        #region 上传图片
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="picture">文件输入流</param>
        /// <param name="type">图片类型 1.用户头像 2.身份证图片 3.自动回复 4.朋友圈  5.图片分析，6.手机截频.</param>
        /// <returns>图片全路径</returns>
        /// <remarks>add by dingyong,2016-09-02 09:51:36 </remarks>
        public string UploadImg(Stream picture, int type)
        {
            fileName = DateTime.Now.ToFileTime().ToString() + StrRandom(4, true);
            byte[] bytes = new byte[picture.Length];
            string typeName = "WZK/Common";
            switch (type)
            {
                case 1:
                    typeName = "WZK/Avatar";
                    break;
                case 2:
                    typeName = "WZK/Idcard";
                    break;
                case 3:
                    typeName = "WZK/AutoReply";
                    break;
                case 4:
                    typeName = "WZK/Moments";
                    break;
                case 5:
                    typeName = "WZK/Analysis";
                    break;
                case 6:
                    typeName = "WZK/ScreenCapture";
                    break;
            }
            string key = string.Format(@"" + typeName + "/{0}{1}", savePath + "original/original_", fileName);   //相对路径
            picture.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            picture.Seek(0, SeekOrigin.Begin);
            AliyunStore store = new AliyunStore();
            string sourcePictureUrl = store.UploadToPath(bytes, key + extensions);
            if (!string.IsNullOrWhiteSpace(sourcePictureUrl))   //图片有返回地址
            {
                var picSize = new List<dynamic>();
                switch (type)
                {
                    case 1:      //用户头像
                        picSize.Add(new { width = 100, height = 100 });
                        picSize.Add(new { width = 200, height = 200 });
                        break;
                    case 2:    //身份证认证图片
                        break;
                    case 3:  //自动回复
                        break;
                    case 4:   //朋友圈
                        break;
                    case 5:   //图片分析
                        break;
                    case 6:
                        break;
                }
                foreach (var item in picSize)//上传缩略图
                {
                    //压缩图片
                    var newimageByte = ImageProcessor.Resize(bytes, item.width, item.height, "");
                    string syPictureUrl = store.UploadToPath(newimageByte, key.Replace("original/original", item.width + "_" + item.height + "/" + item.width + "_" + item.height + "") + extensions);
                    if (type == 6 && item.width == 800 && item.height == 800)   //添加水印
                    {
                        WebImage pic = new WebImage(new WebClient().DownloadData(syPictureUrl));   //800的商品放大图加水印
                        if (pic != null)
                        {
                            //水印图片
                            using (WebClient MyWebClient = new WebClient())
                            {
                                Byte[] pageData = MyWebClient.DownloadData(@"http://thdingcan.oss-cn-shanghai.aliyuncs.com/Watermark/watermark.jpg");
                                WebImage mask = new WebImage(pageData);
                                pic.AddImageWatermark(
                                    mask,
                                    width: item.width,
                                    height: item.height,
                                    horizontalAlign: "Center",
                                    verticalAlign: "Middle",
                                    opacity: 100,   //透明度
                                    padding: 0
                                );
                                pic.Crop(0, 0, 1, 1);
                                store.UploadToPath(pic.GetBytes(), key.Replace("original/original", item.width + "_" + item.height + "/" + item.width + "_" + item.height + "") + extensions);
                            }
                        }
                    }
                }
            }
            return sourcePictureUrl;
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="url">要删除的文件路径</param>
        public void DelectPhoto(string url)
        {
            if (File.Exists(url))
            {
                File.Delete(url);
            }
        }
        #endregion

        #region 获取自定义尺码图片路径        
        /// <summary>
        ///获取自定义尺码图片路径
        /// </summary>
        /// <param name="imgPath">图片路径.</param>
        /// <param name="size">1.用户头像 100_100,200_200  2.自动回复 100_100  3.朋友圈 170_170  4.图片分析 100_100</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-09-02 10:10:56 </remarks>
        public string GetImgBySize(string imgPath,string size)
        {
            return imgPath.Replace("original/original", size.Trim() + "/"+ size.Trim());
        }
        #endregion

        #region 获取图片
        /// <summary>
        /// 获取图片.
        /// </summary>
        /// <returns>WebImage.</returns>
        /// <remarks>add by dingyong,2016-09-02 10:37:33 </remarks>
        public WebImage GetImageFromRequest()
        {
            var request = System.Web.HttpContext.Current.Request;

            if (request.Files.Count == 0)
            {
                return null;
            }

            else if (request.Files[0].ContentType.ToLower().Contains("image") == false)
            {
                //showError("文件只能是正确的图片文件,注意你的文件是否含有病毒威胁！");
                return null;
            }
            try
            {
                var postedFile = request.Files[0];
                var image = new WebImage(postedFile.InputStream)
                {
                    FileName = postedFile.FileName
                };
                return image;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取多张图片
        /// <summary>
        /// 获取多张图片.
        /// </summary>
        /// <returns>WebImage.</returns>
        /// <remarks>add by dingyong,2016-09-02 10:37:33 </remarks>
        public List<WebImage> GetMuchImageFromRequest()
        {
            var request = System.Web.HttpContext.Current.Request;

            if (request.Files.Count == 0)
            {
                return null;
            }
            try
            {
                List<WebImage> image = new List<WebImage>();
                var postedFile = request.Files;
                for(int i=0;i<postedFile.Count;i++)
                {
                    if (i >= 9)
                        break;
                    if(postedFile[i].ContentType.ToLower().Contains("image") == false)
                        continue;
                    image.Add(new WebImage(postedFile[i].InputStream)
                    {
                        FileName = postedFile[i].FileName
                    });
                }
                return image;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 生成随机字母与数字
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-08-31 19:37:18 </remarks>
        public string StrRandom(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        } 
        #endregion
    }
}
