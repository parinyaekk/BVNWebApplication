using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
//using BizzErpLib.Models;

namespace BizErpBVN.Models
{
    public static class DBSys
    {
        public static string gServer = "27.254.173.130";
        public static int gPort = 5432;
        public static string gLogin = "postgres";
        public static string gPwd = "bizcorp@2021";
        public static string gDbName = "bz_sys";
        public static NpgsqlConnection gCnnObj;
        public static string gUsrOid;
        public static string gUsrCode;
        public static int gErrorResult = 0;
        public static string gErrorMsg = null;
        public static string gAppType = "BVNWEB";
        public static Guid gUUID; //ค่า UUID
        public static DateTime gDate;   //วันที่ปัจจุบัน
        public static string gSDate;    //วันที่ปัจจุบัน YMD
        public static DateTime gTimestamp;  //วันที่เวลาปัจจุบัน
        public static string gSTimestamp;   //วันที่เวลาปัจจุบัน YMD

        //เปิดฐานข้อมูล system
        public static bool GetConnectDB()
        {
            bool result = false;
            gErrorResult = 0;
            gErrorMsg = null;
            gUsrOid = null;
            gUsrCode = null;
            string s = "Host="+ gServer + 
                ";Username="+ gLogin + 
                ";Password="+ gPwd+
                ";Database="+ gDbName;
            try
            {
                gCnnObj = new NpgsqlConnection(s);
                gCnnObj.Open();
                result = true;
            }
            catch (Exception ex)
            {
                gErrorResult = ex.HResult;
                gErrorMsg = ex.Message;
                result = false;
            }
            return result;
        }

        //ปิดฐานข้อมูล
        public static void CloseDB()
        {
            gErrorResult = 0;
            gErrorMsg = null;
            if (gCnnObj == null) return;

            try
            {
                if (gCnnObj.State == System.Data.ConnectionState.Open)
                    gCnnObj.Close();
                gUsrOid = null;
                gUsrCode = null;
            }
            catch (Exception ex)
            {
                gErrorResult = ex.HResult;
                gErrorMsg = ex.Message;
            }
            finally
            {
                gCnnObj.Dispose();
                gCnnObj = null;
            }
        }

        //Login
        public static bool GetLogin(string ucode,string upwd,string uhost)
        {
            bool result = false;
            gErrorResult = 0;
            gErrorMsg = null;
            gUsrOid = null;
            string  sql = "CALL sp_usrlogin(@ucode,@upwd,@uhost,@apptype,@result,@uoid)";
            using (NpgsqlCommand cmdObj = new NpgsqlCommand(sql, gCnnObj))
            {
                try
                {
                    Guid g = Guid.NewGuid();
                    cmdObj.Parameters.AddWithValue("@ucode", ucode);
                    cmdObj.Parameters.AddWithValue("@upwd", upwd);
                    cmdObj.Parameters.AddWithValue("@uhost", uhost);
                    cmdObj.Parameters.AddWithValue("@apptype", gAppType);
                    cmdObj.Parameters.Add(new NpgsqlParameter("@result", NpgsqlTypes.NpgsqlDbType.Boolean));
                    cmdObj.Parameters["@result"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmdObj.Parameters["@result"].Value = (bool)false;
                    cmdObj.Parameters.Add(new NpgsqlParameter("@uoid", NpgsqlTypes.NpgsqlDbType.Uuid));
                    cmdObj.Parameters["@uoid"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmdObj.Parameters["@uoid"].Value = (Guid)g;
                    cmdObj.ExecuteNonQuery();

                    result = (bool)cmdObj.Parameters["@result"].Value;
                    if (result)
                    {
                        gUsrOid = cmdObj.Parameters["@uoid"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    gErrorResult = ex.HResult;
                    gErrorMsg = ex.Message;
                }
            }
            return result;
        }

        public static void GetUsrLogin()
        {
            gUsrCode = null;
            gErrorResult = 0;
            gErrorMsg = null;

            try
            {
                string sql = "select mt_code from mt_usr where oid = @oid limit 1;";
                using (NpgsqlCommand cmdObj = new NpgsqlCommand(sql, gCnnObj))
                {
                    cmdObj.Parameters.Add(new NpgsqlParameter("@oid", gUsrOid));
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmdObj);
                    DataSet _ds = new DataSet();
                    da.Fill(_ds);
                    foreach (DataRow dr in _ds.Tables[0].Rows)
                    {
                        gUsrCode = dr["mt_code"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                gErrorResult = ex.HResult;
                gErrorMsg = ex.Message;
            }
        }

        //Load Utils
        public static bool LoadUtils()
        {            
            bool result = false;
            gErrorResult = 0;
            gErrorMsg = null;

            try
            {
                string sql = "select * from q_utils;";
                using (NpgsqlCommand cmdObj = new NpgsqlCommand(sql, gCnnObj))
                {
                    NpgsqlDataReader rdObj = cmdObj.ExecuteReader();
                    if (rdObj.HasRows)
                    {
                        gUUID = (Guid)rdObj["f_uuid"];
                        gDate = (DateTime)rdObj["f_date"];
                        gSDate = (string)rdObj["f_sdate"];
                        gTimestamp = (DateTime)rdObj["f_timestamp"];
                        gSTimestamp = (string)rdObj["f_stimestamp"];
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                gErrorResult = ex.HResult;
                gErrorMsg = ex.Message;
                result = false;
            }

            return result;
        }


    }
}
