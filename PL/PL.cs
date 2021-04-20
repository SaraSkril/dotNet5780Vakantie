using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class PL
    {/*
        static void Main(string[] args)
        {
           
            IBL ibl = FactoryBl.GetBL();
     /*       #region add
            Console.WriteLine("hello");

            Guest guest1 = new Guest();
            {
                guest1.ID = "123456789";
                guest1.FirstName = "moo";
                guest1.LastName = "parah";
                guest1.EmailAddress = "parah@gmail.com";
                guest1.GuestStatus = Status.Not_Treated;
                guest1.RegistrationDate = DateTime.Now;
                guest1.EntryDate = new DateTime(2020, 7, 31);
                guest1.ReleaseDate = new DateTime(2020, 10, 2);
                guest1.TypeUnit = TypeUnit.Hotel;
                guest1.Area = Area.Jerusalem;
                guest1.Adults = 2;
                guest1.Children = 5;
                guest1.Pool = Pool.Interested;
                guest1.Jacuzzi = Jacuzzi.Maybe;
                guest1.Garden = Garden.NotIntersted;
                guest1.ChildrensAttractions = ChildrensAttractions.Interested;
                guest1.Wifi = Wifi.Interested;

            }
            try
            {
                ibl.AddGuestReq(guest1);
            }
            catch (Exception e)

            {
                if (e is DuplicateWaitObjectException || e is ArgumentOutOfRangeException || e is Exception)
                    Console.WriteLine(e.Message);
            }
            Host host1 = new Host();
            {
                host1.ID = "319031530";
                host1.FirstName = "Sara Raizel";
                host1.LastName = "Skriloff";
                host1.PhoneNumber = 0527148093;
                host1.EmailAddress = "srskriloff@gmail.com";
                host1.BankDetails = new BankAccount() { BankName = "Poalim", BankNumber = 12, BranchAddress = "Kanfei Nesharim 55", BranchCity = "Jerusalem", BranchNumber = 446 };
                host1.BankAccountNumber = 11245;
                host1.CollectionClearance = CollectionClearance.Yes;
            }
            HostingUnit hostingUnit1 = new HostingUnit();
            {
                hostingUnit1.HostingUnitName = "Ramada2";
                hostingUnit1.Owner = host1;
                hostingUnit1.area = Area.Jerusalem;
                hostingUnit1.pool = false;
                hostingUnit1.Jacuzzi = false;
                hostingUnit1.Garden = false;
                hostingUnit1.ChildrensAttractions = true;
                hostingUnit1.Wifi = true;
                hostingUnit1.TypeUnit = TypeUnit.Hotel;

            }
            try
            {
                ibl.AddHostingUnit(hostingUnit1);
            }
            catch (Exception e)

            {
                if (e is DuplicateWaitObjectException || e is ArgumentOutOfRangeException || e is Exception)
                    Console.WriteLine(e.Message);
            }

            Order order1 = new Order();
            {

                order1.GuestRequestKey = ibl.GetGuestKeyByID(guest1.ID);

                order1.HostingUnitKey = ibl.GetHUkeyBuName(hostingUnit1.HostingUnitName);

            }
            try
            {
                ibl.AddOrder(order1);
            }
            catch (DuplicateWaitObjectException e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

        //    #region print before update
            Console.WriteLine("--------------------------------------------------------------");
            foreach (HostingUnit hosting in ibl.GetAllHostingUnits())
                Console.WriteLine(hosting.ToString());
            Console.WriteLine("--------------------------------------------------------------");
            foreach (Guest guest in ibl.GetAllGuests())
                Console.WriteLine(guest.ToString());
            Console.WriteLine("--------------------------------------------------------------");
            foreach (Order order in ibl.GetAllOrders())
                Console.WriteLine(order.ToString());
            Console.WriteLine("--------------------------------------------------------------");

            #endregion

            #region Update
            guest1.Children = 10;

            try
            {
                ibl.UpdateGuestReq(guest1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            hostingUnit1.pool = true;
            try
            {
                ibl.UpdateHostUnit(hostingUnit1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            order1.Status = Status.Mail_Sent;
            try
            {
                ibl.UpdateOrder(order1);
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException || e is TaskCanceledException)
                    Console.WriteLine(e.Message);
            }
           
            
            #endregion
            #region print after update   
            foreach (Order order in ibl.GetAllOrders())
                Console.WriteLine(order.ToString());
            Console.WriteLine("--------------------------------------------------------------");
            #endregion

            try
            {
                ibl.DelHostingUnit(hostingUnit1);
            }
            catch (Exception e)
            {
                if (e is DuplicateWaitObjectException || e is ArgumentOutOfRangeException || e is Exception)
                    Console.WriteLine(e.Message);
            }

            Console.WriteLine("--------------------------------------------------------------");
            foreach (HostingUnit hosting in ibl.GetAllHostingUnits())
                Console.WriteLine(hosting.ToString());*/

        // IEnumerable<IGrouping<Area, Guest>> g = ibl.GetGuestsGroupsByArea();
        //   foreach(IEnumerable<Guest> g1 in g )

        //    foreach(Guest g2 in g1)
        //         Console.WriteLine(g2);
        //   }
        // Console.WriteLine("-----------------");


        //  string s = Console.ReadLine();
    }

}
    


