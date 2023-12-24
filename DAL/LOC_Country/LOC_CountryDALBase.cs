using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Nice_Admin_Panal.Areas.LOC_Country.Models;
using System.Data;
using System.Data.Common;

namespace Nice_Admin_Panal.DAL.LOC_Country
{
    public class LOC_CountryDALBase:DALHelper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
        #region dbo.PR_LOC_Country_selectall
        public DataTable dbo_PR_LOC_Country_selectall() 
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_selectall");

                DataTable dtCountry= new DataTable();
                using (IDataReader dr=sqlDB.ExecuteReader(dbCMD))
                {
                    dtCountry.Load(dr);
                }
                return dtCountry;
            }
            catch (Exception ex)
            {
                return null;
            } 
        }
        #endregion

        #region dbo_PR_LOC_Country_Delete
        public bool? dbo_PR_LOC_Country_DELETEBYPK(int? CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_DELETEBYPK");
                sqlDB.AddInParameter(dbCMD,"@CountryID",DbType.Int32,CountryID);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue ==1?false:true);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region  Method : dbo.PR_LOC_Country_InsertData & dbo.PR_LOC_Country_UpdateByPk   name:dbo_PR_LOC_Country_Save
        public bool dbo_PR_LOC_Country_Save(LOC_CountryModel lOC_CountryModel) {
            try
            {
                Console.WriteLine(lOC_CountryModel.CountryID);
                if (lOC_CountryModel.CountryID==0)
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_InsertData");
                    sqlDB.AddInParameter(dbCMD,"@CountryName",DbType.String,lOC_CountryModel.CountryName);
                    sqlDB.AddInParameter(dbCMD, "@CountryCode", DbType.String, lOC_CountryModel.CountryCode);
                    //sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    //sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);
                    sqlDB.ExecuteNonQuery(dbCMD);
                    return true;
                }
                else
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPk");
                    sqlDB.AddInParameter(dbCMD, "@CountryID", DbType.Int32, lOC_CountryModel.CountryID);
                    sqlDB.AddInParameter(dbCMD, "@CountryName", DbType.String, lOC_CountryModel.CountryName);
                    sqlDB.AddInParameter(dbCMD, "@CountryCode", DbType.String, lOC_CountryModel.CountryCode);
                    //sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);
                    sqlDB.ExecuteNonQuery(dbCMD);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_Country_SelectByPK
        public LOC_CountryModel dbo_PR_LOC_Country_SelectByPK(int? CountryID)
        {
            LOC_CountryModel lOC_CountryModel = new LOC_CountryModel();
            try
            {
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("dbo.PR_LOC_COUNTRY_SELECTBYPK");
                sqlDB.AddInParameter(dbCommand, "@CountryID", DbType.Int32, CountryID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    lOC_CountryModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                    lOC_CountryModel.CountryName = dataRow["CountryName"].ToString();
                    lOC_CountryModel.CountryCode = dataRow["CountryCode"].ToString();
                    //lOC_CountryModel.Created = Convert.ToDateTime(dataRow["Created"].ToString());
                    //lOC_CountryModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                }
                return lOC_CountryModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method :dbo_PR_LOC_Country_Filter
        public DataTable dbo_PR_LOC_Country_Filter(LOC_CountryFilterModel  lOC_CountryFilterModel)
        {
            //LOC_CountryModel lOC_CountryModel = new LOC_CountryModel();
            try
            {
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("PR_LOC_Country_filter");
                sqlDB.AddInParameter(dbCommand, "@CountryName", DbType.String, lOC_CountryFilterModel.CountryName);
                sqlDB.AddInParameter(dbCommand, "@CountryCode", DbType.String, lOC_CountryFilterModel.CountryCode);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }

}
