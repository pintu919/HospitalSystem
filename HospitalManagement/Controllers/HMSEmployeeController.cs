using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.BizLogic;
using HMS.Data;
using HMS;
using System.Web.Mvc;
using HospitalManagement.Models;
using System.Data;
using System.IO;
using ImageResizer;
using System.Configuration;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSEmployeeController : _Base
    {
        // GET: HMSEmployee
        readonly HospitalDepartment HosDep;
        readonly employee_Master emp;
        readonly Employee_Entity entity;
        readonly EmployeeTiming_Entity Tentity;
        readonly EmployeeSalary_Entity Aentity;
        public HMSEmployeeController()
        {
            HosDep = new HospitalDepartment(bsInfo);
            emp = new employee_Master(bsInfo);
            entity = new Employee_Entity();
            Tentity = new EmployeeTiming_Entity();
            Aentity = new EmployeeSalary_Entity();
        }
        public ActionResult HMSEmployeeList()
        {
            EmployeeData Model = new EmployeeData();
            var dt = emp.GetAll(Convert.ToString(Session["ClinicCode"]));
            List<HMSEmployeeModel> EM = new List<HMSEmployeeModel>();
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                var ListPro = new HMSEmployeeModel
                {
                    EmployeeId = Convert.ToInt32(dr["employee_id"]),
                    EmployeeFirstname = Convert.ToString(dr["employee_firstname"]),
                    EmployeeCode = Convert.ToString(dr["employee_code"]),
                    Email = Convert.ToString(dr["email"]),
                    Phone = Convert.ToString(dr["phone"]),
                    JoinigDate = Convert.ToString(dr["joinig_date"]),
                    RoleName = Convert.ToString(dr["rolename"]),
                    EmployeeType = Convert.ToInt32(dr["employee_type"]),
                    Photo = Convert.ToString(dr["photo"]),
                    Ctrl = Convert.ToBoolean(dr["ctrl"]),
                    Designation = Convert.ToString(dr["Designation"])
                };
                EM.Add(ListPro);
            }
            Model.Rolelistdata = Extend.ToList<Usergroup_Entity>(dt.Tables[1]).OrderBy(o => o.usergroup_id).ToList();
            Model.EmpList = EM;
            return View("HMSEmployeeList", Model);
        }
        [HttpPost]
        public PartialViewResult FilterEmployee(EmployeeData EMP)
        {
            EmployeeData Model = new EmployeeData();
            List<HMSEmployeeModel> EM = new List<HMSEmployeeModel>();
            if (EMP.EmployeeCode == null && EMP.EmployeeFirstName == null && EMP.EmployeeroleId ==0)
            {
                var dt = emp.GetAll(Convert.ToString(Session["ClinicCode"]));
                foreach (DataRow dr in dt.Tables[0].Rows)
                {
                    var ListPro = new HMSEmployeeModel
                    {
                        EmployeeId = Convert.ToInt32(dr["employee_id"]),
                        EmployeeFirstname = Convert.ToString(dr["employee_firstname"]),
                        EmployeeCode = Convert.ToString(dr["employee_code"]),
                        Email = Convert.ToString(dr["email"]),
                        Phone = Convert.ToString(dr["phone"]),
                        JoinigDate = Convert.ToString(dr["joinig_date"]),
                        RoleName = Convert.ToString(dr["rolename"]),
                        EmployeeType = Convert.ToInt32(dr["employee_type"]),
                        Photo = Convert.ToString(dr["photo"]),
                        Ctrl = Convert.ToBoolean(dr["ctrl"]),
                        Designation = Convert.ToString(dr["Designation"])
                    };
                    EM.Add(ListPro);
                }
            }
            else
            {
                var dt = emp.GetAllFilter(Convert.ToString(Session["ClinicCode"]), EMP.EmployeeCode, EMP.EmployeeFirstName, EMP.EmployeeroleId);
                foreach (DataRow dr in dt.Tables[0].Rows)
                {
                    var ListPro = new HMSEmployeeModel
                    {
                        EmployeeId = Convert.ToInt32(dr["employee_id"]),
                        EmployeeFirstname = Convert.ToString(dr["employee_firstname"]),
                        EmployeeCode = Convert.ToString(dr["employee_code"]),
                        Email = Convert.ToString(dr["email"]),
                        Phone = Convert.ToString(dr["phone"]),
                        JoinigDate = Convert.ToString(dr["joinig_date"]),
                        RoleName = Convert.ToString(dr["rolename"]),
                        EmployeeType = Convert.ToInt32(dr["employee_type"]),
                        Photo = Convert.ToString(dr["photo"]),
                        Ctrl = Convert.ToBoolean(dr["ctrl"]),
                        Designation = Convert.ToString(dr["Designation"])
                    };
                    EM.Add(ListPro);
                }
            }
            Model.EmpList = EM;
            Model.Rolelistdata = new List<Usergroup_Entity>();
            return PartialView("HMSEmployeeList", Model);
        }
        public ActionResult AddEmployee(HMSEmployeeModel Emplo, string Submit)
        {
            string[] Status = new string[1];
            Emplo.HosDepartmentLst = new List<HospitalDepartment_Entity>();
            Emplo.Rolelst = new List<Usergroup_Entity>();
            if (Submit == "Search")
            {
                if (Emplo.DoctorCode == null)
                {
                    Status[0] = "Please Enter Doctor Code or Doctor BMDC No.!!";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
                var DataSet = emp.ExistingDoctor(Emplo.DoctorCode, Emplo.HosDepartmentcode, Emplo.RoleId);
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    Emplo.EmployeeFirstname = Convert.ToString(DataSet.Tables[0].Rows[0]["doctor_name"]);
                    Emplo.Email = Convert.ToString(DataSet.Tables[0].Rows[0]["work_email"]);
                    Emplo.Phone = Convert.ToString(DataSet.Tables[0].Rows[0]["doctor_mobile"]);
                    string fname =  Convert.ToString(DataSet.Tables[0].Rows[0]["doctor_image"]);
                    //if (System.IO.File.Exists(fname))
                    //{
                    //    fname = (ConfigurationManager.AppSettings["doctorUrl"] + Convert.ToString(DataSet.Tables[0].Rows[0]["doctor_image"]));
                    //}
                    Emplo.Photo = fname;
                    Emplo.DMBCNO = Convert.ToString(DataSet.Tables[0].Rows[0]["doctror_bmdc_reg_no"]);
                    Emplo.DoctorCode = Convert.ToString(DataSet.Tables[0].Rows[0]["doctor_code"]);
                    Emplo.DeparmentName = Convert.ToString(DataSet.Tables[0].Rows[0]["Hos_department_name"]);
                    Emplo.status = 1;
                    return PartialView("_FindDoctorDetails", Emplo);
                    //Status = "Doctor Record Allrady Exists!";
                    //return Json(Status, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Emplo.EmployeeFirstname = "";
                    Emplo.Email = "";
                    Emplo.Phone = "";
                    Emplo.Photo = "";
                    Emplo.DMBCNO = "";
                    Emplo.DeparmentName = "";
                    Emplo.status = 0;
                    return PartialView("_FindDoctorDetails", Emplo);
                }
            }
            Emplo.RoleName = emp.GetRolName(Emplo.RoleId);
            if (Emplo.RoleName.ToLower() == "doctor" && Submit == "Create Employee")
            {
                if (Emplo.DoctorCode == null)
                {
                    Status[0] = "Please Enter Doctor Code or Doctor BMDC No.!!";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var DataSet = emp.ExistingDoctor(Emplo.DoctorCode, Emplo.HosDepartmentcode, Emplo.RoleId);
                    if (DataSet.Tables[0].Rows.Count > 0)
                    {
                        var entity = SetEntityData(Emplo);
                        entity.doctor_code = Convert.ToString(DataSet.Tables[0].Rows[0]["doctor_code"]);
                        var res = emp.Add(entity);
                        if (res != "same")
                        {
                            if (res.ToInt() > 0 ? true : false)
                            {
                                Status[0] = "Record Added Successfully!";
                            }
                            else
                                Status[0] = "Record Not Saved!";
                        }
                        else
                            Status[0] = "Doctor record allrady exists in our hospital !!";
                    }
                    else
                        Status[0] = "Doctor Inactive";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
            }
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var EM = DateTime.Now.ToString("yyMMddHHmmssff");
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase imgfile = files[0];
                    var folderpath = ConfigurationManager.AppSettings["addEmpimgUrl"];
                    string myfile = EM + Path.GetExtension(imgfile.FileName);
                    string fname = folderpath + "" + myfile.Replace(" ", string.Empty);

                    if (System.IO.File.Exists(folderpath + Emplo.Photo))
                    {
                        System.IO.File.Delete(folderpath + Emplo.Photo);
                    }
                    imgfile.SaveAs(fname);
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = 200,
                        Height = 200,
                        Format = "jpg"
                    };
                    ImageBuilder.Current.Build(fname, fname, resizeSetting);
                    Emplo.Photo = myfile.Replace(" ", string.Empty);
                    //Emplo.Photo = "/Image/EmployeeProfile/" + imgfile.FileName.Replace(" ", string.Empty);
                }
                if (Emplo.EmployeeFirstname != null)
                {
                    var entity = SetEntityData(Emplo);
                    string password = GeneratePassword();
                    entity.password = emp.EncString(password);
                    entity.confirmpass = emp.EncString(password);
                    var res = emp.Add(entity);
                    if (res != "same")
                    {
                        if (res.ToInt() > 0 ? true : false)
                        {
                            var emaildetails = emp.GetEmployeeEmailDetails(res.ToInt(), password);
                            string msg = new Email_Master(bsInfo).SendEmail(emaildetails);
                            if (msg == "1")
                            {
                                Status[0] = "Record Added Successfully! Username And Password Send To Email Address";
                                ModelState.Clear();
                            }
                            else
                                Status[0] = "Record Added Successfully! Error When Send Email Address";
                        }
                        else
                            Status[0] = "Record Not Saved!";
                    }
                    else
                        Status[0] = "Record Allrady Exists!";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
                ModelState.Clear();
            }
            return View("AddHMSEmployee", Emplo);
        }
        public ActionResult AddHMSEmployee()
        {
            HMSEmployeeModel model = new HMSEmployeeModel();
            var dataset = HosDep.GetAllActiveDept_Role(Convert.ToString(Session["ClinicCode"]));
            model.HosDepartmentLst = Extend.ToList<HospitalDepartment_Entity>(dataset.Tables[0]);
            model.Rolelst = Extend.ToList<Usergroup_Entity>(dataset.Tables[1]).OrderBy(o => o.usergroup_id).ToList();
            return View("AddHMSEmployee", model);
        }
        public Employee_Entity SetEntityData(HMSEmployeeModel EMP)
        {
            if (EMP.EmployeeFirstname != null)
                entity.employee_firstname = EMP.EmployeeFirstname.Trim();
            if (EMP.EmployeeLastname != null)
                entity.employee_lastname = EMP.EmployeeLastname.Trim();
            entity.employee_code = EMP.EmployeeCode;
            entity.Ctrl = EMP.Ctrl.ToInt();
            entity.phone = EMP.Phone;
            if (EMP.Email != null)
                entity.email = EMP.Email.Trim();
            entity.joinig_date = EMP.JoinigDate == null ? DateTime.Now.ToString("dd/MM/yyyy").Replace('-','/') : EMP.JoinigDate;
            entity.roleid = EMP.RoleId;
            entity.doctor_code = EMP.DoctorCode;
            entity.hos_code = Convert.ToString(Session["ClinicCode"]);
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.hosde_code = EMP.HosDepartmentcode;
            entity.employee_type = EMP.EmployeeType;
            entity.photo = EMP.Photo;
            entity.designation = EMP.Designation;
            //entity.verification_code = Dr.VerificationCode;
            return entity;
        }
        public ActionResult EditEmployee(HMSEmployeeModel Emp, string Submit)
        {
            //string Status = "";
            string[] Status = new string[1];
            // var dataset = HosDep.GetAllActiveDept_Role(Convert.ToString(Session["ClinicCode"]));
            Emp.HosDepartmentLst = new List<HospitalDepartment_Entity>(); //Extend.ToList<HospitalDepartment_Entity>(dataset.Tables[0]).OrderByDescending(o => o.Hos_department_id).ToList();
            Emp.Rolelst = new List<Usergroup_Entity>();//Extend.ToList<Usergroup_Entity>(dataset.Tables[1]).OrderByDescending(o => o.usergroup_id).ToList();
            if (Submit == "Search")
            {
                if (Emp.DoctorCode == null)
                {
                    Status[0] = "Please Enter Doctor Code or Doctor BMDC No.!!";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
                var DataSetedit = emp.ExistingDoctor(Emp.DoctorCode, Emp.HosDepartmentcode, Emp.RoleId);
                if (DataSetedit.Tables[0].Rows.Count > 0)
                {
                    Emp.EmployeeFirstname = Convert.ToString(DataSetedit.Tables[0].Rows[0]["doctor_name"]);
                    Emp.Email = Convert.ToString(DataSetedit.Tables[0].Rows[0]["work_email"]);
                    Emp.Phone = Convert.ToString(DataSetedit.Tables[0].Rows[0]["doctor_mobile"]);
                    Emp.Photo = Convert.ToString(DataSetedit.Tables[0].Rows[0]["doctor_image"]);
                    Emp.DMBCNO = Convert.ToString(DataSetedit.Tables[0].Rows[0]["doctror_bmdc_reg_no"]);
                    Emp.DoctorCode = Convert.ToString(DataSetedit.Tables[0].Rows[0]["doctor_code"]);
                    Emp.DeparmentName = Convert.ToString(DataSetedit.Tables[0].Rows[0]["Hos_department_name"]);
                    Emp.status = 1;
                    return PartialView("_FindDoctorDetails", Emp);
                    //Status = "Doctor Record Allrady Exists!";
                    //return Json(Status, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Emp.EmployeeFirstname = "";
                    Emp.Email = "";
                    Emp.Phone = "";
                    Emp.Photo = "";
                    Emp.DMBCNO = "";
                    Emp.DeparmentName = "";
                    Emp.status = 0;
                    return PartialView("_FindDoctorDetails", Emp);
                }
            }
            Emp.RoleName = emp.GetRolName(Emp.RoleId);
            if (Submit == "Save" && Emp.RoleName.ToLower() == "doctor")
            {
                if (Emp.DoctorCode == null)
                {
                    Status[0] = "Please Enter Doctor Code or Doctor BMDC No.!!";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
                var DataSet = emp.ExistingDoctor(Emp.DoctorCode, Emp.HosDepartmentcode, Emp.RoleId);
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    var entity = SetEntityData(Emp);
                    var res = emp.Update(entity);
                    if (res == true)
                    {
                        Status[0] = "Record Update Sucessfully";
                    }
                    else
                        Status[0] = "Record Does Not Update Sucessfully";
                }
                else
                    Status[0] = "Doctor Inactive";
                return Json(Status, JsonRequestBehavior.AllowGet);
            }
            else if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var EM = DateTime.Now.ToString("yyMMddHHmmssff");
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase imgfile = files[0];
                    var folderpath = ConfigurationManager.AppSettings["addEmpimgUrl"];
                    string myfile = EM + Path.GetExtension(imgfile.FileName);
                    string fname = folderpath + "" + myfile.Replace(" ", string.Empty);
                    if (System.IO.File.Exists(folderpath + Emp.Photo))
                    {
                        System.IO.File.Delete(folderpath + Emp.Photo);
                    }
                    imgfile.SaveAs(fname);
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = 200,
                        Height = 200,
                        Format = "jpg"
                    };
                    ImageBuilder.Current.Build(fname, fname, resizeSetting);
                    Emp.Photo = myfile.Replace(" ", string.Empty);
                    //Emp.Photo = "/Image/EmployeeProfile/" + imgfile.FileName.Replace(" ", string.Empty);
                }
                var entity = SetEntityData(Emp);
                var res = emp.Update(entity);
                if (res == true)
                {
                    Status[0] = "Record Update Sucessfully!!";
                }
                else
                    Status[0] = "Record Does Not Update Sucessfully!!";
                return Json(Status, JsonRequestBehavior.AllowGet);
            }
            return View("EditHMSEmployee", Emp);
        }
        public ActionResult EditHMSEmployee()
        {
            HMSEmployeeModel model = new HMSEmployeeModel();
            //var code = Request.Path.Split('/')[3];
            var code = Convert.ToString(Session["UniqRow"]);
            var DataSet = emp.GetByCode(code, Convert.ToString(Session["ClinicCode"]));
            var CModel = Extend.ToList<Employee_Entity>(DataSet.Tables[0]).SingleOrDefault();
            model.HosDepartmentLst = Extend.ToList<HospitalDepartment_Entity>(DataSet.Tables[1]);
            model.Rolelst = Extend.ToList<Usergroup_Entity>(DataSet.Tables[2]).OrderByDescending(o => o.usergroup_id).ToList();
            model.EmployeeFirstname = CModel.employee_firstname;
            model.EmployeeLastname = CModel.employee_lastname;
            model.EmployeeCode = CModel.employee_code;
            model.Age = CModel.age;
            model.Phone = CModel.phone;
            model.Email = CModel.email;
            model.ZipCode = CModel.zip;
            model.PresentAddress = CModel.present_address;
            model.Gender = CModel.gender;
            model.DateOfBirth = CModel.dob;
            model.MaritalStatus = CModel.marital_status;
            model.JoinigDate = CModel.joinig_date;
            model.RoleId = CModel.roleid;
            model.RoleName = CModel.rolename;
            model.DoctorCode = CModel.doctor_code;
            model.HosDepartmentcode = CModel.hosde_code;
            model.HosCode = CModel.hos_code;
            model.UserName = CModel.username;
            model.Password = CModel.password;
            model.Confirmpass = CModel.confirmpass;
            model.Ctrl = CModel.Ctrl.ToBool();
            model.EmployeeType = CModel.employee_type;
            model.Photo = Convert.ToString(CModel.photo);
            model.Designation = CModel.designation;
            return View("EditHMSEmployee", model);
        }
        public ActionResult EmployeeDelete(int EmployeeId)
        {
            bool result = false;
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = emp.Delete(EmployeeId, user_code);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region "Common Method For Time,Salary,Reset Password"
        public PartialViewResult GetData(string Submit)
        {
            EmployeeData Model = new EmployeeData();
            Model.ActionType = Submit.Split('/')[0];
            if (Submit.Split('/')[0] == "Timing")
            {
                Model.EmpTime = EmployeeTime(Submit.Split('/')[1]);
                Model.EmpList = null;
                Model.EmpSalary = null;
            }
            else if (Submit.Split('/')[0] == "Salary")
            {
                Model.EmpSalary = EmployeeSalary(Submit.Split('/')[1]);
                Model.EmpTime = null;
                Model.EmpList = null;
            }

            return PartialView("HMSEmployeeList", Model);
        }

        public PartialViewResult GetSalaryCalculate(string EmployeeId, string Amount)
        {
            EmployeeData Model = new EmployeeData();
            Model.EmpTime = null;
            Model.EmpList = null;
            Model.EmpSalary = EmployeeSalaryCalculate(Convert.ToString(Session["ClinicCode"]), EmployeeId, Convert.ToDecimal(Amount));
            return PartialView("HMSEmployeeList", Model);
        }
        public List<HMSEmployeeSalary> EmployeeSalaryCalculate(string hoscode, string employeecode, decimal amount)
        {
            List<HMSEmployeeSalary> tm = new List<HMSEmployeeSalary>();
            var rs = emp.GetAllEmpSlaryCalculate(Convert.ToString(Session["ClinicCode"]), employeecode, Convert.ToDecimal(amount));
            tm.Add(new HMSEmployeeSalary
            {
                EmployeeNetSalary = rs == null ? 0 : rs.emp_netsalary,
                EmployeeBasicSlary = rs == null ? 0 : rs.emp_basicsalary,
                EmployeeTDS = rs == null ? 0 : 0,
                EmpDearnessAllowance = rs == null ? "" : "",
                EmpStateInsurance = rs == null ? 0 : 0,
                EmpHouserentAllowance = rs == null ? "" : rs.emp_hra,
                EmpProvidentFund = rs == null ? 0 : 0,
                EmpConveyance = rs == null ? 0 : rs.emp_Conveyance,
                EmpAllowance = rs == null ? 0 : rs.emp_allowance,
                EmpProfTax = rs == null ? 0 : 0,
                EmpMedicalAllowance = rs == null ? 0 : rs.emp_medical_allowance,
                EmpLabourWelfare = rs == null ? 0 : 0,
                EmpFund = rs == null ? 0 : 0,
                EmpOther = rs == null ? "" : "",
                EmployeeCode = employeecode,
                Ctrl = rs == null ? false : Convert.ToBoolean(1)
            });
            return tm;
        }
        public HMSEmployeeTime EmployeeTime(string Emp_Code)
        {
            HMSEmployeeTime tm = new HMSEmployeeTime();
            var res = emp.GetAllTiming(Convert.ToString(Session["ClinicCode"]), Emp_Code);
            tm = SetDatainModel(res);
            tm.EmployeeCode = Emp_Code;
            return tm;
        }
        public HMSEmployeeTime SetDatainModel(DataSet res)
        {

            HMSEmployeeTime tm = new HMSEmployeeTime();
            tm.DayesCheckBoxItems = new List<CheckBoxModel>()
                {
                    new CheckBoxModel {Text="Sunday",IsChecked=false },
                    new CheckBoxModel {Text="Monday",IsChecked=false },
                    new CheckBoxModel {Text="Tuesday",IsChecked=false },
                    new CheckBoxModel {Text="Wednesday" ,IsChecked=false},
                    new CheckBoxModel {Text="Thursday",IsChecked=false },
                    new CheckBoxModel {Text="Friday" ,IsChecked=false},
                    new CheckBoxModel {Text="Saturday" ,IsChecked=false}
                };
            if (res.Tables.Count == 2)
            {
                if(res.Tables[0].Rows.Count > 0)
                {
                    tm.EffectiveDate = Convert.ToString(res.Tables[0].Rows[0]["effective_date"]);
                    tm.InTime = Convert.ToString(res.Tables[0].Rows[0]["in_time"]);
                    tm.OutTime = Convert.ToString(res.Tables[0].Rows[0]["out_time"]);
                    tm.IsCustom = false;
                    tm.Status = Convert.ToString(res.Tables[0].Rows[0]["Status"]);
                    tm = SetWeekenddayes(res.Tables[0], tm);
                }
                if (res.Tables[1].Rows.Count > 0)
                {
                    tm.SunIn = Convert.ToString(res.Tables[1].Rows[0]["sun_in"]);
                    tm.SunOut = Convert.ToString(res.Tables[1].Rows[0]["sun_out"]);
                    tm.MonIn = Convert.ToString(res.Tables[1].Rows[0]["mon_in"]);
                    tm.MonOut = Convert.ToString(res.Tables[1].Rows[0]["mon_out"]);
                    tm.TueIn = Convert.ToString(res.Tables[1].Rows[0]["tue_in"]);
                    tm.TueOut = Convert.ToString(res.Tables[1].Rows[0]["tue_out"]);
                    tm.WedIn = Convert.ToString(res.Tables[1].Rows[0]["wed_in"]);
                    tm.WedOut = Convert.ToString(res.Tables[1].Rows[0]["wed_out"]);
                    tm.ThuIn = Convert.ToString(res.Tables[1].Rows[0]["thu_in"]);
                    tm.ThuOut = Convert.ToString(res.Tables[1].Rows[0]["thu_out"]);
                    tm.FriIn = Convert.ToString(res.Tables[1].Rows[0]["fri_in"]);
                    tm.FriOut = Convert.ToString(res.Tables[1].Rows[0]["fri_out"]);
                    tm.SatIn = Convert.ToString(res.Tables[1].Rows[0]["sat_in"]);
                    tm.SatOut = Convert.ToString(res.Tables[1].Rows[0]["sat_out"]);
                }                                              
            }
            else
            {
                if (res.Tables[0].Rows.Count > 0)
                {
                    tm.EffectiveDate = Convert.ToString(res.Tables[0].Rows[0]["effective_date"]);
                    tm.SunIn = Convert.ToString(res.Tables[0].Rows[0]["sun_in"]);
                    tm.SunOut = Convert.ToString(res.Tables[0].Rows[0]["sun_out"]);
                    tm.MonIn = Convert.ToString(res.Tables[0].Rows[0]["mon_in"]);
                    tm.MonOut = Convert.ToString(res.Tables[0].Rows[0]["mon_out"]);
                    tm.TueIn = Convert.ToString(res.Tables[0].Rows[0]["tue_in"]);
                    tm.TueOut = Convert.ToString(res.Tables[0].Rows[0]["tue_out"]);
                    tm.WedIn = Convert.ToString(res.Tables[0].Rows[0]["wed_in"]);
                    tm.WedOut = Convert.ToString(res.Tables[0].Rows[0]["wed_out"]);
                    tm.ThuIn = Convert.ToString(res.Tables[0].Rows[0]["thu_in"]);
                    tm.ThuOut = Convert.ToString(res.Tables[0].Rows[0]["thu_out"]);
                    tm.FriIn = Convert.ToString(res.Tables[0].Rows[0]["fri_in"]);
                    tm.FriOut = Convert.ToString(res.Tables[0].Rows[0]["fri_out"]);
                    tm.SatIn = Convert.ToString(res.Tables[0].Rows[0]["sat_in"]);
                    tm.SatOut = Convert.ToString(res.Tables[0].Rows[0]["sat_out"]);
                    tm.IsCustom = Convert.ToBoolean(res.Tables[0].Rows[0]["is_custom"]);
                    tm.Status = Convert.ToString(res.Tables[0].Rows[0]["Status"]);
                    tm = SetWeekenddayes(res.Tables[0], tm);
                }
            }
            return tm;
        }
        public HMSEmployeeTime SetWeekenddayes(DataTable rs, HMSEmployeeTime tm)
        {
            string dayes = Convert.ToString(rs.Rows[0]["weekend_days"]);
            string[] weekenddayes = dayes.Split(',');
            foreach (var item in weekenddayes)
            {
                if (item == "Sunday")
                    tm.DayesCheckBoxItems[0].IsChecked = true;
                else if (item == "Monday")
                    tm.DayesCheckBoxItems[1].IsChecked = true;
                else if (item == "Tuesday")
                    tm.DayesCheckBoxItems[2].IsChecked = true;
                else if (item == "Wednesday")
                    tm.DayesCheckBoxItems[3].IsChecked = true;
                else if (item == "Thursday")
                    tm.DayesCheckBoxItems[4].IsChecked = true;
                else if (item == "Friday")
                    tm.DayesCheckBoxItems[5].IsChecked = true;
                else if (item == "Saturday")
                    tm.DayesCheckBoxItems[6].IsChecked = true;
            }
            return tm;
        }
        public List<HMSEmployeeSalary> EmployeeSalary(string Emp_Code)
        {
            List<HMSEmployeeSalary> tm = new List<HMSEmployeeSalary>();
            var rs = emp.GetAllEmpSlary(Convert.ToString(Session["ClinicCode"]), Emp_Code);
            tm.Add(new HMSEmployeeSalary
            {
                EmployeeNetSalary = rs == null ? 0 : rs.emp_netsalary,
                EmployeeBasicSlary = rs == null ? 0 : rs.emp_basicsalary,
                EmployeeTDS = rs == null ? 0 : rs.emp_tds,
                EmpDearnessAllowance = rs == null ? "" : rs.emp_da,
                EmpStateInsurance = rs == null ? 0 : rs.emp_esi,
                EmpHouserentAllowance = rs == null ? "" : rs.emp_hra,
                EmpProvidentFund = rs == null ? 0 : rs.emp_pf,
                EmpConveyance = rs == null ? 0 : rs.emp_Conveyance,
                EmpAllowance = rs == null ? 0 : rs.emp_allowance,
                EmpProfTax = rs == null ? 0 : rs.emp_prof_tax,
                EmpMedicalAllowance = rs == null ? 0 : rs.emp_medical_allowance,
                EmpLabourWelfare = rs == null ? 0 : rs.emp_labour_Welfare,
                EmpFund = rs == null ? 0 : rs.emp_fund,
                EmpOther = rs == null ? "" : rs.emp_other,
                EmployeeCode = Emp_Code,
                Ctrl = rs == null ? false : Convert.ToBoolean(rs.Ctrl)
            });
            return tm;
        }

        [HttpPost]
        public ActionResult employeeTiming(EmployeeData model)
        {
            string WeekendDay = "";
            var WeekendDayes = model.EmpTime.DayesCheckBoxItems.Where(m => m.IsChecked == true).ToList();
            foreach (var item in WeekendDayes)
            {
                WeekendDay += item.Text + ",";
            }
            WeekendDay = WeekendDay.TrimEnd(',');
            DataTable dt = EmployeeTimeTable();
            DataRow dr = dt.NewRow();
            if (model.EmpTime.IsCustom)
            {
                dr["effectivedate"] = model.EmpTime.EffectiveDate == null ? Convert.ToString(DateTime.Now) : model.EmpTime.EffectiveDate;
                dr["iscustom"] = model.EmpTime.IsCustom;
                dr["sunin"] = model.EmpTime.SunIn;
                dr["sunout"] = model.EmpTime.SunOut;
                dr["monin"] = model.EmpTime.MonIn;
                dr["monout"] = model.EmpTime.MonOut;
                dr["tuein"] = model.EmpTime.TueIn;
                dr["tueout"] = model.EmpTime.TueOut;
                dr["wedin"] = model.EmpTime.WedIn;
                dr["wedout"] = model.EmpTime.WedOut;
                dr["thuin"] = model.EmpTime.ThuIn;
                dr["thuout"] = model.EmpTime.ThuOut;
                dr["friin"] = model.EmpTime.FriIn;
                dr["friout"] = model.EmpTime.FriOut;
                dr["satin"] = model.EmpTime.SatIn;
                dr["satout"] = model.EmpTime.SatOut;
                dr["weekenddays"] = WeekendDay;
                dr["employee_code"] = model.EmpTime.EmployeeCode;
                dr["hos_code"] = Convert.ToString(Session["ClinicCode"]);
            }
            else
            {
                dr["effectivedate"] = model.EmpTime.EffectiveDate==null ? Convert.ToString(DateTime.Now) : model.EmpTime.EffectiveDate;
                dr["iscustom"] = model.EmpTime.IsCustom;
                dr["sunin"] = model.EmpTime.InTime;
                dr["sunout"] = model.EmpTime.OutTime;
                dr["monin"] = model.EmpTime.InTime;
                dr["monout"] = model.EmpTime.OutTime;
                dr["tuein"] = model.EmpTime.InTime;
                dr["tueout"] = model.EmpTime.OutTime;
                dr["wedin"] = model.EmpTime.InTime;
                dr["wedout"] = model.EmpTime.OutTime;
                dr["thuin"] = model.EmpTime.InTime;
                dr["thuout"] = model.EmpTime.OutTime;
                dr["friin"] = model.EmpTime.InTime;
                dr["friout"] = model.EmpTime.OutTime;
                dr["satin"] = model.EmpTime.InTime;
                dr["satout"] = model.EmpTime.OutTime;
                dr["weekenddays"] = WeekendDay;
                dr["employee_code"] = model.EmpTime.EmployeeCode;
                dr["hos_code"] = Convert.ToString(Session["ClinicCode"]);
            }
            dt.Rows.Add(dr);
            EmployeeTiming_Entity ent = new EmployeeTiming_Entity();
            ent.Employeetimetable = dt;
            ent.employee_code = model.EmpTime.EmployeeCode;
            ent.hos_code = Convert.ToString(Session["ClinicCode"]);
            ent.user_code = Convert.ToString(Session["UserCode"]);
            DataSet res = emp.AddEmpTiming(ent);
            EmployeeData Model = new EmployeeData();
            Model.EmpSalary = null;
            Model.EmpList = null;
            Model.EmpTime = SetDatainModel(res);
            Model.EmpTime.EmployeeCode = model.EmpTime.EmployeeCode;
            return PartialView("HMSEmployeeList", Model);
        }
        public DataTable EmployeeTimeTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("effectivedate", typeof(string));
            dt.Columns.Add("iscustom", typeof(bool));
            dt.Columns.Add("sunin", typeof(string));
            dt.Columns.Add("sunout", typeof(string));
            dt.Columns.Add("monin", typeof(string));
            dt.Columns.Add("monout", typeof(string));
            dt.Columns.Add("tuein", typeof(string));
            dt.Columns.Add("tueout", typeof(string));
            dt.Columns.Add("wedin", typeof(string));
            dt.Columns.Add("wedout", typeof(string));
            dt.Columns.Add("thuin", typeof(string));
            dt.Columns.Add("thuout", typeof(string));
            dt.Columns.Add("friin", typeof(string));
            dt.Columns.Add("friout", typeof(string));
            dt.Columns.Add("satin", typeof(string));
            dt.Columns.Add("satout", typeof(string));
            dt.Columns.Add("weekenddays", typeof(string));
            dt.Columns.Add("employee_code", typeof(string));
            dt.Columns.Add("hos_code", typeof(string));
            return dt;
        }
        public ActionResult AddEmployeeSalary(EmployeeData model)
        {
            var Aentity = SetEntityDataSalary(model.EmpSalary[0]);
            string Status = "";
            var res = emp.AddEmpSalary(Aentity);
            if (res.ToInt() > 0 ? true : false)
            {
                Status = "Record Added Successfully!";
            }
            else
                Status = "Record Update Successfully!";

            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        //public EmployeeTiming_Entity SetEntityDataTiming(HMSEmployeeTime EMP)
        //{
        //    Tentity.Sunday = EMP.Sunday;
        //    Tentity.Monday = EMP.Monday;
        //    Tentity.Tuesday = EMP.Tuesday;
        //    Tentity.Wednesday = EMP.Wednesday;
        //    Tentity.Thursday = EMP.Thursday;
        //    Tentity.Friday = EMP.Friday;
        //    Tentity.Saturday = EMP.Saturday;
        //    Tentity.emp_strattime = EMP.FromTime;
        //    Tentity.emp_endtime = EMP.ToTime;
        //    Tentity.emp_code = EMP.EmployeeCode;
        //    Tentity.Ctrl = Convert.ToInt32(EMP.Ctrl);
        //    return Tentity;
        //}

        public EmployeeSalary_Entity SetEntityDataSalary(HMSEmployeeSalary EMP)
        {
            Aentity.emp_netsalary = EMP.EmployeeNetSalary;
            Aentity.emp_basicsalary = EMP.EmployeeBasicSlary;
            Aentity.emp_tds = EMP.EmployeeTDS;
            Aentity.emp_da = EMP.EmpDearnessAllowance;
            Aentity.emp_esi = EMP.EmpStateInsurance;
            Aentity.emp_hra = EMP.EmpHouserentAllowance;
            Aentity.emp_pf = EMP.EmpProvidentFund;
            Aentity.emp_Conveyance = EMP.EmpConveyance;
            Aentity.emp_allowance = EMP.EmpAllowance;
            Aentity.emp_prof_tax = EMP.EmpProfTax;
            Aentity.emp_medical_allowance = EMP.EmpMedicalAllowance;
            Aentity.emp_labour_Welfare = EMP.EmpLabourWelfare;
            Aentity.emp_code = EMP.EmployeeCode;
            Aentity.emp_fund = EMP.EmpFund;
            Aentity.emp_other = EMP.EmpOther;
            Aentity.emp_code = EMP.EmployeeCode;
            Aentity.Ctrl = Convert.ToInt32(EMP.Ctrl);
            Aentity.user_code = Convert.ToString(Session["UserCode"]);
            Aentity.hos_code = Convert.ToString(Session["ClinicCode"]);
            return Aentity;
        }
        public ActionResult EmployeeResetPassword(string EmployeeCode)
        {
            bool result = false;
            string password = GeneratePassword();
            entity.employee_code = EmployeeCode;
            entity.password = emp.EncString(password);
            entity.confirmpass = emp.EncString(password);
            entity.user_code= Convert.ToString(Session["UserCode"]);
            entity.hos_code = Convert.ToString(Session["ClinicCode"]);
            result = emp.ResetEmployeePassword(entity);
            if (result == true)
            {
                var emaildetails = emp.getforgetpass(EmployeeCode, password);

                string msg = new Email_Master(bsInfo).SendEmail(emaildetails);
                if (msg == "1")
                    result = true;
                else
                    result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public string GeneratePassword()
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(6); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }
        #endregion
    }
}