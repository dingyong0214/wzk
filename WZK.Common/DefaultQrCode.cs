// ==============================================
// Author	  : dingyong
// Create date: 2015-04-21 18:38:18
// Description: DefaultQrCode
// Update	  : 
// ===============================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using ThoughtWorks.QRCode.Codec;
namespace WZK.Common
{
    /// <summary>  
    /// 含有QR码的描述类和包装编码和渲染  
    /// </summary> 
    public class DefaultQrCode
    {
        #region 二维码生成

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="codeNumber">要生成二维码的字符串</param>
        /// <param name="logoUrl">背景图片路径.</param>
        /// <returns>二维码图片</returns>
        /// <remarks>add by dingyong,2015-07-31 09:22:34 </remarks>
        public byte[] CreateImgCode(string codeNumber, string logoUrl)
        {
            int qRCodeVersion = 0;//0代表自动按照字符多少生成适当尺寸的二维码
            int backimageWidth =60;//logo压缩到尺寸
            if(codeNumber.Length<100){
                qRCodeVersion = 7;
                backimageWidth = 50;
            }

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = qRCodeVersion;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Image image = qrCodeEncoder.Encode(codeNumber, System.Text.Encoding.UTF8);
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            image.Save(mStream,ImageFormat.Png);
            System.IO.MemoryStream mStream1 = new System.IO.MemoryStream();
            CombinImage(image, logoUrl, backimageWidth).Save(mStream1,ImageFormat.Png);
            return mStream1.ToArray();
        }

        /// <summary>
        /// 调用此函数后使此两种图片合并，类似相册，有个
        /// 背景图，中间贴自己的目标图片
        /// </summary>
        /// <param name="imgBack">粘贴的源图片</param>
        /// <param name="destImg">粘贴的目标图片</param>
        /// <param name="backimageWidth">背景照片尺寸</param>
        /// <returns>Image.</returns>
        /// <remarks>add by dingyong,2015-07-31 09:24:33 </remarks>
        private Image CombinImage(Image imgBack, string destImg,int backimageWidth)
        {
            Image img = Image.FromFile(destImg);        //logo图片    
            if (img.Height != backimageWidth || img.Width != backimageWidth)
            {
                img = KiResizeImage(img, backimageWidth, backimageWidth);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   

            g.FillRectangle(Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }

        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的图片</returns>
        /// <remarks>add by dingyong,2015-07-31 09:24:38 </remarks>
        private Image KiResizeImage(Image bmp, int newW, int newH)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量  
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 根据背景图片创建二维码.
        /// </summary>
        /// <param name="codeNumber">二维码内容.</param>
        /// <param name="bytes">图片字节.</param>
        /// <returns>System.Byte[].</returns>
        /// <remarks>add by dingyong,2015-07-31 09:24:41 </remarks>
        public byte[] CreateImgCode(string codeNumber, byte[] bytes)
        {
            int qRCodeVersion = 0;//0代表自动按照字符多少生成适当尺寸的二维码
            int backimageWidth = 60;//logo压缩到尺寸
            if (codeNumber.Length < 50)
            {
                qRCodeVersion = 5;
                backimageWidth = 30;
            }

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = qRCodeVersion;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Image image = qrCodeEncoder.Encode(codeNumber, System.Text.Encoding.UTF8);
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            image.Save(mStream, ImageFormat.Png);
            System.IO.MemoryStream mStream1 = new System.IO.MemoryStream();
            CombinImage(image, bytes, backimageWidth).Save(mStream1, ImageFormat.Png);
            return mStream1.ToArray();
        }

        /// <summary>
        /// 调用此函数后使此两种图片合并，类似相册，有个
        /// 背景图，中间贴自己的目标图片
        /// </summary>
        /// <param name="imgBack">粘贴的源图片</param>
        /// <param name="destImg">粘贴的目标图片</param>
        /// <param name="backimageWidth">背景照片尺寸</param>
        /// <returns>Image.</returns>
        /// <remarks>add by dingyong,2015-07-31 09:24:43 </remarks>
        private Image CombinImage(Image imgBack, byte[] destImg, int backimageWidth)
        {
            MemoryStream ms = new MemoryStream(destImg);
            Image img = Image.FromStream(ms);        //logo图片    
            if (img.Height != backimageWidth || img.Width != backimageWidth)
            {
                img = KiResizeImage(img, backimageWidth, backimageWidth);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   

            //g.FillRectangle(Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }
    }
}