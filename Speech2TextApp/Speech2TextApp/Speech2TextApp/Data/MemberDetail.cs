using System;
using System.Collections.Generic;
using System.Text;

namespace Speech2TextApp.Data
{
    public class MemberDetail
    {
        /// <summary>
        /// 稱謂
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否同住
        /// </summary>
        public string LiveTogether { get; set; }

        /// <summary>
        /// 身心健康狀況
        /// </summary>
        public string HealthStatus { get; set; }

        /// <summary>
        /// 就業狀況
        /// </summary>
        public string WorkStatus { get; set; }

        /// <summary>
        /// 轉介就業服務
        /// </summary>
        public string Referrals { get; set; }

        /// <summary>
        //  是否安置於療養院所
        /// </summary>
        public string IsInNursingHome { get; set; }


    }
}
