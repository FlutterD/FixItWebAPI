using fixitupWebAPI.Models;
using fixitupWebAPI.Services;
using FixItWebAPI.Data;
using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class JobTechRepository
    {
        public JobTech[] GetAllJobTechs()
        {
            String connection = "DATA SOURCE=GPPKONLINE;PERSIST SECURITY INFO=True;USER ID=VIEWTECH;Password=viewtech_321";

            JobTechTableAdapter adapter = new JobTechTableAdapter();// queryString, connection);

            MainDataset.JobTechDataTable dtc = new MainDataset.JobTechDataTable();
            adapter.Fill(dtc);
            MainDataset.JobTechRow[] jobs = (MainDataset.JobTechRow[])dtc.Select();

            JobTableAdapter jobAdapter = new JobTableAdapter();// queryString, connection);

            MainDataset.JobDataTable jobDt;// = jobAdapter.GetDataByJobCode(code);
            VisitRepository visitRepository = new VisitRepository();



            var jobTechList = new List<JobTech>();
            foreach (var item in jobs)
            {
                jobTechList.Add(new Models.JobTech()
                {
                    Code = item.JTEC_CODE,
                    //EmployeeCode = (item.IsEMP_CODENull()) ? 0 : item.EMP_CODE,
                    JobCode = item.JOB_CODE,
                    Date = (item.IsJTEC_DATENull()) ? DateTime.MinValue : item.JTEC_DATE,
                    EndDate = (item.IsJTEC_EDATENull()) ? DateTime.MinValue : item.JTEC_EDATE,
                    //Visits =visitRepository.GetVisitByJobTechCode(item.JTEC_CODE)
                });
            }

            return jobTechList.ToArray();
        }

        public JobTech GetJobTechByCode(long jobTech)

        {
            var jobTechTemp = new JobTech();
            try
            {

           
            JobTechTableAdapter adapter = new JobTechTableAdapter();

            JobTechDataTable dtc = new JobTechDataTable();
            adapter.Fill(dtc);
            JobTechRow jobTechRow = (JobTechRow)dtc
                    .Select().SingleOrDefault(jtr => ((JobTechRow)jtr).JTEC_CODE == jobTech);

            JobTableAdapter jobAdapter = new JobTableAdapter();

            EmployeeRepository employeeRepository = new EmployeeRepository();
            JobRepository jobRepository = new JobRepository();
            JobTechDetailRepository jobTechDetailRepository = new JobTechDetailRepository();

            if (jobTechRow == null) return new JobTech();
            Employee tempEmployee = employeeRepository.GetEmployeeByCode(jobTechRow.EMP_CODE);


           
            jobTechTemp = new JobTech()
            {
                Code = jobTechRow.JTEC_CODE,
                //job = (item.IsEMP_CODENull()) ? 0 : item.EMP_CODE,
                JobCode = jobTechRow.JOB_CODE,
                Date = (jobTechRow.IsJTEC_DATENull()) ? DateTime.MinValue : jobTechRow.JTEC_DATE,
                EndDate = (jobTechRow.IsJTEC_EDATENull()) ? DateTime.MinValue : jobTechRow.JTEC_EDATE,

                Job = jobRepository.GetJobByCode(jobTechRow.JOB_CODE),
                Employee = tempEmployee,
                EmployeeName = tempEmployee.FullName,
                JobTechDetails = jobTechDetailRepository.GetAllJobTechDetailsByJobTechCode(jobTechRow.JTEC_CODE),
                //Visits =visitRepository.GetVisitByJobTechCode(item.JTEC_CODE)
            };
            }
            catch (Exception ex)
            {

                throw(ex);
            }

            return jobTechTemp;
        }

        public JobTech[] GetAllJobTechsbyEmployee(long employeeCode)
        {

            JobTechTableAdapter adapter = new JobTechTableAdapter();// queryString, connection);

            JobTechDataTable dtc = new JobTechDataTable();
            adapter.Fill(dtc);

            JobRepository jobRepository = new JobRepository();

            JobTechRow[] jobTechs = (JobTechRow[])dtc.Select();

            var jobTechList = new List<JobTech>();
            foreach (var item in jobTechs)
            {
                if (item.IsEMP_CODENull() || item.EMP_CODE != employeeCode)
                    continue;
                if (item.IsJTEC_DATENull() && !(
                    ((DateTime)item.JTEC_DATE).Date > DateTime.Now.Date ||
                        (((DateTime)item.JTEC_DATE).Date < DateTime.Now.AddMonths(-2).Date))
                        )
                    continue;

                jobTechList.Add(new JobTech
                {
                    Code = item.JTEC_CODE,
                    //EmployeeCode = (item.IsEMP_CODENull()) ? 0 : item.EMP_CODE,
                    JobCode = item.JOB_CODE,
                    Date = (item.IsJTEC_DATENull()) ? DateTime.MinValue : item.JTEC_DATE,
                    EndDate = (item.IsJTEC_EDATENull()) ? DateTime.MinValue : item.JTEC_EDATE,
                    // Job = jobRepository.GetJobByCode(item.JOB_CODE)
                });
            }

            return jobTechList.ToArray();
        }
        public JobTech[] GetAllTodayJobTechsbyEmployee(int employeeCode)
        {

            JobTechTableAdapter adapter = new JobTechTableAdapter();// queryString, connection);

            MainDataset.JobTechDataTable dtc = new MainDataset.JobTechDataTable();
            adapter.Fill(dtc);

            JobTechRow[] jobTechs = (JobTechRow[])dtc
                                    .Select();

            JobRepository jobRepository = new JobRepository();
            //!(jt).IsEMP_CODENull() && (jt).EMP_CODE == employeeCode


            var jobTechList = new List<JobTech>();
            foreach (var item in jobTechs)
            {
                //if( (item.IsEMP_CODENull() || item.EMP_CODE != employeeCode)
                //&&
                if (item.IsEMP_CODENull() || item.EMP_CODE != employeeCode) continue;
                if (!(item.IsJTEC_DATENull() || ((DateTime)item.JTEC_DATE).Date >= DateTime.Today)) continue;
                jobTechList.Add(new JobTech
                {
                    Code = item.JTEC_CODE,
                    //EmployeeCode = (item.IsEMP_CODENull()) ? 0 : item.EMP_CODE,
                    JobCode = item.JOB_CODE,
                    Date = (item.IsJTEC_DATENull()) ? DateTime.MinValue : item.JTEC_DATE,
                    EndDate = (item.IsJTEC_EDATENull()) ? DateTime.MinValue : item.JTEC_EDATE,
                    Job = jobRepository.GetJobByCode(item.JOB_CODE)
                });
            }

            return jobTechList.ToArray();
        }
        public JobTech[] GetAllJobTechsbyJobCode(string jobCode)
        {

            JobTechTableAdapter adapter = new JobTechTableAdapter();// queryString, connection);

            MainDataset.JobTechDataTable dtc = new MainDataset.JobTechDataTable();
            adapter.Fill(dtc);

            JobTechRow[] jobTechs = (JobTechRow[])dtc
                                    .Select();

            JobRepository jobRepository = new JobRepository();
            JobTechDetailRepository jobTechRepository = new JobTechDetailRepository();
            EmployeeRepository employeeRepository = new EmployeeRepository();

            JobTechDetail[] tempJobTechDetail;
            Employee tempEmployee;
            var jobTechList = new List<JobTech>();
            foreach (var item in jobTechs)
            {

                if (item.JOB_CODE != jobCode) continue;
                tempJobTechDetail = jobTechRepository.GetAllJobTechDetailsByJobTechCode(item.JTEC_CODE);
                tempEmployee = employeeRepository.GetEmployeeByCode(item.EMP_CODE);

                jobTechList.Add(new JobTech
                {
                    Code = item.JTEC_CODE,
                    Employee = new EmployeeRepository().GetEmployeeByCode(item.EMP_CODE),
                    //EmployeeCode = (item.IsEMP_CODENull()) ? 0 : item.EMP_CODE,
                    JobCode = item.JOB_CODE,
                    Date = (item.IsJTEC_DATENull()) ? DateTime.MinValue : item.JTEC_DATE,
                    EndDate = (item.IsJTEC_EDATENull()) ? DateTime.MinValue : item.JTEC_EDATE,
                    //Comments = (tempJobTechDetail != null) ? tempJobTechDetail.Comment : "",
                    EmployeeName = (tempEmployee != null) ? tempEmployee.FullName : "",
                    JobTechDetails = tempJobTechDetail //new JobTechDetailRepository().GetAllJobTechDetailsByJobTechCode(item.JTEC_CODE),
                    //Job = jobRepository.GetJobByCode(item.JOB_CODE)
                });
            }

            return jobTechList.ToArray();
        }
        public JobTechDetail[] GetAllJobTechDetailsbyJobCode(string jobCode)
        {

            JobTechTableAdapter adapter = new JobTechTableAdapter();

            JobTechDataTable dtc = new JobTechDataTable();
            adapter.Fill(dtc);

            JobTechRow[] jobTechs = (JobTechRow[])dtc
                                    .Select();

            JobRepository jobRepository = new JobRepository();
            JobTechDetailRepository jobTechRepository = new JobTechDetailRepository();
            EmployeeRepository employeeRepository = new EmployeeRepository();

            JobTechDetail[] tempJobTechDetail;
            Employee tempEmployee;

            var jobTechList = new List<JobTech>();
            var jobTechDetailList = new List<JobTechDetail>();

            foreach (var item in jobTechs)
            {

                if (item.JOB_CODE != jobCode) continue;
                tempEmployee = employeeRepository.GetEmployeeByCode(item.EMP_CODE);
                tempJobTechDetail = jobTechRepository.GetAllJobTechDetailsByJobTechCode(item.JTEC_CODE,tempEmployee.FullName);

                jobTechDetailList.AddRange(tempJobTechDetail);

                //jobTechList.Add(new JobTech
                //{
                //    Code = item.JTEC_CODE,
                //    Employee = new EmployeeRepository().GetEmployeeByCode(item.EMP_CODE),
                //    //EmployeeCode = (item.IsEMP_CODENull()) ? 0 : item.EMP_CODE,
                //    JobCode = item.JOB_CODE,
                //    Date = (item.IsJTEC_DATENull()) ? DateTime.MinValue : item.JTEC_DATE,
                //    EndDate = (item.IsJTEC_EDATENull()) ? DateTime.MinValue : item.JTEC_EDATE,
                //    //Comments = (tempJobTechDetail != null) ? tempJobTechDetail.Comment : "",
                //    EmployeeName = (tempEmployee != null) ? tempEmployee.FullName : "",
                //    JobTechDetails = tempJobTechDetail //new JobTechDetailRepository().GetAllJobTechDetailsByJobTechCode(item.JTEC_CODE),
                //    //Job = jobRepository.GetJobByCode(item.JOB_CODE)
                //});
            }

            return jobTechDetailList.ToArray();
        }

        public JobTech[] GetAllTodayJobTechsbyEmployee_new(int employeeCode)
        {

            JobTechTableAdapter adapter = new JobTechTableAdapter();// queryString, connection);
            adapter.

            MainDataset.JobTechDataTable dtc = new MainDataset.JobTechDataTable();
            adapter.Fill(dtc);

            JobTechRow[] jobTechs = (JobTechRow[])dtc
                                    .Select();

            JobRepository jobRepository = new JobRepository();
            //!(jt).IsEMP_CODENull() && (jt).EMP_CODE == employeeCode


            var jobTechList = new List<JobTech>();
            foreach (var item in jobTechs)
            {
                //if( (item.IsEMP_CODENull() || item.EMP_CODE != employeeCode)
                //&&
                if (item.IsEMP_CODENull() || item.EMP_CODE != employeeCode) continue;
                if (!(item.IsJTEC_DATENull() || ((DateTime)item.JTEC_DATE).Date >= DateTime.Today)) continue;
                jobTechList.Add(new JobTech
                {
                    Code = item.JTEC_CODE,
                    //EmployeeCode = (item.IsEMP_CODENull()) ? 0 : item.EMP_CODE,
                    JobCode = item.JOB_CODE,
                    Date = (item.IsJTEC_DATENull()) ? DateTime.MinValue : item.JTEC_DATE,
                    EndDate = (item.IsJTEC_EDATENull()) ? DateTime.MinValue : item.JTEC_EDATE,
                    Job = jobRepository.GetJobByCode(item.JOB_CODE)
                });
            }

            return jobTechList.ToArray();
        }

    }
}