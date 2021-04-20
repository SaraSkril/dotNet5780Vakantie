using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public  class Order
    {

        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        public int OrderKey { get; set; }        //check if need to put in configure
        public Status Status { get; set; }//enum status
        public DateTime CreateDate { get; set; }//date order was created 
        public DateTime OrderDate { get; set; }//date email was sent  to client - guest
        public override string ToString()
        {
            return this.ToStringProperty();
        }


    }
}
