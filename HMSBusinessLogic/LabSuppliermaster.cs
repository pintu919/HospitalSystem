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
    public sealed class LabSupplier_Master : BLayer
    {
        public LabSupplier_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        #region "Lab Supplier Added"
        public DataSet GetAllLab(string hos_code)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", hos_code);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_labsupplier_master", param);
            return rs;
        }
        public DataSet SaveLabSupplier(string hos_code, int id, string name, string reg, string mobil, string address, int ctrl, string UserCode)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@hos_code", hos_code);
            param[2] = new SqlParameter("@supplier_id", id);
            param[3] = new SqlParameter("@lab_name", name);
            param[4] = new SqlParameter("@reg_no", reg);
            param[5] = new SqlParameter("@mobile", mobil);
            param[6] = new SqlParameter("@address", address);
            param[7] = new SqlParameter("@ctrl", ctrl);
            param[8] = new SqlParameter("@user_code", UserCode);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_labsupplier_master", param);
            return rs;
        }
        public DataTable Getdata(string hos_code, int id)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@supplier_id", id);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_labsupplier_master", param);
            return rs.Tables[0];
        }
        public DataSet DeleteData(string hos_code, int id, string UserCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@supplier_id", id);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@user_code", UserCode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_labsupplier_master", param);
            return rs;
        }
        #endregion

        #region "LabSupplier Price"
        public DataSet GetAllGroupName(string hos_code)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getalldata");
            param[1] = new SqlParameter("@hoscode", hos_code);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_Supplierinvestigation_price", param);
            return rs;
        }
        public DataSet GetAllData(string CategoryId, string Hoscode, int Supplier_Id)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@id", CategoryId);
            param[2] = new SqlParameter("@supplir_id", Supplier_Id);
            param[3] = new SqlParameter("@hoscode", Hoscode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_Supplierinvestigation_price", param);
            return rs;
        }
        public bool AddSupplierInvestigationPrice(DataTable dt, int supplir_id, string user_code)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@supplir_id", supplir_id);
            param[3] = new SqlParameter("@user_code", user_code);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_Supplierinvestigation_price", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }
        #endregion
    }
}
