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
        public static ApplyResult dataCurrent { get; set; }

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
                Id = "000001",
                ApplyName = "江玉榮",
                VisitName = "江小明",
                Relatoinship = "父子",
                Phone = "0987654321",
                AddressType = "戶籍地址",
                Address1 = "台北市中山區新生里",
                Address2 = "台北市中山區新生里",
                Status = "N",
                VisitDetails = new List<ApplyDetail>(),
                VisitDetail = new ApplyDetail() {
                    Members = new List<MemberDetail>(),
                    Status = "Y",
                    VisitDesc = string.Empty,
                    ApplyReason = string.Empty,
                    ApplyType = new List<string>(),
                    LiveCityStatus = string.Empty,
                    LiveRentMoney = string.Empty,
                    LiveRentStatus = string.Empty,
                    LiveStatus = string.Empty,
                    OtherDesc = string.Empty,
                    OtherPeople = string.Empty
                }
            };
            MemberDetail memberDetail11 = new MemberDetail()
            {
                Title = "母女",
                Name = "江裕如",
                LiveTogether = "是",
                HealthStatus = "長期患病",
                WorkStatus = "臨時工作",
                Referrals = "不同意",
                IsInNursingHome = "否"
            };

            MemberDetail memberDetail12 = new MemberDetail()
            {
                Title = "母女",
                Name = "江小花",
                LiveTogether = "是",
                HealthStatus = "身心障礙(肢障)",
                WorkStatus = "臨時工作",
                Referrals = "不同意",
                IsInNursingHome = "否"
            };
            result1.VisitDetail.Members.Add(memberDetail11);
            result1.VisitDetail.Members.Add(memberDetail12);
            string json = JsonConvert.SerializeObject(result1);
            string destPath = Path.Combine(descDocument, $"{result1.Id}.json");
            File.WriteAllText(destPath, json);

            ApplyResult result2 = new ApplyResult()
            {
                Id = "000002",
                ApplyName = "陳鳳貴",
                VisitName = "陳阿嬌",
                Relatoinship = "母女",
                Phone = "0923877900",
                AddressType = "戶籍地址",
                Address1 = "台北市中山區新生里",
                Address2 = "台北市中山區新生里",
                Address3 = "",
                Status = "N",
                VisitDetails = new List<ApplyDetail>(),
                 VisitDetail = new ApplyDetail()
                 {
                     Members = new List<MemberDetail>(),
                     Status = "Y",
                     VisitDesc = string.Empty,
                     ApplyReason = string.Empty,
                     ApplyType = new List<string>(),
                     LiveCityStatus = string.Empty,
                     LiveRentMoney = string.Empty,
                     LiveRentStatus = string.Empty,
                     LiveStatus = string.Empty,
                     OtherDesc = string.Empty,
                     OtherPeople = string.Empty
                 }
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
            destPath = Path.Combine(descDocument, $"{result2.Id}.json");
            File.WriteAllText(destPath, json);


            ApplyResult resule3 = new ApplyResult()
            {
                Id = "000003",
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
                LiveStatus = "租賃",
                ApplyType = new List<string>() { "低收入戶", "中低收入戶" },
                ApplyReason = "負擔家計者失業",
                OtherPeople = "是",
                OtherDesc = "此案件居住環境不佳，屋況老舊且有隨時坍塌的可能，且住的人口眾多，母親中風長期臥病在床",
                VisitDesc = string.Empty,
                LiveRentMoney = "3000",
                LiveRentStatus = "一般租屋",
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
            destPath = Path.Combine(descDocument, $"{resule3.Id}.json");
            File.WriteAllText(destPath, json);

        }
    }
}
