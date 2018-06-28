namespace WZK.Common.Authentication
{
    /// <summary>
    /// 认证接口.
    /// </summary>
    public interface IAuthenticationService<out T> where T : IAuthUser
    {
        /// <summary>
        /// 是否登陆
        /// </summary>
        /// <value><c>true</c> if this instance is login; otherwise, <c>false</c>.</value>
        bool IsLogin { get; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        void SignIn(IAuthUser user, bool createPersistentCookie);

        /// <summary>
        /// 退出
        /// </summary>
        void SignOut();

        /// <summary>
        /// 获取登录用户.
        /// </summary>
        /// <returns>IUser.</returns>
        T GetSiginUser();
    }
}
