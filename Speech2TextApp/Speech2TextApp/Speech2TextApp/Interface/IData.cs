using Speech2TextApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Speech2TextApp.Interface
{
    public interface IData
    {
        void ImportData();

        List<ApplyResult> GetDatas(string descDocument);

        void GenData(string descDocument);

        void ExportData(ApplyResult result);
    }
}
