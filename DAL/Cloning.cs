using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Reflection;
using System.Linq.Expressions;



namespace DAL
{
    public static class Cloning
    {
        public static HostingUnit Clone (this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.HostingUnitKey = original.HostingUnitKey;
            target.Owner = original.Owner;
            target.HostingUnitName = original.HostingUnitName;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    target.Diary[i, j] = original.Diary[i, j];

            target.area = original.area;
            target.TypeUnit = original.TypeUnit;
            target.pool = original.pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;
            target.Wifi = original.Wifi;
            return target;
        }
        public static Host Clone(this Host original)
        {
            Host target = new Host();
            target.FirstName = original.FirstName;
            target.LastName = original.LastName;
            target.BankAccountNumber = original.BankAccountNumber;
            BankAccount b= new BankAccount();
            b.BankName = original.BankDetails.BankName;
            b.BankNumber = original.BankDetails.BankNumber;
            b.BranchAddress = original.BankDetails.BranchAddress;
            b.BranchCity = original.BankDetails.BranchCity;
            b.BranchNumber = original.BankDetails.BranchNumber;
            target.BankDetails = b;
            target.CollectionClearance = original.CollectionClearance;
            target.commission = original.commission;
            target.ID = original.ID;
            target.PhoneNumber = original.PhoneNumber;
            target.EmailAddress = original.EmailAddress;
            
            return target;

        }
        public static T Clone<T>(this T original) where T : new() //generic clonig function 
        {

            T target = (T)Activator.CreateInstance(original.GetType());

            foreach (var originalProp in original.GetType().GetProperties())
            {
            
                originalProp.SetValue(target, originalProp.GetValue(original));
            }
            
            return target;
        }
    }

}

