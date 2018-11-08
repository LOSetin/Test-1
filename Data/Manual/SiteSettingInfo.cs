using System;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteSettingInfo : EntityBase
    {
        /// <summary>
        /// ��Ȩ
        /// </summary>
        public string Copyright
        {
            set { SetExtraField("Copyright", value); }
            get { return GetExtraField("Copyright"); }
        }

        /// <summary>
        /// ��˾���
        /// </summary>
        public string CompanyDesc
        {
            set { SetExtraField("CompanyDesc", value); }
            get { return GetExtraField("CompanyDesc"); }
        }

        /// <summary>
        /// ��ϵ����
        /// </summary>
        public string LinkUs
        {
            set { SetExtraField("LinkUs", value); }
            get { return GetExtraField("LinkUs"); }
        }



    }
}
