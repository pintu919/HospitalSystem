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

    public sealed class BillingService_Entity
    {
        [SqlKey(Name = "@id")]
        public int id { get; set; }

        [SqlKey(Name = "@service_name")]
        public string service_name { get; set; }

      
    }

    public sealed class BillingServiceItem_Entity
    {
        [SqlKey(Name = "@id")]
        public int id { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@service_name")]
        public string service_name { get; set; }

        [SqlKey(Name = "@cost")]
        public decimal cost { get; set; }

        [SqlKey(Name = "@selling_price")]
        public decimal selling_price { get; set; }

        [SqlKey(Name = "@minimum_selling_price")]
        public decimal minimum_selling_price { get; set; }

        [SqlKey(Name = "@type")]
        public string type { get; set; }

        [SqlKey(Name = "@is_realtime")]
        public int is_realtime { get; set; }

        [SqlKey(Name = "@discount")]
        public decimal discount { get; set; }

        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@remark")]
        public string remark { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@invoice_code")]
        public string invoice_code { get; set; }

        [SqlKey(Name = "@referal_doctor")]
        public string referal_doctor { get; set; }

        [SqlKey(Name = "@patient_name", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_name { get; set; }

        [SqlKey(Name = "@inv_collection_item", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int inv_collection_item { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

    }

    public sealed class Patient_admission_entity
    {
        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string @user_code { get; set; }

        [SqlKey(Name = "@doctor_code")]
        public string doctor_code { get; set; }

        [SqlKey(Name = "@department_code")]
        public string @department_code { get; set; }

        [SqlKey(Name = "@addmission_note")]
        public string @addmission_note { get; set; }

        [SqlKey(Name = "@document_inclusion")]
        public string document_inclusion { get; set; }

        [SqlKey(Name = "@gardianname")]
        public string gardianname { get; set; }

        [SqlKey(Name = "@mobile")]
        public string mobile { get; set; }

        [SqlKey(Name = "@roomtype_id")]
        public int @roomtype_id { get; set; }

        [SqlKey(Name = "@admit_id")]
        public int admit_id { get; set; }

        [SqlKey(Name = "@no_of_bed")]
        public int no_of_bed { get; set; }

        [SqlKey(Name = "@roomfacility")]
        public string roomfacility { get; set; }

        [SqlKey(Name = "@dtMenutable", SqlType = SqlDbType.Structured)]
        public DataTable hms_room_details { get; set; }

        [SqlKey(Name = "@comission_agent_id")]
        public int comission_agent_id  { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

    }

    public sealed class Service_Master : BLayer
    {
        public Service_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        DataSet ds = new DataSet();
        public DataSet Insert(string service_name, string hos_code, decimal cost, decimal selling_price, decimal minimum_selling_price, string type, int is_realtime, int is_admission, int ctrl,string user_code)
        {
            try
            {
                var param = new SqlParameter[12];
                param[0] = new SqlParameter("@action", "insert");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@service_name", service_name);
                param[3] = new SqlParameter("@cost", cost);
                param[4] = new SqlParameter("@selling_price", selling_price);
                param[5] = new SqlParameter("@minimum_selling_price", minimum_selling_price);
                param[6] = new SqlParameter("@type", type);
                param[7] = new SqlParameter("@is_realtime", is_realtime);
                param[8] = new SqlParameter("@is_admission", is_admission);
                param[9] = new SqlParameter("@ctrl", ctrl);
                param[10] = new SqlParameter("@user_code", user_code);
                param[11] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[11].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_service_item_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet Update(int id, string service_name, string hos_code, decimal cost, decimal selling_price, decimal minimum_selling_price, string type, int is_realtime, int is_admission, int ctrl, string user_code)
        {
            try
            {
                var param = new SqlParameter[13];
                param[0] = new SqlParameter("@action", "update");
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@hos_code", hos_code);
                param[3] = new SqlParameter("@service_name", service_name);
                param[4] = new SqlParameter("@cost", cost);
                param[5] = new SqlParameter("@selling_price", selling_price);
                param[6] = new SqlParameter("@minimum_selling_price", minimum_selling_price);
                param[7] = new SqlParameter("@type", type);
                param[8] = new SqlParameter("@is_realtime", is_realtime);
                param[9] = new SqlParameter("@is_admission", is_admission);
                param[10] = new SqlParameter("@ctrl", ctrl);
                param[11] = new SqlParameter("@user_code", user_code);
                param[12] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[12].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_service_item_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet Delete(int id, string hos_code, string user_code)
        {
            try
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@action", "delete");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@id", id);
                param[3] = new SqlParameter("@user_code", user_code);
                param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[4].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_service_item_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetAll(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "getall");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_service_item_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetAllServices(string HosCode ,string uniqrow)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getallservices");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@uniqrow", uniqrow);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_billing_service_item_master", param);
            return rs;
        }



        public DataSet GetServicecost(string ServiceId, string Type, string HosCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "getservicecost");
            param[1] = new SqlParameter("@id", ServiceId);
            param[2] = new SqlParameter("@type", Type);
            param[3] = new SqlParameter("@hos_code", HosCode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_billing_service_item_master", param);
            return rs;
        }

        public string Add(BillingServiceItem_Entity entity)
        {
           
            var param = ToSqlParams(entity);
            
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("hos_billing_service_item_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@statusmessage").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

        public string EditAddServices(BillingServiceItem_Entity entity)
        {

            var param = ToSqlParams(entity);
            
            param.Add(new SqlParameter("@action", "insert_edititem"));
            try
            {
                var rs = ApplyChanges("hos_billing_service_item_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@statusmessage").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

        public DataSet GetPatientBillingItem(string HosCode, string patientCode,string Type,string invoice_code)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "getbiilingservices");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@patient_code", patientCode);
            param[3] = new SqlParameter("@type", Type);
            param[4] = new SqlParameter("@invoice_code", invoice_code);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_billing_service_item_master", param);
            return rs;
        }

        public bool AddBillingServiceItemInvoice(DataTable dt, string HosCode, string usercode, int Comission_Agent_Id)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "insertinvoice");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@hos_code", HosCode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@Comission_Agent_Id", Comission_Agent_Id);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_billing_service_item_master", param.ToArray(), Actions.Add);
            return param[6].Value.ToInt() > 0 ? true : false;
        }

        public bool DeleteBillingService(int id, string HosCode, string invoice_code, string Type,string usercode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action",Type);
            param[1] = new SqlParameter("@id", id);
            param[2] = new SqlParameter("@hos_code", HosCode);
            param[3] = new SqlParameter("@invoice_code", invoice_code);
            param[4] = new SqlParameter("@user_code", usercode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_billing_service_item_master", param.ToArray(), Actions.Add);
            return param[6].Value.ToInt() > 0 ? true : false;
        }

        public bool confirmimsertservice(string Patientcode, string referaldcotor,int Serviceid, decimal sellingcost, decimal minsellingcost,decimal disount, string HosCode, string invoicecode)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "insert_confirm_service");
            param[1] = new SqlParameter("@patient_code", Patientcode);
            param[2] = new SqlParameter("@referal_doctor", referaldcotor);
            param[3] = new SqlParameter("@id", Serviceid);
            param[4] = new SqlParameter("@selling_price", sellingcost);
            param[5] = new SqlParameter("@minimum_selling_price", minsellingcost);
            param[6] = new SqlParameter("@discount", disount);
            param[7] = new SqlParameter("@invoice_code", invoicecode);
            param[8] = new SqlParameter("@hos_code", HosCode);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[10].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_billing_service_item_master", param.ToArray(), Actions.Add);
            return param[10].Value.ToInt() > 0 ? true : false;
        }

        public DataSet GetAllavialbaleINV(string CategoryId, string Hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getallavailbleinv");
            param[1] = new SqlParameter("@remark", CategoryId);
            param[2] = new SqlParameter("@hos_code", Hoscode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_billing_service_item_master", param);
            return rs;
        }

        public DataSet GetByCode(int id)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "record");
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_service_item_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public DataTable GetAllPatientAdmission(string Prefix, string hos_code)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getallpatientaddmission");
            param[1] = new SqlParameter("@patient_code", Prefix);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Patient_Admission", param);
            return rs.Tables[0];
         

        }

        //public List<Patient_Entity> GetAllPatientAdmission(string Prefix, string hos_code)
        //{
        //    var param = new SqlParameter[4];
        //    param[0] = new SqlParameter("@action", "getallpatientaddmission");
        //    param[1] = new SqlParameter("@patient_code", Prefix);
        //    param[2] = new SqlParameter("@hos_code", hos_code);
        //    param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[3].Direction = ParameterDirection.Output;
        //    var rs = SP_Read<Patient_Entity>("Hos_Patient_Admission", param);
        //    return rs;

        //}

        public DataSet GetAdmissionPatientdetail(string HosCode, string patientCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getadmissionpatientdetail");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@patient_code", patientCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Patient_Admission", param);
            return rs;
        }

        public DataSet GetAllService(string CliniqUniqrow = null)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Patient_Admission", param);
            return rs;
        }

        public DataSet GetRefDoctor(string CliniqUniqrow = null, string departmentcode = null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getrefdoctor");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@department_code", departmentcode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Patient_Admission", param);
            return rs;
        }

        public string AddPatientAdmission(Patient_admission_entity entity)
        {

            var param = ToSqlParams(entity);

            param.Add(new SqlParameter("@action", "admitpatient"));
            try
            {
                var rs = ApplyChanges("Hos_Patient_Admission", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@statusmessage").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

    }
}
