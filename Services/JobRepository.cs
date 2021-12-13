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
    public class JobRepository
    {
        public Job[] GetAllJobs()
        {
            String connection = "DATA SOURCE=GPPKONLINE;PERSIST SECURITY INFO=True;USER ID=VIEWTECH;Password=viewtech_321";

            JobTableAdapter adapter = new JobTableAdapter();// queryString, connection);

            MainDataset.JobDataTable dtc = new MainDataset.JobDataTable();
            adapter.Fill(dtc);

            MainDataset.JobRow empRow = dtc.NewJobRow();

            MainDataset.JobRow[] jobs = (MainDataset.JobRow[])dtc.Select();

            var jobList = new List<Job>();
            foreach (var item in jobs)
            {
                jobList.Add(new Models.Job() { Code = item.JOB_CODE, });
            }

            return jobList.ToArray();
        }
        public JobStatus[] GetAllJoBStatuses()
        {
            JobStatusTableAdapter adapter = new JobStatusTableAdapter();// queryString, connection);

            MainDataset.JobStatusDataTable dtc = new MainDataset.JobStatusDataTable();
            adapter.Fill(dtc);

            MainDataset.JobStatusRow empRow = dtc.NewJobStatusRow();

            MainDataset.JobStatusRow[] jobs = (MainDataset.JobStatusRow[])dtc.Select();

            var jobStatusList = new List<JobStatus>();
            foreach (var item in jobs)
            {
                jobStatusList.Add(new Models.JobStatus() { Code = item.JTECD_STATUS, Description = item.JTECD_STATUS_DESC, });
            }

            return jobStatusList.ToArray();
        }
        public Job[] GetEmployeeJobs(int employeeCode)
        {
            String connection = "DATA SOURCE=GPPKONLINE;PERSIST SECURITY INFO=True;USER ID=VIEWTECH;Password=viewtech_321";

            JobTableAdapter adapter = new JobTableAdapter();// queryString, connection);

            MainDataset.JobDataTable dtc = new MainDataset.JobDataTable();
            adapter.Fill(dtc);

            MainDataset.JobRow empRow = dtc.NewJobRow();

            MainDataset.JobRow[] jobs = (MainDataset.JobRow[])dtc.Select();

            var jobList = new List<Job>();
            foreach (var item in jobs)
            { //if(item.)
                jobList.Add(new Job() { Code = item.JOB_CODE, });
            }

            return jobList.ToArray();
        }
        public Job[] GetEmployeeTodayJobs(int employeeCode)
        {
            String connection = "DATA SOURCE=GPPKONLINE;PERSIST SECURITY INFO=True;USER ID=VIEWTECH;Password=viewtech_321";

            JobTableAdapter adapter = new JobTableAdapter();// queryString, connection);

            MainDataset.JobDataTable dtc = new MainDataset.JobDataTable();
            adapter.Fill(dtc);

            MainDataset.JobRow empRow = dtc.NewJobRow();

            MainDataset.JobRow[] jobs = (MainDataset.JobRow[])dtc.Select();

            var jobList = new List<Job>();
            foreach (var item in jobs)
            { //if(item.)

                if (!(item.IsJOB_DATENull() || ((DateTime)item.JOB_DATE).Date >= DateTime.Today)) continue;
                jobList.Add(new Job() { Code = item.JOB_CODE, });
            }

            return jobList.ToArray();
        }

        public Job GetJobByCode(String code)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            BrandRepository brandRepository = new BrandRepository();
            PartNeededRepository partNeededRepository = new PartNeededRepository();
            JobTechRepository jobTechRepository = new JobTechRepository();

            JobTableAdapter adapter = new JobTableAdapter();// queryString, connection);
            MainDataset.JobDataTable dtc = new MainDataset.JobDataTable();
            dtc = adapter.GetData();

            MainDataset.JobRow empRow =
                (JobRow)dtc
                .Select()
                .SingleOrDefault(
                    j => (((JobRow)j).JOB_CODE == code)
                    );
            if (empRow == null) return new Job();
            Job job = new Job
            {
                Code = empRow.JOB_CODE,
                Model = empRow.IsJOB_MODELNull() ? "" : empRow.JOB_MODEL,
                SerialNumber = empRow.IsJOB_SNNull() ? "" : empRow.JOB_SN,
                Status = empRow.JOB_STATUS,
                Comment = (empRow.IsJOB_COMMENTNull()) ? "" : empRow.JOB_COMMENT,
                //warrenty
                WarrentyF = (empRow.IsJOB_FWARNull()) ? "" : empRow.JOB_FWAR,
                WarrentyDate = (empRow.IsJOB_WAR_DATENull()) ? DateTime.MinValue : empRow.JOB_WAR_DATE,
                WarrentyEndDate = (empRow.IsJOB_WAR_EDATENull()) ? DateTime.MinValue : empRow.JOB_WAR_EDATE,
                WarrentyNumber = (empRow.IsJOB_WAR_NBRNull()) ? "" : empRow.JOB_WAR_NBR,

                Customer = empRow.IsJOB_CUS_CODENull() ?
                    new Customer() :
                    customerRepository.GetCustomerByCode(empRow.JOB_CUS_CODE),
                Brand = empRow.IsJOB_BRANDNull() ? "" :
                    brandRepository.GetBrandByCode(empRow.JOB_BRAND).Description,
                PartsNeeded = partNeededRepository.GetAllPartsNeededByJobCode(empRow.JOB_CODE),

                JobTechs = jobTechRepository.GetAllJobTechsbyJobCode(empRow.JOB_CODE),

            };

            return job;
        }
    }
}