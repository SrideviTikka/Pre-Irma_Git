using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreIRMA.DataContract;
using PreIRMA.DAL;
using System.Data;
using System.Data.Common;

namespace PreIRMA.BAL
{
    public class BLCAPADetails : BLBaseClass
    {
        DCCAPADetails objDCCAPADetails = null;
        DataOperationResponse objDataOperationResponse = null;
        DatabaseHelper objDatabaseHelper = null;
        List<DCCAPADetails> lstDCCAPADetails = null;

        #region Add Update CAPA Details
        /// <summary>
        /// This method is used to Add Update CAPA Details
        /// </summary>
        /// <param name="objDCCAPADetails"></param>
        /// <returns></returns>
        public DataOperationResponse AddUpdateCAPADetails(DCCAPADetails objDCCAPADetails)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pCAPADetailsId", objDCCAPADetails.CAPADetailsId == 0 ? DBNull.Value : (object)objDCCAPADetails.CAPADetailsId);
                objDatabaseHelper.AddParameter("pSectionId", objDCCAPADetails.SectionId == 0 ? DBNull.Value : (object)objDCCAPADetails.SectionId);
                objDatabaseHelper.AddParameter("pRiskScoreId", objDCCAPADetails.RiskScoreId == 0 ? 0 : (object)objDCCAPADetails.RiskScoreId);
                objDatabaseHelper.AddParameter("pRiskCriteria", objDCCAPADetails.RiskCriteria == string.Empty ? DBNull.Value : (object)objDCCAPADetails.RiskCriteria);
                objDatabaseHelper.AddParameter("pRiskCriteriaDescription", objDCCAPADetails.RiskCriteriaDescription == string.Empty ? DBNull.Value : (object)objDCCAPADetails.RiskCriteriaDescription);
                objDatabaseHelper.AddParameter("pConfirmationOfComplianceImprovement", objDCCAPADetails.ConfirmationOfComplianceImprovement == string.Empty ? DBNull.Value : (object)objDCCAPADetails.ConfirmationOfComplianceImprovement);
                objDatabaseHelper.AddParameter("pImpact", objDCCAPADetails.Impact == string.Empty ? DBNull.Value : (object)objDCCAPADetails.Impact);
                objDatabaseHelper.AddParameter("pMitigation", objDCCAPADetails.Mitigation == string.Empty ? DBNull.Value : (object)objDCCAPADetails.Mitigation);
                objDatabaseHelper.AddParameter("pUserId", objDCCAPADetails.CreatedBy == 0 ? DBNull.Value : (object)objDCCAPADetails.CreatedBy);
                objDatabaseHelper.AddParameter("pAssessmentTypeId", objDCCAPADetails.AssessmentTypeId == 0 ? DBNull.Value : (object)objDCCAPADetails.AssessmentTypeId);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_ADDUPDATECAPADETAILS, CommandType.StoredProcedure);
                if (result > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = objDCCAPADetails.CAPADetailsId == 0 ? "Added Successfully" : "Updated Successfully";
                }
                else
                {
                    objDataOperationResponse.Code = GetErrorCode;
                    objDataOperationResponse.Message = "Unable to add";
                }
                return objDataOperationResponse;
            }
            catch (Exception exc)
            {

                throw exc;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }

        }
        #endregion

        #region Get CAPA Details
        /// <summary>
        /// This method is used to Get CAPA Details
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="RiskScoreId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<DCCAPADetails> GetCAPADetails(int SectionId, int RiskScoreId, int UserId, int AssessmentTypeId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCCAPADetails = new List<DCCAPADetails>();
                objDatabaseHelper.AddParameter("pSetionId", SectionId == 0 ? DBNull.Value : (object)SectionId);
                objDatabaseHelper.AddParameter("pRiskScoreId", RiskScoreId == 0 ? 0 : (object)RiskScoreId);
                objDatabaseHelper.AddParameter("pUserID", UserId == 0 ? DBNull.Value : (object)UserId);
                objDatabaseHelper.AddParameter("pAssessmentTypeId", AssessmentTypeId == 0 ? DBNull.Value : (object)AssessmentTypeId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETCAPADETAILS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCCAPADetails = new DCCAPADetails();
                        objDCCAPADetails.CAPADetailsId = reader.IsDBNull(reader.GetOrdinal("CAPADetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("CAPADetailsId"));
                        objDCCAPADetails.ConfirmationOfComplianceImprovement = reader.IsDBNull(reader.GetOrdinal("ConfirmationOfComplianceImprovement")) ? string.Empty : reader.GetString(reader.GetOrdinal("ConfirmationOfComplianceImprovement"));
                        objDCCAPADetails.Impact = reader.IsDBNull(reader.GetOrdinal("Impact")) ? string.Empty : reader.GetString(reader.GetOrdinal("Impact"));
                        objDCCAPADetails.Mitigation = reader.IsDBNull(reader.GetOrdinal("Mitigation")) ? string.Empty : reader.GetString(reader.GetOrdinal("Mitigation"));
                        objDCCAPADetails.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteria")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteria"));
                        objDCCAPADetails.RiskCriteriaDescription = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaDescription")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteriaDescription"));
                        objDCCAPADetails.RiskScoreId = reader.IsDBNull(reader.GetOrdinal("RiskScoreId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RiskScoreId"));
                        objDCCAPADetails.SectionId = reader.IsDBNull(reader.GetOrdinal("SectionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SectionId"));
                        lstDCCAPADetails.Add(objDCCAPADetails);
                    }
                }
                return lstDCCAPADetails;
            }
            catch (Exception exec)
            {
                throw exec;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }

        }
        #endregion

        #region Delete CAPA Details

        public DataOperationResponse DeleteCAPADetails(int intCAPADetailsId)
        {
            try
            {
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pCAPADetailsId", intCAPADetailsId == 0 ? DBNull.Value : (object)intCAPADetailsId);
                int intResult = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETECAPADETAILS, CommandType.StoredProcedure);
                if (intResult > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "Deleted Successfully";
                }
                else
                {
                    objDataOperationResponse.Code = GetErrorCode;
                    objDataOperationResponse.Message = "Unable to Deleted, Please try again";
                }
                return objDataOperationResponse;
            }
            catch (Exception exec)
            {
                throw exec;
            }

        }
        #endregion

        #region Get CAPA Details By CAPAId
        /// <summary>
        /// This method is used to Get CAPA Details By CAPAId
        /// </summary>
        /// <param name="CAPADetailsId"></param>
        /// <returns></returns>
        public List<DCCAPADetails> GetCAPADetailsByCAPAId(int CAPADetailsId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCCAPADetails = new List<DCCAPADetails>();
                objDatabaseHelper.AddParameter("pCAPADetailsId", CAPADetailsId == 0 ? DBNull.Value : (object)CAPADetailsId);

                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETCAPADETAILSBYCAPAID, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCCAPADetails = new DCCAPADetails();
                        objDCCAPADetails.CAPADetailsId = reader.IsDBNull(reader.GetOrdinal("CAPADetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("CAPADetailsId"));
                        objDCCAPADetails.ConfirmationOfComplianceImprovement = reader.IsDBNull(reader.GetOrdinal("ConfirmationOfComplianceImprovement")) ? string.Empty : reader.GetString(reader.GetOrdinal("ConfirmationOfComplianceImprovement"));
                        objDCCAPADetails.Impact = reader.IsDBNull(reader.GetOrdinal("Impact")) ? string.Empty : reader.GetString(reader.GetOrdinal("Impact"));
                        objDCCAPADetails.Mitigation = reader.IsDBNull(reader.GetOrdinal("Mitigation")) ? string.Empty : reader.GetString(reader.GetOrdinal("Mitigation"));
                        objDCCAPADetails.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteria")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteria"));
                        objDCCAPADetails.RiskCriteriaDescription = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaDescription")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteriaDescription"));
                        objDCCAPADetails.RiskScoreId = reader.IsDBNull(reader.GetOrdinal("RiskScoreId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RiskScoreId"));
                        objDCCAPADetails.SectionId = reader.IsDBNull(reader.GetOrdinal("SectionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SectionId"));

                        lstDCCAPADetails.Add(objDCCAPADetails);
                    }
                }
                return lstDCCAPADetails;
            }
            catch (Exception exec)
            {
                throw exec;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

    }
}
