using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class JobPartRepository
    {
        public JobPart[] GetAllJobParts()

        {
            JobPartsTableAdapter adapter = new JobPartsTableAdapter();// queryString, connection);

            JobPartsDataTable dtc = new JobPartsDataTable();
            adapter.Fill(dtc);


            JobPartsRow[] jobPartRows = (JobPartsRow[])dtc.Select();

            var jobPartList = new List<JobPart>();

            foreach (var item in jobPartRows)
            {

                var brd = new JobPart
                {
                    Code = item.JPART_CODE,
                    JobCode = item.JOB_CODE,
                    ProductCode = item.PRODUCT_CODE,
                    Quantity = item.JPART_QTY_USED,
                    IssueDate = item.JPART_IDATE
                };
                jobPartList.Add(brd);
            }
            return jobPartList.ToArray();
        }

        internal JobPart[] GetAllJobPartsByJobCode(string jobCode)
        {
            ProductRepository productRepository = new ProductRepository();

            JobPartsTableAdapter adapter = new JobPartsTableAdapter();// queryString, connection);
            JobPartsDataTable dtc = new JobPartsDataTable();
            adapter.Fill(dtc);




            JobPartsRow[] jobPartRows = (JobPartsRow[])dtc.Select();

            var jobPartList = new List<JobPart>();
            Product tempProduct = new Models.Product();

            foreach (var item in jobPartRows)
            {
                if (item.IsJOB_CODENull()) continue;
                if (item.JOB_CODE != jobCode) continue;

                tempProduct = productRepository.GetProductByCode(item.PRODUCT_CODE);

                var brd = new JobPart
                {
                    Code = item.JPART_CODE,
                    JobCode = item.JOB_CODE,
                    ProductCode = item.PRODUCT_CODE,
                    ProductModel = tempProduct.ModelNumber,
                    ProductDescription = tempProduct.Description,
                    ProductPriceLBP = tempProduct.PriceLBP,
                    ProductPriceUSD = tempProduct.PriceUSD,
                    Quantity = item.JPART_QTY_USED,
                    IssueDate = item.JPART_IDATE
                };
                jobPartList.Add(brd);
            }
            return jobPartList.ToArray();

        }

        //        SELECT V_MACC_JOB_PARTS.*,
        //V_MACC_JOB_PARTS.JPART_QTY_USED,
        //V_MACC_JOB_RETURNS.JRET_RQTY,
        //(V_MACC_JOB_PARTS.JPART_QTY_USED - V_MACC_JOB_RETURNS.JRET_RQTY)  as Quantity
        //FROM
        //    V_MACC_JOB_PARTS
        //inner join
        //    V_MACC_JOB_RETURNS
        //ON V_MACC_JOB_PARTS.JPART_CODE = V_MACC_JOB_RETURNS.JPART_CODE


        internal JobPart[] GetJobPartsExceptReturnedByJobCode(string jobCode)
        {
            ProductRepository productRepository = new ProductRepository();

            JobPartsTableAdapter adapter = new JobPartsTableAdapter();// queryString, connection);
            JobPartsDataTable dtc = new JobPartsDataTable();
            adapter.Fill(dtc);

            OracleCommand selectCommand = new OracleCommand();
            selectCommand.Connection = adapter.Connection;
            selectCommand.CommandText =
                String.Format("SELECT "
                            + "V_MACC_JOB_PARTS.JPART_CODE,"
                            + "V_MACC_JOB_PARTS.JOB_CODE,"
                            + "V_MACC_JOB_PARTS.PRODUCT_CODE,"
                            //+ "V_MACC_JOB_PARTS.JPART_QTY_USED,"
                            + "V_MACC_JOB_PARTS.JPART_IDATE,"
                            //+ "V_MACC_JOB_RETURNS.JRET_RQTY,"
                            + "(V_MACC_JOB_PARTS.JPART_QTY_USED - coalesce(V_MACC_JOB_RETURNS.JRET_RQTY, 0)) as JPART_QTY_USED"
                            + " FROM "
                            + "    V_MACC_JOB_PARTS"
                            + " left outer  join "
                            + "    V_MACC_JOB_RETURNS "
                            + " ON V_MACC_JOB_PARTS.JPART_CODE = V_MACC_JOB_RETURNS.JPART_CODE"
                            + " where "
                                + "(V_MACC_JOB_PARTS.JPART_QTY_USED - coalesce(V_MACC_JOB_RETURNS.JRET_RQTY, 0)) > 0"
                            + " and "
                            + " V_MACC_JOB_PARTS.JOB_CODE = '{0}'", jobCode);

            adapter.Adapter.SelectCommand = selectCommand;
            Product tempProduct = new Models.Product();
            var jobPartList = new List<JobPart>();
            JobPartsRow[] jobPartRows;
            try
            {

                selectCommand.Connection.Open();
                OracleDataReader dataReader = adapter.Adapter.SelectCommand.ExecuteReader();

                JobPartsDataTable dataTable = new JobPartsDataTable();
                if (dataReader.HasRows)
                {
                    dataTable.Load(dataReader);
                    jobPartRows = (JobPartsRow[])dataTable.Select();


                    foreach (var item in jobPartRows)
                    {
                        if (item.IsJOB_CODENull()) continue;
                        if (item.JOB_CODE != jobCode) continue;

                        tempProduct = productRepository.GetProductByCode(item.PRODUCT_CODE);

                        var brd = new JobPart
                        {
                            Code = item.JPART_CODE,
                            JobCode = item.JOB_CODE,
                            ProductCode = item.PRODUCT_CODE,
                            ProductModel = tempProduct.ModelNumber,
                            ProductDescription = tempProduct.Description,
                            ProductPriceLBP = tempProduct.PriceLBP,
                            ProductPriceUSD = tempProduct.PriceUSD,
                            Quantity = item.JPART_QTY_USED,
                            IssueDate = item.JPART_IDATE
                        };
                        jobPartList.Add(brd);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                selectCommand.Connection.Close();
            }



            return jobPartList.ToArray();

        }
    }
}