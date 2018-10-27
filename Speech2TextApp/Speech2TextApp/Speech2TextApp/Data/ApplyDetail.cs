using System;
using System.Collections.Generic;
using System.Text;

namespace Speech2TextApp.Data
{
    public class ApplyDetail
    {
        /// <summary>
        /// 地址狀態
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// 戶籍地址
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// 居住地址
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// 其他地址
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// 受訪者是否在家
        /// Y:是 N:否
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 訪視時間
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 訪視概述
        /// </summary>
        public string VisitDesc { get; set; }

        /// <summary>
        /// 申請人是否實際居住本市
        /// Y:是 N:否; E:其它
        /// </summary>
        public string LiveCityStatus { get; set; }

        /// <summary>
        /// 住宅狀況
        /// </summary>
        public string LiveStatus { get; set; }

        public string LiveRentStatus { get; set; }

        public string LiveRentMoney { get; set; }

        /// <summary>
        /// 申請項目
        /// </summary>
        public List<string> ApplyType { get; set; }

        /// <summary>
        /// 申請低收入主要原因
        /// </summary>
        public string ApplyReason { get; set; }

        /// <summary>
        /// 列計人口
        /// </summary>
        public List<MemberDetail> Members { get; set; }

        /// <summary>
        /// 有無人口之外其他共同居住之人口
        /// </summary>
        public string OtherPeople { get; set; }

        /// <summary>
        /// 其他家戶概述
        /// </summary>
        public string OtherDesc { get; set; }
    }
}
