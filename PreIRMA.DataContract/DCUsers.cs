using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreIRMA.DataContract
{
    public enum UserType
    {
        U, // User
        A //Admin
    }
    public enum Status
    {
        Active,
        InActive
    }
    public class DCUsers : DataOperationResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClientName { get; set; }
        public string EmailAddress { get; set; }
        public string UserPassword { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public UserType UserType { get; set; }
        public string AssessmentType { get; set; }
        public int ClientDetailsId { get; set; }
        public string SponserName { get; set; }
        public int SponserDetailsId { get; set; }
        public int ProtocalDetailsId { get; set; }
        public string ProtocalName { get; set; }
        public string ClientNameAbbr { get; set; }
        public string SponserNameAbbr { get; set; }
        public string ProtocolNameAbbr { get; set; }
        public string VersionName { get; set; }
        public int VersionNo { get; set; }
        public string AddAssesment { get; set; }
        public string AssessmentTypeAbbr { get; set; }
        public int AssessmentTypeId { get; set;}
        public string Activetab { get; set; }
        public string Status { get; set; }
        public string SponsorApproval { get; set; }
    }

    public class DataOperationResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int LoggedInUserId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DateTime { get; set; }
    }
}
