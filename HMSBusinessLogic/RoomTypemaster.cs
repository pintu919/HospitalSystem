using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace HMS.BizLogic
{
    public sealed class RoomType_Entity
    {
        [SqlKey(Name = "@dtMenutable", SqlType = SqlDbType.Structured)]
        public DataTable hms_room_details { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@roomtype_id")]
        public int roomtype_id { get; set; }

        [SqlKey(Name = "@roomfacility")]
        public string roomfacility { get; set; }

        [SqlKey(Name = "@admit_id")]
        public int admit_id { get; set; }

        //[SqlKey(Name = "@no_of_room")]

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string room_no { get; set; }

        [SqlKey(Name = "@no_of_bed")]
        public int bed_no { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string @hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string message { get; set; }

       
    }
    public sealed class Patient_Allotment_Entity
    {
        [SqlKey(Name = "@admit_id")]
        public int admit_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patientname { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctorname { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string roomtype { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string hos_doctor_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string room_no { get; set; }

        [SqlKey(Name = "@roomtype_id")]
        public int roomtype_id { get; set; }

        [SqlKey(Name = "@no_of_bed")]
        public int bed_no { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string bed_prefix { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool isrelease { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string message { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@patuniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid patuniqrow { get; set; }
    }

    public sealed class wardType_Entity
    {
        [SqlKey(Name = "@ward_type")]
        public string ward_type { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@ward_id")]
        public int ward_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string message { get; set; }

    }

    public sealed class Room_Entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string room_no { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int roomtype_id { get; set; }

    }
    public sealed class Bed_Entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int bed_no { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string bed_prefix { get; set; }
    }
    public sealed class RoomType_Master : BLayer
    {
        public RoomType_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public DataSet Add(DataTable dt, string hoscode, string usercode,string prefix, int start_serial)
        {
            RoomType_Entity entity = new RoomType_Entity();
            entity.hms_room_details = dt;
            entity.hos_code = hoscode;
            entity.user_code = usercode;
            entity.roomfacility = prefix;
            entity.bed_no = start_serial;
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "add"));
            try
            {
                var rs = SP_ResultSet("Hos_RoomType_master", param.ToArray());
                return rs;
            }
            catch (DException) { return null; }
        }
        public bool Updateallotment(Patient_Allotment_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "updateallotment"));
            try
            {
                ApplyChanges("Hos_RoomType_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public DataSet GetAll(string CliniqUniqrow = null)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_RoomType_master", param);
            return rs;
        }
        public bool DeleteRoomType(DataTable dt, string hoscode, string usercode)
        {

            RoomType_Entity entity = new RoomType_Entity();
            entity.hms_room_details = dt;
            entity.hos_code = hoscode;
            entity.user_code = usercode;
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "delete"));
            try
            {
                ApplyChanges("Hos_RoomType_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public bool UpdateRoomType(DataTable dt, string hoscode, string usercode)
        {

            RoomType_Entity entity = new RoomType_Entity();
            entity.hms_room_details = dt;
            entity.hos_code = hoscode;
            entity.user_code = usercode;
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "updateroomrate"));
            try
            {
                ApplyChanges("Hos_RoomType_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public DataSet GetAllotment(string CliniqUniqrow = null)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getallotment");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_RoomType_master", param);
            return rs;
        }

        public DataSet GetAllotmentApproval(string CliniqUniqrow = null)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getallotmentapprove");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_RoomType_master", param);
            return rs;
        }

        public DataSet GetRoomAllotment(string CliniqUniqrow = null, int admitid=0)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getroomallotment");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@admit_id", admitid);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_RoomType_master", param);
            return rs;
        }

        public DataSet GetRooms(string CliniqUniqrow = null, int roomtypeid = 0, string roomfacility ="")
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "getrooms");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@roomtype_id", roomtypeid);
            param[3] = new SqlParameter("@roomfacility", roomfacility);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_RoomType_master", param);
            return rs;
        }

        public DataSet GetBeds(string CliniqUniqrow = null, int roomtypeid = 0, int noofroom=0)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getbeds");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@roomtype_id", noofroom);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_RoomType_master", param);
            return rs;
        }

        public DataSet GetWards(string CliniqUniqrow = null, string roomfacility = "")
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getwards");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@roomfacility", roomfacility);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_RoomType_master", param);
            return rs;
        }

        public DataTable AddWardType(wardType_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = SP_ResultSet("Hos_WardType_master", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }

        public DataSet GetAllWard(string CliniqUniqrow = null)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_WardType_master", param);
            return rs;
        }

        public bool DeleteWardType(int wardid, string hoscode,string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@ward_id", wardid);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_WardType_master", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public int ApprovepPtientAdmission(string admitid, string gardianname, string mobile, string hoscode, string usercode)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "approveadmitpateint");
            param[1] = new SqlParameter("@admit_id", Convert.ToInt32(admitid));
            param[2] = new SqlParameter("@gardianname", gardianname);
            param[3] = new SqlParameter("@mobile", mobile);
            param[4] = new SqlParameter("@hos_code", hoscode);
            param[5] = new SqlParameter("@user_code", usercode);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_RoomType_master", param.ToArray(), Actions.Update);
                return param[7].Value.ToInt() > 0 ? 1 : 0;
            }
            catch (DException) { return 0; }
        }


    }
}
