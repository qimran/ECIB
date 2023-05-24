using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ECIB.App_Code
{
    public class BLL
    {
        public static void CreateErrorLog(string UserName,string ErrorTitle, string ErrorMsg)
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_InsertErrorLogs";
                dal.CreateProcedureCommand();
                dal.Parameters("@UserName", SqlDbType.VarChar, 100, UserName.ToLower());
                dal.Parameters("@ErrorTitle", SqlDbType.VarChar, 100, ErrorTitle.Trim());
                dal.Parameters("@ErrorMsg", SqlDbType.VarChar, 5000, ErrorMsg);
                dal.Execute();
                dal.CloseConnection();
            }
            catch (Exception exp)
            {
                
            }
        }

        public static DataTable GetUserRole(string UserName)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.openConnection();
                objDal.ProcName = "sp_SelectUserRoleId";
                objDal.CreateProcedureCommand();
                objDal.Parameters("@Username", SqlDbType.VarChar, 100, UserName);
                DataTable dt = objDal.ExecuteFillTable();
                objDal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "GetUserRole", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "GetUserRole", exp.Message);
                }
                return null;
            }
        }

        public static int AddVerifyRecord(string CNIC, string Name, string DOB, string HouseNo, string Street, string Village, string City,Int16 ProductID, int UserID)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.openConnection();
                objDal.ProcName = "sp_InsertVerifyRecord";
                objDal.CreateProcedureCommand();
                objDal.Parameters("@CNIC", SqlDbType.NVarChar, 15, CNIC);
                objDal.Parameters("@Name", SqlDbType.NVarChar, 150, Name);
                objDal.Parameters("@DOB", SqlDbType.NVarChar, 11, DOB);
                objDal.Parameters("@HouseNo", SqlDbType.NVarChar, 150, HouseNo);
                objDal.Parameters("@Street", SqlDbType.NVarChar, 150, Street);
                objDal.Parameters("@Village", SqlDbType.NVarChar, 150, Village);
                objDal.Parameters("@City", SqlDbType.NVarChar, 150, City);
                objDal.Parameters("@ProductID", SqlDbType.TinyInt, 2, ProductID);
                objDal.Parameters("@UserID", SqlDbType.Int, 4, UserID);
                int result = objDal.Execute();
                objDal.CloseConnection();
                return result;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "AddVerifyRecord", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "AddVerifyRecord", exp.Message);
                }
                return 0;
            }
        }

        public static int AddNadraRecord(string CNIC,string UserName, int UserID)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.openConnection();
                objDal.ProcName = "sp_InsertNadraRecord";
                objDal.CreateProcedureCommand();
                objDal.Parameters("@CNIC", SqlDbType.NVarChar, 13, CNIC);
                objDal.Parameters("@UserName", SqlDbType.NVarChar,50, UserName);
                objDal.Parameters("@UserID", SqlDbType.Int, 4, UserID);
               
                int result = objDal.Execute();
                objDal.CloseConnection();
                return result;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "AddVerifyRecord", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "AddVerifyRecord", exp.Message);
                }
                return 0;
            }
        }

        public static int AddUser(string User, Int16 RoleID, int BranchID, string CreatedBy)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.openConnection();
                objDal.ProcName = "sp_InsertUser";
                objDal.CreateProcedureCommand();
                objDal.Parameters("@User", SqlDbType.NVarChar, 100, User);
                objDal.Parameters("@RoleID", SqlDbType.TinyInt, 2, RoleID);
                objDal.Parameters("@BranchID", SqlDbType.Int, 4, BranchID);
                objDal.Parameters("@CreatedBy", SqlDbType.NVarChar, 50, CreatedBy);

                int result = objDal.Execute();
                objDal.CloseConnection();
                return result;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "AddUser", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "AddUser", exp.Message);
                }
                return 0;
            }
        }

        public static int DeleteUser(int UserID)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.openConnection();
                objDal.ProcName = "sp_DeleteUser";
                objDal.CreateProcedureCommand();
                objDal.Parameters("@UserID", SqlDbType.Int, 4, UserID);

                int result = objDal.Execute();
                objDal.CloseConnection();
                return result;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "DeleteUser", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "DeleteUser", exp.Message);
                }
                return 0;
            }
        }

        public static DataTable LoadRequests(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_ViewRequest";
                dal.CreateProcedureCommand();
                dal.Parameters("@FromDate", SqlDbType.DateTime, 50, FromDate);
                dal.Parameters("@ToDate", SqlDbType.DateTime, 50, ToDate);
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadRequests", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadRequests", exp.Message);
                }
                return null;
            }
        }

        public static DataTable LoadNadraRequests(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_ViewNADRARequest";
                dal.CreateProcedureCommand();
                dal.Parameters("@FromDate", SqlDbType.DateTime, 50, FromDate);
                dal.Parameters("@ToDate", SqlDbType.DateTime, 50, ToDate);
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadRequests", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadRequests", exp.Message);
                }
                return null;
            }
        }
        //public static DataTable LoadVerisys(DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        DAL dal = new DAL();
        //        dal.openConnection();
        //       dal.ProcName = "sp_Insertnadrarecord";
        //        dal.CreateProcedureCommand();
        //        dal.Parameters("@FromDate", SqlDbType.DateTime, 50, FromDate);
        //        dal.Parameters("@ToDate", SqlDbType.DateTime, 50, ToDate);

        //        DataTable dt = dal.ExecuteFillTable();
        //        dal.CloseConnection();
        //        return dt;
        //    }
        //    catch (Exception exp)
        //    {
        //        if (HttpContext.Current.Session["UserName"].ToString() == null)
        //        {
        //            CreateErrorLog("System", "LoadRequests", exp.ToString());
        //        }
        //        else
        //        {
        //            CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadRequests", exp.Message);
        //        }
        //        return null;
        //    }
        //}

        public static DataTable LoadRequestsByUserID(DateTime FromDate, DateTime ToDate, int UserID)
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_ViewRequestByUserID";
                dal.CreateProcedureCommand();
                dal.Parameters("@FromDate", SqlDbType.DateTime, 50, FromDate);
                dal.Parameters("@ToDate", SqlDbType.DateTime, 50, ToDate);
                dal.Parameters("@UserID", SqlDbType.Int, 2, UserID);
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadRequests", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadRequests", exp.Message);
                }
                return null;
            }
        }

        public static DataTable LoadRequestsByUser(DateTime FromDate, DateTime ToDate, string UserName, Int16 ProductID)
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_ViewRequestByUser";
                dal.CreateProcedureCommand();
                dal.Parameters("@FromDate", SqlDbType.DateTime, 50, FromDate);
                dal.Parameters("@ToDate", SqlDbType.DateTime, 50, ToDate);
                dal.Parameters("@UserName", SqlDbType.NVarChar, 50, UserName);
                dal.Parameters("@ProductID", SqlDbType.TinyInt, 2, ProductID);
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadRequestsByUser", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadRequestsByUser", exp.Message);
                }
                return null;
            }
        }

        public static DataTable LoadNadraRequestsByUserID(DateTime FromDate, DateTime ToDate, int UserID)
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_ViewNADRARequestByUserID";
                dal.CreateProcedureCommand();
                dal.Parameters("@FromDate", SqlDbType.DateTime, 50, FromDate);
                dal.Parameters("@ToDate", SqlDbType.DateTime, 50, ToDate);
                dal.Parameters("@UserID", SqlDbType.Int, 2, UserID);
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadRequests", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadRequests", exp.Message);
                }
                return null;
            }
        }

        public static DataTable LoadProduct()
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_LoadProduct";
                dal.CreateProcedureCommand();
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadProduct", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadProduct", exp.Message);
                }
                return null;
            }
        }

        public static DataTable LoadRole()
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_LoadRole";
                dal.CreateProcedureCommand();
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadRole", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadRole", exp.Message);
                }
                return null;
            }
        }

        public static DataTable LoadBranches()
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_LoadBranches";
                dal.CreateProcedureCommand();
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadBranches", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadBranches", exp.Message);
                }
                return null;
            }
        }

        public static DataTable LoadUsers()
        {
            try
            {
                DAL dal = new DAL();
                dal.openConnection();
                dal.ProcName = "sp_LoadUsers";
                dal.CreateProcedureCommand();
                DataTable dt = dal.ExecuteFillTable();
                dal.CloseConnection();
                return dt;
            }
            catch (Exception exp)
            {
                if (HttpContext.Current.Session["UserName"].ToString() == null)
                {
                    CreateErrorLog("System", "LoadUsers", exp.ToString());
                }
                else
                {
                    CreateErrorLog(HttpContext.Current.Session["UserName"].ToString(), "LoadUsers", exp.Message);
                }
                return null;
            }
        }
    }
}