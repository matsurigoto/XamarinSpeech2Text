using Newtonsoft.Json;
using Speech2TextApp.Data;
using Speech2TextApp.Interface;
using System;
using System.Collections.Generic;
using System.IO;

namespace Speech2TextApp.Service
{
    public class DataService : IData
    {
        public void ExportData(ApplyResult result)
        {
            throw new NotImplementedException();
        }
        public List<ApplyResult> GetDatas(string descDocument)
        {
            List<ApplyResult> datas = new List<ApplyResult>();
            try
            {
                
                foreach (string txtName in Directory.GetFiles(descDocument, "*.json"))
                {
                    using (StreamReader sr = new StreamReader(txtName))
                    {
                        datas.Add(JsonConvert.DeserializeObject<ApplyResult>(sr.ReadToEnd()));

                    }
                }
            }
            catch (Exception e) {

            }
            return datas;
        }

        /// <summary>
        /// 讀取資料
        /// </summary>
        public void ImportData()
        {
            
        }

        /// <summary>
        /// 建立資料
        /// </summary>
        public void GenData(string descDocument)
        {
            ApplyResult result1 = new ApplyResult() {
                ApplyName = "江玉榮",
                VisitName = "江小明",
                Relatoinship = "父子",
                Phone = "0987654321",
                AddressType = "戶籍地址",
                Address1 = "台北市中山區新生里",
                Address2 = "台北市中山區新生里",
                Status = "N",
                VisitDetails = new List<ApplyDetail>()
            };
            string json = JsonConvert.SerializeObject(result1);
            string destPath = Path.Combine(descDocument, "data1.json");
            File.WriteAllText(destPath, json);

            ApplyResult result2 = new ApplyResult()
            {
                ApplyName = "陳鳳貴",
                VisitName = "陳阿嬌",
                Relatoinship = "母女",
                Phone = "0923877900",
                AddressType = "戶籍地址",
                Address1 = "台北市中山區新生里",
                Address2 = "台北市中山區新生里",
                Address3 = "",
                Status = "N",
                VisitDetails = new List<ApplyDetail>()
            };
            ApplyDetail resultDetal1 = new ApplyDetail()
            {
                AddressType = "戶籍地址",
                Address1 = "台北市中山區新生里",
                Address2 = "台北市中山區新生里",
                Address3 = "",
                VisitDate = new DateTime(2017, 12, 3, 15,23,33),
                Status = "否",
                VisitDesc = "今天探訪但不在家,鄰居說很久沒看到他了",
                Members = new List<MemberDetail>()
            };

            ApplyDetail resultDetal2 = new ApplyDetail()
            {
                AddressType = "戶籍地址",
                Address1 = "台北市中山區新生里",
                Address2 = "台北市中山區新生里",
                Address3 = "",
                VisitDate = new DateTime(2018, 1, 10, 18, 23, 33),
                Status = "否",
                VisitDesc = "晚上探訪,雖說屋內燈光是亮著,但敲門沒人回應",
                Members = new List<MemberDetail>()
            };
            result2.VisitDetails.Add(resultDetal1);
            result2.VisitDetails.Add(resultDetal2);
            result2.VisitDetail = resultDetal2;
            json = JsonConvert.SerializeObject(result2);
            destPath = Path.Combine(descDocument, "data2.json");
            File.WriteAllText(destPath, json);


            ApplyResult resule3 = new ApplyResult()
            {
                ApplyName = "李淑貴",
                VisitName = "李淑貴",
                Relatoinship = "本人",
                Phone = "09123456",
                AddressType = "戶籍地址",
                Address1 = "新平路165號",
                Address2 = "新平路165號",
                Address3 = "",
                Status = "Y",
                VisitDetails = new List<ApplyDetail>()
            };
            ApplyDetail resultDetal31 = new ApplyDetail()
            {
                AddressType = "戶籍地址",
                Address1 = "新平路165號",
                Address2 = "新平路165號",
                Address3 = "",
                VisitDate = new DateTime(2017, 12, 3, 15, 23, 33),
                Status = "否",
                VisitDesc = "今天探訪但不在家,鄰居說很久沒看到他了",
                Members = new List<MemberDetail>()
            };

            ApplyDetail resultDetal32 = new ApplyDetail()
            {
                AddressType = "戶籍地址",
                Address1 = "新平路165號",
                Address2 = "新平路165號",
                Address3 = "",
                VisitDate = new DateTime(2018, 1, 10, 18, 23, 33),
                Status = "否",
                VisitDesc = "晚上探訪,雖說屋內燈光是亮著,但敲門沒人回應",
                Members = new List<MemberDetail>()
            };

            ApplyDetail resultDetal33 = new ApplyDetail()
            {
                AddressType = "戶籍地址",
                Address1 = "新平路165號",
                Address2 = "新平路165號",
                Address3 = "",
                VisitDate = new DateTime(2018, 3, 10, 18, 23, 33),
                Status = "否",
                VisitDesc = "晚上探訪,敲門沒人回應",
                Members = new List<MemberDetail>()
            };

            ApplyDetail resultDetal34 = new ApplyDetail()
            {
                AddressType = "戶籍地址",
                Address1 = "新平路165號",
                Address2 = "新平路165號",
                Address3 = "",
                VisitDate = new DateTime(2018, 7, 10, 18, 23, 33),
                Status = "是",
                LiveCityStatus = "是",
                LiveStatus = "租賃(一般租屋 3000元/月)",
                ApplyType = new string[] { "低收入戶", "中低收入戶" },
                ApplyReason = "負擔家計者失業",
                OtherPeople = "是",
                OtherDesc = "此案件居住環境不佳，屋況老舊且有隨時坍塌的可能，且住的人口眾多，母親中風長期臥病在床",
                Members = new List<MemberDetail>()
            };

            MemberDetail memberDetail1 = new MemberDetail()
            {
                Title = "母女",
                Name = "陳裕如",
                LiveTogether = "是",
                HealthStatus = "長期患病",
                WorkStatus = "臨時工作",
                Referrals = "不同意",
                IsInNursingHome = "否"
            };

            MemberDetail memberDetail2 = new MemberDetail()
            {
                Title = "母女",
                Name = "陳小花",
                LiveTogether = "是",
                HealthStatus = "身心障礙(肢障)",
                WorkStatus = "臨時工作",
                Referrals = "不同意",
                IsInNursingHome = "否"
            };

            resule3.VisitDetails.Add(resultDetal31);
            resule3.VisitDetails.Add(resultDetal32);
            resule3.VisitDetails.Add(resultDetal33);
            resultDetal34.Members.Add(memberDetail1);
            resultDetal34.Members.Add(memberDetail2);
            resule3.VisitDetails.Add(resultDetal34);
            resule3.VisitDetail = resultDetal34;
            json = JsonConvert.SerializeObject(resule3);
            destPath = Path.Combine(descDocument, "data3.json");
            File.WriteAllText(destPath, json);

        }
    }
}
