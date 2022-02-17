using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter.ViewModel
{
    [Table("Items")]
    public class ItemViewModel
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 1)]
        public int ItemId { get; set; }
        //[Key, Column(Order = 2)]
        public int CategoryId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public decimal ItemPrice { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase ImagePath { get; set; }
        
        public string ImagePath { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public IEnumerable<SelectListItem> CategorySelectListItem { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase File { get; set; }
    }
}