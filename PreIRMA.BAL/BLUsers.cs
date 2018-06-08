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
    public class BLUsers : BLBaseClass
    {
        DCUsers objDCUsers = null;
        List<DCUsers> lstDCUsers = null;
        DatabaseHelper objDatabaseHelper = null;
        DataOperationResponse objDataOperationResponse;

        #region UserLogon
        /// <summary>
        /// This method is used for user login 
        /// </summary>
        /// <param name="strEmailAddress"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public DCUsers UserLogon(string strEmailAddress, string strPassword)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            DCUsers dcUsers = new DCUsers();
            try
            {
                dbHelper.AddParameter("pEmailAddress", strEmailAddress);
                dbHelper.AddParameter("pUserPassword", strPassword);
                DbDataReader reader = dbHelper.ExecuteReader(BLDBRoutines.SP_LOGIN, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    string strUserType = string.Empty;
                    while (reader.Read())
                    {
                        dcUsers.UserId = reader.IsDBNull(reader.GetOrdinal("UserID")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserID"));
                        dcUsers.FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                        dcUsers.LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                        dcUsers.EmailAddress = reader.IsDBNull(reader.GetOrdinal("EmailAddress")) ? string.Empty : reader.GetString(reader.GetOrdinal("EmailAddress"));
                        strUserType = reader.IsDBNull(reader.GetOrdinal("UserType")) ? string.Empty : reader.GetString(reader.GetOrdinal("UserType"));
                        switch (strUserType)
                        {
                            case "U":
                                dcUsers.UserType = UserType.U;
                                break;
                            case "A":
                                dcUsers.UserType = UserType.A;
                                break;
                        }
                    }
                    dcUsers.Code = GetSuccessCode;
                }
                else
                    dcUsers.Code = GetErrorCode;
                return dcUsers;
            }
            catch (Exception exception)
            {
                dcUsers.Code = GetErrorCode;
                dcUsers.Message = exception.Message;
                return dcUsers;
            }
            finally
            {
                if (dbHelper != null)
                    dbHelper.Dispose();
            }
        }
        #endregion

        #region Get Users
        /// <summary>
        /// Author : Satish
        /// Description : GetUsers() this method used to get all the users from DataBase
        /// Date : 07/09/2016
        /// </summary>
        /// <returns></returns>
        public List<DCUsers> GetUsers(int intEmpId)
        {

            objDatabaseHelper = new DatabaseHelper();
            objDCUsers = new DCUsers();
            lstDCUsers = new List<DCUsers>();
            try
            {
                objDatabaseHelper.AddParameter("pUserId", intEmpId == 0 ? DBNull.Value : (object)intEmpId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETUSERS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCUsers = new DCUsers();
                        objDCUsers.UserId = reader.IsDBNull(reader.GetOrdinal("UserID")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserID"));
                        objDCUsers.AssessmentType = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? string.Empty : reader.GetString(reader.GetOrdinal("AssessmentTypeId"));
                        objDCUsers.FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                        objDCUsers.LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                        objDCUsers.EmailAddress = reader.IsDBNull(reader.GetOrdinal("EmailAddress")) ? string.Empty : reader.GetString(reader.GetOrdinal("EmailAddress"));
                        objDCUsers.ClientName = reader.IsDBNull(reader.GetOrdinal("ClientName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ClientName"));
                        objDCUsers.SponserName = reader.IsDBNull(reader.GetOrdinal("SponserName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SponserName"));
                        objDCUsers.ProtocalName = reader.IsDBNull(reader.GetOrdinal("ProtocalName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ProtocalName"));
                        objDCUsers.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? string.Empty : reader.GetString(reader.GetOrdinal("Status"));
                        objDCUsers.SponsorApproval = reader.IsDBNull(reader.GetOrdinal("SponsorApproval")) ? string.Empty : reader.GetString(reader.GetOrdinal("SponsorApproval"));
                        if (reader.IsDBNull(reader.GetOrdinal("ExpiryDate")) == false)
                            objDCUsers.ExpiryDate = reader.GetDateTime(reader.GetOrdinal("ExpiryDate"));

                        lstDCUsers.Add(objDCUsers);
                    }
                }
                return lstDCUsers;
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

        #region AddUpdate User
        /// <summary>
        /// Author : Satish
        /// Description : AddUpdateEmployee this method used to add or update the Employees 
        /// Date : 07/09/2016
        /// </summary>
        /// <returns></returns>
        public DataOperationResponse AddUpdateUser(DCUsers objDCUsers, string XMLData)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pUserId", objDCUsers.UserId == 0 ? DBNull.Value : (object)objDCUsers.UserId);
                objDatabaseHelper.AddParameter("pFirstName", string.IsNullOrEmpty(objDCUsers.FirstName) ? DBNull.Value : (object)objDCUsers.FirstName);
                objDatabaseHelper.AddParameter("pLastName", string.IsNullOrEmpty(objDCUsers.LastName) ? DBNull.Value : (object)objDCUsers.LastName);
                objDatabaseHelper.AddParameter("pEmailAddress", string.IsNullOrEmpty(objDCUsers.EmailAddress) ? DBNull.Value : (object)objDCUsers.EmailAddress);
                objDatabaseHelper.AddParameter("pUserPassword", string.IsNullOrEmpty(objDCUsers.LastName) ? DBNull.Value : (object)objDCUsers.LastName);
                objDatabaseHelper.AddParameter("pExpiryDate", objDCUsers.ExpiryDate == null ? DBNull.Value : (object)objDCUsers.ExpiryDate);
                objDatabaseHelper.AddParameter("pUserAssessmentXMLData", string.IsNullOrEmpty(XMLData) ? DBNull.Value : (object)XMLData);
                objDatabaseHelper.AddParameter("pClientName", string.IsNullOrEmpty(objDCUsers.ClientName) ? DBNull.Value : (object)objDCUsers.ClientName);
                objDatabaseHelper.AddParameter("pSponserName", string.IsNullOrEmpty(objDCUsers.SponserName) ? DBNull.Value : (object)objDCUsers.SponserName);
                objDatabaseHelper.AddParameter("pProtocalName", string.IsNullOrEmpty(objDCUsers.ProtocalName) ? DBNull.Value : (object)objDCUsers.ProtocalName);
                objDatabaseHelper.AddParameter("pClientNameAbbr", string.IsNullOrEmpty(objDCUsers.ClientNameAbbr) ? DBNull.Value : (object)objDCUsers.ClientNameAbbr);
                objDatabaseHelper.AddParameter("pSponserNameAbbr", string.IsNullOrEmpty(objDCUsers.SponserNameAbbr) ? DBNull.Value : (object)objDCUsers.SponserNameAbbr);
                objDatabaseHelper.AddParameter("pProtocalNameAbbr", string.IsNullOrEmpty(objDCUsers.ProtocolNameAbbr) ? DBNull.Value : (object)objDCUsers.ProtocolNameAbbr);
                objDatabaseHelper.AddParameter("pSponsorApproval", string.IsNullOrEmpty(objDCUsers.SponsorApproval) ? DBNull.Value : (object)objDCUsers.SponsorApproval);
                objDatabaseHelper.AddParameter("oRegisterMessage", string.Empty, ParameterDirection.Output);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_ADDUPDATEUSERS, CommandType.StoredProcedure);
                string strMessage = Convert.ToString(objDatabaseHelper.Command.Parameters["oRegisterMessage"].Value);
                if (result >= 0 && strMessage.Contains("success"))
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = strMessage;
                }
                else
                {
                    objDataOperationResponse.Code = GetErrorCode;
                    objDataOperationResponse.Message = strMessage;
                }
            }
            catch (Exception exce)
            {

                throw exce;
            }

            return objDataOperationResponse;
        }
        #endregion

        #region Delete User

        public DataOperationResponse DeleteUser(int intUserId)
        {
            try
            {
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pUserId", intUserId == 0 ? DBNull.Value : (object)intUserId);
                int intResult = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETEUSER, CommandType.StoredProcedure);
                if (intResult > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "User deleted successfully";
                }
                else
                {
                    objDataOperationResponse.Code = GetErrorCode;
                    objDataOperationResponse.Message = "Unable to delete the User, Please try again";
                }
                return objDataOperationResponse;
            }
            catch (Exception exec)
            {
                throw exec;
            }

        }
        #endregion

        #region Forgot Password
        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <returns></returns>
        public List<DCUsers> ForgotPassword(string strEmailAddress)
        {
            lstDCUsers = new List<DCUsers>();
            objDataOperationResponse = new DataOperationResponse();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                objDCUsers = new DCUsers();
                objDatabaseHelper.AddParameter("pEmailAddress", strEmailAddress);                
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_FORGOTPASSWORD, CommandType.StoredProcedure);                ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        objDCUsers.FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                        objDCUsers.LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                        objDCUsers.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        objDCUsers.EmailAddress = reader.IsDBNull(reader.GetOrdinal("EmailAddress")) ? string.Empty : reader.GetString(reader.GetOrdinal("EmailAddress"));
                        if (reader.IsDBNull(reader.GetOrdinal("ExpiryDate")) == false)
                            objDCUsers.ExpiryDate = reader.GetDateTime(reader.GetOrdinal("ExpiryDate"));
                        lstDCUsers.Add(objDCUsers);
                    }
                    objDCUsers.Code = GetSuccessCode;                              
                }
                else
                {
                    objDCUsers.Code = GetErrorCode;                    
                }
                return lstDCUsers;
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

        public List<DCUsers> GetClientDetails()
        {
            objDatabaseHelper = new DatabaseHelper();
            objDCUsers = new DCUsers();
            lstDCUsers = new List<DCUsers>();
            try
            {
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETCLIENTDETAILS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCUsers = new DCUsers();
                        objDCUsers.ClientDetailsId = reader.IsDBNull(reader.GetOrdinal("ClientDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientDetailsId"));
                        objDCUsers.ClientName = reader.IsDBNull(reader.GetOrdinal("ClientName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ClientName"));                       
                        lstDCUsers.Add(objDCUsers);
                    }                
                }
                return lstDCUsers;
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

        public List<DCUsers> GetSponserDetails(int ClientDetailsId)
        {
            objDatabaseHelper = new DatabaseHelper();
            objDCUsers = new DCUsers();
            lstDCUsers = new List<DCUsers>();
            try
            {
                objDatabaseHelper.AddParameter("pClientDetailsId", ClientDetailsId == 0 ? DBNull.Value : (object)ClientDetailsId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETSPONSERDETAILS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCUsers = new DCUsers();
                        objDCUsers.ClientDetailsId = reader.IsDBNull(reader.GetOrdinal("ClientDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientDetailsId"));
                        objDCUsers.SponserName = reader.IsDBNull(reader.GetOrdinal("SponserName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SponserName"));                        
                        objDCUsers.SponserDetailsId = reader.IsDBNull(reader.GetOrdinal("SponserDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SponserDetailsId"));
                        lstDCUsers.Add(objDCUsers);
                    }
                }
                return lstDCUsers;
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

        public List<DCUsers> GetProtocolDetails(int SponserDetailsId)
        {
            objDatabaseHelper = new DatabaseHelper();
            objDCUsers = new DCUsers();
            lstDCUsers = new List<DCUsers>();
            try
            {
                objDatabaseHelper.AddParameter("pSponserDetailsId", SponserDetailsId == 0 ? DBNull.Value : (object)SponserDetailsId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETPROTOCOLDETAILS, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCUsers = new DCUsers();
                        objDCUsers.ProtocalDetailsId = reader.IsDBNull(reader.GetOrdinal("ProtocalDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ProtocalDetailsId"));
                        objDCUsers.ProtocalName = reader.IsDBNull(reader.GetOrdinal("ProtocalName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ProtocalName"));                        
                        objDCUsers.SponserDetailsId = reader.IsDBNull(reader.GetOrdinal("SponserDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SponserDetailsId"));
                        lstDCUsers.Add(objDCUsers);
                    }
                }
                return lstDCUsers;
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

        public List<DCUsers> GetVersionsByProtocolId(int ClientDetailsId, int SponserDetailsId, int ProtocalDetailsId)
        {
            objDatabaseHelper = new DatabaseHelper();
            objDCUsers = new DCUsers();
            lstDCUsers = new List<DCUsers>();
            try
            {
                objDatabaseHelper.AddParameter("pClientDetailsId", ClientDetailsId == 0 ? DBNull.Value : (object)ClientDetailsId);
                objDatabaseHelper.AddParameter("pSponserDetailsId", SponserDetailsId == 0 ? DBNull.Value : (object)SponserDetailsId);
                objDatabaseHelper.AddParameter("pProtocalDetailsId", ProtocalDetailsId == 0 ? DBNull.Value : (object)ProtocalDetailsId);               
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETVERSIONSBYPROTOCOLID, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCUsers = new DCUsers();
                        objDCUsers.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        objDCUsers.VersionName = reader.IsDBNull(reader.GetOrdinal("VersionName")) ? string.Empty : reader.GetString(reader.GetOrdinal("VersionName"));
                        objDCUsers.AddAssesment = reader.IsDBNull(reader.GetOrdinal("AddAssesment")) ? string.Empty : reader.GetString(reader.GetOrdinal("AddAssesment"));
                        objDCUsers.AssessmentType = reader.IsDBNull(reader.GetOrdinal("AssessmentType")) ? string.Empty : reader.GetString(reader.GetOrdinal("AssessmentType"));
                        objDCUsers.VersionNo = reader.IsDBNull(reader.GetOrdinal("VersionNo")) ? 0 : reader.GetInt32(reader.GetOrdinal("VersionNo"));
                        objDCUsers.AssessmentTypeAbbr = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeAbbr")) ? string.Empty : reader.GetString(reader.GetOrdinal("AssessmentTypeAbbr"));
                        objDCUsers.AssessmentTypeId = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? 0 :reader.GetInt32(reader.GetOrdinal("AssessmentTypeId"));
                        objDCUsers.ClientDetailsId = reader.IsDBNull(reader.GetOrdinal("ClientDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientDetailsId"));
                        objDCUsers.SponserDetailsId = reader.IsDBNull(reader.GetOrdinal("SponserDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SponserDetailsId"));
                        objDCUsers.ProtocalDetailsId = reader.IsDBNull(reader.GetOrdinal("ProtocalDetailsId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ProtocalDetailsId"));
                        lstDCUsers.Add(objDCUsers);                    
                    }
                }
                return lstDCUsers;
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

    }
}
