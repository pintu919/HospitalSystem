using HMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Models
{


    public partial class InPatient
    {

        public AddInvestigation Investigation { get; set; }
        public AddOTTransfer OTTransfer { get; set; }
        public AddIPservices IPservices { get; set; }
        public InPatientVital InPatientvitaldata { get; set; }
        public investigationparaamount parameter { get; set; }
        public List<vital_listdata> InpatientVitalList { get; set; }
        public List<InPatientData> InPatientlist { get; set; }
        public List<investigationparaamount> investigationPriceList { get; set; }
        public List<Vital_Parameter> vital_para { get; set; }
        public string uniqrowid { get; set; }
        public List<SubMedicine_List_entity> InPatientMedecineList { get; set; }
        public List<Doctor_list> doc_list { get; set; }

        public List<SuggestDoctor_list> SuggestDoctor_list { get; set; }

        public DoctorData InPatientDoctorData { get; set; }

        public List<doctor_suggestion> doctorsuggestion { get; set; }

        public string statusmsg { get; set; }

        public string status { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }
    }
    public class Vital_Parameter
    {
        [SqlKey(Display = true)]
        public string vital_code { get; set; }

        [SqlKey(Display = true)]
        public string vital_name { get; set; }
    }
    public partial class IP_OT_Transfer
    {
        public string appointment_code { get; set; }

        public int TransferId { get; set; }
        public List<Inpatient_OT_List> OT_transfer_List { get; set; }
        public OT_PreOperative OTPreOperative { get; set; }
        public string statusmsg { get; set; }
        public OT_PostOperative OTPostOperative { get; set; }
        public List<MedicineRemark_List> RemarkList { get; set; }
        public OT_Detail OTdetail { get; set; }
        public OT_Service OTservice { get; set; }

    }
  
    public partial class OT_PostMedicine
    {

        [Required(ErrorMessage = "*")]
        public string genericname { get; set; }

        [Required(ErrorMessage = "*")]
        public int mappingid { get; set; }
        public string brand_code { get; set; }

        [Required(ErrorMessage = "*")]
        public string Dosages { get; set; }

        [Required(ErrorMessage = "*")]
        public int Used { get; set; }

        [Required(ErrorMessage = "*")]
        public int Duration { get; set; }

        [SqlKey(Display = true)]
        public string Remark { get; set; }
        public string MedicineIds { get; set; }

        [SqlKey(Display = true)]
        public int id { get; set; }
        public bool other { get; set; }
    }

    public class Inpatient_OT_List
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string operation_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string operation_time { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string operation_type { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid patuniqrow { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string appointment_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_status { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int TransferId { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Age { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Gender { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Mobile { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string DepartmentName { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Profile { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public DateTime OperationDatetime { get; set; }

    }
    public class OT_PreOperative
    {
        [Required(ErrorMessage = "*")]
        public string pre_operative_date { get; set; }
        [Required(ErrorMessage = "*")]
        public string pre_operative_time { get; set; }
        public string pre_operative_advice { get; set; }
        public string pre_operative_gen_advice { get; set; }
        public string appointment_code { get; set; }
        public int TransferId { get; set; }
        public int ctrl { get; set; }

    }
    public class OT_PostOperative
    {
        [Required(ErrorMessage = "*")]

        public string post_operative_date { get; set; }

        [Required(ErrorMessage = "*")]
        public string post_operative_time { get; set; }
        public string appointment_code { get; set; }
        public OT_PostMedicine otpost_med { get; set; }
        public List<med_entity> med_list { get; set; }
        public string post_operative_advice { get; set; }
        public int TransferId { get; set; }

    }
    public class OT_Detail
    {
        public string Name_of_Operation { get; set; }
        public string Indication { get; set; }
        public string[] Surgeon { get; set; }
        public string[] Asst_Surgeon { get; set; }
        public string Findings { get; set; }
        public string Procedure { get; set; }
        public string Histology { get; set; }
        public string Anaesthetist { get; set; }
        public string Anaesthetic_Used { get; set; }
        public string Unista_Blood_Transfusion { get; set; }
        public string From_Time { get; set; }
        public string To_Time { get; set; }
        public string OT_Date { get; set; }
        public string OT_Nurse { get; set; }
        public string appointment_code { get; set; }
        public int TransferId { get; set; }
        public int ctrl { get; set; }
        //public List<Doctor_list> surgeon_lst { get; set; }

        public List<SelectListItem> surgeon_lst { get; set; }
        //public List<asstt_surgeon_Doctor_list> asst_surgeon_lst { get; set; }

        public List<SelectListItem> asst_surgeon_lst { get; set; }
        public string Asst_Surgeon_name { get; set; }


    }

    public class OT_Service
    {
        public List<OTService_Entity> ot_service_list { get; set; }
        public int Service_id { get; set; }
        public int is_realtime { get; set; }
        public decimal SellingCost { get; set; }
        public decimal Discount { get; set; }
        public string ServiceName { get; set; }
        public decimal Cost { get; set; }
        public decimal MinSellingCost { get; set; }
        public string Remark { get; set; }
        public string ServiceType { get; set; }
        public string Appointment_code { get; set; }

        public int TransferId { get; set; }

        public List<ServiceItem_Entity> Ot_Service_entity { get; set; }
    }

    public sealed class ServiceItem_Entity
    {
        [SqlKey(Name = "@id")]
        public int id { get; set; }

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

        [SqlKey(Name = "@remark")]
        public string remark { get; set; }

        

    }

    public sealed class OTService_Entity
    {
        [SqlKey(Name = "@id")]
        public int id { get; set; }

        [SqlKey(Name = "@service_name")]
        public string service_name { get; set; }


    }
    public sealed class med_entity
    {
        [SqlKey(Name = "@mapping_id")]
        public int mappingid { get; set; }
        [SqlKey(Name = "@brand_code")]
        public string brand_code { get; set; }
        [SqlKey(Name = "@Brand_Name")]
        public string Brand_Name { get; set; }

        [SqlKey(Name = "@drug_generic_name")]
        public string drug_generic_name { get; set; }

        [SqlKey(Name = "@genericname")]
        public string genericname { get; set; }

        [SqlKey(Name = "@formulationname")]
        public string formulationname { get; set; }

        [SqlKey(Name = "@strength")]
        public string strength { get; set; }

        [SqlKey(Name = "@Dosages")]
        public string Dosages { get; set; }

        [SqlKey(Name = "@Used")]
        public int Used { get; set; }

        [SqlKey(Name = "@Duration")]
        public int Duration { get; set; }

        [SqlKey(Name = "@Remark")]
        public string Remark { get; set; }

        [SqlKey(Name = "@MedicineIds")]
        public string MedicineIds { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }
        
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string frequency { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool other_med { get; set; }

       


    }

    public class SubMedicine_List_entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string appoinment_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Brand_Name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string drug_generic_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string strength { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string formulationname { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Used { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Duration { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Dosages { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Remark { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string UsingFrom { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string LastUsed { get; set; }

        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public bool IsSelected { get; set; }

        [SqlKey(Display = true)]
        public int id { get; set; }

        [SqlKey(Display = true)]
        public int status { get; set; }

        [SqlKey(Display = true)]
        public string medicinetiming { get; set; }

        [SqlKey(Display = true)]
        public int IsAlert { get; set; }

        [SqlKey(Display = true)]
        public bool isrelease { get; set; }

        [SqlKey(Display = true)]
        public string medicine_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }

    }


    public class Doctor_list
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }


    }

    public class asstt_surgeon_Doctor_list
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }


    }

    public class SuggestDoctor_list
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Hos_department_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Assign_Date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Added_By { get; set; }


    }

    public class doctor_suggestion
    {
        [SqlKey(Display = true)]
        public string suggestion { get; set; }
        [SqlKey(Display = true)]
        public string sugdate { get; set; }
        [SqlKey(Display = true)]
        public string doctor_name { get; set; }

    }

    public class vital_listdata
    {
        [SqlKey(Display = true)]
        public string vital_para { get; set; }

        [SqlKey(Display = true)]
        public string vital_value { get; set; }

        [SqlKey(Display = true)]
        public string vital_date { get; set; }

    }

    public partial class InPatientData
    {
        [SqlKey(Display = true)]
        public string UniqRow { get; set; }

        [SqlKey(Display = true)]
        public string AppoinmentCode { get; set; }

        [SqlKey(Display = true)]
        public string PatientCode { get; set; }

        [SqlKey(Display = true)]
        public string PatientName { get; set; }

        [SqlKey(Display = true)]
        public string Mobile { get; set; }

        [SqlKey(Display = true)]
        public string Age { get; set; }

        [SqlKey(Display = true)]
        public string InDate { get; set; }

        [SqlKey(Display = true)]
        public string Gender { get; set; }

        [SqlKey(Display = true)]
        public string Profile { get; set; }

        [SqlKey(Display = true)]
        public string room_no { get; set; }

        [SqlKey(Display = true)]
        public string bed_no { get; set; }

        [SqlKey(Display = true)]
        public string ward_type { get; set; }

        [SqlKey(Display = true)]
        public bool isrelease { get; set; }

        [SqlKey(Display = true)]

        public int IsAlert { get; set; }

        [SqlKey(Display = true)]
        public DateTime appointment_datetime { get; set; }

        [SqlKey(Display = true)]
        public int ottransfer { get; set; }

        [SqlKey(Display = true)]
        public Guid patuniqrow { get; set; }

        [SqlKey(Display = true)]
        public string DoctorName { get; set; }

        [SqlKey(Display = true)]
        public string DepartmentName { get; set; }

        [SqlKey(Display = true)]
        public string ReleaseDate { get; set; }

        [SqlKey(Display = true)]
        public int otstatus { get; set; }

        [SqlKey(Display = true)]
        public int otcomplete { get; set; }


    }

    public partial class InPatientVital
    {


        [Required(ErrorMessage = "*")]
        public string vital_para { get; set; }

        [Required(ErrorMessage = "*")]
        public string vital_value { get; set; }

        [Required(ErrorMessage = "*")]
        public TimeSpan vital_time { get; set; }
        public string hiddencode { get; set; }
    }

    public partial class DoctorData
    {


        [Required(ErrorMessage = "*")]
        public string doctorcode { get; set; }

        [Required(ErrorMessage = "*")]
        public string uniqrow { get; set; }
    }

    public class AddInvestigation
    {
        [Required(ErrorMessage = "*")]
        public int investigationmaster_id { get; set; }
        //public string AppointmentCode { get; set; }
        public string uniqrowid { get; set; }
        public string Instruction { get; set; }
        public List<InvListData> InvList { get; set; }

        public int checkexisting { get; set; }

        public List<investigationparaamount> existinvestigationList { get; set; }
    }



    public class AddOTTransfer
    {
        [Required(ErrorMessage = "*")]
        public string operation_date { get; set; }
        //public string AppointmentCode { get; set; }
        public string operation_time { get; set; }
        public string operation_type { get; set; }
        public List<Doctor_list> IPDoctor_lst { get; set; }
        public string doctor_code { get; set; }
        public string uniqrowid { get; set; }

    }

    public class AddIPservices
    {
        public string uniqrowid { get; set; }

        [Required(ErrorMessage = "*")]
        public int Service_id { get; set; }

        public string ServiceType { get; set; }
        public decimal Discount { get; set; }
        public int is_realtime { get; set; }
        public string ServiceName { get; set; }
        public decimal Cost { get; set; }
        public decimal MinSellingCost { get; set; }
        public decimal SellingCost { get; set; }
        public string Remark { get; set; }
        public List<IPBillingService_Item> IPBillingServicelst { get; set; }
        public List<IPBillingService_Item_list> IPBillingServicelstdata { get; set; }
        public string status { get; set; }
    }
    public class InvListData
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigationmaster_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationmaster_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal inv_cost { get; set; }
    }

    public class IPBillingService_Item
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string service_name { get; set; }




    }

    public class IPBillingService_Item_list
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string service_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Price { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Discount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }


    }

    // complete ot details

    public class Operation
    {
        public int OtId { get; set; }
        public string name_of_operation { get; set; }
        public string OperationDate { get; set; }
        public string OperationTime { get; set; }
        public string doctor_name { get; set; }
        public PostOperative Postoperative { get; set; }
        public List<POmedicine> PoMedicinelist { get; set; }
        public PreOperative Preoperative { get; set; }
        public OtDetails Otdetail { get; set; }
        public List<OtServices> Services { get; set; }
    }
    public class PostOperative
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int OtId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PostOperativedate { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PostOperativetime { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string post_operative_advice { get; set; }
    }
    public class POmedicine
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int OtId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Brand_Name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string drug_generic_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string formulationname { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string strength { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Used { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string dosage { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int duration_days { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string remark { get; set; }
    }
    public class PreOperative
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int OtId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PreOperativedate { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PreOperativetime { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string pre_operative_advice { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string pre_operative_gen_advice { get; set; }
    }
    public class OtDetails
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int OtId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string name_of_operation { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string indication { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Surgeon { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Asst_Surgeon { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string findings { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ot_procedure { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string histology { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string anaesthetist { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string anaesthetic_used { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string UBTR { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ot_from_time { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ot_to_time { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ot_date { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ot_nurse { get; set; }
    }
    public class OtServices
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int OtId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ServiceName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal selling_cost { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal discount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }
    }

    /// end complete OT Details


}