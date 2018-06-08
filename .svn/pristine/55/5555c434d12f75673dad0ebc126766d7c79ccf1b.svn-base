using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreIRMA.DataContract;
using PreIRMA.DAL;
using System.Data.Common;
using System.Data;

namespace PreIRMA.BAL
{
    public class BLSDVDetails : BLBaseClass
    {
        DCSDVDetails objDCSDVDetails = null;
        DataOperationResponse objDataOperationResponse = null;
        DatabaseHelper objDatabaseHelper = null;
        List<DCSDVDetails> lstDCSDVDetails = null;
        DCSections objDCSections = null;
        List<DCSections> lstDCSections = null;
        List<DCQuestions> lstDCQuestions = null;
        DCQuestions objDCQuestions = null;

        #region Add Update SDV Details
        /// <summary>
        /// This method is used to Add Update SDV Details
        /// </summary>
        /// <param name="objDCSDVDetails"></param>
        /// <param name="XMLDate"></param>
        /// <returns></returns>
        public DataOperationResponse AddUpdateSDVDetails(string XMLData, int intUserId)
        {
            objDatabaseHelper = new DatabaseHelper();
            objDataOperationResponse = new DataOperationResponse();
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pUserId", intUserId == 0 ? DBNull.Value : (object)intUserId);
                objDatabaseHelper.AddParameter("pSDVXMLData", XMLData == string.Empty ? DBNull.Value : (object)XMLData);
                objDatabaseHelper.AddParameter("oRegisterMessage", string.Empty, ParameterDirection.Output);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_ADDUPDATESDVDETAILS, CommandType.StoredProcedure);
                string strMessage = Convert.ToString(objDatabaseHelper.Command.Parameters["oRegisterMessage"].Value);
                if (result >= 0)
                {
                    objDCSDVDetails = new DCSDVDetails();
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = strMessage;
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

        #region Get SDV Details
        /// <summary>
        /// This method is used to Get SDV Details
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="RiskScoreId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<DCSDVDetails> GetSDVDetails(int SectionId, int UserId, int intRefKey)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCSDVDetails = new List<DCSDVDetails>();
                objDatabaseHelper.AddParameter("pSectionId", SectionId == 0 ? DBNull.Value : (object)SectionId);
                objDatabaseHelper.AddParameter("pUserID", UserId == 0 ? DBNull.Value : (object)UserId);
                objDatabaseHelper.AddParameter("pRefKey", intRefKey == 0 ? DBNull.Value : (object)intRefKey);

                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETSDVDETAILS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCSDVDetails = new DCSDVDetails();
                        objDCSDVDetails.SDVDetailsId = reader.IsDBNull(reader.GetOrdinal("SDVDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SDVDetailsId"));
                        objDCSDVDetails.SectionId = reader.IsDBNull(reader.GetOrdinal("SectionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SectionId"));
                        objDCSDVDetails.RiskScoreId = reader.IsDBNull(reader.GetOrdinal("RiskScoreId")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskScoreId"));
                        objDCSDVDetails.RefKey = reader.IsDBNull(reader.GetOrdinal("RefKey")) ? 0 : reader.GetInt32(reader.GetOrdinal("RefKey"));
                        objDCSDVDetails.SectionName = reader.IsDBNull(reader.GetOrdinal("SectionName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SectionName"));
                        //objDCSDVDetails.DOCReview = reader.IsDBNull(reader.GetOrdinal("DOCReview")) ? string.Empty : reader.GetString(reader.GetOrdinal("DOCReview"));
                        //objDCSDVDetails.MAT = reader.IsDBNull(reader.GetOrdinal("MAT")) ? 0 : reader.GetInt32(reader.GetOrdinal("MAT"));
                        objDCSDVDetails.RiskSerialId = reader.IsDBNull(reader.GetOrdinal("RiskSerialId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RiskSerialId"));
                        objDCSDVDetails.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        objDCSDVDetails.AssessmentType = reader.IsDBNull(reader.GetOrdinal("AssessmentType")) ? string.Empty : reader.GetString(reader.GetOrdinal("AssessmentType"));
                        objDCSDVDetails.AssessmentTypeId = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AssessmentTypeId"));
                        lstDCSDVDetails.Add(objDCSDVDetails);
                    }
                }
                return lstDCSDVDetails;
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

        #region Delete SDV Details

        public DataOperationResponse DeleteSDVDetails(int RefKey, int intUserId)
        {
            try
            {
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pRefKey", RefKey == 0 ? DBNull.Value : (object)RefKey);
                objDatabaseHelper.AddParameter("pUserId", intUserId == 0 ? DBNull.Value : (object)intUserId);
                int intResult = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETESDVDETAILS, CommandType.StoredProcedure);
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
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }

        }
        #endregion

        #region Get SDV Details By SDV Id
        /// <summary>
        /// This method is used to Get SDV Details By SDV Id
        /// </summary>
        /// <param name="SDVDetailsId"></param>
        /// <returns></returns>
        public List<DCSDVDetails> GetSDVDetailsBySDVId(int SDVDetailsId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCSDVDetails = new List<DCSDVDetails>();
                objDatabaseHelper.AddParameter("pSDVDetailsId", SDVDetailsId == 0 ? DBNull.Value : (object)SDVDetailsId);

                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETQUESTIONSBYSDVDETAILSID, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCSDVDetails = new DCSDVDetails();
                        objDCSDVDetails.SDVDetailsId = reader.IsDBNull(reader.GetOrdinal("SDVDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SDVDetailsId"));
                        objDCSDVDetails.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"));
                        objDCSDVDetails.SectionId = reader.IsDBNull(reader.GetOrdinal("SectionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SectionId"));
                        lstDCSDVDetails.Add(objDCSDVDetails);
                    }
                }
                return lstDCSDVDetails;
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

        public List<DCSections> GetSDV_Sections(int intUserId, int intAssessmentTypeId, int intSectionId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCSections = new List<DCSections>();
                objDatabaseHelper.AddParameter("pUserId", intUserId == 0 ? DBNull.Value : (object)intUserId);
                objDatabaseHelper.AddParameter("pAssessmentTypeId", intAssessmentTypeId == 0 ? DBNull.Value : (object)intAssessmentTypeId);
                objDatabaseHelper.AddParameter("pSectionId", intSectionId == 0 ? DBNull.Value : (object)intSectionId);

                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETSDV_SECTIONS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCSections = new DCSections();
                        objDCSections.SectionId = reader.IsDBNull(reader.GetOrdinal("SectionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SectionId"));
                        objDCSections.SectionName = reader.IsDBNull(reader.GetOrdinal("SectionName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SectionName"));
                        objDCSections.SectionNumber = reader.IsDBNull(reader.GetOrdinal("SectionNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("SectionNumber"));
                        objDCSections.RefKey = reader.IsDBNull(reader.GetOrdinal("RefKey")) ? 0 : reader.GetInt32(reader.GetOrdinal("RefKey"));
                        //objDCSections.AssessmentTopic=reader.IsDBNull(reader.GetOrdinal("WAC"))?string.Empty:reader.GetString(reader.GetOrdinal("WAC"));
                        //objDCSections.AssessmentTopicItem = reader.IsDBNull(reader.GetOrdinal("DOCReview")) ? string.Empty : reader.GetString(reader.GetOrdinal("DOCReview"));
                        lstDCSections.Add(objDCSections);
                    }
                }
                return lstDCSections;
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

        #region Get Questions By SDVDetails Id
        /// <summary>
        /// 
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>

        public List<DCQuestions> GetSDVQuestionsBySDVDetailsId(string RiskScoreId, int RefKey)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCQuestions = new List<DCQuestions>();
                objDatabaseHelper.AddParameter("pRiskScoreId", RiskScoreId == string.Empty ? DBNull.Value : (object)RiskScoreId);
                objDatabaseHelper.AddParameter("pRefKey", RefKey == 0 ? DBNull.Value : (object)RefKey);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETQUESTIONSBYSDVDETAILSIDTEST, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCQuestions = new DCQuestions();
                        objDCQuestions.QuestionId = reader.IsDBNull(reader.GetOrdinal("QuestionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("QuestionId"));
                        objDCQuestions.RiskscoreId = reader.IsDBNull(reader.GetOrdinal("RiskScoreId")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskScoreId"));
                        objDCQuestions.SDVDetailsId = reader.IsDBNull(reader.GetOrdinal("SDVDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SDVDetailsId"));
                        objDCQuestions.QuestionNumber = reader.IsDBNull(reader.GetOrdinal("QuestionNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("QuestionNumber"));
                        objDCQuestions.RefKey = reader.IsDBNull(reader.GetOrdinal("RefKey")) ? 0 : reader.GetInt32(reader.GetOrdinal("RefKey"));
                        lstDCQuestions.Add(objDCQuestions);
                    }
                }
                return lstDCQuestions;
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
