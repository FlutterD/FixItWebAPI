using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class CustomerRepository
    {
        public Customer[] GetAllCustomers()

        {

            CustomerTableAdapter adapter = new CustomerTableAdapter();// queryString, connection);

            CustomerDataTable dtc = new CustomerDataTable();
            adapter.Fill(dtc);

            CustomerRow[] customers = (CustomerRow[])dtc.Select();

            var customersList = new List<Customer>();

            foreach (var customer in customers)
            {

                var cust = new Customer
                {
                    Code = customer.CUS_CODE.ToString(),
                    FullName = String.Concat(
                            (customer.IsCUS_NAMENull() ? "" : customer.CUS_NAME),
                            " ",
                            (customer.IsCUS_LNAMENull() ? "" : customer.CUS_LNAME)),
                    MobileNumber = (customer.IsCUS_MOBILENull()) ? "" : customer.CUS_MOBILE,
                    Address = (customer.IsCUS_ADDRESSNull()) ? "" : customer.CUS_ADDRESS
                };

                customersList.Add(cust);
            }
            return customersList.ToArray();


        }
        public Customer GetCustomerByCode(long code)

        {

            CustomerTableAdapter adapter = new CustomerTableAdapter();// queryString, connection);

            CustomerDataTable dtc = new CustomerDataTable();
            adapter.Fill(dtc);

            CustomerRow empRow = dtc.NewCustomerRow();

            CustomerRow customerRow = (CustomerRow)dtc.Select()
                .SingleOrDefault(
                cus => (((CustomerRow)cus).CUS_CODE == code));


            var customer = new Customer
            {
                Code = customerRow.CUS_CODE.ToString(),
                FullName = String.Concat(
                            (customerRow.IsCUS_NAMENull() ? "" : customerRow.CUS_NAME),
                            " ",
                            (customerRow.IsCUS_LNAMENull() ? "" : customerRow.CUS_LNAME)),
                MobileNumber = (customerRow.IsCUS_MOBILENull()) ? "" : customerRow.CUS_MOBILE,
                Address = (customerRow.IsCUS_ADDRESSNull()) ? "" : customerRow.CUS_ADDRESS
            };

            return customer;
        }
    }
}