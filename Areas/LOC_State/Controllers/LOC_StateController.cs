using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Nice_Admin_Panal.Areas.LOC_Country.Models;
using Nice_Admin_Panal.Areas.LOC_State.Models;

namespace Nice_Admin_Panal.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        private IConfiguration configuration;

        public LOC_StateController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        #region State List

        public IActionResult LOC_StateList()
        {
            string connectionStr = this.configuration.GetConnectionString("myConnectionString");


            #region Country DropDown
            SqlConnection connection1 = new SqlConnection(connectionStr);
            connection1.Open();
            SqlCommand objCmd1 = connection1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_Country_ComboBox";
            SqlDataReader reader1 = objCmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);
            connection1.Close();

            List<LOC_CountryModel> list = new List<LOC_CountryModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryModel countryModel = new LOC_CountryModel();
                countryModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryList = list;

            #endregion



            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_State_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            return View("LOC_StateList", dt);
        }

        #endregion

        #region LOC_StateDelete
        public IActionResult LOC_StateDelete(int StateID)
        {
            string connctionstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connctionstr);
            conn.Open();

            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_State_DELETEBYPK";
            objcmd.Parameters.AddWithValue("@StateID", StateID);
            objcmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("LOC_StateList");
        }
        #endregion

        #region State_Add/update
        public IActionResult Save(LOC_StateModel model)
        {
            string connstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;

            if (model.StateID == null)
            {
                objcmd.CommandText = "PR_LOC_State_InsertDATA";

            }
            else
            {
                objcmd.CommandText = "PR_LOC_State_updatebypk";
                objcmd.Parameters.AddWithValue("@StateID", model.StateID);
            }

            objcmd.Parameters.AddWithValue("@StateName", model.StateName);
            objcmd.Parameters.AddWithValue("@StateCode", model.StateCode);
            objcmd.Parameters.AddWithValue("@CountryID", model.CountryID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("LOC_StateList");
        }
        public IActionResult LOC_StateAdd(int? StateID)
        {

            string connstr = this.configuration.GetConnectionString("myConnectionString");

            #region Country_DropDown

            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_Country_ComboBox";
            SqlDataReader reader1 = objcmd.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);
            conn.Close();

            List<LOC_CountryModel> list = new List<LOC_CountryModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryModel countryModel = new LOC_CountryModel();
                countryModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryList = list;

            #endregion

            if (StateID != null)
            {
                SqlConnection connection = new SqlConnection(connstr);
                connection.Open();
                SqlCommand objCmd = connection.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_State_SelectByPk";
                objCmd.Parameters.AddWithValue("@StateID", StateID);
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();

                dt.Load(objSDR);
                LOC_StateModel model = new LOC_StateModel();
                foreach (DataRow dr in dt.Rows)
                {
                    model.StateID = Convert.ToInt32(dr["StateID"]);
                    model.StateName = (string)dr["StateName"];
                    model.StateCode = (string)dr["StateCode"];
                    model.CountryID = Convert.ToInt32(dr["CountryID"]);
                    model.CountryName = (string)dr["CountryName"];
                }
                return View("LOC_StateAdd", model);
            }
            else
            {
                return View("LOC_StateAdd");
            }

        }
        #endregion

        #region Filter

        public IActionResult LOC_StateFilter(LOC_StateFilterModel filterModel)
        {
            string connectionStr = this.configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_State_filter";
            objCmd.Parameters.AddWithValue("@CountryID", filterModel.CountryID);
            objCmd.Parameters.AddWithValue("@StateName", filterModel.StateName);
            objCmd.Parameters.AddWithValue("@StateCode", filterModel.StateCode);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            Console.WriteLine(filterModel.CountryID + " " + "hello");
            Console.WriteLine(filterModel.StateName + " " + filterModel.StateCode);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["CountryName"]);
                Console.WriteLine(dr["StateName"]);
                Console.WriteLine(dr["StateCode"]);
            }
            ModelState.Clear();

            #region Country DropDown

            SqlConnection connection1 = new SqlConnection(connectionStr);
            connection1.Open();
            SqlCommand objCmd1 = connection1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_Country_ComboBox";
            SqlDataReader reader1 = objCmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel countryModel = new LOC_CountryDropDownModel();
                countryModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryList = list;

            #endregion

            return View("LOC_StateList", dt);
        }

        #endregion

        public IActionResult Back() { return RedirectToAction("LOC_StateList"); }

    }
}
