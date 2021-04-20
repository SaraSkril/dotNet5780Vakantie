using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
namespace BL
{
    public interface IBL
    {
        #region Host
        void addHost(Host host);
        void UpdateHost(Host host);
        #endregion
        #region Guest
        void AddGuestReq(Guest guest);//Adds a new Guest Request
        void UpdateGuestReq(Guest guest);//Updates guest

        #endregion
        #region HostingUnit
        void AddHostingUnit(HostingUnit hostingUnit);//Adds new Hosting unit;
        void DelHostingUnit(HostingUnit hosting);//Deletes Hosting Unit
        void UpdateHostUnit(HostingUnit hostingUnit);//Updates Hosting Unit;

        #endregion
        #region Order
        void AddOrder(Order order);//adds a new order
        void UpdateOrder(Order order,string text,string pic);//Updates Order

        #endregion
        #region GetAll
        List<HostingUnit> GetAllHostingUnits();//returns a lists with all hosting unit
        List<Host> GetAllHosts();//returns a lists with all hosts
        List<Guest> GetAllGuests();//returns a list with all Guests
        List<Order> GetAllOrders();//returns a list with all orders
        IEnumerable<BankAccount> GetAllBankAccounts();//returns a list with all Bank Accounts 
        IEnumerable<Guest> GetAllGuests(Func<Guest, bool> predicate = null);//recieves a predicate and returns all guests that  satisfy the predicate condition
        #endregion
        #region Check
        bool checkifOrderExist(int orderKey, int guestKey);//checks if theres a open order
        bool checkifHost(string id);
        bool checkID(string id);
        bool CheckDate(DateTime start, DateTime end);//check if end day is longer than 1 day by start
        bool CheckIsBankAllowed(Host host, Order order);//check if Host allows access to bank acct
        bool IsAvailible(HostingUnit hostingUnit, DateTime start, DateTime end);//checks if dates in hosting unit are availble 
        bool CheckOffDates(HostingUnit hostingUnit, DateTime start, DateTime end);//when status is changed to closed, uodate diary in hosting unit 
        bool IsHostingUnitActive(HostingUnit hostingUnit);//checks if hosting unit is active in any order, if so w cant delete
        bool CheckifOrderIsClosed(Order order);//if order is closed we cant change the status (return false) else return true, if status is canged to closed we have to charg 10 nis 
        bool Charge(Host host, int numdays);//when order is closed we charge the owner 10 nis
        bool HostExist(Host host);
        bool GuestExist(Guest guest);
        bool HostingUnitExist(HostingUnit hostingUnit);
        Host FindHost(int key);//recieves hosting unit key and returns the host that ownes it
        Host FindHost(string id);//recieves host id and returns the host
        HostingUnit FindHostingUnit(string name,string id);
        #endregion
        #region Other Function
        bool ChangeCollectionClearance(HostingUnit hostingUnit);//checks if theres a open order, if so we cannot change collection clearance
        void SendMail(Order order,string text,string pic);//when status of order is changed to "sent mail", this function will send the mail
        List<HostingUnit> AllDays(DateTime date, int duration);//returns all hosting units with avilble date
        int DaysBetween(DateTime start/*end=now*/);//returns the days between the start day and now 
        int DaysBetween(DateTime start, DateTime end);//returns the days between the start and last day
        List<Order> DaysFromOrder(int num);//returns all orders that "num"days have past since they were created / sent email (num or bigger)
        int NumOfVacationers(Guest g);//recieves a guest and returns num of vacationers
        int NumOfHostingUnits(Host h);//recieves a host and returns num of hosting units he ownes
        int NumForGuest(Guest guest);//counts and returns how many orders have been sent to him
        int NumForUnit(HostingUnit hostingUnit);//counts how many orders were  closed/sent for hosting unit
        bool checkEmail(string email);
        #endregion
        #region Group
        List<string> GetHubyHost(string id);
        IEnumerable<IGrouping<int, BankAccount>> GetBanksbyBranchesNumbers(IEnumerable<BankAccount> ba);//groups bank accounts according to branch numbers
        IEnumerable<IGrouping<int, BankAccount>> GetBanksbyBankNumbers();//groups bank accounts according to bank numbers
        IEnumerable<IGrouping<Area, Guest>> GetGuestsGroupsByArea();//groups geusts according to area
        IEnumerable<IGrouping<int, Guest>> GetGuestsGroupsByVacationers();//groups guests according to num vacationers
        IEnumerable<IGrouping<int, Host>> GetHostsGroupsByHostingUnits();//groups hosts according to num of hosting units
        IEnumerable<IGrouping<CollectionClearance, Host>> GetHostsGroupsByClearance();//groups hosts according to num of hosting units
        IEnumerable<IGrouping<Area, HostingUnit>> GetHUGroupsByArea();//groups hosting units according to area
        IEnumerable<IGrouping<TypeUnit, HostingUnit>> GetHUGroupsByType();//groups hosting units according to area
        IEnumerable<IGrouping<Status, Order>> GetOrderByStatus();//groups orders according to status

        #endregion
        #region Config
        DateTime GetLastUpdated();//returns the date that the orders were last updated 
        void UpdateLastUpdated();//updates
        #endregion
    }
}
#region random
//Func -דלגייט שקיים  מקבל משהו ומחזיר משהו
//predicate-בודק אם תנאי מסוים שמציבים בו מתקיים
// IEnumeable מאפשר לנו לעבור על איברי האובייקט שלנו, לסדר אותם, לסנן אותם או סתם לדלות מהם מידע.
#endregion
