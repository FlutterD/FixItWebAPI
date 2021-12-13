using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class BrandRepository
    {
        public Brand[] GetAllBrands()
        {
            BrandTableAdapter adapter = new BrandTableAdapter();// queryString, connection);

            BrandDataTable dtc = new BrandDataTable();
            adapter.Fill(dtc);


            BrandRow[] brands = (BrandRow[])dtc.Select();

            var brandsList = new List<Brand>();

            foreach (var brand in brands)
            {

                var brd = new Brand
                {
                    Code = brand.BRAND_CODE.ToString(),
                    Description = brand.BRAND_DESC
                };
                brandsList.Add(brd);
            }
            return brandsList.ToArray();
        }
        public Brand GetBrandByCode(string code)

        {
            BrandTableAdapter adapter = new BrandTableAdapter();

            BrandDataTable dtc = new BrandDataTable();
            adapter.Fill(dtc);

            BrandRow brandRow = (BrandRow)dtc.Select().SingleOrDefault(
                brd => (((BrandRow)brd).BRAND_CODE == code));

            var brand = new Brand
            {
                Code = brandRow.BRAND_CODE.ToString(),
                Description = brandRow.BRAND_DESC
            };

            return brand;
        }
    }
}