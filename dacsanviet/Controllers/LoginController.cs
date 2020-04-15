using dacsanviet.Models.Business;
using dacsanviet.Models.DTO;
using dacsanviet.Models.Entity;
using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace dacsanviet.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //Xác nhận email
        public ActionResult Register_Confirm()
        {
            return View();
        }

        //Đăng ký thành công
        public ActionResult Register_Success(string email, long user_ID, string firstName)
        {
            new UserBusiness().updateStatus(email);
            return View();
        }

        [HttpPost]
        public ActionResult Register(User entity)
        {
            var userModel = new UserBusiness();
            if (ModelState.IsValid)
            {
                var user = new User();
                user.firstName = entity.firstName;
                user.email = entity.email;
                user.passWord = new MD5().Encrypt_MD5(entity.passWord);
                user.birthday = entity.birthday;
                user.gender = entity.gender;
                user.createdDate = DateTime.Now.ToString();
                user.status = false;

                var res = userModel.addUser(user);
                if (res)
                {
                    Session["User"] = user;
                    SendEmail(user.email);
                    return Redirect("/xac-nhan-e-mail");
                }
                else
                    return View();
            }
            else
                return View();
        }

        //check login
        public JsonResult CheckLogin(string email, string passWord)
        {
            var userModel = new UserBusiness();
            string encrypt_Pass = new MD5().Encrypt_MD5(passWord);
            if (userModel.isLogin(email, encrypt_Pass))
            {
                Session["isLogin"] = userModel.FindUser(email);
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        //Quên mật khẩu
        public JsonResult ForgetPass(string email)
        {
            SendEmailForgetPass(email);
            return Json(new
            {
                status = true
            });
        }


        //Thông báo đã gửi email thay đổi mk
        public ActionResult Confirm_ForgetPass(string email)
        {
            email = email.Replace(" ", "+");
            string De_email = new MD5().Decrypt_MD5(email);
            var model = new UserBusiness();
            if (model.checkExist_Email(De_email))
            {
                ViewBag.email = De_email;
                return View();
            }
            else
                return RedirectToAction("Page404", "Home");
        }

        //đổi mật khẩu
        public JsonResult ChangePass(string email, string pass)
        {
            new UserBusiness().ChangPass(email, new MD5().Encrypt_MD5(pass));
            return Json(new
            {
                status = true
            });
        }

        //Login gmail
        public JsonResult LoginGmail(string email, string fullname)
        {
            var userModel = new UserBusiness();
            var user = new User();
            user.firstName = fullname;
            user.email = email;
            user.createdDate = DateTime.Now.ToString();
            user.status = true;
            if (userModel.checkExist_Email(email))//Tồn tại email thì k lưu csdl
            {
                Session["isLogin"] = user;
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                var res = userModel.addUser(user);
                if (res)
                {
                    Session["isLogin"] = user;
                    return Json(new
                    {
                        status = true
                    });
                }

                else
                    return Json(new
                    {
                        status = false
                    });
            }
           
        }


        private Uri RedirecUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallBack");
                return uriBuilder.Uri;
            }
        }

        //Xử lý sau khi đăng nhập facebook thành công
        public ActionResult FacebookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token",new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirecUri.AbsoluteUri,
                code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                var userModel = new UserBusiness();
                var user = new User();
                user.firstName = me.first_name + " " +  me.middle_name + " " + me.last_name;
                user.email = me.email;
                user.createdDate = DateTime.Now.ToString();
                user.status = true;

                if (userModel.checkExist_Email(me.email))//Tồn tại email thì k lưu csdl
                {
                    Session["isLogin"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var res = userModel.addUser(user);
                    if (res)
                    {
                        Session["isLogin"] = user;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect("/error");
                    }
                }
            }
            else
            {
               return RedirectToAction("Index", "Home");
            }
        }
        //login facebook
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirecUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            }) ;
            return Redirect(loginUrl.AbsoluteUri);
        }
        //Đăng xuất
        public ActionResult Logout()
        {
            Session["isLogin"] = null;
            return RedirectToAction("Index", "Home");
        }


        //check email exits
        public JsonResult CheckExitsEmail(string email)
        {
            var userModel = new UserBusiness();
            if (userModel.checkExist_Email(email))
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        //send email
        public void SendEmail(string address)
        {
            string email = "nongsanvn20@gmail.com";
            string password = "HungCong15021996";
            var user = (User)Session["User"];

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = "Nông sản Việt Nam - Email xác nhận đăng ký tài khoản";
            msg.Body = "<div style='width: 600px; height: 100 %; margin: auto'>";
            msg.Body += "<div style ='margin:0 250px'>";
            msg.Body +=     "<a style='color:#871fff;text-decoration:none' target='blank'" +
                                "style='vertical-align:middle;width:45%;height:auto;max-width:100%;border-width:0'"+
                                "class='CToWUd'>"+
                                    "<img src='https://lh3.googleusercontent.com/1LIw-IQwGPyWHez1tuBM7QwgHKAhvGfnpKjTsT1wf0Onk4ADg20r2PMwCFOXb9-getwcPhUS0zrwRQJwBX4fn1uGkNM-Rf7vTS1ONms0SLGL6Vi6wd8gnu3bBQViLa6Nxp-K_k3f7n_5NeI1j57MaT-HURsxPo70zQfQm36axC62bQUlvNUqj03KRi_lmJVAIPVvXIx7ZtfJaVgG4evv8GexGHL5ngmVuR_OG7OZdzpr8HuzJ9weMsoR4Y7EghQ9odxkgOtyShkSLUnEWCkvXsIrDbdGYsI-mPMox-fVpDJXo80RDdtiBy9lH_9bkQ-O5fCWGUvc1RFjVrn50IxGaBwdRuFD1Yzj3B6Z_ejZDhILGX_ptYOPjeAUGFngSV5JfT4TN22v8GisY7v8dLOhG6uW8bg8BgGynua5PA1gxoNQRGyjmGDREbV2f6loMv4xDLMrlf4ND2gf04Iapxh8_AePp9EHBzORmCVIB2ZSGMYdARYAl61mQnXBjBesdMdAGVOgIzL4LoADQtjQWRwOC5re_R3puuYJLLAHaF1rUrDhKgaxir0_ilGhY22o1mHmlEphXGyDuAdKV3plJgDp57J7wzorQhK8A69VR1yjxe9xY8rJin3miMMlYRItmaGoDkzB-y8ZmKdTlMCzyUF3YYiAwi5Z9Tejv_UwNNmjrS1qUsGcISZKFkZuSyA=w235-h66-no'" +
                                    "style='vertical-align:middle;border-width:0'" +
                                    "class='CToWUd'>"+
                            "</a>";
            msg.Body += "<div>"+
                            "<div style='font-weight: normal;line-height: 48px'>" +
                                "<h1>Xác nhận đăng ký tài khoản</h1>" +
                            "</div>" +
                        "</div>";
            msg.Body += "<div>" +
                            "<div style ='margin-top:24px;font-size:16px'>" +
                                "Hi &nbsp;" + "<b style='color:blue'>"+ user.firstName.ToString()+ "</b>" + ","+
                            "</div>" +
                         "<div style='font-family: " +
                                "'Roboto'" +
                                ", sans-serif;'>" +
                            "<p style='font-size:16px;margin-bottom:16px;line-height:24px'> " +
                                "Cám ơn bạn đã đăng ký tài khoản của Nông sản Việt Nam." +
                            "</p>" +
                            "<p style='font-size:16px;margin-bottom:16px;line-height:24px'> " +
                                "Vui lòng nhấn xác nhận đăng ký bên dưới để hoàn tất quy trình đăng ký tài khoản.</p>" +
                            "<p style='font-size:16px;margin-bottom:16px;line-height:24px'>" +
                                "<a href=https://localhost:44391/activation-acount-and-verify-your-e-mail?email=" + user.email.ToString() + "&user_ID=" + user.user_ID + "&firstName=" + user.firstName.ToString() +" style ='background:#131415;color:#fff;font-size:14px;border:0;border-radius:4px;display:inline-block;line-height:24px;margin:8px 0;min-height:20px;outline:0;padding:8px 20px;text-align:center;vertical-align:middle;white-space:nowrap;text-decoration:none'" +
                                    "target='_blank'>" +
                                    "Xác nhận đăng ký" +
                                "</a>"+
                            "</p>" +
                         "</div>" +
                        "</div>";
            msg.Body += "<p style='font-family: 'Roboto', sans-serif;'>" +
                            "<span style='line-height:24px;font-size:16px'> Nông sản Việt Nam,</span><br>" +
                            "<span style='line-height:24px;font-size:16px'> Dành cho bạn những sản phẩm nông sản tốt nhất</span >" +
                        "</p>" +

                        "<hr style='margin:40px 0 20px 0;display:block;height:1px;border:0;border-top:1px solid #c4cdd5;padding:0'>" +
                        "<footer style='font-family: 'Roboto', sans-serif; margin-bottom:40px'>" +
                            "<span style='color:#919eab;line-height:28px;font-size:12px'> Nông sản Việt Nam, Trụ sở: Hà Mòn - Đăk Hà - Kon Tum, Việt Nam</span><br>" +
                            "<span style='color:#919eab;line-height:28px;font-size:12px'> Mail xác nhận việc đăng ký tài khoản website nongsanvn.vn. Do not reply </span ><br >";
            msg.Body += "</div>";
            msg.Body += "</div>";
            msg.BodyEncoding = UTF8Encoding.UTF8;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

        }

        //send email forget password
        public void SendEmailForgetPass(string address)
        {
            string email = "nongsanvn20@gmail.com";
            string password = "HungCong15021996";
            var user = (User)Session["User"];

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = "Nông sản Việt Nam - Thông báo quên mật khẩu đăng nhập";
            msg.Body = "<div style='width: 600px; height: 100 %; margin: auto'>";
            msg.Body += "<div style ='margin:0 250px'>";
            msg.Body += "<a style='color:#871fff;text-decoration:none' target='blank'" +
                                "style='vertical-align:middle;width:45%;height:auto;max-width:100%;border-width:0'" +
                                "class='CToWUd'>" +
                                    "<img src='https://lh3.googleusercontent.com/1LIw-IQwGPyWHez1tuBM7QwgHKAhvGfnpKjTsT1wf0Onk4ADg20r2PMwCFOXb9-getwcPhUS0zrwRQJwBX4fn1uGkNM-Rf7vTS1ONms0SLGL6Vi6wd8gnu3bBQViLa6Nxp-K_k3f7n_5NeI1j57MaT-HURsxPo70zQfQm36axC62bQUlvNUqj03KRi_lmJVAIPVvXIx7ZtfJaVgG4evv8GexGHL5ngmVuR_OG7OZdzpr8HuzJ9weMsoR4Y7EghQ9odxkgOtyShkSLUnEWCkvXsIrDbdGYsI-mPMox-fVpDJXo80RDdtiBy9lH_9bkQ-O5fCWGUvc1RFjVrn50IxGaBwdRuFD1Yzj3B6Z_ejZDhILGX_ptYOPjeAUGFngSV5JfT4TN22v8GisY7v8dLOhG6uW8bg8BgGynua5PA1gxoNQRGyjmGDREbV2f6loMv4xDLMrlf4ND2gf04Iapxh8_AePp9EHBzORmCVIB2ZSGMYdARYAl61mQnXBjBesdMdAGVOgIzL4LoADQtjQWRwOC5re_R3puuYJLLAHaF1rUrDhKgaxir0_ilGhY22o1mHmlEphXGyDuAdKV3plJgDp57J7wzorQhK8A69VR1yjxe9xY8rJin3miMMlYRItmaGoDkzB-y8ZmKdTlMCzyUF3YYiAwi5Z9Tejv_UwNNmjrS1qUsGcISZKFkZuSyA=w235-h66-no'" +
                                    "style='vertical-align:middle;border-width:0'" +
                                    "class='CToWUd'>" +
                            "</a>";
            msg.Body += "<div>" +
                            "<div style='font-weight: normal;line-height: 48px'>" +
                                "<h1>Xác nhận Email</h1>" +
                            "</div>" +
                        "</div>";
            msg.Body += "<div>" +
                            "<p style='font-size:16px;margin-bottom:16px;line-height:24px'> " +
                                "Vui lòng nhấn xác nhận bên dưới để tiến hành thay đổi mật khẩu.</p>" +
                            "<p style='font-size:16px;margin-bottom:16px;line-height:24px'>" +
                                "<a href=https://localhost:44391/confirm-change-pass?email=" + new MD5().Encrypt_MD5(address) + " style ='background:#131415;color:#fff;font-size:14px;border:0;border-radius:4px;display:inline-block;line-height:24px;margin:8px 0;min-height:20px;outline:0;padding:8px 20px;text-align:center;vertical-align:middle;white-space:nowrap;text-decoration:none'" +
                                    "target='_blank'>" +
                                    "Xác nhận" +
                                "</a>" +
                            "</p>" +
                        "</div>";
            msg.Body += "<p style='font-family: 'Roboto', sans-serif;'>" +
                            "<span style='line-height:24px;font-size:16px'> Nông sản Việt Nam,</span><br>" +
                            "<span style='line-height:24px;font-size:16px'> Dành cho bạn những sản phẩm nông sản tốt nhất</span >" +
                        "</p>" +

                        "<hr style='margin:40px 0 20px 0;display:block;height:1px;border:0;border-top:1px solid #c4cdd5;padding:0'>" +
                        "<footer style='font-family: 'Roboto', sans-serif; margin-bottom:40px'>" +
                            "<span style='color:#919eab;line-height:28px;font-size:12px'> Nông sản Việt Nam, Trụ sở: Hà Mòn - Đăk Hà - Kon Tum, Việt Nam</span><br>" +
                            "<span style='color:#919eab;line-height:28px;font-size:12px'> Mail xác nhận việc đăng ký tài khoản website nongsanvn.vn. Do not reply </span ><br >";
            msg.Body += "</div>";
            msg.Body += "</div>";
            msg.BodyEncoding = UTF8Encoding.UTF8;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

        }
    }
}


//msg.Body  = "<table border="+ 0 +" cellspacing="+ 0 + " cellpadding="+ 0 +" style=" + "padding - bottom:20px; max - width:516px; min - width:220px" + ">" +
//             "< tbody >" +
//                "< tr >" + 
//                    "< td width = " + 8 + " style = " + "width:8px" + "></ td >" +
//                       "< td >"+
//                           "< div style = " + "border-style:solid;border-width:thin;border-color:#dadce0;border-radius:8px;padding:40px 20px" + 
//                    "align = " + "center" + "class=" + "m_8986768429616635038mdv2rw"+ ">" +
//                    "<img" +
//                        "src = " + "Asset/Client/img/logo/1.png" +
//                        "width="+ 74 + " height="+ 24 + " aria-hidden="+ "true" +"style=" + "margin-bottom:16px" + "alt="+ "Google"+
//                        "class="+ "CToWUd" + ">"+
//                    "<div " + 
//                        "style = "+ "font-family:'Google Sans',Roboto,RobotoDraft,Helvetica,Arial,sans-serif;border-bottom:thin solid #dadce0;color:rgba(0,0,0,0.87);line-height:32px;padding-bottom:24px;text-align:center;word-break:break-word" +">"+
//                        "< div style=" + "font-size:24px" + "><a>Cám</a> ơn bạn đã đăng ký tài khoản Nông sản&nbsp;Việt Nam</div>" +
//                        "<table align = " + "center" + "style= "+"margin-top:8px" +">"+
//                             "< tbody >"+ 
//                                 "< tr style= "+"line-height:normal" + ">"+
//                                     "< td align= "+"right" +  "style= "+"padding-right:8px" +">"+
//                                         "< img width= "+20+"height= "+20+
//                                             "style= "+"width:20px;height:20px;vertical-align:sub;border-radius:50%"+
//                                             "src= "+ "https://lh3.googleusercontent.com/a-/AOh14GjzCmlhoE2Kmua-C2Fyj58GjNmdty9jcoPpLFh8=s96"+
//                                             "class="+ "CToWUd" + "></td>"+
//                                    "<td>"+
//                                        "<a style = "+"font-family:'Google Sans',Roboto,RobotoDraft,Helvetica,Arial,sans-serif;color:rgba(0,0,0,0.87);font-size:14px;line-height:20px" +">"+
//                                            user.email.ToString() +
//                                        "</a>"+
//                                    "</td>"+
//                                "</tr>"+
//                            "</tbody>"+
//                        "</table>"+
//                    "</div>"+
//                    "<div"+
//                        "style = "+"font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:14px;color:rgba(0,0,0,0.87);line-height:20px;padding-top:20px;text-align:left" +">"+
//                        "< br > Vui lòng nhấn xác nhận đăng ký bên dưới để hoàn tất quy trình đăng ký tài khoản.<div"+
//                               "style = "+"padding-top:32px;text-align:center" +">< a"+
//                                "href="+ "https://localhost:44391/activation/acountActive-"+user.email.ToString() +
//                                "style="+"font-family:'Google Sans',Roboto,RobotoDraft,Helvetica,Arial,sans-serif;line-height:16px;color:#ffffff;font-weight:400;text-decoration:none;font-size:14px;display:inline-block;padding:10px 24px;background-color:#4184f3;border-radius:5px;min-width:90px"+
//                                "target="+"_blank"+
//                                "data-saferedirecturl="+"https://www.google.com/url?q=https://accounts.google.com/AccountChooser?Email%3Dhungdocong82@gmail.com%26continue%3Dhttps://myaccount.google.com/alert/nt/1583847134000?rfn%253D127%2526rfnc%253D1%2526eid%253D-6206469006219688524%2526et%253D0&amp;source=gmail&amp;ust=1586481275794000&amp;usg=AFQjCNEQphKQBRQXzCVzX6XpkU6-NlJfzw" + ">"+
//                                "Xác nhận đăng ký</a></div>"+
//                    "</div>"+
//                "</div>"+
//                "<div style = "+"text-align:left" +">"+
//                    "< div"+
//                        "style= "+"font-family:Roboto-Regular,Helvetica,Arial,sans-serif;color:rgba(0,0,0,0.54);font-size:11px;line-height:18px;padding-top:12px;text-align:center" +">"+
//                        "< div > Mail xác nhận việc đăng ký tài khoản website nongsanvn.vn</div>"+
//                        "<div style = "+"direction:ltr" +">© 2020 nongsanvn.vn, <a class="+ "m_8986768429616635038afal" +
//                                "style=" + "font-family:Roboto-Regular,Helvetica,Arial,sans-serif;color:rgba(0,0,0,0.54);font-size:11px;line-height:18px;padding-top:12px;text-align:center" + ">1600"+
//                                "Hà Mòn - Đăk Hà - Kon Tum, Việt Nam</a></div>"+
//                    "</div>"+
//                "</div>"+
//            "</td>"+
//            "<td width = "+8+" style="+"width:8px"+"></td>"+
//        "</tr>"+
//    "</tbody>"+
//"</table>";