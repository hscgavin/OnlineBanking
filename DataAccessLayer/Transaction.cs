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
    
    public partial class Transaction
    {
        [Required(ErrorMessage = "Transaction ID is required.")]
        [Display(Name = "Transaction ID")]
        public int TransactionID { get; set; }

        [Required(ErrorMessage = "Source account is required.")]
        [Display(Name = "Source Account")]
        public int AccountNumber { get; set; }

        [Display(Name = "Destination Account")]
        public Nullable<int> DestinationAccount { get; set; }

        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Number required.")]
        public decimal Amount { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Modified Date"), DataType(DataType.Date)]
        public Nullable<System.DateTime> ModifyDate { get; set; }

        [Required(ErrorMessage = "Transaction Type ID is required.")]
        [Display(Name = "Transaction ID")]
        public int TransactionTypeID { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Account Account1 { get; set; }
        public virtual TransactionType TransactionType1 { get; set; }
    }
}