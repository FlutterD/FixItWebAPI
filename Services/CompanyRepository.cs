using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class CompanyRepository
    {
       public Company GetComapanyDetails()

        {
            CompanyTableAdapter adapter = new CompanyTableAdapter();

            CompanyDataTable dtc = new CompanyDataTable();
            adapter.Fill(dtc);

            CompanyRow companyRow = (CompanyRow)dtc.Select().First();


            var company = new Company
            {
                CompanyName = companyRow.CO_NAME,
                CompanyAddress = companyRow.CO_ADDRESS,
                BranchName = companyRow.BR_NAME,
                VATReference = companyRow.CO_VATREF
            };

            return company;
        }
    }
}