using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.Models
{
    public class CustomerModel
    {
        //public int CustomerID { get; set; }
        //public string TFN { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string PostCode { get; set; }
        //public string Phone { get; set; }
        //public DateTime ModifyDate { get; set; }
        public Customer Customer { get; set; }
                
    }
}