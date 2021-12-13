using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Services
{
    public class ProductRepository

    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Product[] GetAllProducts()
        {

            ProductTableAdapter adapter = new ProductTableAdapter();// queryString, connection);

            Data.MainDataset.ProductDataTable dtc = new Data.MainDataset.ProductDataTable();
            adapter.Fill(dtc);
            Data.MainDataset.ProductRow[] products = (Data.MainDataset.ProductRow[])dtc.Select();

            ProductTableAdapter jobAdapter = new ProductTableAdapter();// queryString, connection);

            //JobDataTable productDt;// = jobAdapter.GetDataByJobCode(code);
            BrandRepository brandRepository = new BrandRepository();
            var jobTechList = new List<Product>();
            try
            {
                foreach (var item in products)
                {
                    jobTechList.Add(new Product()
                    {
                        Code = item.PRODUCT_CODE,
                        Description = item.IsPRODUCT_DESCNull() ? "" : item.PRODUCT_DESC,
                        ModelNumber = item.IsMODEL_NONull() ? "" : item.MODEL_NO,
                        PriceLBP = item.IsPRODUCT_PRICE_LBPNull() ? 0 : item.PRODUCT_PRICE_LBP,
                        PriceUSD = item.IsPRODUCT_PRICE_USDNull() ? 0 : item.PRODUCT_PRICE_USD,
                        Brand = item.IsBRAND_CODENull() ? "" : brandRepository.GetBrandByCode(item.BRAND_CODE).Description
                    });
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Get Products");
                throw new Exception((ex.Message));
            }




            return jobTechList.ToArray();
        }
        public Product GetProductByCode(String Code)
        {
            ProductTableAdapter adapter = new ProductTableAdapter();// queryString, connection);

            Oracle.ManagedDataAccess.Client.OracleDataAdapter myProductAdapter = new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
            Oracle.ManagedDataAccess.Client.OracleCommand selectCommandByCode = new Oracle.ManagedDataAccess.Client.OracleCommand();
            selectCommandByCode.CommandText = "Select * from V_MACC_Product where PRODUCT_CODE = '" + Code + "'";
            selectCommandByCode.Connection = adapter.Connection;

            myProductAdapter.SelectCommand = selectCommandByCode;

            Data.MainDataset.ProductDataTable dtc = new Data.MainDataset.ProductDataTable();
            myProductAdapter.Fill(dtc);

            Data.MainDataset.ProductRow[] productRows = (Data.MainDataset.ProductRow[])dtc.Select();//.Select(fliterExpression);


            Product product = new Product();
            if (productRows != null && productRows.Length > 0)
            {
                product.Brand = productRows[0].BRAND_CODE;
                product.Code = productRows[0].CATEGORY_CODE;
                product.Description = productRows[0].PRODUCT_DESC;
                product.ModelNumber = productRows[0].MODEL_NO;
                product.PriceLBP = productRows[0].PRODUCT_PRICE_LBP;
                product.PriceUSD = productRows[0].PRODUCT_PRICE_USD;
            }

            return product;
        }
    }
}