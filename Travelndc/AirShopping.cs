using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelndc
{
    public class AirShopping
    {
        public string message { get; set; }
        public object code { get; set; }
        public object priceCalendar { get; set; }
        public object airOfferGroupList { get; set; }
        public Airoffergrouplistbysplit[][] airOfferGroupListBySplit { get; set; }
        public Passengerlist[] passengerList { get; set; }
        public object airOfferList { get; set; }
        public Airofferlistbysplit[][] airOfferListBySplit { get; set; }
        public string[] lowestOfferPrice { get; set; }
        public string[] heigestOfferPrice { get; set; }
        public string[] lowestTime { get; set; }
        public string[] hiegestTime { get; set; }
        public object owerList { get; set; }
        public Owerlistbysplit[] owerListBySplit { get; set; }
        public bool successed { get; set; }
    }

    public class Airoffergrouplistbysplit
    {
        public string price { get; set; }
        public object disprePrice { get; set; }
        public string airOwner { get; set; }
        public object airOwnerName { get; set; }
        public string owner { get; set; }
        public object shipmentName { get; set; }
        public string[] flightRefs { get; set; }
        public Flightsegmentgroup[][] flightSegmentGroup { get; set; }
        public string[] flyTime { get; set; }
        public Airoffer[] airOffers { get; set; }
    }

    public class Flightsegmentgroup
    {
        public int id { get; set; }
        public object segmentKey { get; set; }
        public string departureAirportCode { get; set; }
        public string departureAirport { get; set; }
        public string departureDate { get; set; }
        public string departureTime { get; set; }
        public string departureAirportName { get; set; }
        public string departureTerminal { get; set; }
        public string arrivalAirportCode { get; set; }
        public string arrivalAirport { get; set; }
        public string arrivalDate { get; set; }
        public string arrivalTime { get; set; }
        public string arrivalAirportName { get; set; }
        public string arrivalTerminal { get; set; }
        public string airline { get; set; }
        public string airlineName { get; set; }
        public string flightNo { get; set; }
        public string aircraftCode { get; set; }
        public object equipmentName { get; set; }
        public object flightDistance { get; set; }
        public string changeOfDay { get; set; }
        public object flightDuration { get; set; }
        public object operatingCarrier { get; set; }
        public object departureAirportData { get; set; }
        public object arrivalAirportData { get; set; }
        public object airlineData { get; set; }
        public object stayDate { get; set; }
        public object cabinDesignator { get; set; }
        public object cabin { get; set; }
        public object status { get; set; }
        public object ticket { get; set; }
        public object addTime { get; set; }
        public object[] detailList { get; set; }
        public object rule { get; set; }
        public string stopQuantity { get; set; }
        public object brandName { get; set; }
        public object classOfService { get; set; }
        public object serviceType { get; set; }
        public object orderId { get; set; }
        public string flightTime { get; set; }
        public object positionName { get; set; }
        public object positionCode { get; set; }
        public string shipmentId { get; set; }
        public string shipmentName { get; set; }
        public string shipmentFlightNum { get; set; }
        public object fBcode { get; set; }
        public object delFlag { get; set; }
        public object orderInfoId { get; set; }
        public object stopLocation { get; set; }
        public object idnumber { get; set; }
    }

    public class Airoffer
    {
        public string responseId { get; set; }
        public string offerId { get; set; }
        public string[] offerItemIds { get; set; }
        public string currency { get; set; }
        public string simpleCurrencyPrice { get; set; }
        public object discountPreSimpleCurrencyPrice { get; set; }
        public string owner { get; set; }
        public string ownerType { get; set; }
        public object ownerName { get; set; }
        public string baseAmount { get; set; }
        public string taxes { get; set; }
        public int serviceCount { get; set; }
        public int? flightRefsCount { get; set; }
        public string flightRef { get; set; }
        public int adtCount { get; set; }
        public string adtSimpleBasePrice { get; set; }
        public string adtSimpleBaseTax { get; set; }
        public string adtSimpleTotalPrice { get; set; }
        public int ythCount { get; set; }
        public object ythSimpleBasePrice { get; set; }
        public object ythSimpleBaseTax { get; set; }
        public object ythSimpleTotalPrice { get; set; }
        public int chdCount { get; set; }
        public object chdSimpleBasePrice { get; set; }
        public object chdSimpleBaseTax { get; set; }
        public object chdSimpleTotalPrice { get; set; }
        public int babyCount { get; set; }
        public object babySimpleBasePrice { get; set; }
        public object babySimpleTotalPrice { get; set; }
        public object babySimpleBaseTax { get; set; }
        public object priceClassKey { get; set; }
        public string[] priceClassRefNames { get; set; }
        public Farecomponentlist[] fareComponentList { get; set; }
        public Descriptionmap descriptionMap { get; set; }
        public object penaltyList { get; set; }
        public string[][] priceClassCodeList { get; set; }
        public object seatLeftList { get; set; }
        public string[] fbcodeList { get; set; }
        public string[][] fareBasisCodeList { get; set; }
        public string[][] describeListFor1E { get; set; }
        public object groupList { get; set; }
        public object priceClassmap { get; set; }
        public object flightTimeList { get; set; }
        public string[] baggageAllowanceRefList { get; set; }
        public string[] fareRulesList { get; set; }
        public object anonymousTravelerList { get; set; }
        public Travelermap travelerMap { get; set; }
    }

    public class Descriptionmap
    {
        public string[] MU_行李优享 { get; set; }
        public string[] MU_行李优享_label { get; set; }
        public object[] MU_行李优享_photo { get; set; }
    }

    public class Travelermap
    {
        public int ADT { get; set; }
    }

    public class Farecomponentlist
    {
        public string priceClassRef { get; set; }
        public string segmentRefs { get; set; }
    }

    public class Passengerlist
    {
        public string passengerId { get; set; }
        public string ptc { get; set; }
        public object ptcName { get; set; }
        public string simpleBasePrice { get; set; }
        public string simpleTotalPrice { get; set; }
        public string simpleBaseTax { get; set; }
    }

    public class Airofferlistbysplit
    {
        public string responseId { get; set; }
        public string offerId { get; set; }
        public string[] offerItemIds { get; set; }
        public string currency { get; set; }
        public string simpleCurrencyPrice { get; set; }
        public object discountPreSimpleCurrencyPrice { get; set; }
        public string owner { get; set; }
        public string ownerType { get; set; }
        public object ownerName { get; set; }
        public string baseAmount { get; set; }
        public string taxes { get; set; }
        public int serviceCount { get; set; }
        public int? flightRefsCount { get; set; }
        public string flightRef { get; set; }
        public int adtCount { get; set; }
        public string adtSimpleBasePrice { get; set; }
        public string adtSimpleBaseTax { get; set; }
        public string adtSimpleTotalPrice { get; set; }
        public int ythCount { get; set; }
        public object ythSimpleBasePrice { get; set; }
        public object ythSimpleBaseTax { get; set; }
        public object ythSimpleTotalPrice { get; set; }
        public int chdCount { get; set; }
        public object chdSimpleBasePrice { get; set; }
        public object chdSimpleBaseTax { get; set; }
        public object chdSimpleTotalPrice { get; set; }
        public int babyCount { get; set; }
        public object babySimpleBasePrice { get; set; }
        public object babySimpleTotalPrice { get; set; }
        public object babySimpleBaseTax { get; set; }
        public object priceClassKey { get; set; }
        public string[] priceClassRefNames { get; set; }
        public Farecomponentlist1[] fareComponentList { get; set; }
        public Descriptionmap1 descriptionMap { get; set; }
        public object penaltyList { get; set; }
        public string[][] priceClassCodeList { get; set; }
        public object seatLeftList { get; set; }
        public string[] fbcodeList { get; set; }
        public string[][] fareBasisCodeList { get; set; }
        public string[][] describeListFor1E { get; set; }
        public object groupList { get; set; }
        public object priceClassmap { get; set; }
        public object flightTimeList { get; set; }
        public string[] baggageAllowanceRefList { get; set; }
        public string[] fareRulesList { get; set; }
        public object anonymousTravelerList { get; set; }
        public Travelermap1 travelerMap { get; set; }
    }

    public class Descriptionmap1
    {
        public string[] MU_行李优享 { get; set; }
        public string[] MU_行李优享_label { get; set; }
        public object[] MU_行李优享_photo { get; set; }
    }

    public class Travelermap1
    {
        public int ADT { get; set; }
    }

    public class Farecomponentlist1
    {
        public string priceClassRef { get; set; }
        public string segmentRefs { get; set; }
    }

    public class Owerlistbysplit
    {
        public string MU { get; set; }
    }

    //public class AirShopping
    //{
    //    public List<AirOfferGroupListBySplit> airOfferGroupListBySplit { get; set; }
    //}

    //public class AirOfferGroupListBySplit
    //{
    //    public List<AirOfferGroupListBySplitItem> flightSegmentGroup { get; set; }
    //}

    //public class AirOfferGroupListBySplitItem
    //{
    //    public string price { get; set; }
    //    public string flyTime { get; set; }
    //    public List<FlightSegmentGroup> flightSegmentGroup { get; set; }
    //}

    //public class FlightSegmentGroup
    //{
    //    public string departureDate { get; set; }
    //    public string departureTime { get; set; }
    //    public string departureAirportName { get; set; }
    //    public string departureTerminal { get; set; }
    //    public string arrivalDate { get; set; }
    //    public string arrivalTime { get; set; }
    //    public string arrivalAirportName { get; set; }
    //    public string arrivalTerminal { get; set; }
    //    public string airlineName { get; set; }
    //    public string flightNo { get; set; }
    //    public string aircraftCode { get; set; }

    //}
}
