using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreIRMA.DAL;
using PreIRMA.DataContract;
using System.Data.Common;
using System.Data;

namespace PreIRMA.BAL
{
    public class BLQuestions : BLBaseClass
    {
        List<DCQuestions> lstDCQuestions = null;
        DCQuestions objDCQuestions = null;
        DatabaseHelper objDatabaseHelper = null;
        DataOperationResponse objDataOperationResponse = null;

        #region Get Questions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>

        public List<DCQuestions> GetQuestions(int QuestionId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCQuestions = new List<DCQuestions>();
                objDatabaseHelper.AddParameter("pQuestionId", QuestionId == 0 ? DBNull.Value : (object)QuestionId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETQUESTIONS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCQuestions = new DCQuestions();
                        objDCQuestions.SectionId = reader.IsDBNull(reader.GetOrdinal("SectionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SectionId"));
                        objDCQuestions.QuestionId = reader.IsDBNull(reader.GetOrdinal("QuestionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("QuestionId"));
                        objDCQuestions.Question = reader.IsDBNull(reader.GetOrdinal("Question")) ? string.Empty : reader.GetString(reader.GetOrdinal("Question"));
                        objDCQuestions.Probability = reader.IsDBNull(reader.GetOrdinal("Probability")) ? 0 : reader.GetInt32(reader.GetOrdinal("Probability"));
                        objDCQuestions.Severity = reader.IsDBNull(reader.GetOrdinal("Severity")) ? 0 : reader.GetInt32(reader.GetOrdinal("Severity"));
                        objDCQuestions.QuestionNumber = reader.IsDBNull(reader.GetOrdinal("QuestionNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("QuestionNumber"));
                        objDCQuestions.AssessmentTypeId = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AssessmentTypeId"));
                        objDCQuestions.Detectability = reader.IsDBNull(reader.GetOrdinal("Detectability")) ? 0 : reader.GetInt32(reader.GetOrdinal("Detectability"));
                        objDCQuestions.Instructions = reader.IsDBNull(reader.GetOrdinal("Instructions")) ? string.Empty : reader.GetString(reader.GetOrdinal("Instructions"));
                        objDCQuestions.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaTopic")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteriaTopic"));
                        objDCQuestions.RiskCriteriaDescription = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaDescription")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteriaDescription"));

                        objDCQuestions.Impact = reader.IsDBNull(reader.GetOrdinal("Impact")) ? string.Empty : reader.GetString(reader.GetOrdinal("Impact"));
                        objDCQuestions.ConfirmationOfCompliance = reader.IsDBNull(reader.GetOrdinal("ConformationComplience")) ? string.Empty : reader.GetString(reader.GetOrdinal("ConformationComplience"));
                       // objDCQuestions.Mitigation = reader.IsDBNull(reader.GetOrdinal("Mitigation")) ? string.Empty : reader.GetString(reader.GetOrdinal("Mitigation"));
                        //objDCQuestions.QusetionActionItemId = reader.IsDBNull(reader.GetOrdinal("QusetionActionItemId")) ? 0 : reader.GetInt32(reader.GetOrdinal("QusetionActionItemId"));
                        lstDCQuestions.Add(objDCQuestions);
                        // while (result.Read())
                        //{
                        //   string vActionItems = result.IsDBNull(result.GetOrdinal("ActionItems")) ? string.Empty : reader.GetString(reader.GetOrdinal("ActionItems"));
                        //   lstDCQuestions[0].ActionItems.Add(vActionItems);
                        //}    
                    }
                    objDCQuestions.Code = GetSuccessCode;
                }
                return lstDCQuestions;
            }
            catch (Exception exce)
            {

                throw exce;
            }

            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

        public List<string> GetActionItemsbyQuestionId(int QuestionId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                List<string> lstString = new List<string>();
                objDatabaseHelper.AddParameter("pQuestionId", QuestionId == 0 ? DBNull.Value : (object)QuestionId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETACTIONITEMSBYQUESTIONID, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string vActionItems = reader.IsDBNull(reader.GetOrdinal("ActionItems")) ? string.Empty : reader.GetString(reader.GetOrdinal("ActionItems"));
                        lstString.Add(vActionItems);
                    }
                }
                return lstString;
            }
            catch (Exception exce)
            {

                throw exce;
            }

            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }

        public List<string> GetMitigationbyQuestionId(int QuestionId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                List<string> lstString = new List<string>();
                objDatabaseHelper.AddParameter("pQuestionId", QuestionId == 0 ? DBNull.Value : (object)QuestionId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETMITIGATIONBYQUESTIONID, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string vMtigation = reader.IsDBNull(reader.GetOrdinal("Mitigation")) ? string.Empty : reader.GetString(reader.GetOrdinal("Mitigation"));
                        lstString.Add(vMtigation);
                    }
                }
                return lstString;
            }
            catch (Exception exce)
            {

                throw exce;
            }

            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }

        #region Get Questions By SectionId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>

        public List<DCQuestions> GetQuestionsBySectionId(int SectionId, int pUserId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCQuestions = new List<DCQuestions>();
                objDatabaseHelper.AddParameter("pSectionId", SectionId == 0 ? DBNull.Value : (object)SectionId);
                objDatabaseHelper.AddParameter("pUserId", pUserId == 0 ? DBNull.Value : (object)pUserId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_QUESTIONSBYSECTIONID, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCQuestions = new DCQuestions();
                        objDCQuestions.SectionId = reader.IsDBNull(reader.GetOrdinal("SectionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SectionId"));
                        objDCQuestions.QuestionId = reader.IsDBNull(reader.GetOrdinal("QuestionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("QuestionId"));
                        objDCQuestions.Question = reader.IsDBNull(reader.GetOrdinal("Question")) ? string.Empty : reader.GetString(reader.GetOrdinal("Question"));
                        objDCQuestions.Probability = reader.IsDBNull(reader.GetOrdinal("Probability")) ? 0 : reader.GetInt32(reader.GetOrdinal("Probability"));
                        objDCQuestions.Severity = reader.IsDBNull(reader.GetOrdinal("Severity")) ? 0 : reader.GetInt32(reader.GetOrdinal("Severity"));
                        objDCQuestions.Detectability = reader.IsDBNull(reader.GetOrdinal("Detectability")) ? 0 : reader.GetInt32(reader.GetOrdinal("Detectability"));
                        objDCQuestions.QuestionNumber = reader.IsDBNull(reader.GetOrdinal("QuestionNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("QuestionNumber"));
                        objDCQuestions.AssessmentTypeId = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AssessmentTypeId"));
                        lstDCQuestions.Add(objDCQuestions);
                    }
                    objDCQuestions.Code = GetSuccessCode;
                }
                return lstDCQuestions;
            }
            catch (Exception exce)
            {

                throw exce;
            }

            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

        #region Add Update Question
        /// <summary>
        /// Author : Satish
        /// Description : Add Update Sections this method used to add or update the Sections 
        /// Date : 12/10/2016
        /// </summary>
        /// <returns></returns>
        public DataOperationResponse AddUpdateQuestions(DCQuestions objDCQuestions, string XMLData, string XMLQueMitigation)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pSectionId", objDCQuestions.SectionId == 0 ? DBNull.Value : (object)objDCQuestions.SectionId);
                objDatabaseHelper.AddParameter("pQuestion", objDCQuestions.Question == string.Empty ? DBNull.Value : (object)objDCQuestions.Question);
                objDatabaseHelper.AddParameter("pUserId", objDCQuestions.CreatedBy == 0 ? DBNull.Value : (object)objDCQuestions.CreatedBy);
                objDatabaseHelper.AddParameter("pQuestionId", objDCQuestions.QuestionId == 0 ? DBNull.Value : (object)objDCQuestions.QuestionId);
                objDatabaseHelper.AddParameter("pQuestionNumber", objDCQuestions.QuestionNumber == string.Empty ? DBNull.Value : (object)objDCQuestions.QuestionNumber);
                objDatabaseHelper.AddParameter("pProbability", objDCQuestions.Probability == 0 ? 0 : (object)objDCQuestions.Probability);
                objDatabaseHelper.AddParameter("pSeverity", objDCQuestions.Severity == 0 ? 0 : (object)objDCQuestions.Severity);
                objDatabaseHelper.AddParameter("pAssessmentTypeId", objDCQuestions.AssessmentTypeId == 0 ? DBNull.Value : (object)objDCQuestions.AssessmentTypeId);
                objDatabaseHelper.AddParameter("pDetectability", objDCQuestions.Detectability == 0 ? 0 : (object)objDCQuestions.Detectability);
                objDatabaseHelper.AddParameter("pInstructions", objDCQuestions.Instructions == string.Empty ? DBNull.Value : (object)objDCQuestions.Instructions);
                objDatabaseHelper.AddParameter("pRiskCriteriaTopic", objDCQuestions.RiskCriteria == string.Empty ? DBNull.Value : (object)objDCQuestions.RiskCriteria);
                objDatabaseHelper.AddParameter("pRiskCriteriaDescription", objDCQuestions.RiskCriteriaDescription == string.Empty ? DBNull.Value : (object)objDCQuestions.RiskCriteriaDescription);
                objDatabaseHelper.AddParameter("pImpact", objDCQuestions.Impact == string.Empty ? DBNull.Value : (object)objDCQuestions.Impact);
                objDatabaseHelper.AddParameter("pConformationComplience", objDCQuestions.ConfirmationOfCompliance == string.Empty ? DBNull.Value : (object)objDCQuestions.ConfirmationOfCompliance);
               // objDatabaseHelper.AddParameter("pMitigation", objDCQuestions.Mitigation == string.Empty ? DBNull.Value : (object)objDCQuestions.Mitigation);
                objDatabaseHelper.AddParameter("pActionItems", XMLData == string.Empty ? DBNull.Value : (object)XMLData);
                objDatabaseHelper.AddParameter("pQusetionActionItemId", objDCQuestions.QusetionActionItemId == 0 ? DBNull.Value : (object)objDCQuestions.QusetionActionItemId);
                objDatabaseHelper.AddParameter("pMitigation", XMLQueMitigation == string.Empty ? DBNull.Value : (object)XMLQueMitigation);
                objDatabaseHelper.AddParameter("pQuestionMitigationId", objDCQuestions.QuestionMitigationId == 0 ? DBNull.Value : (object)objDCQuestions.QuestionMitigationId);
                objDatabaseHelper.AddParameter("pOldSectionId", objDCQuestions.OldSectionId == 0 ? DBNull.Value : (object)objDCQuestions.OldSectionId);
                objDatabaseHelper.AddParameter("oRegisterMessage", string.Empty, ParameterDirection.Output);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_ADDUPDATEQUESTIONS, CommandType.StoredProcedure);
                string strMessage = Convert.ToString(objDatabaseHelper.Command.Parameters["oRegisterMessage"].Value);
                if (result > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = objDCQuestions.QuestionId == 0 ? "Question Added Successfully" : "Question Updated Successfully";
                    //objDataOperationResponse.Message = strMessage;
                }
                else
                {
                    objDataOperationResponse.Code = GetErrorCode;
                    objDataOperationResponse.Message = strMessage;
                }
                return objDataOperationResponse;
            }
            catch (Exception exce)
            {

                throw exce;
            }

            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

        #region Delete Question

        public DataOperationResponse DeleteQuestion(int intQuestionId)
        {
            try
            {
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pQuestionId", intQuestionId == 0 ? DBNull.Value : (object)intQuestionId);
                int intResult = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETEQUESTIONS, CommandType.StoredProcedure);
                if (intResult > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "Question Deleted Successfully";
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

        public List<DCQuestions> QuestionsSortingBySortKey(int QuestionId, int SectionId, string Arrow, int AssessmentTypeId, int UserId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDCQuestions = new List<DCQuestions>();
            objDCQuestions = new DCQuestions();
            objDatabaseHelper.AddParameter("pSectionId", SectionId == 0 ? DBNull.Value : (object)SectionId);
            objDatabaseHelper.AddParameter("pUserId", UserId == 0 ? DBNull.Value : (object)UserId);
            objDatabaseHelper.AddParameter("pArrow", Arrow == string.Empty ? DBNull.Value : (object)Arrow);
            objDatabaseHelper.AddParameter("pAssessmentTypeId", AssessmentTypeId == 0 ? DBNull.Value : (object)AssessmentTypeId);
            objDatabaseHelper.AddParameter("pQuestionId", QuestionId == 0 ? DBNull.Value : (object)QuestionId);
            int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_QUESTIONSSORTINGBYSORTKEY, CommandType.StoredProcedure);
            if (result > 0)
            {
                objDCQuestions.Code = GetSuccessCode;
            }
            else
            {
                objDCQuestions.Code = GetErrorCode;
            }
            lstDCQuestions.Add(objDCQuestions);
            return lstDCQuestions;         
        }
    
    }
}
