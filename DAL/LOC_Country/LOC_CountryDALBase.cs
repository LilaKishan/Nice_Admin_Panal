using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Nice_Admin_Panal.Areas.LOC_Country.Models;
using System.Data;
using System.Data.Common;

namespace Nice_Admin_Panal.DAL.LOC_Country
{
    public class LOC_CountryDALBase:DALHelper
    {
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

 
    }

}
