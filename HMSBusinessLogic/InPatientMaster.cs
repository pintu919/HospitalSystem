using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BizLogic
{
    public sealed class InPatient_Master : BLayer
    {
        public InPatient_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public DataSet GetInPatientData(string hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_inpatient");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
        }

        public DataSet GetAllfilter(string hoscode, int status, string fromdate, string todate)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_inpatient_filter");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@from_date", fromdate);
            param[4] = new SqlParameter("@to_date", todate);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
        }

        public DataSet GetInPatienInvandMed(string hoscode, string uniqrow)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getInv_med");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@uniqrow", uniqrow);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
        }

        public DataSet AddInPatientvital(string vital_para, string vital_value, TimeSpan vital_time, string code, string hoscode, string usercode)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "addinpatientvital");
            param[1] = new SqlParameter("@vital_para", vital_para);
            param[2] = new SqlParameter("@vital_value", vital_value);
            param[3] = new SqlParameter("@vital_time", vital_time);
            param[4] = new SqlParameter("@uniqrow", code);
            param[5] = new SqlParameter("@hoscode", hoscode);
            param[6] = new SqlParameter("@usercode", usercode);
            param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[8].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
            //try
            //{
            //    var rs = ApplyChanges("Hos_inpatient", param.ToArray(), Actions.Add);
            //    return param[6].Value.ToInt() > 0 ? 1 : 0;
            //}
            //catch (DException) { return 0; }
        }

        public DataSet Adddoctorsuggestion(string Doctorcode, string uniqrow, string hoscode, string usercode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "adddoctorsuggestion");
            param[1] = new SqlParameter("@vital_para", Doctorcode);
            param[2] = new SqlParameter("@uniqrow", uniqrow);
            param[3] = new SqlParameter("@hoscode", hoscode);
            param[4] = new SqlParameter("@usercode", usercode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;

        }

        public bool AddPateintMedStatus(int med_id, int status, string timing)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "addinpatientmedstatus");
            param[1] = new SqlParameter("@med_id", med_id);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@timing", timing);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_inpatient", param.ToArray(), Actions.Add);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet Patient_Admission(string Appointcode,string hoscode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "pat_admission");
            param[1] = new SqlParameter("@Appointcode", Appointcode);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient_admission_print", param);
            return rs;

        }

        public DataSet GetAllinvestigationList(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getinvestigation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
        }
        public DataSet AddNewInvestigation(int investigationid, string instruction, string Uniqrow, string HosCode, string usercode, int checkexisting)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "addnewinvestigation");
            param[1] = new SqlParameter("@investigationid", investigationid);
            param[2] = new SqlParameter("@instruction", instruction);
            param[3] = new SqlParameter("@uniqrow", Uniqrow);
            param[4] = new SqlParameter("@hoscode", HosCode);
            param[5] = new SqlParameter("@usercode", usercode);
            param[6] = new SqlParameter("@checkexisting", checkexisting);
            param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
        }

        public DataSet GetAllInPatientDoctor(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getinpatientdoctor");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
        }

        public bool AddPatientOTTransfer(string doctor_code, string operation_date, string operation_time, string operation_type, string Uniqrow, string HosCode,string usercode)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "addPatientottransfer");
            param[1] = new SqlParameter("@ipdoctor_code", doctor_code);
            param[2] = new SqlParameter("@operation_date", operation_date);
            param[3] = new SqlParameter("@operation_time", operation_time);
            param[4] = new SqlParameter("@operation_type", operation_type);
            param[5] = new SqlParameter("@uniqrow", Uniqrow);
            param[6] = new SqlParameter("@hoscode", HosCode);
            param[7] = new SqlParameter("@usercode", usercode);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_inpatient", param.ToArray(), Actions.Add);
                return param[9].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet AddPatientIPServices(int service_id, string service_name, decimal cost, decimal selling_cost, decimal minimum_selling_cost, decimal discount, string service_type, int is_realtime, string remark, string Uniqrow, string HosCode,string usercode)
        {
            var param = new SqlParameter[15];
            param[0] = new SqlParameter("@action", "addPatientservices");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@service_name", service_name);
            param[3] = new SqlParameter("@cost", cost);
            param[4] = new SqlParameter("@selling_cost", selling_cost);
            param[5] = new SqlParameter("@minimum_selling_cost", minimum_selling_cost);
            param[6] = new SqlParameter("@discount", discount);
            param[7] = new SqlParameter("@service_type", service_type);
            param[8] = new SqlParameter("@is_realtime", is_realtime);
            param[9] = new SqlParameter("@remark", remark);
            param[10] = new SqlParameter("@uniqrow", Uniqrow);
            param[11] = new SqlParameter("@hoscode", HosCode);
            param[12] = new SqlParameter("@usercode", usercode);
            param[13] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[13].Direction = ParameterDirection.Output;
            param[14] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[14].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
            //try
            //{
            //    var rs = ApplyChanges("Hos_inpatient", param.ToArray(), Actions.Add);
            //    return param[14].Value.ToInt() > 0 ? true : false;
            //}
            //catch (DException) { return false; }
        }

        public DataSet GetOttransfer(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getottransfer");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }

        public DataSet GetAllIPServices(string HosCode, string Uniqrow)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getallipservices");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@uniqrow", Uniqrow);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
        }

        public DataSet GetOTtrasferDetails(string hoscode, int TransferId)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getotdteails");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@TransferId", TransferId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }

        public DataSet AddPreoperativedata(string Date, string Time, string advice, string general_advice, int TransferId, string hoscode, string usercode)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "addpreoperative");
            param[1] = new SqlParameter("@Pre_op_date", Date);
            param[2] = new SqlParameter("@Pre_op_time", Time);
            param[3] = new SqlParameter("@advice", advice);
            param[4] = new SqlParameter("@general_advice", general_advice);
            param[5] = new SqlParameter("@TransferId", Convert.ToInt32(TransferId));
            param[6] = new SqlParameter("@hoscode", hoscode);
            param[7] = new SqlParameter("@usercode", usercode);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }

        public DataSet Addpostoperativedata(string Date, string Time, string advice,  int TransferId, string hoscode, string usercode)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "addpostoperative");
            param[1] = new SqlParameter("@Pre_op_date", Date);
            param[2] = new SqlParameter("@Pre_op_time", Time);

            param[3] = new SqlParameter("@advice", advice);
            param[4] = new SqlParameter("@TransferId", Convert.ToInt32(TransferId));
            param[5] = new SqlParameter("@hoscode", hoscode);
            param[6] = new SqlParameter("@usercode", usercode);
            param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[8].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }

        public DataSet AddPostmedicine(string brand_code,string genericname, int mappingid, string Dosages, int Duration, int Used, string Remark, int TransferId, bool other, string hoscode, string usercode)
        {
            var param = new SqlParameter[14];
            param[0] = new SqlParameter("@action", "addpostmedicine");
            param[1] = new SqlParameter("@genericname", genericname);
            param[2] = new SqlParameter("@mappingid", mappingid);
            param[3] = new SqlParameter("@Dosages", Dosages);
            param[4] = new SqlParameter("@Duration", Duration);
            param[5] = new SqlParameter("@Used", Used);
            param[6] = new SqlParameter("@Remark", Remark);
            param[7] = new SqlParameter("@TransferId", Convert.ToInt32(TransferId));
            param[8] = new SqlParameter("@other_med", other);
            param[9] = new SqlParameter("@hoscode", hoscode);
            param[10] = new SqlParameter("@usercode", usercode);
            param[11] = new SqlParameter("@brand_code", brand_code);
            param[12] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[12].Direction = ParameterDirection.Output;
            param[13] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[13].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }
        public DataSet AddOtdetails(string Nameofoperation, string Indication, string Surgon, string asstt_Surgon, string Finding, string Procedure, string Histology, string Anaesthetist,string Anaesthetic_Used,string Unista_Blood_Transfusion,string FromTime,string ToTime, string OT_Date, string OT_Nurse, int TransferId, string hoscode, string usercode, string asstt_surgon_name)
        {
            var param = new SqlParameter[21];
            param[0] = new SqlParameter("@action", "addotdetails");
            param[1] = new SqlParameter("@Name_of_Operation", Nameofoperation);
            param[2] = new SqlParameter("@Indication", Indication);
            param[3] = new SqlParameter("@Surgon", Surgon);
            param[4] = new SqlParameter("@Asst_surgon", asstt_Surgon);
            param[5] = new SqlParameter("@Findings", Finding);
            param[6] = new SqlParameter("@Procedure", Procedure);
            param[7] = new SqlParameter("@Histology", Histology);
            param[8] = new SqlParameter("@Anaesthetist", Anaesthetist);
            param[9] = new SqlParameter("@Anaesthetic_Used", Anaesthetic_Used);
            param[10] = new SqlParameter("@Unista_Blood_Transfusion",Unista_Blood_Transfusion);
            param[11] = new SqlParameter("@From_Time", FromTime);
            param[12] = new SqlParameter("@To_Time", ToTime);
            param[13] = new SqlParameter("@OT_Date", OT_Date);
            param[14] = new SqlParameter("@OT_Nurse", OT_Nurse);
            param[15] = new SqlParameter("@TransferId", Convert.ToInt32(TransferId));
            param[16] = new SqlParameter("@hoscode", hoscode);
            param[17] = new SqlParameter("@usercode", usercode);
            param[18] = new SqlParameter("@Asst_surgon_name", asstt_surgon_name);
            param[19] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[19].Direction = ParameterDirection.Output;
            param[20] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[20].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }
        public DataSet GetServicecost(string ServiceId, string Type, string HosCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getservicecost");
            param[1] = new SqlParameter("@mappingid", ServiceId);
            param[2] = new SqlParameter("@hoscode", HosCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }

        public DataSet AddOTService(int serviceid, string servicename, decimal cost, decimal sellingcost, decimal minimumsellingcost, decimal discount, string Remark, string servicetype, int isrealtime, int TransferId, string hoscode, string usercode)
        {
            var param = new SqlParameter[15];
            param[0] = new SqlParameter("@action", "addotservice");
            param[1] = new SqlParameter("@Service_id", serviceid);
            param[2] = new SqlParameter("@service_name", servicename);
            param[3] = new SqlParameter("@cost", cost);
            param[4] = new SqlParameter("@selling_price", sellingcost);
            param[5] = new SqlParameter("@minimum_selling_price", minimumsellingcost);
            param[6] = new SqlParameter("@discount", discount);
            param[7] = new SqlParameter("@Remark", Remark);
            param[8] = new SqlParameter("@type", servicetype);
            param[9] = new SqlParameter("@is_realtime", isrealtime);
            param[10] = new SqlParameter("@TransferId", Convert.ToInt32(TransferId));
            param[11] = new SqlParameter("@hoscode", hoscode);
            param[12] = new SqlParameter("@usercode", usercode);
            param[13] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[13].Direction = ParameterDirection.Output;
            param[14] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[14].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }

        public bool DeleteMed(int id, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "deletemed");
            param[1] = new SqlParameter("@Service_id", id);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@usercode", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_inpatient_Ot_Details", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public bool DeleteService(int id, string AppCode, string hoscode, string usercode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "deleteservice");
            param[1] = new SqlParameter("@Service_id", id);
            param[2] = new SqlParameter("@appointment_code", AppCode);
            param[3] = new SqlParameter("@hoscode", hoscode);
            param[4] = new SqlParameter("@usercode", usercode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_inpatient_Ot_Details", param.ToArray(), Actions.Delete);
                return param[6].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }
        public bool ReleaseOT(int Serviceid, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "releaseot");
            param[1] = new SqlParameter("@TransferId", Convert.ToInt32(Serviceid));
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@usercode", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_inpatient_Ot_Details", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet GetOTDetails(string APPCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "ot_details");
            param[1] = new SqlParameter("@code", APPCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("drsp_inpatient", param);
            return rs;
        }

        public DataSet GetOPDOTDetails(string APPCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "opdot_details");
            param[1] = new SqlParameter("@appointment_code", APPCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient_Ot_Details", param);
            return rs;
        }

        public bool ReleaseOPDOT(int Serviceid, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "releaseopdot");
            param[1] = new SqlParameter("@TransferId", Convert.ToInt32(Serviceid));
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@usercode", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_OPD_Patient_Transfer_OT", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }



    }
}
