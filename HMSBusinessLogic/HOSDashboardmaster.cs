using HMS.BizLayer;
using HMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{
    public sealed class Hos_Dashbord_Master : BLayer
    {
        public Hos_Dashbord_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

      
        public DataSet GetDashboarddata(string hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("get_dashboarddata_v1", param);
            return rs;
        }
        public DataSet GetPatientratio(string hoscode,string ratio)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getpatientratio");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@ratio", ratio);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("get_dashboarddata_v1", param);
            return rs;
        }

        public DataSet Getbillingdata(string hoscode, string ratio)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getbilling");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@ratio", ratio);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("get_dashboarddata_v1", param);
            return rs;
        }

    }
}
