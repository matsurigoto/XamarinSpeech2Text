using System;
using System.Collections.Generic;
using System.Text;

namespace Speech2TextApp.Data
{
    public class ApplyResult
    {
        public string Id { get; set; }

        /// <summary>
        /// 申請人
        /// </summary>
        public string ApplyName { get; set; }

        /// <summary>
        /// 受訪者
        /// </summary>
        public string VisitName { get; set; }

        /// <summary>
        /// 申請人與受訪者關係
        /// </summary>
        public string Relatoinship { get; set; }

        /// <summary>
        /// 連絡電話
        /// </summary>
        public string Phone { get; set; }

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
        /// 送出狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 最後修改結果
        /// </summary>
        public ApplyDetail VisitDetail { get; set; }

        /// <summary>
        /// 訪問歷程
        /// </summary>
        public List<ApplyDetail> VisitDetails { get; set; }

        /// <summary>
        /// 訪視次數
        /// </summary>
        public int VisitCount { get; set; }

        public bool IsLast { get; set; }
    }

   
}
