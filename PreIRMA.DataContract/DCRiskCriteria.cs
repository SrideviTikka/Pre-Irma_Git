using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreIRMA.DataContract
{
    public class DCRiskCriteria : DataOperationResponse
    {
        public int RiskCriteriaId { get; set; }
        public string RiskCriteria { get; set; }
        public string Attribute { get; set; }
        public string DataValue { get; set; }
        public string ActionItem { get; set; }
        public string RiskCriteriaDescription { get; set; }
        public int RCActionItemId { get; set; }
        public string Mitigation { get; set; }
        public int RCMitigationId { get; set; }
    }
}
