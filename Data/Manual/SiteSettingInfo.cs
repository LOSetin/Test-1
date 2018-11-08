using System;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteSettingInfo : EntityBase
    {
        /// <summary>
        /// 版权
        /// </summary>
        public string Copyright
        {
            set { SetExtraField("Copyright", value); }
            get { return GetExtraField("Copyright"); }
        }

        /// <summary>
        /// 公司简介
        /// </summary>
        public string CompanyDesc
        {
            set { SetExtraField("CompanyDesc", value); }
            get { return GetExtraField("CompanyDesc"); }
        }

        /// <summary>
        /// 联系我们
        /// </summary>
        public string LinkUs
        {
            set { SetExtraField("LinkUs", value); }
            get { return GetExtraField("LinkUs"); }
        }



    }
}
