using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreIRMA.DataContract
{
    public class DCQuestions : DataOperationResponse
    {
        public int QuestionId { get; set; }
        public int SectionId { get; set; }
        public string Question { get; set; }
        public string QuestionNumber { get; set; }
        public string SectionName { get; set; }
        public int Probability { get; set; }
        public int Severity { get; set; }
        public int AssessmentTypeId { get; set; }
        public int SDVDetailsId { get; set; }
        public int SDVQuestionsId { get; set; }
        public int Detectability { get; set; }
        public string Instructions { get; set; }
        public string RiskCriteria { get; set; }
        public string RiskCriteriaDescription { get; set; }
        public string Impact { get; set; }
        public string ConfirmationOfCompliance { get; set; }
        //public string Mitigation { get; set; }
        public List<string> ActionItems { get; set; }
        public string vActionItems { get; set; }
        public int QusetionActionItemId { get; set; }
        public string RiskscoreId { get; set; }
        public int RefKey { get; set; }
        public List<string> Mitigation { get; set; }
        public int QuestionMitigationId { get; set; }
        public string viewUpButton { get; set; }
        public string viewDownButton { get; set; }
        public int OldSectionId { get; set; }
    }
}
