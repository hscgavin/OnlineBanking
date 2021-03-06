//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WDTAssignment2NWBA.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Payee
    {
        public Payee()
        {
            this.BillPays = new HashSet<BillPay>();
        }

        [Display(Name = "Payee ID")]
        public int PayeeID { get; set; }

        [Required(ErrorMessage = "Payee name is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Payee name cannot be longer than 50 characters.")]
        [Display(Name = "Name")]
        public string PayeeName { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Address cannot be longer than 50 characters.")]
        public string Address { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "City cannot be longer than 40 characters.")]
        public string City { get; set; }

        [StringLength(20, MinimumLength = 1, ErrorMessage = "State cannot be longer than 20 characters.")]
        public string State { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "Post code cannot be longer than 10 characters.")]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Phone cannot be longer than 15 characters.")]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number! Phone must be in the format (61)- XXXX XXXX.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Modify date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public System.DateTime ModifyDate { get; set; }
    
        public virtual ICollection<BillPay> BillPays { get; set; }
    }
}
