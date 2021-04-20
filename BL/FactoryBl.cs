using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactoryBl
    {
        static IBL bl = null;
        public static IBL GetBL()
        {
            if (bl == null)//we only want one object of bl(of the functions)=singelton
                bl = new IBL_Imp();
            return bl;
        }
    }
}
