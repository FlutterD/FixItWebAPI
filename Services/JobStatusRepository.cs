using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class JobStatusRepository
    {
        public JobStatus[] GetAllJobStauses()
        {
            JobStatusTableAdapter adapter = new JobStatusTableAdapter();// queryString, connection);

            JobStatusDataTable dtc = new JobStatusDataTable();
            adapter.Fill(dtc);


            JobStatusRow[] jobStatuses = (JobStatusRow[])dtc.Select();

            var jobStatusesList = new List<JobStatus>();

            foreach (var st in jobStatuses)
            {

                var jobstatus = new JobStatus
                {
                    Code = st.JTECD_STATUS,
                    Description = st.JTECD_STATUS_DESC
                };
                jobStatusesList.Add(jobstatus);
            }
            return jobStatusesList.ToArray();
        }
    }
}