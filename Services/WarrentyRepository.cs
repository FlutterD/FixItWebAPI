using System;
using FixItWebAPI.Models;
using FixItWebAPI.Data.MainDatasetTableAdapters;
using static FixItWebAPI.Data.MainDataset;
using System.Collections.Generic;
using System.Linq;
using FixItWebAPI.Data;

namespace FixItWebAPI.Controllers
{
    internal class WarrentyRepository
    {
        internal Warrenty[] GetAllWarrenties()
        {
            WarrentyTableAdapter adapter = new WarrentyTableAdapter();
            WarrentyDataTable dtc = new WarrentyDataTable();
            adapter.Fill(dtc);

            WarrentyRow[] warrentyRows = (WarrentyRow[])dtc.Select();

            List<Warrenty> warrenties = new List<Warrenty>();

            foreach (var item in warrentyRows)
            {
                var warrenty = new Warrenty
                {
                    Number = item.WAR_NBR,
                    SerialNumber = (item.IsWAR_SNNull()) ? "" : item.WAR_SN,
                    Model = item.WAR_MODEL,
                    StartDate = item.IsWAR_SDATENull() ? DateTime.MinValue : item.WAR_SDATE,
                    EndDate = item.IsWAR_SDATENull() ? DateTime.MinValue : item.WAR_EDATE

                };
                warrenties.Add(warrenty);


            }
            return warrenties.ToArray();
        }

        internal Warrenty GetWarrenty(string serialnumber)
        {


            WarrentyTableAdapter adapter = new WarrentyTableAdapter();

            MainDataset dataset = new MainDataset();
            Oracle.ManagedDataAccess.Client.OracleDataAdapter myAdapterWarrenty = new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
            Oracle.ManagedDataAccess.Client.OracleCommand selectCommandBySerialNumber = new Oracle.ManagedDataAccess.Client.OracleCommand();
            selectCommandBySerialNumber.CommandText = "Select * from V_MACC_WARRANTIES where WAR_SN = '" + serialnumber + "'";
            selectCommandBySerialNumber.Connection = adapter.Connection;

            myAdapterWarrenty.SelectCommand = selectCommandBySerialNumber;

            WarrentyDataTable dtc = new WarrentyDataTable();
            myAdapterWarrenty.Fill(dtc);

            WarrentyRow[] warrentyRows = (WarrentyRow[])dtc.Select();//.Select(fliterExpression);


            Warrenty war = new Warrenty();
            if (warrentyRows != null && warrentyRows.Length > 0)
            {
                war.SerialNumber = warrentyRows[0].WAR_SN;
                war.Number = warrentyRows[0].WAR_NBR;
                war.Model = warrentyRows[0].WAR_MODEL;
                war.StartDate = warrentyRows[0].WAR_SDATE;
                war.EndDate = warrentyRows[0].WAR_EDATE;

            }

            return war;




            // WarrentyTableAdapter adapter = new WarrentyTableAdapter();


            //WarrentyDataTable dtc = new WarrentyDataTable();
            //adapter.Fill(dtc);
            //String fliterExpression = "WAR_SN = '" + serialnumber + "'";
            //QueriesTableAdapter tableAdapter = new QueriesTableAdapter();
            //Warrenty warrenty = new Warrenty();
            //String Number = "";
            //DateTime? StartDate;
            //DateTime? EndDate;
            //tableAdapter.GET_WARRANTY("201999004368", serialnumber, out Number, out StartDate, out EndDate);
            //warrenty.Number = Number;
            //warrenty.StartDate = StartDate ?? DateTime.MinValue;
            //warrenty.EndDate = EndDate ?? DateTime.MinValue;

            //Warrenty[] allWarrenties = GetAllWarrenties();
            //var w = (from wr in allWarrenties
            //         where wr.SerialNumber == serialnumber
            //         select wr);
        }
        internal Warrenty GetWarrenty(string serialNumber,string modelNumber)
        {


            WarrentyTableAdapter adapter = new WarrentyTableAdapter();

            MainDataset dataset = new MainDataset();
            Oracle.ManagedDataAccess.Client.OracleDataAdapter myAdapterWarrenty = new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
            Oracle.ManagedDataAccess.Client.OracleCommand selectCommandBySerialNumber = new Oracle.ManagedDataAccess.Client.OracleCommand();
            selectCommandBySerialNumber.CommandText = "Select * from V_MACC_WARRANTIES where WAR_SN = '" + serialNumber 
                                        + "' and WAR_MODEL = '" + modelNumber +"'";
            selectCommandBySerialNumber.Connection = adapter.Connection;

            myAdapterWarrenty.SelectCommand = selectCommandBySerialNumber;

            WarrentyDataTable dtc = new WarrentyDataTable();
            myAdapterWarrenty.Fill(dtc);

            WarrentyRow[] warrentyRows = (WarrentyRow[])dtc.Select();//.Select(fliterExpression);


            Warrenty war = new Warrenty();
            if (warrentyRows != null && warrentyRows.Length > 0)
            {
                war.SerialNumber = warrentyRows[0].WAR_SN;
                war.Number = warrentyRows[0].WAR_NBR;
                war.Model = warrentyRows[0].WAR_MODEL;
                war.StartDate = warrentyRows[0].WAR_SDATE;
                war.EndDate = warrentyRows[0].WAR_EDATE;

            }

            return war;


            
        }
    }
}