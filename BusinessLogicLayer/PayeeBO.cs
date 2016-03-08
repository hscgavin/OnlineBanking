using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.BusinessLogicLayer
{
    public class PayeeBO
    {
        internal PayeeBO()
        {
        }


        public List<Payee> GetAll()
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Payees.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}