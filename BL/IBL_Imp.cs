
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

using BE;
using DAL;
//blah
namespace BL
{
    internal class IBL_Imp : IBL

    {
        DAL.Idal dal;

        public IBL_Imp()//ctor
        {
            dal = DAL.FactoryDal.GetDal();
         
        }
        #region Add
        public void AddGuestReq(Guest guest)//Adds a new Guest Request
        {
            if (!CheckDate(guest.EntryDate, guest.ReleaseDate))
                throw new ArgumentOutOfRangeException("Dates are not valid\n ");
            
            if (checkEmail(guest.EmailAddress) == false)
                throw new ArgumentOutOfRangeException("Invalid Email Address\n");
            try
            {
                dal.AddGuestReq(guest.Clone());
            }
            catch (DuplicateWaitObjectException s)
            {
                throw s;
            }

        }
        public void AddHostingUnit(HostingUnit hostingUnit)//Adds new Hosting unit;
        {
            if (checkID(hostingUnit.Owner.ID))
            {
                try
                {
                    dal.AddHostingUnit(hostingUnit.Clone());
                }
                catch (DuplicateWaitObjectException e)
                {
                    throw e;
                }
            }
            else
            {
                throw new KeyNotFoundException("Owner ID  is not valid\n");
            }
        }
        public void AddOrder(Order order)//adds a new order
        {
            HostingUnit hosting = dal.GetHostingUnit(order.HostingUnitKey);
            if (hosting == null)
                throw new KeyNotFoundException("Invalid Hosting Unit");
            Guest guest = dal.GetGuest(order.GuestRequestKey);
            if (guest == null)
                throw new KeyNotFoundException("Invalid Guest");
            order.Status = Status.Active;
            order.CreateDate = DateTime.Now;
            
            try
            {
                dal.AddOrder(order.Clone());
            }
            catch (DuplicateWaitObjectException e)
            {
                throw e;
            }

        }

        
      
        public void addHost(Host host)
        {
            if (checkID(host.ID))
                try
                {
                    dal.AddHost(host);
                }
                catch (DuplicateWaitObjectException e)
                {
                    throw e;
                }
            else
                throw new KeyNotFoundException("Invalid ID");
        }
      public void UpdateHost(Host host)
        {
            if (checkID(host.ID))
                try
                {
                    dal.UpdateHost(host);
                }
                catch(DuplicateWaitObjectException e)
                {
                    throw e;
                }
        }
        #endregion
        #region Update
        public void UpdateGuestReq(Guest guest)//Updates guest
        {
            if (!CheckDate(guest.EntryDate, guest.ReleaseDate))
                throw new ArgumentOutOfRangeException("Dates are not valid/n ");
           
            try
            {
                dal.UpdateGuestReq(guest.Clone());
            }
            catch(KeyNotFoundException e)
            {
                throw e;
            }
        }
        public void UpdateHostUnit(HostingUnit hostingUnit)//Updates Hosting Unit;
        {
            if (checkID(hostingUnit.Owner.ID))
                try
                {
                    dal.UpdateHostUnit(hostingUnit.Clone());
                }
                catch (DuplicateWaitObjectException e)
                {
                    throw e;
                }
            else
                throw new ArgumentOutOfRangeException("Owner ID  is not valid\n");
        }
        public void UpdateOrder(Order order,string text,string pic)//Updates Order
        {
            Order orig = GetAllOrders().FirstOrDefault(t => t.OrderKey == order.OrderKey);
            if ((orig.Status == Status.Closed_ClientRequest || orig.Status == Status.Closed_NoReply) )
                throw new TaskCanceledException("Status cannot be changed");

            if (orig.Status == Status.Active && order.Status == Status.Active)
                return;
            if (orig.Status != Status.Active && order.Status == Status.Active)
                return;
                if (orig.Status == Status.Mail_Sent && order.Status == Status.Mail_Sent)
                try
                {
                    //dal.UpdateOrder(order.Clone());

                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
            if (order.Status == Status.Closed_NoReply)
            {
                if (order.OrderDate == default(DateTime))
                    throw new TaskCanceledException("Cannot Update order to closed before an email was sent");
                try
                {

                    dal.UpdateOrder(order.Clone());

                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
            }
            if (order.Status == Status.Closed_ClientRequest)
            {
                

                if (order.OrderDate == default(DateTime))
                    throw new TaskCanceledException("Cannot Update order to closed before an email was sent");
                Guest guest = dal.GetGuest(order.GuestRequestKey);
                guest.GuestStatus = Status.Closed_ClientRequest;
                UpdateGuestReq(guest);
                
                HostingUnit tmp = dal.GetHostingUnit(order.HostingUnitKey);
                if (!CheckOffDates(tmp, guest.EntryDate, guest.ReleaseDate))
                    throw new TaskCanceledException("could not book dates");
                Charge(FindHost(order.HostingUnitKey), DaysBetween(guest.EntryDate, guest.ReleaseDate));//charges the host 10 nis 
               // Charge(tmp, DaysBetween(guest.EntryDate, guest.ReleaseDate));
                UpdateHostUnit(tmp.Clone());//need if we figure out how to clone diary
                foreach (Order order1 in dal.GetAllOrders())//closes all orders that are open for this guest
                {
                    if (order1.GuestRequestKey == guest.GuestRequestKey)
                    {
                        order1.Status = Status.Closed_ClientRequest;
                        dal.UpdateOrder(order1);
                    }

                    
                }
                return;
            }
            if (order.Status == Status.Mail_Sent)
            {
                HostingUnit hosting = dal.GetHostingUnit(order.HostingUnitKey);
                if (!CheckIsBankAllowed(hosting.Owner, order))
                    throw new TaskCanceledException("Cannot send mail. No debit authorization\n please update Automatic billing.");
                SendMail(order, text,pic);
                order.OrderDate = DateTime.Now;
            }
            try
            {
                dal.UpdateOrder(order.Clone());

            }
            catch (KeyNotFoundException e)
            {
                throw e;
            }

        }
        
        #endregion
        #region Delete

        public void DelHostingUnit(HostingUnit hosting)//Deletes Hosting Unit
        {
            if (!IsHostingUnitActive(hosting))
                try
                {
                    dal.DelHostingUnit(hosting.HostingUnitKey);
                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
            else
                throw new TaskCanceledException("Hosting Unit cannot be deleted\n");
        }
        #endregion
        #region GetAll
        public List<HostingUnit> GetAllHostingUnits()//returns a lists with all hosting unit
        {
            List<HostingUnit> hu = new List<HostingUnit>();
            foreach(HostingUnit unit in dal.GetAllHostingUnits())
            {
                hu.Add(unit.Clone());
            }
            return hu;
        }
        public List<Host> GetAllHosts()//returns a lists with all hosts
        {
            List<Host> hu = new List<Host>();
            foreach (Host h in dal.GetHosts())
            {
                hu.Add(h.Clone());
            }
            return hu;
        }
        public List<Guest> GetAllGuests()//returns a list with all Guests
        {
            List<Guest> g = new List<Guest>();
            foreach (Guest guest in dal.GetAllGuests())
            {
                g.Add(guest.Clone());

            }
            return g;
        }
        public List<Order> GetAllOrders()//returns a list with all orders
        {
            List<Order> Order = new List<Order>();
            foreach(Order orders in dal.GetAllOrders())
            {
                Order.Add(orders.Clone());
            }
            return Order;
        }
        public IEnumerable<BankAccount> GetAllBankAccounts()//returns a list with all Bank Accounts 
        {
            return dal.GetAllBankAccounts();
        }
        #endregion
        #region check
        
        public bool checkifOrderExist(int hostingkey, int guestKey)//if theres an open order with hu & guest then return true 
        {
            foreach (Order ord in GetAllOrders())
                if (ord.HostingUnitKey == hostingkey && ord.GuestRequestKey == guestKey)
                    return true;
            return false;

        }
        public bool checkifHost(string id)
        {
            Host h = dal.GetHost(id);
            if (h == null)
                return false;
            return true;

        }
        public bool checkEmail(string email)
        {
            
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool checkID(string id)
        {
            if (Int32.Parse(id) < 100000000 || Int32.Parse(id) > 999999999)
                return false;
            return true;
        }
        public bool CheckDate(DateTime start, DateTime end)//check if end day is longer than 1 day by start
        {
            DateTime temp = start.AddDays(1);
            if (temp > end)
                return false;
            return true;
        }
        public bool CheckIsBankAllowed(Host host, Order order)//check if host allows access to bank acct, if so sends email else returns false 
        {
            if (host.CollectionClearance == CollectionClearance.Yes)//checks if theres access to bank acct 
                return true;
            return false;

        }
        public bool IsAvailible(HostingUnit hostingUnit, DateTime start, DateTime end)//checks if dates in hosting unit are availble 
        {
            DateTime tempstart = start;
            while (tempstart != end)
            {
                if (hostingUnit.Diary[tempstart.Month-1, tempstart.Day-1] == true)//if date accupied
                    return false;
                tempstart=tempstart.AddDays(1);
            }
            return true;//dates were available
        }
        public bool CheckifOrderIsClosed(Order order)//checks if status can be changed
        {
            if (order.Status == Status.Closed_ClientRequest || order.Status == Status.Closed_NoReply)//order closed
                return true;//status cannot be changed
            return false;
        }
        public bool Charge(Host host, int numdays)//when order is closed we charge the owner 10 nis
        {
            try
            {
                host.commission += Configuration.commission * numdays;
                dal.UpdateHost(host);
                return true;
            }
            catch
            {
                throw new TaskCanceledException(" Not able to charge comission");
            }

        }
        
        public Host FindHost(int key)//recieves hosting unit key and returns the host that ownes it
        {
            HostingUnit hostingUnit = dal.GetHostingUnit(key);
            return hostingUnit.Owner;//returns owner
        }
      public Host FindHost(string id)//recieves host id and returns the host
        {
            return dal.GetHost(id);
        }
        public HostingUnit FindHostingUnit(string name, string id)//recieves host id and returns the host
        {
           return dal.GetHostingUnit(name, id);
        }
        public bool CheckOffDates(HostingUnit hostingUnit, DateTime start, DateTime end)//when status is changed to closed, update diary in hosting unit 
        {
            if (IsAvailible(hostingUnit, start, end) == false)
                return false;
            
            DateTime tempstart = start;
            while (tempstart != end)
            {
                hostingUnit.Diary[tempstart.Month-1, tempstart.Day-1] = true;//marks that date is accupied

                tempstart=tempstart.AddDays(1);
            }
            return true;//dates were available

        }

        public bool IsHostingUnitActive(HostingUnit hostingUnit)//checks if hosting unit is active in any order, if so w cant delete
        {

            var orders = from order in dal.GetAllOrders()//gets all orders from this unit
                         where order.HostingUnitKey==hostingUnit.HostingUnitKey
                         select order;
            foreach (var ord in orders)//checks if any order is active if so returns false so we cant delete the hosting unit
            {
                if (ord.Status == Status.Active || ord.Status == Status.Closed_ClientRequest||ord.Status==Status.Mail_Sent)
                    return true;
            }
            return false;

        }
        public bool ChangeCollectionClearance(HostingUnit hostingUnit)//checks if theres a open order, if so we cannot change collection clearance
        {
            if (IsHostingUnitActive(hostingUnit))//if hosting unit is in a active status then we cant change collection clearance 
                return false;
            hostingUnit.Owner.CollectionClearance = CollectionClearance.No;//change collection clearence
            return true;
        }
        public void SendMail(Order order, string text, string pic)//when status of order is changed to "sent mail", this function will send the mail
        {
            Guest g = dal.GetGuest(order.GuestRequestKey);
            HostingUnit hu = dal.GetHostingUnit(order.HostingUnitKey);
            Host h = dal.GetHost(hu.Owner.ID);
            try
            {
                checkEmail(g.EmailAddress);
                checkEmail(h.EmailAddress);
            }
            catch (InvalidOperationException e)
            {
                throw e;

            }

            new Thread(() =>
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("vakantiebooking@gmail.com");
                    mail.To.Add(g.EmailAddress);
                    mail.Subject = "Vakantie vacation offer";
                    mail.Body = text;

                    if (pic != "")
                    {
                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(pic);
                        mail.Attachments.Add(attachment);
                    }

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("vakantiebooking@gmail.com", "vakantie2020");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }).Start();
        }
        public List<HostingUnit> AllDays(DateTime date, int duration)//returns all hosting units with avilble date
        {
            DateTime end = date.AddDays(duration);
            var result = from item in dal.GetAllHostingUnits()//selects all hosting units with available dates  into a list
                         where IsAvailible(item, date, end) == true
                         select item;
            return (List<HostingUnit>)result;//returns the list
        }
        public int DaysBetween(DateTime start/*end=now*/)//returns the days between the start day and now 
        {
            DateTime now = DateTime.Now;
            return (now - start).Days;

        }
        public int DaysBetween(DateTime start, DateTime end)//returns the days between the start and last day
        {
            return (end - start).Days;
        }
        public List<Order> DaysFromOrder(int num)//returns all orders that "num"days have past since they were created / sent email (num or bigger)
        {
            var result = from item in dal.GetAllOrders()
                         where DaysBetween(item.OrderDate) >= num
                         select item;
            return (List<Order>)result;

        }
        public int NumOfVacationers(Guest g)//recieves a guest and returns num of vacationers
        {
            return g.Adults + g.Children;
        }
        public int NumOfHostingUnits(Host h)//recieves a host and returns num of hosting units he ownes
        {
            int count = 0;
            foreach (HostingUnit unit in dal.GetAllHostingUnits())
            {
                if (unit.Owner.ID == h.ID)
                    count++;
            }
            return count;

        }
        public bool HostExist(Host host)
        {
            Host host1 = dal.GetHost(host.ID);
            if (host1 != null)
                return true;
            return false;


        }
        public bool GuestExist(Guest guest)
        {
            Guest guest1 = dal.GetGuest(guest.GuestRequestKey);
            if (guest1 != null)
                return true;
            return false;
        }
        public bool HostingUnitExist(HostingUnit hostingUnit)
        {
            HostingUnit unit = dal.GetHostingUnit(hostingUnit.HostingUnitName,hostingUnit.Owner.ID);
            if (unit != null)
                return true;
            return false;
        }

        //Func -דלגייט שקיים  מקבל משהו ומחזיר משהו
        //predicate-בודק אם תנאי מסוים שמציבים בו מתקיים
        // IEnumeable מאפשר לנו לעבור על איברי האובייקט שלנו, לסדר אותם, לסנן אותם או סתם לדלות מהם מידע.
        public IEnumerable<Guest> GetAllGuests(Func<Guest, bool> predicate = null)//recieves a predicate and returns all guests that  satisfy the predicate condition
        {

            var result = from item in dal.GetAllGuests()
                         where predicate(item)
                         select item;
            return result;
        }
        public int NumForGuest(Guest guest)//counts and returns how many orders have been sent to him
        {
            int count = 0;
            foreach (Order item in dal.GetAllOrders())
            {
                if (item.GuestRequestKey == guest.GuestRequestKey)
                    count++;
            }
            return count;


        }
        public int NumForUnit(HostingUnit hostingUnit)//counts how many orders were  closed/sent for hosting unit
        {
            int count = 0;
            foreach (Order item in dal.GetAllOrders())
            {
                if (item.HostingUnitKey == hostingUnit.HostingUnitKey &&
                    item.Status == Status.Closed_ClientRequest || item.HostingUnitKey == hostingUnit.HostingUnitKey &&
                    item.Status == Status.Mail_Sent)
                    count++;
            }
            return count;
        }
        
        #endregion
        #region Group

        public IEnumerable<IGrouping<int, BankAccount>> GetBanksbyBranchesNumbers(IEnumerable<BankAccount> ba)//groups bank accounts according to branch numbers
        {
            return from item in ba
                   group item by item.BranchNumber
                        into g
                        orderby g.Key
                   select g;
        }
        public IEnumerable<IGrouping<int, BankAccount>> GetBanksbyBankNumbers()//groups bank accounts according to bank numbers
        {
            return from item in dal.GetAllBankAccounts()
                   group item by item.BankNumber
                         into g
                         orderby g.Key
                   select g;
        }

        public IEnumerable<IGrouping<Area, Guest>> GetGuestsGroupsByArea()//groups geusts according to area
        {
            return from item in dal.GetAllGuests()
                   group item by item.Area
                         into g
                         select g;


        }
        public IEnumerable<IGrouping<int, Guest>> GetGuestsGroupsByVacationers()//groups guests according to num vacation
        {
            return from item in dal.GetAllGuests()
                   group item by NumOfVacationers(item)
                       into g
                   orderby g.Key
                   select g;
        }
        public IEnumerable<IGrouping<int, Host>> GetHostsGroupsByHostingUnits()//groups hosts according to num of hosting units
        {
           return from item in dal.GetHosts()
                               let Host = item
                               let numHU = NumOfHostingUnits(item)
                                group Host by numHU into f1
                                orderby f1.Key
                               select f1;

        }
        public IEnumerable<IGrouping<CollectionClearance, Host>> GetHostsGroupsByClearance()//groups hosts according to num of hosting units
        {
            return from item in dal.GetHosts()
                   let Host = item
                   let col = Host.CollectionClearance
                   group Host by col into f1
                   orderby f1.Key
                   select f1;
        }
        public IEnumerable<IGrouping<Area, HostingUnit>> GetHUGroupsByArea()//groups hosting units according to area
        {
            return from item in dal.GetAllHostingUnits()
                   group item by item.area
                       into g
                   orderby g.Key
                   select g;
        }
        public IEnumerable<IGrouping<TypeUnit, HostingUnit>> GetHUGroupsByType()//groups hosting units according to area
        {
            return from item in dal.GetAllHostingUnits()
                   group item by item.TypeUnit
                          into g
                   orderby g.Key
                   select g;

        }
        public List<string> GetHubyHost(string id)
        {
            List<string> result = new List<string>();
            foreach(HostingUnit unit in GetAllHostingUnits())
            {
                if (unit.Owner.ID == id)
                    result.Add(unit.HostingUnitName);
            }
            return result;
        }
       public IEnumerable<IGrouping<Status, Order>> GetOrderByStatus()//groups orders according to status
        {

            return from item in dal.GetAllOrders()
                   group item by item.Status
                          into g
                   orderby g.Key
                   select g;
        }


        #endregion
        #region Config
        public DateTime GetLastUpdated()//returns the date that the orders were last updated 
        {
            return dal.GetLastUpdated();
        }
        public void UpdateLastUpdated()//updates
        {
            dal.UpdateLastUpdated();
        }
        #endregion

      
           /* new Thread(() =>
            {
            if (myDAL.getLastUpdatedStatus() < DateTime.Today)//only updates them if it didn't update today already
            {
                List<Order> orders = myDAL.getAllOrders();//all orders
                myDAL.changeOrderStatus(ord => ord.Status == Enums.OrderStatus.Mailed && ord.OrderDate < DateTime.Today.AddDays(30), Enums.OrderStatus.Expired);
                //changes status of orders more than 30 days old
                myDAL.setLastUpdatedStatus();//updates date to datetime.today
            }
            Thread.Sleep(86400000);//sleeps for 24 hours
        }).Start();//starts it*/
    }
}
