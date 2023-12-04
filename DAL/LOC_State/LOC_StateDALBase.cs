using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Nice_Admin_Panal.DAL.LOC_State
{
    public class LOC_StateDALBase:DALHelper
    {
        #region dbo.PR_LOC_State_SelectAll
        public DataTable dbo_PR_LOC_State_SelectAll() 
        {
            try
            {
                SqlDatabase sqlDB= new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");

                DataTable dtState = new DataTable();
                using (IDataReader drState = sqlDB.ExecuteReader(dbCommand))
                {
                    dtState.Load(drState);
                }
                return dtState;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
