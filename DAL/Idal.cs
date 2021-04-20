using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        #region Guest
        void AddGuestReq(Guest guest);//Adds a new Guest Request
        void UpdateGuestReq(Guest guest);//Updates guest

        #endregion

        #region Host
        void AddHost(Host host);//adds a new host
        void UpdateHost(Host host);
        #endregion

        #region HostingUnit
        void AddHostingUnit(HostingUnit hostingUnit);//Adds new Hosting unit;
        void DelHostingUnit(int key);//Deletes Hosting Unit
        void UpdateHostUnit(HostingUnit hostingUnit);//Updates Hosting Unit;

        #endregion

        #region Order
        void AddOrder(Order order);//adds a new order
        void UpdateOrder(Order order);//Updates Order

        #endregion

        List<HostingUnit> GetAllHostingUnits();//returns a lists with all hosting unit
        List<Guest> GetAllGuests();//returns a list with all Guests
        List<Order> GetAllOrders();//returns a list with all orders
        IEnumerable<BankAccount> GetAllBankAccounts();//returns a list with all Bank Accounts 

        #region finds element
        DateTime GetLastUpdated();//returns the date that the orders were last updated 
        void UpdateLastUpdated();//updates
        Host GetHost(string id);
        HostingUnit GetHostingUnit(int key);
     
        Order GetOrder(int guestkey, int unitkey);
        Order GetOrder(int orderkey);
        HostingUnit GetHostingUnit(string name,string id) ;
        Guest GetGuest(int key);
        List<Host> GetHosts();
        
        #endregion
    }
}
