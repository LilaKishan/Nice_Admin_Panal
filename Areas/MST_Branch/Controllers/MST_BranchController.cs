using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Nice_Admin_Panal.Areas.MST_Branch.Models;

namespace Nice_Admin_Panal.Areas.MST_Branch.Controllers
{
    [Area("MST_Branch")]
    [Route("MST_Branch/[controller]/[action]")]
    public class MST_BranchController : Controller
    {
        private IConfiguration Configuration;
        public MST_BranchController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region MST_BranchList
        public IActionResult MST_BranchList()
        {
            string connstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            DataTable tb = new DataTable();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Branch_selectall";
            SqlDataReader rd = cmd.ExecuteReader();
            tb.Load(rd);
            conn.Close();

            return View("MST_BranchList", tb);
        }
        #endregion

        #region MST_BranchDelete
        public IActionResult MST_BranchDelete(int BranchID)
        {
            string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_MST_Branch_DeleteByPK";
            objCmd.Parameters.AddWithValue("@BranchID", BranchID);
            objCmd.ExecuteNonQuery();
            return RedirectToAction("MST_BranchList");
        }

        #endregion

        #region Add/Edit

        public IActionResult Save(MST_BranchModel model)
        {
            string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if (model.BranchID == null)
            {
                objCmd.CommandText = "PR_MST_Branch_InsertData";
            }
            else
            {
                objCmd.CommandText = "PR_MST_Branch_UpdateByPK";
                objCmd.Parameters.AddWithValue("@BranchID", model.BranchID);
            }
            objCmd.Parameters.AddWithValue("@BranchName", model.BranchName);
            objCmd.Parameters.AddWithValue("@BranchCode", model.BranchCode);
            objCmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("MST_BranchList");
        }

        public IActionResult MST_BranchAdd(int? BranchID)
        {
            if (BranchID != null)
            {
                string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();
                SqlCommand objCmd = connection.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_MST_Branch_SelectByPk";
                objCmd.Parameters.AddWithValue("@BranchID", BranchID);
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();

                dt.Load(objSDR);
                MST_BranchModel model = new MST_BranchModel();
                foreach (DataRow dr in dt.Rows)
                {
                    model.BranchName = (string)dr["BranchName"];
                    model.BranchCode = (string)dr["BranchCode"];
                }
                return View("MST_BranchAdd", model);
            }
            return View("MST_BranchAdd");
        }

        #endregion

        #region Filter

        public IActionResult MST_BranchFilter(MST_BranchFilterModel filterModel)
        {
            string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_MST_Branch_Filter";
            objCmd.Parameters.AddWithValue("@BranchName", filterModel.BranchName);
            objCmd.Parameters.AddWithValue("@BranchCode", filterModel.BranchCode);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            ModelState.Clear();
            return View("MST_BranchList", dt);
        }

        #endregion

        public IActionResult Back() { return RedirectToAction("MST_BranchList"); }
    }
}
