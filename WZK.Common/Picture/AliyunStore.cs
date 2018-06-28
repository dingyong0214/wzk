using Aliyun.OpenServices.OpenStorageService;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using WZK.Common.ConfigData;

namespace WZK.Common
{
    /// <summary>
    /// 阿里云OSS上传.
    /// </summary>
    public class AliyunStore
    {
        /// <summary>
        /// The client
        /// </summary>
        protected readonly OssClient Client = null;

        /// <summary>
        /// 根目录.
        /// </summary>
        /// <value>The root directory.</value>
        private string RootDirectory
        {
            set;
            get;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AliyunStore"/> class.
        /// </summary>
        public AliyunStore()
        {
            Client = new OssClient(AliyunConfig.BucketUrl, AliyunConfig.AliyunAccessId, AliyunConfig.AliyunAccessKey);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AliyunStore"/> class.
        /// </summary>
        /// <param name="rootDirectory">The root directory.</param>
        public AliyunStore(string rootDirectory)
            : this()
        {
            this.RootDirectory = rootDirectory;
        }

        #region 上传文件
        /// <summary>
        /// 上传文件.
        /// </summary>
        public string Upload(byte[] fs, string extension)
        {
            string name = this.GetFileName(extension);
            ObjectMetadata metadata = new ObjectMetadata { ContentType = this.GetContentType(extension) };
            Client.PutObject(AliyunConfig.BucketName, name, new MemoryStream(fs), metadata);
            return this.GetUrl(name);
        } 
        #endregion

        #region 得到文件类型
        /// <summary>
        /// 得到文件类型
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by ****,2015-12-29 16:15:35 </remarks>
        private string GetContentType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".gif":
                    return "image/gif";
                case ".png":
                    return "image/png";
                case ".jpeg":
                    return "image/jpeg";
                case ".jpg":
                    return "image/jpeg";
                case ".jpe":
                    return "image/jpeg";
                case ".bmp":
                    return "application/x-bmp";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/msword";
                case ".pdf":
                    return "application/pdf";
                case ".ppt":
                    return "application/x-ppt";
                case ".pptx":
                    return "application/x-ppt";
                case ".xls":
                    return "application/x-xls";
                case ".xlsx":
                    return "application/x-xls";
                case ".amr":
                    return "audio/amr";
                default:
                    return null;
            }
        } 
        #endregion

        #region 删除目录
        /// <summary>
        /// 删除目录.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>add by ****,2015-12-04 16:29:15</remarks>
        public void DeleteDirectory(string path)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                ObjectListing objList = Client.ListObjects(AliyunConfig.BucketName, path);
                foreach (var obj in objList.ObjectSummaries)
                {
                    Client.DeleteObject(AliyunConfig.BucketName, obj.Key);
                }
            });
        } 
        #endregion

        #region 上传文件到指定目录
        /// <summary>
        /// 上传文件到指定目录.
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <param name="path">上传路径.</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2015-06-26 16:30:24 </remarks>
        public string UploadToPath(byte[] fs, string path)
        {
            ObjectMetadata metadata = new ObjectMetadata { ContentType = this.GetContentType(System.IO.Path.GetExtension(path)) };
            Client.PutObject(AliyunConfig.BucketName, path, new MemoryStream(fs), metadata);

            return this.GetUrl(path);
        } 
        #endregion

        #region 获取存储文件名
        /// <summary>
        /// 获取存储文件名.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>System.String.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GetFileName(string extension = null, string path = null)
        {
            DateTime time = DateTime.Now;
            string savePath = path ?? time.ToString("yyyy/MM/dd/");

            return string.Format("{3}{0}{1}{2}", savePath, time.ToString("yyyyMMddHHmmssfff"), extension, this.RootDirectory == null ? null : this.RootDirectory + "/");
        } 
        #endregion

        #region 获取图片路径
        /// <summary>
        /// 获取图片路径.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        protected string GetUrl(string name)
        {
            return AliyunConfig.AliyunDomain + name;
        } 
        #endregion
    }
}
