using FixItWebAPI.Data.MainDatasetTableAdapters;
using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FixItWebAPI.Data.MainDataset;

namespace FixItWebAPI.Services
{
    public class PartNeededRepository
    {
        public PartNeeded[] GetAllPartsNeeded()

        {
            PartNeededTableAdapter pnAdapter = new PartNeededTableAdapter();
            PartNeededDataTable dtc = new PartNeededDataTable();
            pnAdapter.Fill(dtc);


            PartNeededRow[] partNeededRows = (PartNeededRow[])dtc.Select();
            var partNeededList = new List<PartNeeded>();

            foreach (PartNeededRow item in partNeededRows)

            {
                partNeededList.Add(new PartNeeded
                {
                    Code = item.NPART_CODE,
                    JobCode = item.JOB_CODE,
                    ProductCode = item.PRODUCT_CODE,
                    Comment = item.NPART_COMMENT,
                    Quantity = item.NPART_QTY

                });
            }

            return partNeededList.ToArray();
        }
        public PartNeeded[] GetAllPartsNeededByJobCode(String jobCode)

        {
            PartNeededTableAdapter pnAdapter = new PartNeededTableAdapter();
            PartNeededDataTable dtc = new PartNeededDataTable();
            pnAdapter.Fill(dtc);


            PartNeededRow[] partNeededRows = (PartNeededRow[])dtc
                .Select();
            //.Where(np => ((PartNeededRow)np).JOB_CODE == jobCode);

            var partNeededList = new List<PartNeeded>();

            foreach (PartNeededRow item in partNeededRows)
            {
                if (item.JOB_CODE != jobCode) continue;
                partNeededList.Add(new PartNeeded
                {
                    Code = item.NPART_CODE,
                    JobCode = (item.IsJOB_CODENull()) ? "" : item.JOB_CODE,
                    ProductCode = (item.IsPRODUCT_CODENull()) ? "" : item.PRODUCT_CODE,
                    Comment = (item.IsNPART_COMMENTNull()) ? "" : item.NPART_COMMENT,
                    Quantity = item.NPART_QTY

                });
            }

            return partNeededList.ToArray();
        }


    }


    public class IssuedPartsRepository
    {
        public PartNeeded[] GetAllIssuedParts()

        {
            PartNeededTableAdapter pnAdapter = new PartNeededTableAdapter();
            PartNeededDataTable dtc = new PartNeededDataTable();
            pnAdapter.Fill(dtc);


            PartNeededRow[] partNeededRows = (PartNeededRow[])dtc.Select();
            var partNeededList = new List<PartNeeded>();

            foreach (PartNeededRow item in partNeededRows)

            {
                partNeededList.Add(new PartNeeded
                {
                    Code = item.NPART_CODE,
                    JobCode = item.JOB_CODE,
                    ProductCode = item.PRODUCT_CODE,
                    Comment = item.NPART_COMMENT,
                    Quantity = item.NPART_QTY

                });
            }

            return partNeededList.ToArray();
        }
        public PartNeeded[] GetAllPartsNeededByJobCode(String jobCode)

        {
            PartNeededTableAdapter pnAdapter = new PartNeededTableAdapter();
            PartNeededDataTable dtc = new PartNeededDataTable();
            pnAdapter.Fill(dtc);


            PartNeededRow[] partNeededRows = (PartNeededRow[])dtc
                .Select();
            //.Where(np => ((PartNeededRow)np).JOB_CODE == jobCode);

            var partNeededList = new List<PartNeeded>();

            foreach (PartNeededRow item in partNeededRows)
            {
                if (item.JOB_CODE != jobCode) continue;
                partNeededList.Add(new PartNeeded
                {
                    Code = item.NPART_CODE,
                    JobCode = (item.IsJOB_CODENull()) ? "" : item.JOB_CODE,
                    ProductCode = (item.IsPRODUCT_CODENull()) ? "" : item.PRODUCT_CODE,
                    Comment = (item.IsNPART_COMMENTNull()) ? "" : item.NPART_COMMENT,
                    Quantity = item.NPART_QTY

                });
            }

            return partNeededList.ToArray();
        }


    }
}