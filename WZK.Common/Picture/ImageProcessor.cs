using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace WZK.Common
{
    /// <summary>  
    /// 图像文件的类型  
    /// </summary>  
    public enum ImageType
    {
        None = 0,
        Bmp = 0x4D42,
        Jpg = 0xD8FF,
        Gif = 0x4947,
        Pcx = 0x050A,
        Png = 0x5089,
        Psd = 0x4238,
        Ras = 0xA659,
        Sgi = 0xDA01,
        Tiff = 0x4949
    }
}

namespace WZK.Common
{
    /// <summary>
    /// 图片处理.
    /// </summary>
    public static class ImageProcessor
    {
        /// <summary>
        /// The image tag
        /// </summary>
        private static readonly SortedDictionary<int, ImageType> imageTag;

        static ImageProcessor()
        {
            imageTag = InitImageTag();
        }

        #region 加密图片

        /// <summary>
        /// 加密图片
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encryptKey">The encrypt key.</param>
        /// <returns>System.Byte[].</returns>
        /// <remarks>add by ****2014-12-10 09:20:40 </remarks>
        public static byte[] Encrypt(byte[] stream, string encryptKey)
        {
            using (DESCryptoServiceProvider des = GetDESCrypto(encryptKey))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(stream, 0, stream.Length);
                        cs.FlushFinalBlock();

                        return ms.ToArray();
                    }
                }
            }
        }

        #endregion

        #region 解密图片

        /// <summary>
        /// 解密图片.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="decryptKey">The decrypt key.</param>
        /// <returns>System.Byte[].</returns>
        /// <remarks>add by ****,2015-12-03 16:11:08 </remarks>
        public static byte[] Decrypt(byte[] stream, string decryptKey)
        {
            //通过des解密
            using (DESCryptoServiceProvider des = GetDESCrypto(decryptKey))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(stream, 0, stream.Length);
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }

        #endregion

        #region 压缩图片

        /// <summary>
        /// 调整图片大小，如果要求的尺寸比上传的图片尺寸大，则按上传的图片的最大尺寸比例来裁剪
        /// </summary>
        /// <param name="stream">图片文件流.</param>
        /// <param name="width">宽度.</param>
        /// <param name="height">高度.</param>
        /// <returns>System.Byte[].</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>add by ****2014-12-10 09:25:12 </remarks>
        public static byte[] Resize(byte[] stream, int width, int height, string extension)
        {
            using (Bitmap originBitmap = new Bitmap(new MemoryStream(stream)))
            {
                if (string.IsNullOrWhiteSpace(extension))
                {
                    if (originBitmap.RawFormat.Equals(ImageFormat.Png))
                    {
                        extension = "png";
                    }
                    else if (originBitmap.RawFormat.Equals(ImageFormat.Jpeg))
                    {
                        extension = "jpg";
                    }
                    else if (originBitmap.RawFormat.Equals(ImageFormat.Gif))
                    {
                        extension = "gif";
                    }
                    else if (originBitmap.RawFormat.Equals(ImageFormat.Bmp))
                    {
                        extension = "bmp";
                    }
                }

                decimal scale = 1;
                if (originBitmap.Width >= originBitmap.Height)//宽大于高
                {
                    if (originBitmap.Width >= width)
                    {
                        scale = Math.Round(height * 1m / originBitmap.Height, 5);
                    }                  
                }
                else//竖图
                {
                    if (originBitmap.Height >= height)
                    {
                        scale = Math.Round(width * 1m / originBitmap.Width, 5);
                    }
                }
                width = Convert.ToInt16(originBitmap.Width * scale);
                height = Convert.ToInt16(originBitmap.Height * scale);

                using (var newImage = new Bitmap(width, height))
                {
                    using (var graphic = GetGraphic(originBitmap, newImage))
                    {
                        graphic.DrawImage(originBitmap, 0, 0, width, height);
                        using (var encoderParameters = new EncoderParameters(1))
                        {
                            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                ImageCodecInfo imageCode = ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FilenameExtension.Contains(extension.ToUpperInvariant()));
                                newImage.Save(ms, imageCode, encoderParameters);

                                return ms.ToArray();
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region 剪切图片返回最大长宽

        /// <summary>
        /// 剪切图片返回最大长宽.
        /// </summary>
        /// <param name="stream">图片文件流.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <returns>System.Byte[].</returns>
        /// <remarks>add by ****2014-12-10 09:22:25 </remarks>
        public static byte[] Crop(byte[] stream, int x, int y, int w, int h)
        {
            using (var originalImage = new Bitmap(new MemoryStream(stream)))
            {
                using (Bitmap bmp = new Bitmap(w, h, originalImage.PixelFormat))
                {
                    bmp.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);
                    using (Graphics graphic = Graphics.FromImage(bmp))
                    {
                        graphic.SmoothingMode = SmoothingMode.AntiAlias;
                        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphic.DrawImage(originalImage, new Rectangle(0, 0, w, h), x, y, w, h, GraphicsUnit.Pixel);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            bmp.Save(ms, originalImage.RawFormat);
                            return ms.ToArray();
                        }
                    }
                }
            }
        }

        #endregion

        #region 获取图片格式

        /// <summary>
        /// 获取图片格式.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>ImageType.</returns>
        /// <remarks>add by ****2014-12-13 15:06:27 </remarks>
        public static ImageType GetImageType(Stream image)
        {
            byte[] buf = new byte[2];
            try
            {
                StreamReader sr = new StreamReader(image, Encoding.UTF32);

                int i = sr.BaseStream.Read(buf, 0, buf.Length);
                if (i != buf.Length)
                {
                    return ImageType.None;
                }
            }
            catch (Exception exc)
            {
                return ImageType.None;
            }
            return CheckImageType(buf);
        }

        #endregion

        #region private

        /// <summary>
        /// 获取图形
        /// </summary>
        /// <param name="originImage">The origin image.</param>
        /// <param name="newImage">The new image.</param>
        /// <returns>Graphics.</returns>
        /// <remarks>add by ****,2015-07-08 14:24:32 </remarks>
        private static Graphics GetGraphic(Image originImage, Bitmap newImage)
        {
            newImage.SetResolution(originImage.HorizontalResolution, originImage.VerticalResolution);
            var graphic = Graphics.FromImage(newImage);
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            return graphic;
        }

        /// <summary>
        /// Gets the DES.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>DESCryptoServiceProvider.</returns>
        private static DESCryptoServiceProvider GetDESCrypto(string key)
        {
            //通过des加密
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //计算指定字节组指定区域哈希值
            using (SHA1 ha = new SHA1Managed())
            {
                //获得加密字符串二进制字符
                byte[] keyByteArray = Encoding.Default.GetBytes(key);

                byte[] hb = ha.ComputeHash(keyByteArray);
                //加密密钥数组
                byte[] sKey = new byte[8];
                //加密变量
                byte[] sIV = new byte[8];
                for (int i = 0; i < 8; i++)
                    sKey[i] = hb[i];
                for (int i = 8; i < 16; i++)
                    sIV[i - 8] = hb[i];

                //获取加密密钥
                des.Key = sKey;
                //设置加密初始化向量
                des.IV = sIV;
                return des;
            }
        }

        /// <summary>  
        /// 通过文件的前两个自己判断图像类型  
        /// </summary>  
        /// <param name="buf">至少2个字节</param>  
        /// <returns></returns>  
        private static ImageType CheckImageType(byte[] buf)
        {
            if (buf == null || buf.Length < 2)
            {
                return ImageType.None;
            }

            int key = (buf[1] << 8) + buf[0];
            ImageType s;
            if (imageTag.TryGetValue(key, out s))
            {
                return s;
            }
            return ImageType.None;
        }

        /// <summary>
        /// 初始化图片标签.
        /// </summary>
        /// <returns>SortedDictionary&lt;System.Int32, ImageType&gt;.</returns>
        private static SortedDictionary<int, ImageType> InitImageTag()
        {
            SortedDictionary<int, ImageType> list = new SortedDictionary<int, ImageType>();

            list.Add((int)ImageType.Bmp, ImageType.Bmp);
            list.Add((int)ImageType.Jpg, ImageType.Jpg);
            list.Add((int)ImageType.Gif, ImageType.Gif);
            list.Add((int)ImageType.Pcx, ImageType.Pcx);
            list.Add((int)ImageType.Png, ImageType.Png);
            list.Add((int)ImageType.Psd, ImageType.Psd);
            list.Add((int)ImageType.Ras, ImageType.Ras);
            list.Add((int)ImageType.Sgi, ImageType.Sgi);
            list.Add((int)ImageType.Tiff, ImageType.Tiff);
            return list;
        }

        #endregion
    }
}
