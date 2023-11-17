using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HMS;
using System.Web.Mvc;

namespace HospitalManagement.Models
{
    public class InvestigationPriceModel
    {
        [Required(ErrorMessage = "*")]
        public string CategoryCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string SubCategoryId { get; set; }
        public List<Investigationgroup_Entity> Investigationgrouplst { get; set; }
        public List<PriceListData> PriceListData { get; set; }
    }
    public class PriceListData
    {
        public int id { get; set; }
        public int investigationmaster_id { get; set; }
        public bool IsSelected { get; set; }
        public string invstigationmaster_name { get; set; }
        public string invstigationmaster_Procedure { get; set; }
        public decimal inv_cost { get; set; }
        public decimal Amount { get; set; }
        public bool ctrl { get; set; }
        public PriceListData()
        {
            ctrl = true;
        }
    }
    public class LabDoctorModel
    {
        [Required(ErrorMessage = "*")]
        public string CategoryCode { get; set; }
        public string DoctorCodes { get; set; }
        public List<Investigationgroup_Entity> InvestigationGroupLst { get; set; }
        public List<LabDoctorList> DoctorList { get; set; }
    }
    public class LabDoctorList
    {
        public string doctor_code { get; set; }
        public string doctor_name { get; set; }
        public bool IsSelected { get; set; }
        public string Signature { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase signaturepath { get; set; }

    }

    #region LabtechnicianMapping with investigation Model
    public class LabtechnicianModal
    {
        public string Status { get; set; }
        [Required(ErrorMessage = "*")]
        public string EmpCode { get; set; }
        public List<Employes> EmpList { get; set; }
        [Required(ErrorMessage = "*")]
        public string[] CategoryCode { get; set; }
        public List<SelectListItem> InvGroupList { get; set; }
        public List<LabTachnicianList> LabTechList { get; set; }
    }
    public class Employes
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string EmpCode { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string EmpName { get; set; }
    }
    public class LabTachnicianList
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string InvestigationGroupName { get; set; }
        public string SignaturePath { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Signature { get; set; }
    }
    #endregion

    #region "Investigation Collection Room"

    public class InvestigationCollectionRoomModel
    {
        public List<InvestigationCollectionRoomList> Inv_Collection_Room_Data { get; set; }
    }

    public class InvestigationCollectionRoomList
    {
        public int Id { get; set; }
        public int investigationgroup_id { get; set; }
        public string investigationgroup_code { get; set; }
        public string investigationgroup_name { get; set; }
        public string collection_room_no { get; set; }
        public bool ctrl { get; set; }
        public InvestigationCollectionRoomList()
        {
            ctrl = true;
        }
        public bool IsSelected { get; set; }

    }
    #endregion

}