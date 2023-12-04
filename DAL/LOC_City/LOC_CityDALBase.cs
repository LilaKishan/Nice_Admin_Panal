using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Nice_Admin_Panal.DAL.LOC_City
{
    public class LOC_CityDALBase:DALHelper
    {
        #region dbo.PR_LOC_City_SelectAll
        public DataTable dbo_PR_LOC_City_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqldb.GetStoredProcCommand("PR_LOC_City_SelectAll");

                DataTable dtCity = new DataTable();
                using (IDataReader drCity = sqldb.ExecuteReader(dbCommand))
                {
                    dtCity.Load(drCity);

                }
                return dtCity;
                     
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
