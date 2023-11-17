using HMS.BizLayer;
using HMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;
using System.Text;
using HtmlAgilityPack;

namespace HMS.BizLogic
{
    public sealed class TestAndInvestigation_Entity
    {
        [SqlKey(Name = "@investigationmaster_id")]
        public int investigationmaster_id { get; set; }
        [SqlKey(Name = "@investigationmaster_name")]
        public string investigationmaster_name { get; set; }
        [SqlKey(Name = "@investigationpara_id")]
        public int investigationpara_id { get; set; }
        [SqlKey(Name = "@investigationpara_name")]
        public string investigationpara_name { get; set; }
        [SqlKey(Name = "@ipreference")]
        public string investigationpara_reference { get; set; }
        [SqlKey(Name = "@findipreference")]
        public string find_ip_reference { get; set; }
        [SqlKey(Name = "@appointment_code")]
        public int appointment_code { get; set; }
        [SqlKey(Name="@hoscode")]
        public string hoscode { get; set; }      
    }
    public sealed class TestAndInvestigation_Master : BLayer
    {
        public TestAndInvestigation_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        #region "Test Investigation"
        public bool AddHOSInvestigation(DataTable dt,string doctorcode,string UserCode, string EmployeeCode, int reagent_id)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@doctor_code", doctorcode);
            param[3] = new SqlParameter("@user_code", UserCode);
            param[4] = new SqlParameter("@EmployeeCode", EmployeeCode);
            param[5] = new SqlParameter("@reagent_id", reagent_id);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_TestAndInvestigation", param.ToArray(), Actions.Add);
            return param[7].Value.ToInt() > 0 ? true : false;
        }

        public bool AddHOSInvestigationOutSideReport(DataTable dt,  string UserCode, string reportpath)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "insertoutsidereport");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@reportpath", reportpath);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_TestAndInvestigation", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }

        public bool AddHOSInvestigation_OP(DataTable dt, string doctorcode, string UserCode, string EmployeeCode, int reagent_id)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@doctor_code", doctorcode);
            param[3] = new SqlParameter("@user_code", UserCode);
            param[4] = new SqlParameter("@EmployeeCode", EmployeeCode);
            param[5] = new SqlParameter("@reagent_id", reagent_id);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_TestAndInvestigation_OP", param.ToArray(), Actions.Add);
            return param[7].Value.ToInt() > 0 ? true : false;
        }

        public bool AddHOSInvestigationOutSideReport_OP(DataTable dt, string UserCode, string reportpath)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "insertoutsidereport");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@reportpath", reportpath);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_TestAndInvestigation_OP", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }

        public DataSet GetAllInvestigationAppointment(string HosCode,  int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getinvestigation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }

        public DataSet GetAlloutsidelabInvestigationAppointment(string HosCode, int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getoutsidelabinvestigation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }

        public DataSet GetAlloutsidelabInvestigationAppointment_OP(string HosCode, int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getoutsidelabinvestigation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation_OP", param);
            return rs;
        }

        public DataSet GetAllApproveInvestigationAppointment(string HosCode,  int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getinvestigation_approve");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }

        public DataSet GetAllInvestigationAppointment_OP(string HosCode,  int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getinvestigation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation_OP", param);
            return rs;
        }

        public DataSet GetAllApproveInvestigationAppointment_OP(string HosCode,  int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getinvestigation_approve");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation_OP", param);
            return rs;
        }

        public DataSet GetAllInvestigationforBottleUsed(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getinvestigationforbottleused");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }

        public DataSet GetAlloperationItem(string HosCode,int status,DateTime? fromdate,DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getoperation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }

        public DataSet GetAlloperationItem_OP(string HosCode, int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getoperation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation_OP", param);
            return rs;
        }
        public DataSet GetAllInvetigationPara(int service_id, int Investigationid, string Appointmentcode, string unique_invstigation_id, string HosCode, string InvType,int reagent_id)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigationid", Investigationid);
            param[3] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[4] = new SqlParameter("@unique_invstigation_id", unique_invstigation_id);
            param[5] = new SqlParameter("@hoscode", HosCode);
            //Add by Dhaval
            param[6] = new SqlParameter("@invtype", InvType);
            param[7] = new SqlParameter("@reagent_id", reagent_id);
            //end
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }
        public DataSet getReagentLists(int Investigationid,string HosCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "regent_lists");
            param[1] = new SqlParameter("@investigationid", Investigationid);
            param[2] = new SqlParameter("@hoscode", HosCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }
        public DataSet GetAllInvetigationPara_OP(int service_id, int Investigationid, string invoice_code, 
            string unique_invstigation_id, string HosCode, string InvType,int reagent_id)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigation_id", Investigationid);
            param[3] = new SqlParameter("@invoce_code", invoice_code);
            param[4] = new SqlParameter("@test_id", Convert.ToInt64(unique_invstigation_id));
            param[5] = new SqlParameter("@hoscode", HosCode);
            param[6] = new SqlParameter("@invtype", InvType);
            param[7] = new SqlParameter("@reagent_id", reagent_id);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation_OP", param);
            return rs;
        }
        #endregion
        #region "Sample Investigation"
        public int AddInvestigationsample(int Investigationid, string Appointmentcode, Int64 barcode,string barcodepath)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "updatesample");
            param[1] = new SqlParameter("@investigationid", Investigationid);
            param[2] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[3] = new SqlParameter("@barcode", barcode);
            param[4] = new SqlParameter("@barcodepath", barcodepath);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_TestAndInvestigation", param.ToArray(), Actions.Add);
            return param[6].Value.ToInt() > 0 ? 1 : 0;
        }

        public int AddInvoperation(int service_id, int Investigationid, string Appointmentcode,int supplierid, Int64 barcode, string barcodepath, string hoscode, string UserCode)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "updateoperation");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigationid", Investigationid);
            param[3] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[4] = new SqlParameter("@supplierid", supplierid);
            param[5] = new SqlParameter("@barcode", barcode);
            param[6] = new SqlParameter("@barcodepath", barcodepath);
            param[7] = new SqlParameter("@hoscode", hoscode);
            param[8] = new SqlParameter("@user_code", UserCode);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[10].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_TestAndInvestigation", param.ToArray(), Actions.Add);
            return param[10].Value.ToInt();
        }

        public bool AddInvoperation_OP(int service_id, int Investigationid, string invoice_code, int supplierid, Int64 barcode, string barcodepath, string hoscode, string UserCode)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "updateoperation");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigation_id", Investigationid);
            param[3] = new SqlParameter("@invoce_code", invoice_code);
            param[4] = new SqlParameter("@supplierid", supplierid);
            param[5] = new SqlParameter("@test_id", barcode);
            param[6] = new SqlParameter("@barcodepath", barcodepath);
            param[7] = new SqlParameter("@hoscode", hoscode);
            param[8] = new SqlParameter("@user_code", UserCode);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[10].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_TestAndInvestigation_OP", param.ToArray(), Actions.Add);
            return param[10].Value.ToInt() > 0 ? true : false;
        }

        public DataSet GetAllInvestigationAppointmentSample(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getinvestigationsample");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }

        public DataSet GetAllInvestigationCollection(string HosCode,int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getinvestigationcollection");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
        }

        public DataSet GetAllInvestigationCollection_OP(string HosCode,  int status, DateTime? fromdate, DateTime? todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getinvcollection");
            param[1] = new SqlParameter("@hoscode", HosCode);
            //param[2] = new SqlParameter("@invtype", invtype);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@fromdate", fromdate);
            param[4] = new SqlParameter("@todate", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation_OP", param);
            return rs;
        }

        //public bool AddInvestigationRegentItem(int Investigationid, string Appointmentcode, string HosCode, string UserCode)
        //{
        //    var param = new SqlParameter[7];
        //    param[0] = new SqlParameter("@action", "addregentcollection");
        //    param[1] = new SqlParameter("@investigationid", Investigationid);
        //    param[2] = new SqlParameter("@appointmentcode", Appointmentcode);
        //    param[3] = new SqlParameter("@hoscode", HosCode);
        //    param[4] = new SqlParameter("@user_code", UserCode);
        //    param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[5].Direction = ParameterDirection.Output;
        //    param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
        //    param[6].Direction = ParameterDirection.ReturnValue;
        //    ApplyChanges("Hos_TestAndInvestigation", param.ToArray(), Actions.Add);
        //    return param[6].Value.ToInt() > 0 ? true : false;
        //}

        public DataSet AddInvestigationRegentItem(int service_id, int Investigationid, string Appointmentcode, string HosCode, string UserCode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "addregentcollection");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigationid", Investigationid);
            param[3] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[4] = new SqlParameter("@hoscode", HosCode);
            param[5] = new SqlParameter("@user_code", UserCode);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;
            //param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            //param[6].Direction = ParameterDirection.ReturnValue;
            //ApplyChanges("Hos_TestAndInvestigation", param.ToArray(), Actions.Add);
            //return param[6].Value.ToInt() > 0 ? true : false;
        }

        public DataSet AddInvestigationRegentItem_OP(int service_id, int investigation_id, string invoice_code, string HosCode, string UserCode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "addregentcollection");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigation_id", investigation_id);
            param[3] = new SqlParameter("@invoce_code", invoice_code);
            param[4] = new SqlParameter("@hoscode", HosCode);
            param[5] = new SqlParameter("@user_code", UserCode);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation_OP", param);
            return rs;
           
        }

        public DataSet GetinvestigationBarcode(int Investigationid, string Appointmentcode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getinvbarcode");
            param[1] = new SqlParameter("@investigationid", Investigationid);
            param[2] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_TestAndInvestigation", param);
            return rs;

        }

        #endregion
        public  class InvReport
        {
            public StringBuilder BodyHtml { get; set; }
            public string HeaderHtml { get; set; }
            public string FooterHtml { get; set; }
        }
        public DataSet GetInvestigationID(string Appointmentcode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getinvestigationid");
            param[1] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Investigation_Report_Email", param);
            return rs;
        }
        //public InvReport GetInvestigation(string Appointmentcode, int investigationId, int service_id, string Type, string PrintType)
        //{
        //    try
        //    {
        //        SqlParameter[] para = new SqlParameter[]{
        //                     new SqlParameter("@action",Type),
        //                         new SqlParameter("@appointmentcode",Appointmentcode),
        //                          new SqlParameter("@investigationid",investigationId),
        //                          new SqlParameter("@service_id",service_id),
        //                         new SqlParameter("@PrintType",PrintType)
        //                };
        //        DataSet ds = SP_ResultSet("Investigation_Report_Email", para);
        //        InvReport report = new InvReport();
        //        StringBuilder HtmlStr = new StringBuilder((Convert.ToString(ds.Tables[0].Rows[0]["investigation"])));
        //        string html = string.Empty;

        //        DataRow[] dr = ds.Tables[1].Select();
        //        if (dr.Count() > 0)
        //        {
        //            foreach (var item in dr)
        //            {
        //                DataRow[] result = ds.Tables[2].Select("investigationsubgrop_id=" + Convert.ToInt32(item[0]) + "");
        //                html += " <tr style='margin-top: 10px;display: block;'><th colspan ='3' style='text-align: left;'> " + Convert.ToString(item[1]) + "</th></tr>";
        //                foreach (var para1 in result)
        //                {
        //                    html += "<tr><td style='border: none;'> " + para1[4] + " </td>" +
        //                           "<td style='border: none;'>" + para1[6] + "<span style='margin-left:10px;display:inline-block;'>" + para1[7] + "</span> </td>" +
        //                           "<td style='border: none;'>" + para1[5] + "</td>" +
        //                           "</tr>";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            DataRow[] result = ds.Tables[2].Select();
        //            foreach (var para1 in result)
        //            {
        //                html += "<tr><td style='border: none;'> " + para1[4] + " </td>" +
        //                       "<td style='border: none;'> " + para1[6] + " </td>" +
        //                       "<td style='border: none;'>" + para1[5] + "</td>" +
        //                       "</tr>";
        //            }
        //        }
        //        HtmlStr.Replace("<tr><td>#bind_tbl_row_html</td></tr>", html);
        //        html = string.Empty;
        //        if (ds.Tables[3].Rows.Count > 0)
        //        {
        //            string Designation = "";
        //            if (Convert.ToString(ds.Tables[3].Rows[0]["doctor_designation"]) != null && Convert.ToString(ds.Tables[3].Rows[0]["doctor_designation"]) != "")
        //            {
        //                foreach (var item in Convert.ToString(ds.Tables[3].Rows[0]["doctor_designation"]).Split('.'))
        //                {
        //                    if (item != "")
        //                        Designation += "<p style='margin-bottom:0;margin-top:5px;'>" + item + "</p>";
        //                }
        //            }
        //            html += "<tr><td style='text-align:right;font-size:14px;'><h3 style ='margin: 0; font-size:16px; line-height:1; color:#1f1f1f;'><b>" + Convert.ToString(ds.Tables[3].Rows[0]["doctor_name"]) + "</b></h3>" +
        //                //"<p style='margin-bottom:0;margin-top:5px;'>" + Convert.ToString(ds.Tables[3].Rows[0]["Hos_department_name"]) + "</p>" +
        //                //"<p style='margin-bottom: 0;margin-top:5px;'>" + Convert.ToString(ds.Tables[3].Rows[0]["education"]) + "</p>" +
        //                //"<p style='margin-bottom: 0;margin-top:5px;'>" + Convert.ToString(ds.Tables[3].Rows[0]["clinic"]) + "</p>" +
        //                Designation +
        //                "</td></tr> ";
        //            HtmlStr.Replace("<tr><td>#bind_tbl_doctor</td></tr>", html);
        //            html = string.Empty;
        //        }
        //        if (ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
        //        {
        //            HtmlStr.Replace("<tr><td>#bind_tbl_doctor</td></tr>", "<div style='text-align:center;color:red;font-size:28px;font-weight:600;margin-top:20px;'>Report is under processing</div>");
        //        }
        //        string date = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        //        HtmlStr.Replace("#Print_Date", date);

        //        report.BodyHtml = HtmlStr;
        //        HtmlDocument htmldoc = new HtmlDocument();
        //        htmldoc.LoadHtml(Convert.ToString(ds.Tables[0].Rows[0]["Header"]));
        //        var divs = htmldoc.DocumentNode.SelectNodes("//table");
        //        if (divs != null)
        //        {
        //            foreach (var tag in divs)
        //            {
        //                if (tag.Attributes["id"] != null && string.Compare(tag.Attributes["id"].Value, "removetable", StringComparison.InvariantCultureIgnoreCase) == 0)
        //                {
        //                    tag.Remove();
        //                }
        //            }
        //        }
        //        report.FooterHtml = Convert.ToString(ds.Tables[0].Rows[0]["Footer"]);
        //        report.HeaderHtml = htmldoc.DocumentNode.InnerHtml.ToString();

        //        return report;
        //    }
        //    catch (Exception ex)
        //    {
               
        //        throw;
        //    }
        //}
        public InvReport GetInvestigation(string Appointmentcode, int investigationId, int service_id, string Type, string PrintType,string uniq_id,int regent_id,string invtype)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[]{
                             new SqlParameter("@action",Type),
                                 new SqlParameter("@appointmentcode",Appointmentcode),
                                  new SqlParameter("@investigationid",investigationId),
                                  new SqlParameter("@service_id",service_id),
                                 new SqlParameter("@PrintType",PrintType),
                                 new SqlParameter("@unique_invstigation_id",uniq_id),
                                 new SqlParameter("@reagent_id",regent_id),
                                 new SqlParameter("@InvType",invtype)
                        };
                DataSet ds = SP_ResultSet("Investigation_Report_Print_v1", para);
                InvReport report = new InvReport();
                StringBuilder HtmlStr = new StringBuilder((Convert.ToString(ds.Tables[0].Rows[0]["investigation"])));
                string html = string.Empty;

                //int IsRadiology = 0;
                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    IsRadiology = Convert.ToInt16(ds.Tables[1].Rows[0]["is_radiology"]);
                //}
                if (invtype == "radiology")
                {
                    html += "<tbody>";
                    html += Convert.ToString(ds.Tables[1].Rows[0]["investigationsubgroup_name"]);
                    html += "</tbody>";
                }
                else
                {
                    html += "<thead><tr style='border:1px solid rgb(222, 226, 230)!important;background-color:#ced9e1;background-color:#dadee2;border-color:#dee2e6;'>" +
                        "<td style='padding:8px 5px;border:1px solid rgb(222, 226, 230)!important;border-bottom:none;padding-bottom:5px;text-align:left;color:#556774;border-collapse:collapse;text-transform: uppercase;'>" +
                        "<h4 style='font-weight:600;color:#2e3e4a;margin:0;'>Perameter</h4></td><td style='padding:8px 5px;border:1px6 solid rgb(222, 226, 230)!important;border-bottom:none;padding-bottom: 5px;text-align:left;color:#556774;border-collapse: collapse;text-transform: uppercase;'>" +
                        "<h4 style='font-weight:600;color:#2e3e4a;margin:0;'>Result</h4></td>" +
                        "<td style='width:35%;padding:8px 5px;border:1px solid rgb(222, 226, 230)!important;border-bottom:none;padding-bottom:5px;text-align:left;color:#556774;border-collapse:collapse;text-transform:uppercase;'>" +
                        "<h4 style='font-weight:600;color:#2e3e4a;margin:0;'>Reference Vaule</h4></td></tr> </thead>";

                    DataRow[] dr = ds.Tables[1].Select();
                    html += "<tbody>";
                    if (dr.Count() > 0)
                    {
                        foreach (var item in dr)
                        {
                            DataRow[] result = ds.Tables[2].Select("investigationsubgrop_id=" + Convert.ToInt32(item[2]) + "");
                            html += " <tr> <td style='padding-top: 8px; padding-bottom:6px; font-size:14px;'> " + Convert.ToString(item[3]) + "</td>" +
                                "<td style='color: #556774;padding-bottom:2px;vertical-align:top;'>" + item[7] + "<span style='margin-left:10px;display:inline-block;'>" + item[5] + "</span> </td>" +
                                       "<td style='color: #556774;padding-bottom:2px;vertical-align:top;'>" + SetReference(Convert.ToString(item[6])) + "</td></tr>";
                            foreach (var para1 in result)
                            {
                                html += "<tr><td style='color: #2e3e4a;padding-bottom:2px;vertical-align:top;'> " + para1[6] + " </td>" +
                                       "<td style='color: #556774;padding-bottom:2px;vertical-align:top;'>" + para1[8] + "<span style='margin-left:10px;display:inline-block;'>" + para1[10] + "</span> </td>" +
                                       "<td style='color: #556774;padding-bottom:2px;vertical-align:top;'>" + SetReference(Convert.ToString(para1[7])) + "</td>" +
                                       "</tr>";
                            }
                        }
                    }
                    else
                    {
                        DataRow[] result = ds.Tables[2].Select();
                        foreach (var para1 in result)
                        {
                            html += "<tr><td style='color:#2e3e4a;padding-bottom:2px;vertical-align: top;'> " + para1[5] + " </td>" +
                                   "<td style='color: #556774;padding-bottom:2px;vertical-align: top;'> " + para1[8] + "<span style='margin-left:10px;display:inline-block;'>" + para1[10] + "</span> </td>" +
                                   "<td style='color: #556774;padding-bottom:2px;vertical-align: top;'>"
                                   + SetReference(Convert.ToString(para1[7])) +
                                   "</td>" +
                                   "</tr>";
                        }
                    }
                    html += "</tbody>";
                }

                HtmlStr.Replace("<tr><td>#bind_tbl_row_html</td></tr>", html);
                html = string.Empty;
                if (invtype != "radiology")
                {
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        string Designation = "";
                        string Designation1 = "";
                        string TechnicianName = "";
                        string Techniciansign = "";
                        if (Convert.ToString(ds.Tables[3].Rows[0]["doctor_designation"]) != null && Convert.ToString(ds.Tables[3].Rows[0]["doctor_designation"]) != "")
                        {
                            foreach (var item in Convert.ToString(ds.Tables[3].Rows[0]["doctor_designation"]).Split('.'))
                            {
                                if (item != "")
                                    Designation += "<span style='color:#556774 !important;display:block;font-size:13px;line-height:18px;padding-top:4px;'>" + item + "</span>";

                            }
                        }

                        if (ds.Tables[4].Rows.Count > 0 && Convert.ToString(ds.Tables[4].Rows[0]["department"]) != null && Convert.ToString(ds.Tables[4].Rows[0]["department"]) != "")
                        {
                            //foreach (var item in Convert.ToString(ds.Tables[4].Rows[0]["department"]))
                            //{
                            //if (item != "")
                            Designation1 += "<span style='color:#556774 !important;display:block;font-size:13px;line-height:18px;padding-top:4px;'>" + Convert.ToString(ds.Tables[4].Rows[0]["department"]) + "</span>";
                            TechnicianName = Convert.ToString(ds.Tables[4].Rows[0]["username"]);
                            Techniciansign = Convert.ToString(ds.Tables[4].Rows[0]["EmployeeSignature"]);
                            //}
                        }
                        else
                        {
                            Techniciansign = "http://fileftp.amaderdoctor.com/EmployeeSignature/upload_signature.png";
                        }

                        html += "<tr>" +
                            "<td style='vertical-align: top;text-align:center; padding:2px; width:33%;'><h4 style='margin:0 0px 0px 0px; font-weight:normal; color:#2e3e4a;'><u>Check By</u></h4>" +
                            "<div style='text-align:center; display:block; margin:10px auto 5px auto;'><img src=" + Techniciansign + " style='max-width: 120px;' alt='signature'></div>" +
                        "<b style='color:#2e3e4a;font-size:14px;line-height:18px;'>" + TechnicianName + "</b>" +
                        Designation1 + "</td>" +
                         "<td style='vertical-align: top;text-align:center; padding:2px; width:33%;'><h4 style='margin:0 0px 0px 0px; font-weight:normal; color:#2e3e4a;'><u>Test Done By</u></h4>" +
                            "<div style='text-align:center; display:block; margin:10px auto 5px auto;'><img src=" + Convert.ToString(ds.Tables[5].Rows[0]["EmployeeSignature"]) + " style='max-width: 120px;' alt='signature'></div>" +
                            "<b style='color:#2e3e4a;font-size:14px;line-height:18px;'>" + Convert.ToString(ds.Tables[5].Rows[0]["username"]) + "</b>" +
                            "<span style='color:#556774 !important;display:block;font-size:13px;line-height:18px;padding-top:4px;'>" + Convert.ToString(ds.Tables[5].Rows[0]["department"]) + " </ span ></td>" +
                            "<td style='vertical-align: top;text-align:center; padding:2px; width:33%;'><h4 style='margin:0 0px 0px 0px; font-weight:normal; color:#2e3e4a;'><u>Consultant</u></h4>" +
                            "<div style='text-align:center; display:block; margin:10px auto 5px auto;'><img src=" + Convert.ToString(ds.Tables[3].Rows[0]["signature_path"]) + " style='max-width: 120px;' alt='signature'></div>" +
                            "<b style='color:#2e3e4a;font-size:14px;line-height:18px;'>" + Convert.ToString(ds.Tables[3].Rows[0]["doctor_name"]) + "</b>" +
                        Designation + "</td>" +
                            "</tr> ";

                        HtmlStr.Replace("<tr><td>#bind_tbl_doctor</td></tr>", html);
                        html = string.Empty;
                    }
                    if (ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
                    {
                        HtmlStr.Replace("<tr><td>#bind_tbl_doctor</td></tr>", "<div style='text-align:center;color:red;font-size:28px;font-weight:600;margin-top:20px;'>Report is under processing</div>");
                    }
                }
                else
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        string Designation = "";
                        string Designation1 = "";
                        string TechnicianName = "";
                        string Techniciansign = "";
                        if (Convert.ToString(ds.Tables[2].Rows[0]["doctor_designation"]) != null && Convert.ToString(ds.Tables[2].Rows[0]["doctor_designation"]) != "")
                        {
                            foreach (var item in Convert.ToString(ds.Tables[2].Rows[0]["doctor_designation"]).Split('.'))
                            {
                                if (item != "")
                                    Designation += "<span style='color:#556774 !important;display:block;font-size:13px;line-height:18px;padding-top:4px;'>" + item + "</span>";

                            }
                        }

                        if (ds.Tables[3].Rows.Count > 0 && Convert.ToString(ds.Tables[3].Rows[0]["department"]) != null && Convert.ToString(ds.Tables[3].Rows[0]["department"]) != "")
                        {
                            //foreach (var item in Convert.ToString(ds.Tables[4].Rows[0]["department"]))
                            //{
                            //if (item != "")
                            Designation1 += "<span style='color:#556774 !important;display:block;font-size:13px;line-height:18px;padding-top:4px;'>" + Convert.ToString(ds.Tables[3].Rows[0]["department"]) + "</span>";
                            TechnicianName = Convert.ToString(ds.Tables[3].Rows[0]["username"]);
                            Techniciansign = Convert.ToString(ds.Tables[3].Rows[0]["EmployeeSignature"]);
                            //}
                        }
                        else
                        {
                            Techniciansign = "http://fileftp.amaderdoctor.com/EmployeeSignature/upload_signature.png";
                        }

                        html += "<tr>" +
                            "<td style='vertical-align: top;text-align:center; padding:2px; width:33%;'><h4 style='margin:0 0px 0px 0px; font-weight:normal; color:#2e3e4a;'><u>Check By</u></h4>" +
                            "<div style='text-align:center; display:block; margin:10px auto 5px auto;'><img src=" + Techniciansign + " style='max-width: 120px;' alt='signature'></div>" +
                        "<b style='color:#2e3e4a;font-size:14px;line-height:18px;'>" + TechnicianName + "</b>" +
                        Designation1 + "</td>" +
                         "<td style='vertical-align: top;text-align:center; padding:2px; width:33%;'><h4 style='margin:0 0px 0px 0px; font-weight:normal; color:#2e3e4a;'><u>Test Done By</u></h4>" +
                            "<div style='text-align:center; display:block; margin:10px auto 5px auto;'><img src=" + Convert.ToString(ds.Tables[4].Rows[0]["EmployeeSignature"]) + " style='max-width: 120px;' alt='signature'></div>" +
                            "<b style='color:#2e3e4a;font-size:14px;line-height:18px;'>" + Convert.ToString(ds.Tables[4].Rows[0]["username"]) + "</b>" +
                            "<span style='color:#556774 !important;display:block;font-size:13px;line-height:18px;padding-top:4px;'>" + Convert.ToString(ds.Tables[4].Rows[0]["department"]) + " </ span ></td>" +
                            "<td style='vertical-align: top;text-align:center; padding:2px; width:33%;'><h4 style='margin:0 0px 0px 0px; font-weight:normal; color:#2e3e4a;'><u>Consultant</u></h4>" +
                            "<div style='text-align:center; display:block; margin:10px auto 5px auto;'><img src=" + Convert.ToString(ds.Tables[2].Rows[0]["signature_path"]) + " style='max-width: 120px;' alt='signature'></div>" +
                            "<b style='color:#2e3e4a;font-size:14px;line-height:18px;'>" + Convert.ToString(ds.Tables[2].Rows[0]["doctor_name"]) + "</b>" +
                        Designation + "</td>" +
                            "</tr> ";

                        HtmlStr.Replace("<tr><td>#bind_tbl_doctor</td></tr>", html);
                        html = string.Empty;
                    }
                    if (ds.Tables[1].Rows.Count == 0)
                    {
                        HtmlStr.Replace("<tr><td>#bind_tbl_doctor</td></tr>", "<div style='text-align:center;color:red;font-size:28px;font-weight:600;margin-top:20px;'>Report is under processing</div>");
                    }
                }
                string date = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                HtmlStr.Replace("#Print_Date", date);

                report.BodyHtml = HtmlStr;
                HtmlDocument htmldoc = new HtmlDocument();
                htmldoc.LoadHtml(Convert.ToString(ds.Tables[0].Rows[0]["Header"]));
                var divs = htmldoc.DocumentNode.SelectNodes("//table");
                if (divs != null)
                {
                    foreach (var tag in divs)
                    {
                        if (tag.Attributes["id"] != null && string.Compare(tag.Attributes["id"].Value, "removetable", StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            tag.Remove();
                        }
                    }
                }
                report.FooterHtml = Convert.ToString(ds.Tables[0].Rows[0]["Footer"]);
                report.HeaderHtml = htmldoc.DocumentNode.InnerHtml.ToString();

                return report;
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
        public string SetReference(string val)
        {
            string html = "<ul style='list-style-type: none;padding-left:0;margin:0;'>";
            if (val != "" && val != null)
            {
                foreach (var r in val.Split('$'))
                {
                    html += "<li style='padding-bottom:5px;'>" + r + "</li>";
                }
            }
            html += "</ul>";
            return html;
        }
    }
}
