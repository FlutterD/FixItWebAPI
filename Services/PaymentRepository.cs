using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FixItWebAPI.Models;
using FixItWebAPI.Data.MainDatasetTableAdapters;
using static FixItWebAPI.Data.MainDataset;
using Oracle.ManagedDataAccess.Client;

namespace FixItWebAPI.Services
{
    public class PaymentRepository
    {
        internal void SavePayment(Payment payment)
        {
           PaymentTableAdapter adapter = new PaymentTableAdapter();// queryString, connection);

            PaymentDataTable dtc = new PaymentDataTable();
            adapter.Fill(dtc);
           

            OracleCommand insertcmd = new OracleCommand();
            insertcmd.Connection = adapter.Connection;
            insertcmd.CommandText =
                String.Format("insert into V_VIEWTECH_JOB_PAYMENTS "
                              + "(JOB_CODE,LABOR_VALUE,PART_VALUE,OTHER_VALUE,PAYMENT_DATE)"
                              + " values ('{0}','{1}',to_date('{2}','dd/mm/yyyy'),{3},to_date('{"+ DateTime.Now.ToString()+ "}','dd/mm/yyyy hh:mm:ss'))"
                               , payment.JobCode, payment.LaborValue, payment.PartsValue, payment.OthersValue);
            adapter.Adapter.InsertCommand = insertcmd;


            try
            {
                insertcmd.Connection.Open();
                adapter.Adapter.InsertCommand.ExecuteNonQuery();
                //adapter.InsertQuery(visit.Comment);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                insertcmd.Connection.Close();
            }

        }
    }
}