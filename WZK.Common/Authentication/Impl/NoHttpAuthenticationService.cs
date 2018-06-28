namespace WZK.Common.Authentication.Impl
{
    /// <summary>
    /// 不需要http登录验证.
    /// </summary>
    public class NoHttpAuthenticationService<T> : IAuthenticationService<T> where T : IAuthUser, new()
    {
        /// <summary>
        /// /// 登录
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        public void SignIn(IAuthUser userData, bool createPersistentCookie)
        {

        }

        /// <summary>
        /// 退出
        /// </summary>
        public void SignOut()
        {

        }

        /// <summary>
        /// 获取登录用户
        /// </summary>
        /// <returns>UserData.</returns>
        public T GetSiginUser()
        {
            return new T();
        }

        public bool IsLogin
        {
            get { return true; }
        }
    }
}
