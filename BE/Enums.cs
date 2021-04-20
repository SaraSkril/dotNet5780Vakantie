using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public enum Area//destination area
    {
        All, North, South, Center, Jerusalem
    }
    public enum TypeUnit//type of guest unit
    {
        Zimmer, Hotel, CampingSite, AirBNB
    }
    public enum Pool//if guest wants pool
    {
        Interested, NotIntersted, Maybe
    }
    public enum Jacuzzi//if guest wants jacuzzi
    {
        Interested, NotIntersted, Maybe
    }
    public enum Garden//if guest wants garden
    {
        Interested, NotIntersted, Maybe
    }
    public enum ChildrensAttractions//if guest wants child attractions
    {
        Interested, NotIntersted, Maybe
    }
    public enum Wifi//if guest wants wifi
    {
        Interested, NotIntersted, Maybe
    }
    public enum CollectionClearance //if host lets access to bank accout to withdrawl money
    {
        Yes,No
    }
    public enum Status
    {
        Mail_Sent, Not_Treated, Closed_NoReply, Closed_ClientRequest, Active
    }

}
