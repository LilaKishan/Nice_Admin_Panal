using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Nice_Admin_Panal.Areas.LOC_Country.Models;
using Nice_Admin_Panal.DAL.LOC_Country;
using Nice_Admin_Panal.BAL;

namespace Nice_Admin_Panal.Areas.LOC_Country.Controllers
{
    [CheckAccess]
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        LOC_CountryDAL dalLOC_Country = new LOC_CountryDAL();

        #region LOC_CountryList
        public IActionResult LOC_CountryList()
        {
            //string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

           
            DataTable dt = dalLOC_Country.dbo_PR_LOC_Country_selectall();
            return View("LOC_CountryList", dt);

            //DataTable table = new DataTable();
            //SqlConnection connection = new SqlConnection(connectionstr);
            //connection.Open();
            //SqlCommand objcmd = connection.CreateCommand();
            //objcmd.CommandType = CommandType.StoredProcedure;
            //objcmd.CommandText = "PR_LOC_Country_selectall";
            //SqlDataReader objSDR = objcmd.ExecuteReader();
            //table.Load(objSDR);


        }
        #endregion

        #region Country_Delete
        public IActionResult LOC_Country_Delete(int CountryId)
        {
            //string connstr = this.Configuration.GetConnectionString("myConnectionString");


            //SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //SqlCommand objcmd = connection.CreateCommand();
            //objcmd.CommandType = CommandType.StoredProcedure;
            //objcmd.CommandText = "PR_LOC_Country_DELETEBYPK";
            //objcmd.Parameters.AddWithValue("@CountryID", CountryId);
            //objcmd.ExecuteNonQuery();


            if (Convert.ToBoolean(dalLOC_Country.dbo_PR_LOC_Country_DELETEBYPK(CountryId)))
            {
                return RedirectToAction("LOC_CountryList");
            }

            return RedirectToAction("LOC_CountryList");


        }
        #endregion


        #region Save
        public IActionResult Save(LOC_CountryModel model)
        {
            //string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            //SqlConnection connection = new SqlConnection(connectionStr);
            //connection.Open();
            //SqlCommand objCmd = connection.CreateCommand();
            //objCmd.CommandType = CommandType.StoredProcedure;

            //if (model.CountryID == null)
            //{
            //    objCmd.CommandText = "PR_LOC_Country_InsertData";
            //}
            //else
            //{
            //    objCmd.CommandText = "PR_LOC_Country_UpdateByPk";
            //    objCmd.Parameters.AddWithValue("@CountryID", model.CountryID);
            //}

            //objCmd.Parameters.AddWithValue("@CountryName", model.CountryName);
            //objCmd.Parameters.AddWithValue("@CountryCode", model.CountryCode);
            //objCmd.ExecuteNonQuery();

            //return RedirectToAction("LOC_CountryList");

            if (ModelState.IsValid)
            {
                if (dalLOC_Country.dbo_PR_LOC_Country_Save(model))
                {
                    if (model.CountryID == 0)
                    {
                        //TempData["CountryInsertMsg"] = "Record Inserted Successfully";
                        return RedirectToAction("LOC_CountryList");
                    }
                    else {
                        return RedirectToAction("LOC_CountryList");
                    }
                }
            }
            return View("LOC_CountryAdd");
        }

        #endregion

        #region LOC_CountryAdd
        public IActionResult LOC_CountryAdd(int? CountryID)
        {
            //LOC_CountryModel lOC_CountryModel = dalLOC_Country.dbo_PR_LOC_Country_SelectByPK(CountryID);
            //if (lOC_CountryModel != null)
            //{
                //string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
                //SqlConnection connection = new SqlConnection(connectionStr);
                //connection.Open();
                //SqlCommand objCmd = connection.CreateCommand();
                //objCmd.CommandType = CommandType.StoredProcedure;
                //objCmd.CommandText = "PR_LOC_Country_SelectByPk";
                //objCmd.Parameters.AddWithValue("@CountryID", CountryID);
                //DataTable dt = new DataTable();
                //SqlDataReader objSDR = objCmd.ExecuteReader();

                //dt.Load(objSDR);
                //LOC_CountryModel model = new LOC_CountryModel();
                //foreach (DataRow dr in dt.Rows)
                //{
                //    model.CountryID = Convert.ToInt32(dr["CountryID"]);
                //    model.CountryName = (string)dr["CountryName"];
                //    model.CountryCode = (string)dr["CountryCode"];
                //}
            //    return View("LOC_CountryAdd",lOC_CountryModel );
            //}
            //else
            //{
            //    return View("LOC_CountryAdd");
            //}

            LOC_CountryModel lOC_CountryModel = dalLOC_Country.dbo_PR_LOC_Country_SelectByPK(CountryID);
            if (CountryID != 0)
            {
                //Console.WriteLine(lOC_CountryModel);
                return View("LOC_CountryAdd", lOC_CountryModel);
            }
            else
            {
                return View("LOC_CountryAdd");
            }
        }

        #endregion

        #region Filter

        public IActionResult LOC_CountryFilter(LOC_CountryFilterModel filterModel)
        {
            //string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            //DataTable dt = new DataTable();
            //SqlConnection connection = new SqlConnection(connectionStr);
            //connection.Open();
            //SqlCommand objCmd = connection.CreateCommand();
            //objCmd.CommandType = CommandType.StoredProcedure;
            //objCmd.CommandText = "PR_LOC_Country_filter";
            //objCmd.Parameters.AddWithValue("@CountryName", filterModel.CountryName);
            //objCmd.Parameters.AddWithValue("@CountryCode", filterModel.CountryCode);
            //SqlDataReader objSDR = objCmd.ExecuteReader();
            //dt.Load(objSDR);
            //ModelState.Clear();
            //return View("LOC_CountryList", dt);

            DataTable dataTable = dalLOC_Country.dbo_PR_LOC_Country_Filter(filterModel);
            return View("LOC_CountryList",dataTable);
        }

        #endregion

        public IActionResult Back() { return RedirectToAction("LOC_CountryList"); }
    }
}
