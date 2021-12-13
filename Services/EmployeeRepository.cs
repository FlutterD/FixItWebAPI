using fixitupWebAPI.Models;
using FixItWebAPI.Data;
using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Services;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace fixitupWebAPI.Services
{
    // String connection = 
    //"DATA SOURCE=GPPKONLINE;PERSIST SECURITY INFO=True;USER ID=VIEWTECH;Password=viewtech_321";

    public class EmployeeRepository
    {
        public Employee[] GetAllEmployees()

        {

            EmployeeTableAdapter adapter = new EmployeeTableAdapter();// queryString, connection);

            MainDataset.EmployeeDataTable dtc = new MainDataset.EmployeeDataTable();
            adapter.Fill(dtc);

            MainDataset.EmployeeRow empRow = dtc.NewEmployeeRow();

            MainDataset.EmployeeRow[] employees = (MainDataset.EmployeeRow[])dtc.Select();

            var empList = new List<Employee>();

            JobTechRepository jobTechRepository = new JobTechRepository();



            foreach (var a in employees)
            {
                if (a.EMP_TYPE != 1400) continue;
                var s = new Employee
                {
                    Code = a.EMP_CODE.ToString(),
                    FullName = a.EMP_FNAME + " " + a.EMP_LNAME,
                    MobileNumber = (a.IsEMP_MOBILENull()) ? "" : a.EMP_MOBILE,
                    Status = a.EMP_TYPE.ToString(),
                    //JobTechs = jobTechRepository.GetAllJobTechsbyEmployee(a.EMP_CODE)
                };

                empList.Add(s);
                //if (count > 25) break;
            }
            return empList.OrderBy(emp => emp.FullName).ToArray();
            //return empList.ToArray();


        }
        public Employee GetEmployeeByCode(long code)

        {
            Employee employeeSelected = new Employee();
            try
            {


                EmployeeTableAdapter adapter = new EmployeeTableAdapter();// queryString, connection);

                MainDataset.EmployeeDataTable dtc = new MainDataset.EmployeeDataTable();
                adapter.Fill(dtc);


                EmployeeRow employee = (MainDataset.EmployeeRow)dtc
                    .Select()
                    .SingleOrDefault(
                    emp => (((EmployeeRow)emp).EMP_CODE == code));

                var empList = new List<Employee>();

                JobTechRepository jobTechRepository = new JobTechRepository();


                if (employee != null)

                    employeeSelected = new Employee
                    {
                        Code = employee.EMP_CODE.ToString(),
                        FullName = employee.EMP_FNAME + " " + employee.EMP_LNAME,
                        MobileNumber = (employee.IsEMP_MOBILENull()) ? "" : employee.EMP_MOBILE,
                        Status = employee.EMP_TYPE.ToString(),
                        // JobTechs = jobTechRepository.GetAllJobTechsbyEmployee(employee.EMP_CODE)
                    };



                return employeeSelected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }


        }
    }

}