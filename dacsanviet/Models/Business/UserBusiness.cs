using dacsanviet.Models.DTO;
using dacsanviet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dacsanviet.Models.Business
{
    public class UserBusiness
    {
        private DacSanVietEntities db = new DacSanVietEntities();

        //Add user
        public bool addUser(User entity)
        {
            try
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Tìm user qua email
        public User FindUser(string email)
        {
            return db.Users.Single(x => x.email == email);
        }

        //Đổi mật khẩu
        public void ChangPass(string email, string passWord)
        {
            var user = FindUser(email);
            user.passWord = passWord;
            db.SaveChanges();
        }

        //Đăng nhập
        public bool isLogin(string email, string passWord)
        {
            var res = db.Users.Where(x => x.email == email && x.passWord == passWord).ToList();
            if (res.Count() > 0)
                return true;
            else
                return false;
            
            //var res = db.Users.Where(x => x.email == email && x.passWord == passWord);
            //if (res.Count() > 0)
            //    return true;
            //else
            //    return false;
        }

        //Cập nhật trạng thái
        public void updateStatus(string email)
        {
            var user = db.Users.Single(x => x.email == email);
            user.status = true;
            db.SaveChanges();
        }

        //Kiểm tra email đăng ký có tồn tại?
        public bool checkExist_Email(string email)
        {
            var res = db.Users.Count(x => x.email == email);
            if (res > 0)
                return true;
            return false;
        }
    }
}