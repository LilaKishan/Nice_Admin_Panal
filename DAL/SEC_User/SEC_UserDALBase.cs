using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Nice_Admin_Panal.Areas.SEC_User.Models;
using System.Data;
using System.Data.Common;
using System.Net.Mail;

namespace Nice_Admin_Panal.DAL.SEC_User
{
    public class SEC_UserDALBase:DALHelper
    {
        #region dbo_PR_SEC_User_SelectByUserNamePassword
        public DataTable dbo_PR_SEC_User_SelectByUserNamePassword(string UserName, string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("PR_SEC_User_SelectByUsernameAndPassword");
                sqlDB.AddInParameter(dbCommand,"@UserName",DbType.String,UserName);
                sqlDB.AddInParameter(dbCommand, "@Password", DbType.String, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr=sqlDB.ExecuteReader(dbCommand))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch(Exception ex) { return null; }
        }
        #endregion

        #region  dbo_PR_SEC_User_Register
        public bool dbo_PR_SEC_User_Register(SEC_UserModel sEC_UserModel)
        {
            try
            {

                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_SelectByUsername");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, sEC_UserModel.UserName);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCMD))
                {
                    dataTable.Load(dataReader);
                }
                if (dataTable.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    DbCommand dbCMD1 = sqlDB.GetStoredProcCommand("PR_SEC_User_Insert");
                    sqlDB.AddInParameter(dbCMD1, "@UserName", DbType.String, sEC_UserModel.UserName);
                    sqlDB.AddInParameter(dbCMD1, "@Password", DbType.String, sEC_UserModel.Password);
                    sqlDB.AddInParameter(dbCMD1, "@FirstName", DbType.String, sEC_UserModel.FirstName);
                    sqlDB.AddInParameter(dbCMD1, "@LastName", DbType.String, sEC_UserModel.LastName);
                    sqlDB.AddInParameter(dbCMD1, "@PhotoPath", DbType.String, sEC_UserModel.PhotoPath);
                    sqlDB.AddInParameter(dbCMD1, "@Email", DbType.String, sEC_UserModel.Email);
                   // sqlDB.AddInParameter(dbCMD1, "@Created", SqlDbType.DateTime, DBNull.Value);
                  //  sqlDB.AddInParameter(dbCMD1, "@Modified", SqlDbType.DateTime, DBNull.Value);
                    if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD1)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex) { return false; }
        }
        #endregion
    }
}
