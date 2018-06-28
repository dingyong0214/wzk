using System.Collections.Generic;
using System.Data;
using System.Linq;
usingBoat.Common;
using Boat.Entity;
using Boat.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections;

namespace Boat.Business.Common
{
    /// <summary>
    /// 公用业务
    /// </summary>
    /// <remarks>added by xiongchonglong,2016-08-17</remarks>
    public class ProfileCommon : BaseBusiness
    {
        #region 获取字典数据  - 返回DataTable
        /// <summary>
        /// 获取字典数据  - 返回DataTable
        /// </summary>
        /// <param name="dictEnglishName">字典英文名称.</param>
        /// <returns>字典项列表 -  DataTable</returns>
        /// <remarks>add by xiongchonglong,2016-08-17 14:30:38 </remarks>
        public static DataTable GetDictItem(string dictEnglishName)
        {
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                var sql = from d in context.Dict
                          join di in context.DictItem
                          on d.Id equals di.DictId
                          where d.EnglishName == dictEnglishName
                          select new
                          {
                              di.Id,
                              di.ItemName,//字典项名称
                              di.ItemValue//字典项值
                          };
                var data = sql.ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ItemId", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemValue", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));

                foreach (var item in data)
                {
                    DataRow row = dt.NewRow();
                    row["ItemId"] = item.Id;//字典项编号
                    row["ItemName"] = item.ItemName;//字典项名称
                    row["ItemValue"] = item.ItemValue;//字典项值
                    dt.Rows.Add(row);
                }
                return dt;
            }
        }
        #endregion

        #region 获取字典数据 - 返回List
        /// <summary>
        /// 获取字典数据 - 返回List
        /// </summary>
        /// <param name="dictEnglishName">字典英文名称.</param>
        /// <returns>字典项列表 -  List</returns>
        /// <remarks>add by xiongchonglong,2016-08-17 14:48:17 </remarks>
        public static List<MDictItem> GetDictItemList(string dictEnglishName)
        {
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                var sql = from d in context.Dict
                          join di in context.DictItem
                          on d.Id equals di.DictId
                          where d.EnglishName == dictEnglishName
                          select new MDictItem
                          {
                              TypeName = d.Name,
                              TypeEnglishName = d.EnglishName,
                              ItemId = di.Id,
                              ItemName = di.ItemName,//字典项名称
                              ItemValue = di.ItemValue//字典项值
                          }; 
                return sql.ToList();
            }
        }
        #endregion

        #region 根据字典项名获取对应字典项值        
        /// <summary>
        /// 根据字典项名获取对应字典项值
        /// </summary>
        /// <param name="dictItemList">字典List.</param>
        /// <param name="ItemName">字典项名.</param>
        /// <returns>字典项值.</returns>
        /// <remarks>add by xiongchonglong,2016-08-18 11:49:24 </remarks>
        public static string GetDictItemValue(List<MDictItem> dictItemList, string ItemName)
        {
            var list = dictItemList.Where(p => p.ItemName == ItemName).FirstOrDefault();
            if (list != null)
            {
                return list.ItemValue.Trim();
            }
            return "";
        }
        #endregion

        #region 根据字典项名和字典项名称获取对应字典项值        
        /// <summary>
        /// 根据字典项名和字典项名称获取对应字典项值
        /// </summary>
        /// <param name="dictName">字典英文名称</param>
        /// <param name="itemName">字典项名称.</param>
        /// <returns>字典项名.</returns>
        /// <remarks>add by xiongchonglong,2016-08-18 11:49:59 </remarks>
        public static string GetDictItemValue(string dictName, string itemName)
        {
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                var sql = from d in context.Dict
                          join di in context.DictItem
                          on d.Id equals di.DictId
                          where d.EnglishName == dictName&&di.ItemName==itemName
                          select new MDictItem
                          {
                              TypeName = d.Name,
                              TypeEnglishName = d.EnglishName,
                              ItemId = di.Id,
                              ItemName = di.ItemName,//字典项名称
                              ItemValue = di.ItemValue//字典项值
                          };
                var list = sql.FirstOrDefault();
                if (list != null)
                {
                    return list.ItemValue.Trim();
                }
                return "";
            }
        }
        #endregion

        #region 获取易行通Key
        /// <summary>
        /// 获取易行通Key
        /// </summary>
        public static string EncryptDES(string parameter)
        {
            var dictList = ProfileCommon.GetDictItemList(DictEnum.DictType.YxtKey.ToString());
            string key = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YxtKeyValue.易行通MD5密钥.ToString());  //获取易行通Key
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(parameter);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        #endregion

        #region Json 字符串 转换为 DataTable数据集合
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable("JsonTable");  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = System.Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current,typeof(string));
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        #endregion
    }
}
