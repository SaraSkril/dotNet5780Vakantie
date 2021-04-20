using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DataSource//accessible only in the assembly 
    {
        private static List<Host> hosts;
        private static List<Guest> guests;
        private static List<HostingUnit> hostingUnits;
        private static List<Order> orders;
        private static List<BankAccount> bankAccounts;

        public static List<Order> GetOrders()
        {
            return orders;
        }

        public static List<Host> getHosts()
        {
            return hosts;
        }

        public static List<Guest> getGuests()
        {
            return guests;
        }

        public static List<HostingUnit> getHostingUnits()
        {
            return hostingUnits;
        }

        public static List<BankAccount> GetBankAccounts()
        {
            return bankAccounts;
        }

        static DataSource()
        {
            hosts = new List<Host>();
            guests = new List<Guest>();
            hostingUnits = new List<HostingUnit>();
            orders = new List<Order>();
            bankAccounts = new List<BankAccount>();


            Host host1 = new Host();
            host1.ID = "319031530";
            host1.FirstName = "Sara Raizel";
            host1.LastName = "Skriloff";
            host1.PhoneNumber = 0527148093;
            host1.EmailAddress = "srskriloff@gmail.com";
            host1.BankDetails = new BankAccount() { BankName = "Poalim", BankNumber = 12, BranchAddress = "Kanfei Nesharim 55", BranchCity = "Jerusalem", BranchNumber = 446 };
            host1.BankAccountNumber = 11245;
            host1.CollectionClearance = CollectionClearance.Yes;
            hosts.Add(host1);

            Host host2 = new Host();
            host2.ID = "315207647";
            host2.FirstName = "Elisheva";
            host2.LastName = "Aronstam";
            host2.PhoneNumber = 0528752889;
            host2.EmailAddress = "elishevaronstam@gmail.com";
            host2.BankDetails = new BankAccount() { BankName = "Pag", BankNumber = 14, BranchAddress = "Tzealim 4", BranchCity = "Beit Shemesh", BranchNumber = 122 };
            host1.BankAccountNumber = 25478;
            host2.CollectionClearance = CollectionClearance.Yes;
            hosts.Add(host2);

            Guest guest1 = new Guest();
            guest1.GuestRequestKey = ++Configuration.GuestRequestKey;
            guest1.FirstName = "ssss";
            guest1.LastName = "Skaat";
            guest1.EmailAddress = "srskriloff@gmail.com";
            guest1.GuestStatus = Status.Active;
            guest1.RegistrationDate = DateTime.Now;
            guest1.EntryDate = new DateTime(2019,12,11);
            guest1.ReleaseDate = new DateTime(2020,12,18);
            guest1.TypeUnit = TypeUnit.Hotel;
            guest1.Area = Area.Jerusalem;
            guest1.Adults = 2;
            guest1.Children = 1;
            guest1.Pool = Pool.Interested;
            guest1.Jacuzzi = Jacuzzi.Maybe;
            guest1.Garden = Garden.NotIntersted;
            guest1.ChildrensAttractions = ChildrensAttractions.Interested;
            guest1.Wifi = Wifi.Interested;
            guests.Add(guest1);

            Guest guest2 = new Guest();
            guest2.GuestRequestKey = ++Configuration.GuestRequestKey;
            guest2.FirstName = "Noa";
            guest2.LastName = "Kirel";
            guest2.EmailAddress = "noakila@gmail.com";
            guest2.GuestStatus = Status.Active;
            guest2.RegistrationDate = DateTime.Now;
            guest2.EntryDate = new DateTime(2020, 1, 2);
            guest2.ReleaseDate = new DateTime(2020, 1, 20);
            guest2.TypeUnit = TypeUnit.CampingSite;
            guest2.Area = Area.South;
            guest2.Adults = 2;
            guest2.Children = 0;
            guest2.Pool = Pool.Interested;
            guest2.Jacuzzi = Jacuzzi.Maybe;
            guest2.Garden = Garden.NotIntersted;
            guest2.ChildrensAttractions = ChildrensAttractions.NotIntersted;
            guest2.Wifi = Wifi.Interested;
            guests.Add(guest2);

            Guest guest3 = new Guest();
            guest3.GuestRequestKey = ++Configuration.GuestRequestKey;
            guest3.FirstName = "oshri";
            guest3.LastName = "cohen";
            guest3.EmailAddress = "oshri@gmail.com";
            guest3.GuestStatus = Status.Active;
            guest3.RegistrationDate = DateTime.Now;
            guest3.EntryDate = new DateTime(2020, 2, 1);
            guest3.ReleaseDate = new DateTime(2020, 2, 10);
            guest3.TypeUnit = TypeUnit.AirBNB;
            guest3.Area = Area.All;
            guest3.Adults = 2;
            guest3.Children = 7;
            guest3.Pool = Pool.Interested;
            guest3.Jacuzzi = Jacuzzi.Maybe;
            guest3.Garden = Garden.Interested;
            guest3.ChildrensAttractions = ChildrensAttractions.Interested; ;
            guest3.Wifi = Wifi.Interested;
            guests.Add(guest3);

            HostingUnit hostingUnit1 = new HostingUnit();
            hostingUnit1.HostingUnitKey = ++Configuration.HostingUnitKey;
            hostingUnit1.HostingUnitName = "Ramada";
            hostingUnit1.Owner = host1;
            hostingUnit1.area = Area.Jerusalem;
            hostingUnit1.pool = true;
            hostingUnit1.Jacuzzi = false;
            hostingUnit1.Garden = false;
            hostingUnit1.ChildrensAttractions = true;
            hostingUnit1.Wifi = true;
            hostingUnit1.TypeUnit = TypeUnit.Hotel;
            hostingUnits.Add(hostingUnit1);

      
            HostingUnit hostingUnit2 = new HostingUnit();
            hostingUnit2.HostingUnitKey = ++Configuration.HostingUnitKey;
            hostingUnit2.HostingUnitName = "Golan Heights";
            hostingUnit2.Owner = host2;
            hostingUnit2.area = Area.North;
            hostingUnit2.pool = false;
            hostingUnit2.Jacuzzi = false;
            hostingUnit2.Garden = false;
            hostingUnit2.ChildrensAttractions = true;
            hostingUnit2.Wifi = true;
            hostingUnit2.TypeUnit = TypeUnit.AirBNB;
            hostingUnits.Add(hostingUnit2);

            HostingUnit hostingUnit3 = new HostingUnit();
            hostingUnit3.HostingUnitKey = ++Configuration.HostingUnitKey;
            hostingUnit3.HostingUnitName = "David Citadel";
            hostingUnit3.Owner = host1;
            hostingUnit3.area = Area.Jerusalem;
            hostingUnit3.pool = true;
            hostingUnit3.Jacuzzi =true;
            hostingUnit3.Garden = true;
            hostingUnit3.ChildrensAttractions = true;
            hostingUnit3.Wifi = true;
            hostingUnit3.TypeUnit = TypeUnit.Hotel;
            hostingUnits.Add(hostingUnit3);

           /* BankAccount b1 = new BankAccount(); b1.BankName = "Pag"; b1.BankNumber = 14; b1.BranchAddress = "Tzealim 4"; b1.BranchCity = "Beit Shemesh"; b1.BranchNumber = 122;
            BankAccount b2 = new BankAccount(); b2.BankName = "Poalim"; b2.BankNumber = 12; b2.BranchAddress = "Kanfei Nesharim 55"; b2.BranchCity = "Jerusalem"; b2.BranchNumber = 446;
            BankAccount b3 = new BankAccount(); b3.BankName = "Yahav"; b3.BankNumber = 11; b3.BranchAddress = "Dolev 24"; b3.BranchCity = "Tel Aviv"; b3.BranchNumber = 789;
            BankAccount b4 = new BankAccount(); b4.BankName = "Mizrachi Tfachot"; b4.BankNumber = 17; b4.BranchAddress = "Tkoa"; b4.BranchCity = "Tkoa Citi"; b4.BranchNumber = 20;
            BankAccount b5 = new BankAccount(); b5.BankName = "Bank Israel"; b5.BankNumber = 89; b5.BranchAddress = "King David "; b5.BranchCity = "Jerusalem"; b5.BranchNumber = 63;*/
            /*
            bankAccounts.Add(b1);
            bankAccounts.Add(b2);
            bankAccounts.Add(b3);
            bankAccounts.Add(b4);
            bankAccounts.Add(b5);*/

            Order order = new Order();
            order.OrderKey = ++(Configuration.OrderKey);
            order.GuestRequestKey = guest3.GuestRequestKey;
            order.HostingUnitKey = hostingUnit2.HostingUnitKey;
            order.CreateDate = DateTime.Now;
            order.OrderDate = DateTime.Now;
            order.OrderKey = ++Configuration.OrderKey;
            order.Status = Status.Closed_ClientRequest;
            orders.Add(order);


        }
    }
}

