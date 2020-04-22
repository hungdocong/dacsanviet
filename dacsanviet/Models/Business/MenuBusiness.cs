using dacsanviet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dacsanviet.Models.Business
{
    public class MenuBusiness
    {
        private DacSanVietEntities db = new DacSanVietEntities();

        //lấy dữ liệu menu chính
        public List<Menu> getMenus()
        {
            return db.Menus.Where(x => x.menuParent_ID == 0).OrderBy(x => x.displayOrder).ToList();
        }

        //lấy menu con
        public List<Menu> getParentMenu()
        {
            return db.Menus.Where(x => x.menuParent_ID != 0).OrderBy(x => x.displayOrder).ToList();
        }
    }
}