using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;

namespace BizErpBVN.Models
{
    public class DBCompany
    {
        public static string gServer = "27.254.173.130";
        public static int gPort = 5432;
        public static string gLogin = "postgres";
        public static string gPwd = "bizcorp@2021";
        public static string gDbName = "bz_boonvanit";
        public static NpgsqlConnection gCnnObj;
        public static string gSaleRepOid;
        public static string gSaleRepCode;
        public static string gSaleRepName;
        public static int gErrorResult = 0;
        public static string gErrorMsg = null;
        public static string gAppType = "BVNWEB";
        public static Guid gUUID; //ค่า UUID
        public static DateTime gDate;   //วันที่ปัจจุบัน
        public static string gSDate;    //วันที่ปัจจุบัน YMD
        public static DateTime gTimestamp;  //วันที่เวลาปัจจุบัน
        public static string gSTimestamp;   //วันที่เวลาปัจจุบัน YMD

        //เปิดฐานข้อมูล Company
        public static bool GetConnectDB()
        {
            bool result = false;
            gErrorResult = 0;
            gErrorMsg = null;
            gSaleRepOid = null;
            gSaleRepCode = null;
            gSaleRepName = null;
            string s = "Host=" + gServer +
                ";Username=" + gLogin +
                ";Password=" + gPwd +
                ";Database=" + gDbName;
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
                gSaleRepOid = null;
                gSaleRepCode = null;
                gSaleRepName = null;
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

        //Load ข้อมูลพนักงานขาย
        public static void GetLoginSaleRep()
        {
            gSaleRepOid = null;
            gSaleRepCode = null;
            gSaleRepName = null;
            gErrorResult = 0;
            gErrorMsg = null;

            try
            {
                string sql = "select oid,mt_code,mt_name from mt_emp where usr_oid::text = @oid limit 1;";
                using (NpgsqlCommand cmdObj = new NpgsqlCommand(sql, gCnnObj))
                {
                    cmdObj.Parameters.Add(new NpgsqlParameter("@oid",  DBSys.gUsrOid));
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmdObj);
                    DataSet _ds = new DataSet();
                    da.Fill(_ds);
                    foreach (DataRow dr in _ds.Tables[0].Rows)
                    {
                        gSaleRepOid = dr["oid"].ToString();
                        gSaleRepCode = dr["mt_code"].ToString();
                        gSaleRepName = dr["mt_name"].ToString();
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