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
                sqlDB.AddInParameter(dbCommand,"@UserName",SqlDbType.VarChar,UserName);
                sqlDB.AddInParameter(dbCommand, "@Password", SqlDbType.VarChar, Password);

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
        public bool dbo_PR_SEC_User_Register(string UserName, string Password, string FirstName, string LastName, string Email, string PhotoPath)
        {
            try
            {

                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_SelectByUsername");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
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
                    sqlDB.AddInParameter(dbCMD1, "@UserName", SqlDbType.VarChar, UserName);
                    sqlDB.AddInParameter(dbCMD1, "@Password", SqlDbType.VarChar, Password);
                    sqlDB.AddInParameter(dbCMD1, "@FirstName", SqlDbType.VarChar,FirstName);
                    sqlDB.AddInParameter(dbCMD1, "@LastName", SqlDbType.VarChar, LastName);
                    sqlDB.AddInParameter(dbCMD1, "@PhotoPath", SqlDbType.VarChar,PhotoPath);
                    sqlDB.AddInParameter(dbCMD1, "@Email", SqlDbType.VarChar, Email);
                    sqlDB.AddInParameter(dbCMD1, "Created", SqlDbType.DateTime, DBNull.Value);
                    sqlDB.AddInParameter(dbCMD1, "Modified", SqlDbType.DateTime, DBNull.Value);
                    Console.WriteLine(dbCMD1);
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
