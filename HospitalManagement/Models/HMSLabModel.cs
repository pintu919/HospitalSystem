using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;

namespace HospitalManagement.Models
{

    public class MainPurchase
    {
        public List<HMSPurchase> purchaseItem { get; set; }

        [Required(ErrorMessage = "*")]
        public string supplier { get; set; }

        [Required(ErrorMessage = "*")]
        public string InvoiceRefNo { get; set; }

        [Required(ErrorMessage = "*")]
        public string PurchaseDate { get; set; }
        public string PICode { get; set; }

    }
    public  class HMSPurchase
    {
        public int ReagentId { get; set; }
        public int Qty { get; set; }
        public string ProductExpiry { get; set; }
        public int QtyPerUnit { get; set; }
        public decimal Amount { get; set; }

        public int ValidityDays { get; set; }
        public List<SelectListItem> Reg_List { get; set; }
    }


    public class HosPurchaseItemInovice
    {
        public List<hos_purchaseItem_Inv> hos_purchaseItem_Invoice { get; set; }
        public List<Hos_Purchase_Entity> hos_purchaseItem { get; set; }

    }
    public partial class HosCollectionOprModel
    {
        public int testid { get; set; }

        public int[] regentid { get; set; }

        public int status { get; set; }
        public string Hoscode { get; set; }
        public int ctrl { get; set; }
        public List<TestItem_Entity> testlist { get; set; }

        public List<SelectListItem> Reg_List { get; set; }

        //public List<RegentItem_Entity> RegentList { get; set; }
    }
}