using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreIRMA.DAL;
using PreIRMA.DataContract;
using System.Data;
using System.Data.Common;

namespace PreIRMA.BAL
{
    public class BLRiskCriteria : BLBaseClass
    {
        DatabaseHelper objDatabaseHelper = null;
        List<DCRiskCriteria> lstDCRiskCriteria = null;
        DataOperationResponse objDataOperationResponse = null;
        DCRiskCriteria objDCRiskCriteria = null;

        #region Add Risk Criteria
        /// <summary>
        /// Author : Satish
        /// Description : Add Risk Criteria  this method used to add Risk Criteria
        /// Date : 13/10/2017
        /// </summary>
        /// <returns></returns>
        public DataOperationResponse AddRiskCriteria(DCRiskCriteria objDCRiskCriteria)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pRiskCriteria", objDCRiskCriteria.RiskCriteria == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.RiskCriteria);
                objDatabaseHelper.AddParameter("pUserId", objDCRiskCriteria.CreatedBy == 0 ? DBNull.Value : (object)objDCRiskCriteria.CreatedBy);
                objDatabaseHelper.AddParameter("pAttribute", objDCRiskCriteria.Attribute == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.Attribute);
                objDatabaseHelper.AddParameter("pDataValue", objDCRiskCriteria.DataValue == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.DataValue);
                objDatabaseHelper.AddParameter("oRegisterMessage", string.Empty, ParameterDirection.Output);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_INSERTRISKCRITERIA, CommandType.StoredProcedure);
                string strMessage = Convert.ToString(objDatabaseHelper.Command.Parameters["oRegisterMessage"].Value);
                if (result > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "Risk Criteria Added Successfully";
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

        #region Get Risk Criteria
        /// <summary>
        /// Get Risk Criteria
        /// </summary>
        /// <returns></returns>
        public List<DCRiskCriteria> GetRiskCriteria()
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETRISKCRITERIA, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCRiskCriteria = new DCRiskCriteria();
                        objDCRiskCriteria.RiskCriteriaId = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RiskCriteriaId"));
                        objDCRiskCriteria.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteria")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteria"));
                        objDCRiskCriteria.Attribute = reader.IsDBNull(reader.GetOrdinal("Attribute")) ? string.Empty : reader.GetString(reader.GetOrdinal("Attribute"));
                        objDCRiskCriteria.DataValue = reader.IsDBNull(reader.GetOrdinal("DataValue")) ? string.Empty : reader.GetString(reader.GetOrdinal("DataValue"));

                        lstDCRiskCriteria.Add(objDCRiskCriteria);
                    }
                    objDCRiskCriteria.Code = GetSuccessCode;
                }
                return lstDCRiskCriteria;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

        public List<DCRiskCriteria> GetRCActionItems()
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETRCACTIONITEMS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCRiskCriteria = new DCRiskCriteria();
                        objDCRiskCriteria.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteria")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteria"));
                        objDCRiskCriteria.RiskCriteriaDescription = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaDescription")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteriaDescription"));
                        objDCRiskCriteria.ActionItem = reader.IsDBNull(reader.GetOrdinal("ActionItem")) ? string.Empty : reader.GetString(reader.GetOrdinal("ActionItem"));
                        objDCRiskCriteria.RCActionItemId = reader.IsDBNull(reader.GetOrdinal("RCActionItemId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RCActionItemId"));
                        lstDCRiskCriteria.Add(objDCRiskCriteria);
                    }
                    objDCRiskCriteria.Code = GetSuccessCode;
                }
                return lstDCRiskCriteria;
            }
            catch(Exception exec)            
            {
                throw exec;
            }
            finally 
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }

        #region Delete Risk Criteria

        public DataOperationResponse DeleteRiskCriteria(int RiskCriteriaId)
        {
            try
            {
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pRiskCriteriaId", RiskCriteriaId == 0 ? DBNull.Value : (object)RiskCriteriaId);
                int intResult = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETERISKCRITERIA, CommandType.StoredProcedure);
                if (intResult > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "RiskCriteria Deleted Successfully";
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

        #region Delete Risk Criteria Action Items

        public DataOperationResponse DeleteRCActionItems(int RCActionItemId)
        {
            try
            {
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pRCActionItemId", RCActionItemId == 0 ? DBNull.Value : (object)RCActionItemId);
                int intResult = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETERCACTIONITEMS, CommandType.StoredProcedure);
                if (intResult > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "Action Item Deleted Successfully";
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


        #region Delete RC Mitigation

        public DataOperationResponse DeleteRCMitigation(int RCMitigationId)
        {
            try 
            {
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pRCMitigationId", RCMitigationId == 0 ? DBNull.Value : (object)RCMitigationId);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETERCMITIGATION, CommandType.StoredProcedure);
                if (result > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "Mitigation Deleted Successfully";
                }
                else
                {
                    objDataOperationResponse.Code = GetErrorCode;
                    objDataOperationResponse.Message = "Unable to Deleted, Please try again";
                }
                return objDataOperationResponse;
            }
            catch(Exception exec) 
            {
                return objDataOperationResponse;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

        #region Get Risk Criteria By Attribute Type
        /// <summary>
        /// Get Risk Criteria By Attribute Type
        /// </summary>
        /// <returns></returns>
        public List<DCRiskCriteria> GetRiskCriteriaByAttributeType(string strAttributeType)
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                objDatabaseHelper.AddParameter("pAttributeType", strAttributeType == string.Empty ? DBNull.Value : (object)strAttributeType);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETRISKCRITERIABYATTRIBUTETYPE, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCRiskCriteria = new DCRiskCriteria();
                        objDCRiskCriteria.RiskCriteriaId = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RiskCriteriaId"));
                        objDCRiskCriteria.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteria")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteria"));
                        objDCRiskCriteria.Attribute = reader.IsDBNull(reader.GetOrdinal("Attribute")) ? string.Empty : reader.GetString(reader.GetOrdinal("Attribute"));
                        objDCRiskCriteria.DataValue = reader.IsDBNull(reader.GetOrdinal("DataValue")) ? string.Empty : reader.GetString(reader.GetOrdinal("DataValue"));

                        lstDCRiskCriteria.Add(objDCRiskCriteria);
                    }
                    objDCRiskCriteria.Code = GetSuccessCode;
                }
                return lstDCRiskCriteria;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

        #region Get Risk Criteria Details By Risk Criteria
        /// <summary>
        /// Get Risk Criteria Details By Risk Criteria
        /// </summary>
        /// <returns></returns>
        public List<DCRiskCriteria> GetRiskCriteriaDetailsByRiskCriteria(string strRiskCriteria, string strAttribute)
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                objDatabaseHelper.AddParameter("pRiskCriteria", strRiskCriteria == string.Empty ? DBNull.Value : (object)strRiskCriteria);
                objDatabaseHelper.AddParameter("pAttribute", strAttribute == string.Empty ? DBNull.Value : (object)strAttribute);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETRISKCRITERIADETAILSBYRISKCRITERIA, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCRiskCriteria = new DCRiskCriteria();
                        objDCRiskCriteria.RiskCriteriaId = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RiskCriteriaId"));
                        objDCRiskCriteria.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteria")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteria"));
                        objDCRiskCriteria.Attribute = reader.IsDBNull(reader.GetOrdinal("Attribute")) ? string.Empty : reader.GetString(reader.GetOrdinal("Attribute"));
                        objDCRiskCriteria.DataValue = reader.IsDBNull(reader.GetOrdinal("DataValue")) ? string.Empty : reader.GetString(reader.GetOrdinal("DataValue"));

                        lstDCRiskCriteria.Add(objDCRiskCriteria);
                    }
                    objDCRiskCriteria.Code = GetSuccessCode;
                }
                return lstDCRiskCriteria;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

        public DataOperationResponse AddRCActionItems(DCRiskCriteria objDCRiskCriteria)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pRiskCriteria", objDCRiskCriteria.RiskCriteria == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.RiskCriteria);
                objDatabaseHelper.AddParameter("pRiskCriteriaDescription", objDCRiskCriteria.RiskCriteriaDescription == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.RiskCriteriaDescription);
                objDatabaseHelper.AddParameter("pActionItem", objDCRiskCriteria.ActionItem == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.ActionItem);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_INSERTRCACTIONITEMS, CommandType.StoredProcedure);
                if (result > 0)
                {

                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "Action Items Added Successfully";
                }
                else
                {
                    objDataOperationResponse.Code = GetErrorCode;
                    objDataOperationResponse.Message = "Unable to add/Action Item already exists";
                }
                return objDataOperationResponse;
            }
            catch(Exception exec)
            {
                throw exec;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }        
        }

        public List<DCRiskCriteria> GetActionItemsByRCDescription(string RiskCriteriaDescription)
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objDatabaseHelper = new DatabaseHelper();
            try 
            {
                objDatabaseHelper.AddParameter("pRiskCriteriaDescription", RiskCriteriaDescription == string.Empty ? DBNull.Value : (object)RiskCriteriaDescription);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETACTIONITEMSBYRCDESCRIPTION, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCRiskCriteria = new DCRiskCriteria();
                        objDCRiskCriteria.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteria")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteria"));
                        objDCRiskCriteria.RiskCriteriaDescription = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaDescription")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteriaDescription"));
                        objDCRiskCriteria.ActionItem = reader.IsDBNull(reader.GetOrdinal("ActionItem")) ? string.Empty : reader.GetString(reader.GetOrdinal("ActionItem"));
                        objDCRiskCriteria.RCActionItemId = reader.IsDBNull(reader.GetOrdinal("RCActionItemId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RCActionItemId"));
                        lstDCRiskCriteria.Add(objDCRiskCriteria);
                    }
                    objDCRiskCriteria.Code = GetSuccessCode;
                }
                return lstDCRiskCriteria;
            }
            catch(Exception exec)
            {
                throw exec;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }

        public DataOperationResponse AddRCMitigation(DCRiskCriteria objDCRiskCriteria)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pRiskCriteria", objDCRiskCriteria.RiskCriteria == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.RiskCriteria);
                objDatabaseHelper.AddParameter("pRiskCriteriaDescription", objDCRiskCriteria.RiskCriteriaDescription == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.RiskCriteriaDescription);
                objDatabaseHelper.AddParameter("pMitigation", objDCRiskCriteria.Mitigation == string.Empty ? DBNull.Value : (object)objDCRiskCriteria.Mitigation);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_INSERTRCMITIGATION, CommandType.StoredProcedure);
                if (result > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "Mitigation Added Successfully";
                }
                else
                {
                    objDataOperationResponse.Code = GetErrorCode;
                    objDataOperationResponse.Message = "Unable to add/Mitigation already exists";
                }
                return objDataOperationResponse;            
            }
            catch(Exception exec)
            {
                throw exec;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }        
        }

        public List<DCRiskCriteria> GetRCMitigation()
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETRCMITIGATION, CommandType.StoredProcedure);
                if(reader.HasRows)
                {
                while(reader.Read())
                 {
                    objDCRiskCriteria=new DCRiskCriteria();
                    objDCRiskCriteria.RCMitigationId=reader.IsDBNull(reader.GetOrdinal("RCMitigationId"))?0:reader.GetInt32(reader.GetOrdinal("RCMitigationId"));
                    objDCRiskCriteria.RiskCriteria=reader.IsDBNull(reader.GetOrdinal("RiskCriteria"))?string.Empty:reader.GetString(reader.GetOrdinal("RiskCriteria"));
                    objDCRiskCriteria.RiskCriteriaDescription=reader.IsDBNull(reader.GetOrdinal("RiskCriteriaDescription"))?string.Empty:reader.GetString(reader.GetOrdinal("RiskCriteriaDescription"));
                    objDCRiskCriteria.Mitigation=reader.IsDBNull(reader.GetOrdinal("Mitigation"))?string.Empty:reader.GetString(reader.GetOrdinal("Mitigation"));
                    objDCRiskCriteria.DataValue = reader.IsDBNull(reader.GetOrdinal("DataValue")) ? string.Empty : reader.GetString(reader.GetOrdinal("DataValue"));
                    lstDCRiskCriteria.Add(objDCRiskCriteria);
                 }
                    objDCRiskCriteria.Code=GetSuccessCode;
                }
                return lstDCRiskCriteria;
            }
            catch(Exception exec)
            {
                throw exec;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }        
        }

        public List<DCRiskCriteria> GetMitigationByRCDescription(string RiskCriteriaDescription)
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                objDatabaseHelper.AddParameter("pRiskCriteriaDescription", RiskCriteriaDescription == string.Empty ? DBNull.Value : (object)RiskCriteriaDescription);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETMITIGATIONBYRCDESCRIPTION, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCRiskCriteria = new DCRiskCriteria();
                        objDCRiskCriteria.RiskCriteria = reader.IsDBNull(reader.GetOrdinal("RiskCriteria")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteria"));
                        objDCRiskCriteria.RiskCriteriaDescription = reader.IsDBNull(reader.GetOrdinal("RiskCriteriaDescription")) ? string.Empty : reader.GetString(reader.GetOrdinal("RiskCriteriaDescription"));
                        objDCRiskCriteria.Mitigation = reader.IsDBNull(reader.GetOrdinal("Mitigation")) ? string.Empty : reader.GetString(reader.GetOrdinal("Mitigation"));
                        objDCRiskCriteria.RCMitigationId = reader.IsDBNull(reader.GetOrdinal("RCMitigationId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RCMitigationId"));
                        lstDCRiskCriteria.Add(objDCRiskCriteria);
                    }
                    objDCRiskCriteria.Code = GetSuccessCode;
                }
                return lstDCRiskCriteria;
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
    }
}
