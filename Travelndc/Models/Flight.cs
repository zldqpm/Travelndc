using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travelndc.Models
{
    [SugarIndex("index_createTime_arrivalDate_arrivalDate", nameof(Flight.createTime), OrderByType.Asc,
                        nameof(Flight.departureDate), OrderByType.Asc, nameof(Flight.arrivalDate), OrderByType.Asc)]
    [Table("Flights")]
    public class Flight
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(20)", IsNullable = true)]
        public string price { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(20)", IsNullable = true)]
        public string flyTime { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(20)", IsNullable = true)]
        public string departureDate { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(20)", IsNullable = true)]
        public string departureTime { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(200)", IsNullable = true)]
        public string departureAirportName { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(200)", IsNullable = true)]
        public string departureTerminal { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(20)", IsNullable = true)]
        public string arrivalDate { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(20)", IsNullable = true)]
        public string arrivalTime { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(200)", IsNullable = true)]
        public string arrivalAirportName { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(200)", IsNullable = true)]
        public string arrivalTerminal { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(200)", IsNullable = true)]
        public string airlineName { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(200)", IsNullable = true)]
        public string flightNo { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(200)", IsNullable = true)]
        public string aircraftCode { get; set; }
        public DateTime createTime { get; set; }
    }
}
