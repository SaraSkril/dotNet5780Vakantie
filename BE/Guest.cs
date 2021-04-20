using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Guest
    {
        public int GuestRequestKey { get; set; }
        public string FirstName { get; set; }//guest first name
        public string LastName { get; set; }//guest last name
        public string EmailAddress { get; set; }//guest email address
        public Status GuestStatus { get; set; }
        public DateTime RegistrationDate { get; set; }//guest registration date to website
        public DateTime EntryDate { get; set; }//guests entry date
        public DateTime ReleaseDate { get; set; }//guests release date
        public TypeUnit TypeUnit { get; set; } //enum type
        public Area Area { get; set; }//enum Area
        public int Adults { get; set; }
        public int Children { get; set; }
        public Pool Pool { get; set; }//enum pool
        public Jacuzzi Jacuzzi { get; set; }//enum Jacuzzi
        public Garden Garden { get; set; }//enum Garden
        public ChildrensAttractions ChildrensAttractions { get; set; }//enum ChildrensAttractions
        public Wifi Wifi { get; set; }//enum wifi

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }

    }
