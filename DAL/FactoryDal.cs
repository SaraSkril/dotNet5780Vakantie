using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        //static Idal dal = null;
        public static Idal GetDal()
        {
            // if (dal == null)//we only want one object of dal(of the functions)=singelton
            //     dal= new Dal_XML_imp();//creates an instance 
            //return dal;//that exists
            return new Dal_XML_imp();
        }
    }
}