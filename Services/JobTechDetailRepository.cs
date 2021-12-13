using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class JobTechDetailRepository
    {
        public JobTechDetail[] GetAllJobTechDetails()
        {

            JobTechDetailTableAdapter adapter = new JobTechDetailTableAdapter();// queryString, connection);

            JobTechDetailDataTable dtc = new JobTechDetailDataTable();
            adapter.Fill(dtc);
            JobTechDetailRow[] jobs = (JobTechDetailRow[])dtc.Select();


            var jobTechDetailList = new List<JobTechDetail>();
            foreach (var item in jobs)
            {
                jobTechDetailList.Add(new JobTechDetail
                {
                    Code = item.JTECD_CODE,
                    Date = (item.IsJTECD_DATENull()) ? DateTime.MinValue : item.JTECD_DATE,
                    Time = (item.IsJTECD_TIMENull()) ? DateTime.MinValue : item.JTECD_TIME,
                    Comment = (item.IsJTECD_COMMENTNull()) ? "" : item.JTECD_COMMENT,


                });
            }

            return jobTechDetailList.ToArray();
        }

        public JobTechDetail[] GetAllJobTechDetailsByJobTechCode(long jobTechCode)
        {

            JobTechDetailTableAdapter adapter = new JobTechDetailTableAdapter();// queryString, connection);

            JobTechDetailDataTable dtc = new JobTechDetailDataTable();
            adapter.Fill(dtc);
            JobTechDetailRow[] jobs = (JobTechDetailRow[])dtc.Select();


            var jobTechDetailList = new List<JobTechDetail>();
            foreach (var item in jobs)

            {
                if (item.IsJTEC_CODENull()) continue;
                if (item.JTEC_CODE != jobTechCode) continue;
                jobTechDetailList.Add(new JobTechDetail
                {
                    Code = item.JTECD_CODE,
                    Date = (item.IsJTECD_DATENull()) ? DateTime.MinValue : item.JTECD_DATE,
                    Time = (item.IsJTECD_TIMENull()) ? DateTime.MinValue : item.JTECD_TIME,
                    Comment = (item.IsJTECD_COMMENTNull()) ? "" : item.JTECD_COMMENT,
                    

                });
            }

            return jobTechDetailList.ToArray();
        }

        internal JobTechDetail[] GetAllJobTechDetailsByJobTechCode(long jTEC_CODE, string fullName)
        {
             JobTechDetailTableAdapter adapter = new JobTechDetailTableAdapter();// queryString, connection);

            JobTechDetailDataTable dtc = new JobTechDetailDataTable();
            adapter.Fill(dtc);
            JobTechDetailRow[] jobs = (JobTechDetailRow[])dtc.Select();


            var jobTechDetailList = new List<JobTechDetail>();
            foreach (var item in jobs)

            {
                if (item.IsJTEC_CODENull()) continue;
                if (item.JTEC_CODE != jTEC_CODE) continue;
                jobTechDetailList.Add(new JobTechDetail
                {
                    Code = item.JTECD_CODE,
                    Date = (item.IsJTECD_DATENull()) ? DateTime.MinValue : item.JTECD_DATE,
                    Time = (item.IsJTECD_TIMENull()) ? DateTime.MinValue : item.JTECD_TIME,
                    Comment = (item.IsJTECD_COMMENTNull()) ? "" : item.JTECD_COMMENT,
                    EmployeeFullName = fullName,

                });
            }

            return jobTechDetailList.ToArray();
        }
    }
}