using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace BE
{
   public class HostingUnit
    {
        public int HostingUnitKey { get; set; }//check if need to put in configure
        public Host Owner { get; set; }//owner of hosting unit
        public string HostingUnitName { get; set; }//name of the hosting unit
        [XmlIgnore]
         public bool[,] Diary = new bool[12, 31];//matrix of hosting unit status
        public Area area { get; set; }
        public TypeUnit TypeUnit { get; set; }
        public bool pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
        public bool ChildrensAttractions { get; set; }
        public bool Wifi { get; set; }

        public override string ToString()
        {
            
            return this.ToStringProperty();
        }

    }
}
