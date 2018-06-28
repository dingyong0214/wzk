using WZK.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Business.Admin.Models
{
    public class MMenus : Menus
    {
        public MMenus(Menus menus)
        {
            this.EnglishName = menus.EnglishName;
            this.Id = menus.Id;
            this.InputUser = menus.InputUser;
            this.InsertTime = menus.InsertTime;
            this.Name = menus.Name;
            this.OrderNo = menus.OrderNo;
            this.ParentId = menus.ParentId;
            this.UpdateTime = menus.UpdateTime;
            this.UpdateUser = menus.UpdateUser;
            this.Url = menus.Url;
            this.MenuType = menus.MenuType;
        }

        /// <summary>
        /// 控制器名和Action组合
        /// </summary>
        public string ControllerAction { get; set; }

    }
}
