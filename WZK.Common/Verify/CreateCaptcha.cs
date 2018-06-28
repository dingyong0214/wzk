using System;
using System.Drawing;
using System.IO;
using System.Web.SessionState;
using System.Collections;
using System.Web;

namespace WZK.Common
{
    /// <summary>
    /// 图形验证码相关
    /// author:dingyong
    /// createTime:2015-04-21
    /// </summary>
    public class CreateCaptcha
    {
        private static string _sessionId = "code";
        private static Color _backgroundColor = Color.White;      //背景色
        private static Color[] color = { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.DarkBlue };
        private static string _randomChars;
        private static Random random = new Random();    //随机生成的验证码
        private static int _randomAngle = 40;                          //随机码的旋转角度
        private static int _length = 4;                 //验证码长度
        private static int _fontSize = 20;              //字体最大尺寸
        private static int _chaosNumber = 3;                           //噪点数量 ,0  时不用
        //字体列表，用于验证码 
        private static string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
        //验证码的字符集，去掉了一些容易混淆的字符 
        private static char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
        //字体形状
        private static FontStyle[] fontStyleAll = { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, FontStyle.Strikeout, FontStyle.Underline };

        private static HttpSessionState Session = HttpContext.Current.Session;
        #region 创建随机码图片
        ///  <summary>
        ///  创建随机码图片
        ///  </summary>
        ///  <param  name="context"></param>
        public byte[] CreateImage(int width = 110, int height = 25)
        {
            CreateCode(_sessionId);          //创建验证码
            Bitmap map = new Bitmap(width, height);                            //创建图片背景
            Graphics graph = Graphics.FromImage(map);
            try
            {
                graph.Clear(_backgroundColor);              //清除画面，填充背景
                Point dot = new Point(20, 12);
                //CreaetForeChaos(map);              //前景色噪点
                //文字距中
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                //验证码旋转，防止机器识别
                char[] chars = _randomChars.ToCharArray();            //拆散字符串成单字符数组

                SolidBrush solidBrush = new SolidBrush(color[random.Next(color.Length)]);

                for (int i = 0; i < chars.Length; i++)
                {
                    Font fontstyle = new Font(font[random.Next(font.Length)], random.Next(_fontSize - 3, _fontSize), fontStyleAll[random.Next(fontStyleAll.Length)]);            //字体样式
                    float angle = random.Next(-_randomAngle, _randomAngle);            //转动的度数
                    graph.TranslateTransform(dot.X, dot.Y);          //移动光标到指定位置
                    graph.RotateTransform(angle);
                    graph.DrawString(chars[i].ToString(), fontstyle, solidBrush, -2, 2, format);
                    graph.RotateTransform(-angle);                    //转回去
                    graph.TranslateTransform(2, -dot.Y);          //移动光标到指定位置
                }
                //生成图片
                MemoryStream ms = new MemoryStream();
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
            finally
            {
                graph.Dispose();
                map.Dispose();
            }
        }
        #endregion

        #region 生成随机验证码
        ///  <summary>
        ///  生成随机验证码
        ///  </summary>
        private void CreateCode(string sessionId)
        {
            _randomChars = null;
            for (int i = 0; i < _length; i++)
            {
                _randomChars += character[random.Next(character.Length)];
            }
            //Session[sessionId] = null;
            Session[sessionId] = _randomChars;
        }
        #endregion

        #region 判断验证码是否正确
        /// <summary>
        /// 判断验证码是否正确.
        /// </summary>
        /// <param name="captcha">输入验证码.</param>
        /// <returns><c>true</c> if [is captcha valid] [the specified captcha]; otherwise, <c>false</c>.</returns>
        /// <remarks>add by ****,2015-06-08 11:25:50 </remarks>
        public bool IsCaptchaValid(string captcha)
        {
            bool isValid = false;          
            if (Session != null && Session[_sessionId] != null)
            {
                isValid = captcha.ToLower().Equals(Session[_sessionId].ToString().ToLower());
            }
            Session.Remove(_sessionId);//防止重复
            return isValid;
        }
        #endregion
    }
}