using Microsoft.AspNetCore.Mvc;
using Nice_Admin_Panal.Areas.SEC_User.Models;
using Nice_Admin_Panal.BAL;
using Nice_Admin_Panal.DAL.SEC_User;
using System.Data;
using System.Data.SqlClient;

namespace Nice_Admin_Panal.Areas.SEC_User.Controllers
{
    
    [Area("SEC_User")]
    [Route("SEC_User/[controller]/[action]")]
    public class SEC_UserController : Controller
    {


        //private IConfiguration configuration;
        //public SEC_UserController(IConfiguration _counfiguration)
        //{
        //    configuration = _counfiguration;
        //}

        SEC_UserDALBase DALSec_User = new SEC_UserDALBase();
        public IActionResult Login_Page()
        {
            return View();
        }

        #region Login
        [HttpPost]
        public IActionResult Login(SEC_UserModel userLoginModel)
        {
            string ErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(userLoginModel.UserName))
            {
                ErrorMsg += "Username is required";
            }
            if (string.IsNullOrEmpty(userLoginModel.Password))
            {
                ErrorMsg += "<br/> Password is required";
            }

            if (ErrorMsg != string.Empty)
            {
                TempData["Error"] = ErrorMsg;

                return RedirectToAction("Login_Page", userLoginModel);
            }

            if (ModelState.IsValid)
            {
                //SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("myConnectionString"));
                //conn.Open();

                //SqlCommand sqlCommand = conn.CreateCommand();
                //sqlCommand.CommandType = CommandType.StoredProcedure;
                //sqlCommand.CommandText = "PR_SEC_User_SelectByUsernameAndPassword ";
                //sqlCommand.Parameters.AddWithValue("@UserName", userLoginModel.UserName);
                //sqlCommand.Parameters.AddWithValue("@Password", userLoginModel.Password);

                //SqlDataReader objSDR = sqlCommand.ExecuteReader();



                DataTable dtLogin = DALSec_User.dbo_PR_SEC_User_SelectByUserNamePassword(userLoginModel.UserName,userLoginModel.Password);

                if (dtLogin.Rows.Count==0)
                {
                    TempData["Error"] = "Invalid Username or Password";
                    return RedirectToAction("Login_Page", "SEC_User");
                }
                else
                {
                    //dtLogin.Load(objSDR);
                    if (dtLogin.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtLogin.Rows)
                        {
                            HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                            HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                            HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
                            HttpContext.Session.SetString("Email", dr["Email"].ToString());
                            HttpContext.Session.SetString("Password", dr["Password"].ToString());
                            break;
                        }
                       
                    }
                    //else
                    //{

                    //    TempData["Error"] = ErrorMsg;
                    //    return RedirectToAction("Login_Page", "SEC_User");
                    //}
                    if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                    {
                        Console.WriteLine("Login Success");
                        return RedirectToAction("Index", "Home");
                    }
                    Console.WriteLine("Login Fail");

                    return RedirectToAction("Index", "Home");
                }

                
            }
            return RedirectToAction("Index","Home");
        }
        #endregion

        #region Logout
        //Logout action to clear current session and redirect user to login page
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login_Page","SEC_User");
        }
        #endregion

        public IActionResult SEC_UserRegister()
        {
            return View();
        }

        #region  Register
        public IActionResult Register(SEC_UserModel sEC_UserModel)
        {
           
            bool IsSuccess = DALSec_User.dbo_PR_SEC_User_Register(sEC_UserModel.UserName, sEC_UserModel.Password, sEC_UserModel.FirstName, sEC_UserModel.LastName, sEC_UserModel.Email, sEC_UserModel.PhotoPath);
            Console.WriteLine(IsSuccess);
            if (IsSuccess)
            {
                return RedirectToAction("Login_Page");
            }
            else
            {
                return RedirectToAction("SEC_UserRegister");
            }
        }
        #endregion



    }
}
