using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace WZK.Common
{
    /// <summary>
    /// 微信二维码处理方法
    /// </summary>
    public class QRCodeHelper
    {
        /// <summary>
        /// 嵌入二维码里的logo物理地址
        /// </summary>
        public static string logoUrl = System.Web.HttpContext.Current.Server.MapPath("~/skin/img/sy.png");

        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">微信支付链接地址</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-02-23 16:18 </remarks>
        public static Bitmap Create(string content)
        {
            try
            {
                QRCodeEncoder qRCodeEncoder = new QRCodeEncoder();
                qRCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//设置二维码编码格式 
                qRCodeEncoder.QRCodeScale = 4;//设置编码测量度             
                qRCodeEncoder.QRCodeVersion = 0;//设置编码版本   
                qRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//设置错误校验 

                Bitmap image = qRCodeEncoder.Encode(content);
                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 生成带logo二维码
        /// <summary>
        /// 生成带logo二维码
        /// </summary>
        /// <param name="content">微信支付链接地址</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-02-23 16:43 </remarks>
        public static Bitmap CreateQRCodeWithLogo(string content)
        {
            //生成二维码
            Bitmap qrcode = Create(content);

            //生成logo
            Bitmap logo = GetLocalLog();
            Bitmap finalImage = MergeQrImg(qrcode, logo);
            return finalImage;
        }
        #endregion

        #region 生成二维码返回二进制流
        /// <summary>
        /// 生成二维码返回二进制流.
        /// </summary>
        /// <param name="un">要生成二维码的Url字符串 (无需对Url字符串进行编码).</param>
        /// <returns>二维码二进制流.</returns>
        /// <remarks>add by dingyong,2017-02-23 17:05 </remarks>
        public byte[] MakeWeChatQRCode(string un)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 7;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Bitmap image = qrCodeEncoder.Encode(un);

            try
            {
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                image.Dispose();
            }
        }
        #endregion

        #region 处理方法
        /// <summary>
        /// 获取本地图片
        /// </summary>
        /// <returns></returns>
        private static Bitmap GetLocalLog()
        {
            Bitmap newBmp = new Bitmap(logoUrl);
            return newBmp;
        }

        /// <summary>
        /// 合并用户QR图片和用户头像
        /// </summary>
        /// <param name="qrImg">QR图片</param>
        /// <param name="headerImg">用户头像</param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Bitmap MergeQrImg(Bitmap qrImg, Bitmap headerImg, double n = 0.23)
        {
            int margin = 10;
            float dpix = qrImg.HorizontalResolution;
            float dpiy = qrImg.VerticalResolution;
            var _newWidth = (10 * qrImg.Width - 36 * margin) * 1.0f / 36;
            var _headerImg = ZoomPic(headerImg, _newWidth / headerImg.Width);
            //处理头像
            int newImgWidth = _headerImg.Width + margin;
            Bitmap headerBgImg = new Bitmap(newImgWidth, newImgWidth);
            headerBgImg.MakeTransparent();
            Graphics g = Graphics.FromImage(headerBgImg);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            Pen p = new Pen(new SolidBrush(Color.White));
            Rectangle rect = new Rectangle(0, 0, newImgWidth - 1, newImgWidth - 1);
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, 1))
            {
                g.DrawPath(p, path);
                g.FillPath(new SolidBrush(Color.White), path);
            }
            //画头像
            Bitmap img1 = new Bitmap(_headerImg.Width, _headerImg.Width);
            Graphics g1 = Graphics.FromImage(img1);
            g1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g1.Clear(Color.Transparent);
            Pen p1 = new Pen(new SolidBrush(Color.Gray));
            Rectangle rect1 = new Rectangle(0, 0, _headerImg.Width - 1, _headerImg.Width - 1);
            using (GraphicsPath path1 = CreateRoundedRectanglePath(rect1, 1))
            {
                g1.DrawPath(p1, path1);
                TextureBrush brush = new TextureBrush(_headerImg);
                g1.FillPath(brush, path1);
            }
            g1.Dispose();
            PointF center = new PointF((newImgWidth - _headerImg.Width) / 2, (newImgWidth - _headerImg.Height) / 2);
            g.DrawImage(img1, center.X, center.Y, _headerImg.Width, _headerImg.Height);
            g.Dispose();
            Bitmap backgroudImg = new Bitmap(qrImg.Width, qrImg.Height);
            backgroudImg.MakeTransparent();
            backgroudImg.SetResolution(dpix, dpiy);
            headerBgImg.SetResolution(dpix, dpiy);
            Graphics g2 = Graphics.FromImage(backgroudImg);
            g2.Clear(Color.Transparent);
            g2.DrawImage(qrImg, 0, 0);
            PointF center2 = new PointF((qrImg.Width - headerBgImg.Width) / 2, (qrImg.Height - headerBgImg.Height) / 2);
            g2.DrawImage(headerBgImg, center2);
            g2.Dispose();
            return backgroudImg;
        }

        /// <summary>
        /// 图片按比例缩放
        /// </summary>
        private static Image ZoomPic(Image initImage, double n)
        {
            //缩略图宽、高计算
            double newWidth = initImage.Width;
            double newHeight = initImage.Height;
            newWidth = n * initImage.Width;
            newHeight = n * initImage.Height;
            //生成新图
            //新建一个bmp图片
            System.Drawing.Image newImage = new System.Drawing.Bitmap((int)newWidth, (int)newHeight);
            //新建一个画板
            System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);
            //设置质量
            newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //置背景色
            newG.Clear(Color.Transparent);
            //画图
            newG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
            newG.Dispose();
            return newImage;
        }

        /// <summary>
        /// 创建圆角矩形
        /// </summary>
        /// <param name="rect">区域</param>
        /// <param name="cornerRadius">圆角角度</param>
        /// <returns></returns>
        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            //下午重新整理下，圆角矩形
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        } 
        #endregion
    }
}
