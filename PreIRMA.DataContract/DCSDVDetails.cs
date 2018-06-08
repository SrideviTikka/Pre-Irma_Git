using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreIRMA.DataContract
{
    public class DCSDVDetails : DataOperationResponse
    {
        public int SDVDetailsId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public string RiskScoreId { get; set; }
        public string WAC { get; set; }
        public string DOCReview { get; set; }
        public int MAT { get; set; }
        public int SDV { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public string Questions { get; set; }
        public int AssessmentTypeId { get; set; }
        public string AssessmentType { get; set; }
        public int RefKey { get; set; }
        public int RiskSerialId { get; set; }

    }
}
