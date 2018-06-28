using System;
using System.Linq;

namespace WZK.Common.Authentication
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IAuthUser
    {
        /// <summary>
        /// 唯一标识Id.
        /// </summary>
        /// <value>The user identifier.</value>
        Guid Id { get; }
        /// <summary>
        /// 角色名称.
        /// </summary>
        /// <value>The name of the role.</value>
        string RoleName { get; }
        /// <summary>
        /// 是否匿名.
        /// </summary>
        /// <value><c>true</c> if anonymous; otherwise, <c>false</c>.</value>
        bool IsAnonymous { get; }

        /// <summary>
        /// 用户信息.
        /// </summary>
        /// <value>The user data.</value>
        string UserData { get; }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="role">The role.</param>
        /// <param name="userData">The user data.</param>
        /// <returns>System.Object.</returns>
        /// <remarks>add by dingyong,2016-07-08 14:13:21 </remarks>
        object GetUser(string id,string userData);
    }

    /// <summary>
    /// 用户信息.
    /// </summary>
    public class AuthUser : IAuthUser
    {
        public AuthUser() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CluboUser" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <exception cref="System.ArgumentNullException">userData</exception>
        public AuthUser(Guid id,string role, string name, string mobile)
        {
            Id = id;
            Name = name;
            Mobile = mobile;
            RoleName = role;
            //来源终端
            UserData = string.Format("n={1}Ωt={2}Ωm={3}Ωr={4}", Id, Name, 0, Mobile,role);
        }

        /// <summary>
        /// 用户数据.
        /// </summary>
        /// <value>The user data.</value>
        public string UserData { get; set; }

        /// <summary>
        /// 用户Id.
        /// </summary>
        /// <value>The user identifier.</value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户名称.
        /// </summary>
        /// <value>The name.</value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public string Name { get; set; }

        /// <summary>
        /// 用户角色.
        /// </summary>
        /// <value>The name of the role.</value>
        public string RoleName { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 是否匿名.
        /// </summary>
        /// <value><c>true</c> if anonymous; otherwise, <c>false</c>.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsAnonymous
        {
            get { return Id == Guid.Empty; }
        }

        /// <summary>
        /// 获取用户信息.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        protected static string GetUserData(string userData, string key)
        {
            string[] items = userData.Split('Ω');

            return (from t in items where t.Contains(key + "=") select t.Substring(key.Length + 1)).FirstOrDefault();
        }

        /// <summary>
        /// 获取用户.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userData">The user data.</param>
        /// <returns>IUser.</returns>
        /// <exception cref="System.ArgumentNullException">userData</exception>
        public virtual object GetUser(string id, string userData)
        {
            Id = Guid.Parse(id);
            UserData = userData;
            Name = GetUserData(UserData, "n");
            Mobile = GetUserData(UserData, "m");
            RoleName = GetUserData(UserData, "r");
            return this;
        }
    }

    /// <summary>
    /// 不需要http登录.
    /// </summary>
    public class NotHttpUser : AuthUser
    {
        public NotHttpUser()
        {
            Name = "true";
            Id = Guid.Parse(System.Configuration.ConfigurationManager.AppSettings["UserId"]);
        }
    }

}
