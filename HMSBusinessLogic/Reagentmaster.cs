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
    public sealed class Investigationsubgroup_Entity
    {
        [SqlKey(Name = "@investigationsubgrop_id", Direction = SqlDirect.InOut, SqlType = SqlDbType.Int)]
        public int investigationsubgrop_id { get; set; }
        [SqlKey(Name = "@investigationmaster_id")]
        public int investigationmaster_id { get; set; }
        [SqlKey(Name = "@regent_id")]
        public int reagent_id { get; set; }
        [SqlKey(Name = "@investigationsubgroup_name")]
        public string investigationsubgroup_name { get; set; }

        [SqlKey(Name = "@subgroup_unit")]
        public string unit { get; set; }
        [SqlKey(Name = "@subgroup_refrence")]
        public string reference { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }
        [SqlKey(Name = "@InvType")]
        public string InvType { get; set; }
        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string row { get; set; }
        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }
        [SqlKey(Name = "@statusmessage")]
        public string status { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int subgroup_id { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }
        //[SqlKey(Name = "@dtlog", SqlType = SqlDbType.Structured)]
        //public DataTable dtlogtable { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int indexing { get; set; }

        [SqlKey(Name = "@hoscode")]
        public string hoscode { get; set; }

        [SqlKey(Name = "@usercode")]
        public string usercode { get; set; }
    }
    public sealed class Investigationsubgrouppara_Entity
    {
        [SqlKey(Name = "@investigationpara_id", Direction = SqlDirect.InOut, SqlType = SqlDbType.Int)]
        public int investigationpara_id { get; set; }

        [SqlKey(Name = "@ipname")]
        public string investigationpara_name { get; set; }
        [SqlKey(Name = "@investigationsubgrop_id")]
        public int investigationsubgrop_id { get; set; }
        [SqlKey(Name = "@investigationmaster_id")]
        public int investigationmaster_id { get; set; }
        [SqlKey(Name = "@regent_id")]
        public int reagent_id { get; set; }

        [SqlKey(Name = "@ipreference")]
        public string investigationpara_reference { get; set; }

        [SqlKey(Name = "@units")]
        public string units { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string row { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage")]
        public string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        //[SqlKey(Name = "@dtlog", SqlType = SqlDbType.Structured)]
        //public DataTable dtlogtable { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int indexing { get; set; }

        [SqlKey(Name = "@hoscode")]
        public string hoscode { get; set; }

        [SqlKey(Name = "@usercode")]
        public string usercode { get; set; }
    }
    public sealed class Reagent_Master : BLayer
    {
        public Reagent_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        #region "Add New Reagent"
        public DataTable AddReagent(string HosCode, string reagent_name, string uses_unit, int is_usiesvalidity,
                                 string purchase_unit, int Ctrl, int InvestigationId, string UserCode, string validity_date)
        {
            var param = new SqlParameter[13];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@reagent_name", reagent_name);
            param[2] = new SqlParameter("@use_unit", uses_unit);
            param[3] = new SqlParameter("@is_usevalidity", is_usiesvalidity);
            param[4] = new SqlParameter("@unit_of_purchase", purchase_unit);
            param[5] = new SqlParameter("@ctrl", Ctrl);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[8].Direction = ParameterDirection.ReturnValue;
            param[9] = new SqlParameter("@InvestigationId", InvestigationId);
            param[10] = new SqlParameter("@hoscode", HosCode);
            param[11] = new SqlParameter("@usercode", UserCode);
            param[12] = new SqlParameter("@validity_date", validity_date);
            try
            {
                var rs = SP_ResultSet("hos_reagent_master", param.ToArray());
                return rs.Tables[0]; //param[7].Value.ToString() == "same" ? param[7].Value.ToString() : param[8].Value.ToString();
            }
            catch (DException) { return new DataTable(); }
        }
        public DataTable UpdateReagent(string reagent_name, string uses_unit, int is_usiesvalidity,
                                 string purchase_unit, int Ctrl, int InvestigationId, int regent_id,
                                 string HosCode, string UserCode, string validity_date)
        {
            var param = new SqlParameter[14];
            param[0] = new SqlParameter("@action", "update");
            param[1] = new SqlParameter("@reagent_name", reagent_name);
            param[2] = new SqlParameter("@use_unit", uses_unit);
            param[3] = new SqlParameter("@is_usevalidity", is_usiesvalidity);
            param[4] = new SqlParameter("@unit_of_purchase", purchase_unit);
            param[5] = new SqlParameter("@ctrl", Ctrl);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[8].Direction = ParameterDirection.ReturnValue;
            param[9] = new SqlParameter("@InvestigationId", InvestigationId);
            param[10] = new SqlParameter("@reagent_id", regent_id);
            param[11] = new SqlParameter("@hoscode", HosCode);
            param[12] = new SqlParameter("@usercode", UserCode);
            param[13] = new SqlParameter("@validity_date", validity_date);
            try
            {
                var rs = SP_ResultSet("hos_reagent_master", param.ToArray());
                return rs.Tables[0]; //param[8].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return new DataTable(); }
        }
        public DataSet GetAllInvestigation(string regent_id, string HosCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "investigation_lists");
            param[1] = new SqlParameter("@reagent_id", Convert.ToInt32(regent_id));
            param[2] = new SqlParameter("@hoscode", HosCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_reagent_master", param);
            return rs;
        }
        public string Delete(string HosCode, int Id)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@reagent_id", Id);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            param[5] = new SqlParameter("@hoscode", HosCode);
            try
            {
                var rs = ApplyChanges("hos_reagent_master", param.ToArray(), Actions.Delete);
                return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }
        #endregion

        #region "Reagent Lists"
        public DataSet GetAllReagent(int skipfield, int pagesize, string SearchText, string ParamaterStatus, string HosCode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@skipfield", skipfield);
            param[2] = new SqlParameter("@pagesize", pagesize);
            param[3] = new SqlParameter("@hoscode", HosCode);
            param[4] = new SqlParameter("@SearchText", SearchText);
            param[5] = new SqlParameter("@ParamaterStatus", ParamaterStatus);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_reagent_master", param);
            return rs;
        }
        public DataSet ViewInvestigationPara(int reagent_id, int investigation_id, string HosCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "view_para");
            param[1] = new SqlParameter("@reagent_id", reagent_id);
            param[2] = new SqlParameter("@InvestigationId", investigation_id);
            param[3] = new SqlParameter("@hoscode", HosCode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_reagent_master", param);
            return rs;
        }
        #endregion

        #region "Reagent Master"
        public DataSet GetAllSubGroup(int Id, string HosCode, int Reagentid)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@investigationmaster_id", Id);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@hoscode", HosCode);
            param[4] = new SqlParameter("@regent_id", Reagentid);
            var res = SP_ResultSet("hos_investigation_subgroup_master", param);
            return res;
        }
        public List<Investigationsubgroup_Entity> AddSubGroup(Investigationsubgroup_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            var rs = SP_Read<Investigationsubgroup_Entity>("hos_investigation_subgroup_master", param.ToArray());
            return rs;
        }
        public DataTable AddSubGroup_Unit_Reference(int SubGroupId, string unit, string Reference, int RegId, string HosCode, string ActType, out string status)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "add_para_value");
            param[1] = new SqlParameter("@investigationsubgrop_id", SubGroupId);
            param[2] = new SqlParameter("@subgroup_unit", unit);
            param[3] = new SqlParameter("@subgroup_refrence", Reference);
            param[4] = new SqlParameter("@regent_id", RegId);
            param[5] = new SqlParameter("@ActType", ActType);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            param[8] = new SqlParameter("@hoscode", HosCode);
            DataSet ds=  SP_ResultSet("hos_investigation_subgroup_master", param);
            status = Convert.ToString(param[7].Value);
            return ds.Tables[0];
        }
        public List<Investigationsubgroup_Entity> UpdateSubGroup(Investigationsubgroup_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            var rs = SP_Read<Investigationsubgroup_Entity>("hos_investigation_subgroup_master", param.ToArray());
            return rs;
        }
        public DataTable DeleteInveSubGroup(int ReagentId,string Cliniqcode, string usercode, int id, int inv_master_id)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@investigationsubgrop_id", id);
            param[2] = new SqlParameter("@investigationmaster_id", inv_master_id);
            param[3] = new SqlParameter("@regent_id", ReagentId);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            //param[6] = new SqlParameter("@dtlog", dt);
            param[7] = new SqlParameter("@hoscode", Cliniqcode);
            param[8] = new SqlParameter("@usercode", usercode);
            var rs = SP_ResultSet("hos_investigation_subgroup_master", param);
            return rs.Tables[0];
        }
        public bool SetSubGroupIndex(int SubGroupId, int IndexValue, string HosCode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "updateindex");
            param[1] = new SqlParameter("@investigationsubgrop_id", SubGroupId);
            param[2] = new SqlParameter("@Indexing", IndexValue);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            param[5] = new SqlParameter("@hoscode", HosCode);
            ApplyChanges("hos_investigation_subgroup_master", param.ToArray(), Actions.Update);
            var rs = param[4].Value.ToInt() > 0 ? true : false;
            return rs;
        }
        //Belowe all function for Investigation sub para
        public DataSet GetAllSubGroupPara(int Id, string Type, string HosCode, int Reagentid)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@subtype", Type);
            param[2] = new SqlParameter("@investigationsubgrop_id", Id);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@hoscode", HosCode);
            param[5] = new SqlParameter("@regent_id", Reagentid);
            var res = SP_ResultSet("hos_investigationpara_master", param);
            return res;
        }
        public List<Investigationsubgrouppara_Entity> AddSubGroupPara(Investigationsubgrouppara_Entity entity, string subtype)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            param.Add(new SqlParameter("@subtype", subtype));
            var rs = SP_Read<Investigationsubgrouppara_Entity>("hos_investigationpara_master", param.ToArray());
            return rs;
        }
        public List<Investigationsubgrouppara_Entity> UpdateSubGroupPara(Investigationsubgrouppara_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            var rs = SP_Read<Investigationsubgrouppara_Entity>("hos_investigationpara_master", param.ToArray());
            return rs;
        }
        public DataTable DeleteInveSubGroupPara(int ParaId, int SubGrpId, string Type, string Cliniqcode, string usercode,int ReagentId)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@investigationsubgrop_id", SubGrpId);
            param[2] = new SqlParameter("@investigationpara_id", ParaId);
            param[3] = new SqlParameter("@regent_id", ReagentId);
            param[4] = new SqlParameter("@subtype", Type);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            //param[7] = new SqlParameter("@dtlog", dt);
            param[8] = new SqlParameter("@hoscode", Cliniqcode);
            param[9] = new SqlParameter("@usercode", usercode);
            var rs = SP_ResultSet("hos_investigationpara_master", param);
            return rs.Tables[0];
        }
        public bool SetSubgroupParaIndex(int SubGroupParaId, int IndexValue, string HosCode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "updateindex");
            param[1] = new SqlParameter("@investigationpara_id", SubGroupParaId);
            param[2] = new SqlParameter("@Indexing", IndexValue);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            param[5] = new SqlParameter("@hoscode", HosCode);
            ApplyChanges("hos_investigationpara_master", param.ToArray(), Actions.Update);
            var rs = param[4].Value.ToInt() > 0 ? true : false;
            return rs;
        }
        public DataTable AddSubGroupPara_Unit_Reference(int inv_para_id,int SubGroupId, string unit, string Reference, int RegId, 
            string HosCode, string ActType,string type, out string status)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "add_subpara_unit_reference");
            param[1] = new SqlParameter("@investigationsubgrop_id", SubGroupId);
            param[2] = new SqlParameter("@units", unit);
            param[3] = new SqlParameter("@ipreference", Reference);
            param[4] = new SqlParameter("@regent_id", RegId);
            param[5] = new SqlParameter("@ActType", ActType);
            param[6] = new SqlParameter("@subtype", type);
            param[7] = new SqlParameter("@investigationpara_id", inv_para_id);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            param[10] = new SqlParameter("@hoscode", HosCode);
            DataSet ds = SP_ResultSet("hos_investigationpara_master", param);
            status = Convert.ToString(param[9].Value);
            return ds.Tables[0];
        }
        #endregion
    }
}
