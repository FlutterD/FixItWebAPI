using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class VisitRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Visit[] GetAllVisits()
        {
            VisitTableAdapter adapter = new VisitTableAdapter();// queryString, connection);

            VisitDataTable dtc = new VisitDataTable();
            adapter.Fill(dtc);
            VisitRow[] visitRows = (VisitRow[])dtc.Select();

            var visitList = new List<Visit>();
            JobTechRepository jobTechRepository = new JobTechRepository();
            foreach (var visitRow in visitRows)
            {

                var visit = new Visit
                {
                    Code = visitRow.VISIT_CODE,
                    JobTechCode = visitRow.JTEC_CODE,
                    Date = visitRow.IsVISIT_DATENull() ? DateTime.MinValue : visitRow.VISIT_DATE,
                    LastUpdated = visitRow.IsLAST_UPDATED_DATENull() ? DateTime.MinValue : visitRow.LAST_UPDATED_DATE,

                    JobTech = jobTechRepository.GetJobTechByCode(visitRow.JTEC_CODE)
                };

                visitList.Add(visit);
            }
            return visitList.ToArray();

        }


        public Visit[] GetAllVisitsByEmployee_old(int employeeCode)
        {
            var visitList = new List<Visit>();
            VisitTableAdapter adapter = new VisitTableAdapter();// queryString, connection);
            try
            {
                VisitDataTable dtc = new VisitDataTable();
                adapter.Fill(dtc);
                VisitRow[] visitRowsTemp = (VisitRow[])(dtc.Select());
                VisitRow[] visitRows = visitRowsTemp.OrderByDescending((VisitRow v)=>v.VISIT_DATE).Take(30).ToArray();
                JobTechRepository jobTechRepository = new JobTechRepository();
                foreach (var visitRow in visitRows)
                {
                    //if(DateTime.Compare(visitRow.VISIT_DATE , DateTime.Today)<0) continue;

                    //get jobtech inorder to get employeecode
                    JobTech tempJobTech = jobTechRepository.GetJobTechByCode(visitRow.JTEC_CODE);
                    //if not selected employee skip
                    if (tempJobTech.Employee.Code != employeeCode.ToString()) continue;

                    var visit = new Visit
                    {
                        Code = visitRow.VISIT_CODE,
                        JobTechCode = visitRow.JTEC_CODE,
                        Date = visitRow.IsVISIT_DATENull() ? DateTime.MinValue : visitRow.VISIT_DATE,
                        LastUpdated = visitRow.IsLAST_UPDATED_DATENull() ? DateTime.MinValue : visitRow.LAST_UPDATED_DATE,
                        CustomerFullName = tempJobTech.Job.Customer.FullName,
                        CustomerPhoneNumber = tempJobTech.Job.Customer.MobileNumber,
                        CustomerCode = tempJobTech.Job.Customer.Code,
                        JobTech = tempJobTech,// jobTechRepository.GetJobTechByCode(visitRow.JTEC_CODE)
                    };

                    visitList.Add(visit);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return visitList.ToArray();

        }
        public Visit[] GetAllVisitsByEmployee(int employeeCode)
        {
            VisitTableAdapter adapter = new VisitTableAdapter();// queryString, connection);

            VisitDataTable dtc = new VisitDataTable();
            adapter.Fill(dtc);
            VisitRow[] visitRows = (VisitRow[])dtc.Select();

            var visitList = new List<Visit>();
            JobTechRepository jobTechRepository = new JobTechRepository();
            foreach (var visitRow in visitRows)
            {
                if (DateTime.Compare(visitRow.VISIT_DATE, DateTime.Today) < 0) continue;

                JobTech tempJobTech = jobTechRepository.GetJobTechByCode(visitRow.JTEC_CODE);
                if (tempJobTech.Employee.Code != employeeCode.ToString()) continue;

                var visit = new Visit
                {
                    Code = visitRow.VISIT_CODE,
                    JobTechCode = visitRow.JTEC_CODE,
                    Date = visitRow.IsVISIT_DATENull() ? DateTime.MinValue : visitRow.VISIT_DATE,
                    LastUpdated = visitRow.IsLAST_UPDATED_DATENull() ? DateTime.MinValue : visitRow.LAST_UPDATED_DATE,
                    CustomerFullName = tempJobTech.Job.Customer.FullName,
                    CustomerPhoneNumber = tempJobTech.Job.Customer.MobileNumber,
                    CustomerCode = tempJobTech.Job.Customer.Code,
                    JobTech = tempJobTech,// jobTechRepository.GetJobTechByCode(visitRow.JTEC_CODE)
                };

                visitList.Add(visit);
            }
            return visitList.ToArray();

        }
        public Visit GetVisitByCode(long visitCode)
        {
            VisitTableAdapter adapter = new VisitTableAdapter();// queryString, connection);

            VisitDataTable dtc = new VisitDataTable();
            adapter.Fill(dtc);
            VisitRow visitRow = (VisitRow)dtc
                .Select()
                .SingleOrDefault(
                vs => (((VisitRow)vs).VISIT_CODE == visitCode));

            var visitList = new List<Visit>();



            var visit = new Visit
            {
                Code = visitRow.VISIT_CODE,
                JobTechCode = visitRow.JTEC_CODE,
                Date = visitRow.IsVISIT_DATENull() ? DateTime.MinValue : visitRow.VISIT_DATE,
                LastUpdated = visitRow.IsLAST_UPDATED_DATENull() ? DateTime.MinValue : visitRow.LAST_UPDATED_DATE,
            };

            return visit;
        }
        public Visit[] GetVisitByJobTechCode(long jobTechCode)
        {
            VisitTableAdapter adapter = new VisitTableAdapter();// queryString, connection);

            VisitDataTable dtc = new VisitDataTable();
            adapter.Fill(dtc);

            VisitRow[] visitRows =
                (VisitRow[])dtc //.Where( vs => (((VisitRow)vs).JTEC_CODE == jobTechCode))
                .Select(vs => vs);

            var visitList = new List<Visit>();

            foreach (var visitRow in visitRows)
            {
                visitList.Add(new Visit
                {
                    Code = visitRow.VISIT_CODE,
                    JobTechCode = visitRow.JTEC_CODE,
                    Date = visitRow.IsVISIT_DATENull() ? DateTime.MinValue : visitRow.VISIT_DATE,
                    LastUpdated = visitRow.IsLAST_UPDATED_DATENull() ? DateTime.MinValue : visitRow.LAST_UPDATED_DATE,
                });

            }



            return visitList.ToArray();
        }



        internal void SaveTechnicianVisit(TechVisitDetail visit)
        {
            VTTechDetailsTableAdapter adapter = new VTTechDetailsTableAdapter();
            VTTechDetailsDataTable vtdr = new VTTechDetailsDataTable();

            OracleCommand insertcmd = new OracleCommand();
            insertcmd.Connection = adapter.Connection;
            insertcmd.CommandText =
                String.Format(@"insert into V_VIEWTECH_JOB_TECHS_DETAILS "
                              + "(JTECD_COMMENT,JTEC_CODE,JTECD_STATUS,JTECD_DATE,JTECD_TIME,JTECD_EDATE)"
                              + @" values (q'[{0}]','{1}','{2}',to_date('{3}','yyyy-mm-dd hh24:mi:ss""Z""' ),to_date('{4}','hh:mi:ss'),to_date('{5}','yyyy-mm-dd hh24:mi:ss""Z""'))"
                               //,'dd/mm/yyyy'),to_date('{4}','hh:mi:ss'))"
                               , visit.Comment
                               , visit.JTEC_CODE
                               , visit.JTECD_STATUS
                               , DateTime.Parse(visit.JTECD_DATE).ToString("u")
                               , visit.JTECD_TIME
                               , DateTime.Parse(visit.JTECD_EDATE).ToString("u"));
            adapter.Adapter.InsertCommand = insertcmd;


            try
            {
                logger.Info(String.Format("SaveTechnicianVisit insert Command\t : {0}", insertcmd.CommandText));
                int result = 0;
                //logger.Info(String.Format("SaveTechnicianVisit:\n Insert Command Text: \t {0}", insertcmd.CommandText));
                insertcmd.Connection.Open();
                result = adapter.Adapter.InsertCommand.ExecuteNonQuery();
               // logger.Info(String.Format("SaveTechnicianVisit:\n Insert Command ExecuteNonQuery result: \t {0}", result));

                //adapter.InsertQuery(visit.Comment);
            }
            catch (Exception ex)
            {
                // logger.Error(String.Format("SaveTechnicianVisit:\n Insert Command ExecuteNonQuery result: \t {0}\n Exception:\t {1}", insertcmd.CommandText, ex.Message));
                logger.Error(ex,"SaveTechnicianVisit");
                Console.Write(ex.Message);
            }
            finally
            {
                insertcmd.Connection.Close();
            }

        }
        internal int SaveTechnicianVisit_GetId(TechVisitDetail visit)
        {
            int idReturned = -1;

            VTTechDetailsTableAdapter adapter = new VTTechDetailsTableAdapter();
            VTTechDetailsDataTable vtdr = new VTTechDetailsDataTable();

            OracleCommand insertcmd = new OracleCommand();
            insertcmd.Connection = adapter.Connection;
            insertcmd.CommandText =
                String.Format("INSERT INTO V_VIEWTECH_JOB_TECHS_DETAILS "
                              + " (JTECD_COMMENT,JTEC_CODE,JTECD_STATUS,JTECD_DATE,JTECD_TIME)"
                              + " OUTPUT JTECH_VT_ID "
                              + " values ('{0}','{1}','{2}',to_date('{3}','dd/mm/yyyy'),to_date('{4}','hh:mi:ss'))"
                               , visit.Comment, visit.JTEC_CODE, visit.JTECD_STATUS, visit.JTECD_DATE, visit.JTECD_TIME);
            adapter.Adapter.InsertCommand = insertcmd;


            try
            {
                insertcmd.Connection.Open();
                idReturned = (int)adapter.Adapter.InsertCommand.ExecuteScalar();
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
            return idReturned;
        }
        internal void SaveTechnicianVisitJobParts(List<JobPart> jobParts)
        {
            VTTechDetailsTableAdapter adapter = new VTTechDetailsTableAdapter();
            VTTechDetailsDataTable vtdr = new VTTechDetailsDataTable();

            OracleCommand insertcmd = new OracleCommand();
            insertcmd.Connection = adapter.Connection;
            string strCommand = "";
            try
            {
                insertcmd.Connection.Open();
                foreach (var item in jobParts)
                {
                    insertcmd.CommandText = strCommand = String.Format("insert into V_VIEWTECH_JOB_PARTS "
                   + "(Job_code, Product_Code, JPART_QTY_USED)"
                   + " values ('{0}','{1}',{2})", item.JobCode, item.ProductCode, item.Quantity);
                    adapter.Adapter.InsertCommand = insertcmd;

                logger.Info(String.Format("SaveTechnicianVisitJobParts:\n Insert Command Text: \t {0}", insertcmd));
                    adapter.Adapter.InsertCommand.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                logger.Error(String.Format("SaveTechnicianVisitJobParts:\n Insert Command ExecuteNonQuery result: \t {0}\n Exception:\t {1}", insertcmd.CommandText, ex.Message));

                Console.Write(ex.Message);
            }
            finally
            {
                insertcmd.Connection.Close();
            }

        }
        internal void SaveTechnicianVisitJobNeededParts(List<JobPart> neededParts)
        {
            VTTechDetailsTableAdapter adapter = new VTTechDetailsTableAdapter();
            VTTechDetailsDataTable vtdr = new VTTechDetailsDataTable();

            OracleCommand insertcmd = new OracleCommand();
            insertcmd.Connection = adapter.Connection;
            insertcmd.Connection.Open();

            try
            {
                foreach (var part in neededParts)
                {
                    insertcmd.CommandText = String.Format("insert into V_VIEWTECH_JOB_NEEDED_PARTS "
                       + "(Job_code, Product_Code, NPART_QTY)"
                       + " values ('{0}','{1}',{2})", part.JobCode, part.ProductCode, part.Quantity);

                    logger.Info(String.Format("SaveTechnicianVisitJobNeededParts:\n Insert Command Text: \t {0}", insertcmd));

                    adapter.Adapter.InsertCommand = insertcmd;
                    adapter.Adapter.InsertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("SaveTechnicianVisitJobParts:\n Insert Command ExecuteNonQuery result: \t {0}\n Exception:\t {1}", insertcmd.CommandText, ex.Message));

                Console.Write(ex.Message);
            }
            finally
            {
                insertcmd.Connection.Close();
            }

        }
        internal void SaveCustomerLocation(Location customerLocation)
        {
            WarrentyTableAdapter adapter = new WarrentyTableAdapter();

            // MainDataset dataset = new MainDataset();
            Oracle.ManagedDataAccess.Client.OracleDataAdapter customerAdapter = new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
            Oracle.ManagedDataAccess.Client.OracleCommand insertByCustomerCodeCommand = new Oracle.ManagedDataAccess.Client.OracleCommand();
            insertByCustomerCodeCommand.CommandText =
                String.Format("INSERT INTO V_VIEWTECH_CUSTOMER"
                + "(CUS_LATITUDE, CUS_LONGITUDE, CUS_CODE)"
                + "\nVALUES"
                + "\n('{0}','{1}','{2}')",
                customerLocation.Latitude,
                customerLocation.Longitude,
                customerLocation.CustomerCode);

            insertByCustomerCodeCommand.Connection = adapter.Connection;
            insertByCustomerCodeCommand.Connection.Open();

            customerAdapter.InsertCommand = insertByCustomerCodeCommand;
            try
            {
                logger.Info(String.Format("SaveCustomerLocation insert Command\t : {0}", insertByCustomerCodeCommand.CommandText));

                customerAdapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("SaveCustomerLocation:\n Insert Command ExecuteNonQuery result: \t {0}\n Exception:\t {1}", insertByCustomerCodeCommand.CommandText, ex.Message));


                Console.Write(ex.Message);
                throw ex;
            }
            finally
            {
                insertByCustomerCodeCommand.Connection.Close();
            }


        }
        internal void SaveCustomerReceipt(Payment payment)
        {
            WarrentyTableAdapter adapter = new WarrentyTableAdapter();

            // MainDataset dataset = new MainDataset();
            Oracle.ManagedDataAccess.Client.OracleDataAdapter customerAdapter = new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
            Oracle.ManagedDataAccess.Client.OracleCommand insertByCustomerCodeCommand = new Oracle.ManagedDataAccess.Client.OracleCommand();
            Oracle.ManagedDataAccess.Client.OracleCommand updateByJobOrderCodeCommand = new Oracle.ManagedDataAccess.Client.OracleCommand();
            insertByCustomerCodeCommand.CommandText =


                String.Format("INSERT INTO V_VIEWTECH_JOB_PAYMENTS"
                + "\n (JOB_CODE,PART_VALUE,LABOR_VALUE, OTHER_VALUE,PAYMENT_CURR, PAYMENT_DATE)"
                + "\n VALUES ({0},{1},{2},{3},{4},{5})\n",
                payment.JobCode, payment.PartsValue,
                payment.LaborValue, payment.OthersValue, "'02'",
                "TO_DATE('" + DateTime.Now.ToString() + "', 'mm/dd/yyyy hh:mi:ss AM')");



            adapter.Connection.Open();
            insertByCustomerCodeCommand.Connection = adapter.Connection;

            customerAdapter.InsertCommand = insertByCustomerCodeCommand;
            try
            {
                logger.Info(String.Format("SaveCustomerReceipt insert Command\t : {0}", insertByCustomerCodeCommand.CommandText));

                customerAdapter.InsertCommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                logger.Error(String.Format("SaveCustomerReceipt:\n Insert Command ExecuteNonQuery result: \t {0}\n Exception:\t {1}", insertByCustomerCodeCommand.CommandText, ex.Message));

                updateByJobOrderCodeCommand.CommandText =
                    String.Format("Update V_VIEWTECH_JOB_PAYMENTS"
                    + " SET PART_VALUE = {1}, LABOR_VALUE = {2}, OTHER_VALUE = {3}, PAYMENT_CURR = '02', PAYMENT_DATE = {4}"
                    + " WHERE JOB_CODE ={0} ", payment.JobCode, payment.PartsValue, payment.LaborValue, payment.OthersValue, "TO_DATE('" + DateTime.Now.ToString() + "', 'mm/dd/yyyy hh:mi:ss AM')");
                updateByJobOrderCodeCommand.Connection = adapter.Connection;

                customerAdapter.UpdateCommand = updateByJobOrderCodeCommand;
                //if(adapter.Connection.State ==System.Data.ConnectionState.Closed)
                //     updateByJobOrderCodeCommand.Connection.Open();
                logger.Info(String.Format("SaveCustomerReceipt update Command\t : {0}", insertByCustomerCodeCommand.CommandText));

                customerAdapter.UpdateCommand.ExecuteNonQuery();
               


            }
            finally
            {
                adapter.Connection.Close();
                //insertByCustomerCodeCommand.Connection.Close();
                //updateByJobOrderCodeCommand.Connection.Close();
            }

            //SaveCustomerLocation(
            //      new Location()
            //      {
            //          CustomerCode = payment.CustomerCode,
            //          Latitude = payment.Latitude,
            //          Longitude = payment.Longitude
            //      });
        }

    }
}