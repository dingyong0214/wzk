using System.Data;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace WZK.Common
{
    public static class Json
    {
        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="jsonString">得到的json</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string jsonString)
        {
            //转换json格式
            jsonString = jsonString.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(jsonString).Value;
            DataTable tb = null;
            //去除表名   
            //jsonString = jsonString.Substring(jsonString.IndexOf("[") + 1);
            //jsonString = jsonString.Substring(0, jsonString.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(jsonString);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }

        /// <summary>
        /// 对象序列化字符串
        /// </summary>
        /// <returns></returns>
        public static string Serialize<T>(T serialize)
        {
            JavaScriptSerializer jserializer = new JavaScriptSerializer();
            return jserializer.Serialize(serialize);
        }

        /// <summary>
        /// 将json字符串转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString)
        {
            JavaScriptSerializer jserializer = new JavaScriptSerializer();
            return jserializer.Deserialize<T>(jsonString);
        }
    }
}
