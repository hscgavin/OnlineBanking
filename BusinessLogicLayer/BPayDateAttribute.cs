using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WDTAssignment2NWBA.BusinessLogicLayer
{
    public class BPayDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d.Date >= DateTime.Now.Date;
        }
    }
}