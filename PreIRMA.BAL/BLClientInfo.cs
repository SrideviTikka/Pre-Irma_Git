using PreIRMA.DAL;
using PreIRMA.DataContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreIRMA.BAL
{
    public class BLClientInfo : BLBaseClass
    {
        DCClientInfo objDCClientInfo = null;
        DataOperationResponse objDataOperationResponce = null;
        DatabaseHelper objDatabaseHelper = null;
        List<DCClientInfo> lstDCClientInfo = null;

        #region Add Update Client Info
        /// <summary>
        /// This method is used to  Add Update Client Info
        /// </summary>
        /// <param name="objDCClientInfo"></param>
        /// <returns></returns>
        public List<DCClientInfo> AddUpdateClientInfo(DCClientInfo objDCClientInfo)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponce = new DataOperationResponse();
                lstDCClientInfo = new List<DCClientInfo>();

                objDatabaseHelper.AddParameter("pClientId", objDCClientInfo.ClientId == 0 ? DBNull.Value : (object)objDCClientInfo.ClientId);
                objDatabaseHelper.AddParameter("pFirstName", objDCClientInfo.FirstName == string.Empty ? DBNull.Value : (object)objDCClientInfo.FirstName);
                objDatabaseHelper.AddParameter("pLastName", objDCClientInfo.LastName == string.Empty ? DBNull.Value : (object)objDCClientInfo.LastName);
                objDatabaseHelper.AddParameter("pContactTitle", objDCClientInfo.ContactTitle == string.Empty ? DBNull.Value : (object)objDCClientInfo.ContactTitle);
                objDatabaseHelper.AddParameter("pEmail", objDCClientInfo.EmailAddress == string.Empty ? DBNull.Value : (object)objDCClientInfo.EmailAddress);
                objDatabaseHelper.AddParameter("pCompanyName", objDCClientInfo.CompanyName == string.Empty ? DBNull.Value : (object)objDCClientInfo.CompanyName);
                objDatabaseHelper.AddParameter("pAddressline1", objDCClientInfo.Addressline1 == string.Empty ? DBNull.Value : (object)objDCClientInfo.Addressline1);
                objDatabaseHelper.AddParameter("pAddressline2", objDCClientInfo.Addressline2 == string.Empty ? DBNull.Value : (object)objDCClientInfo.Addressline2);
                objDatabaseHelper.AddParameter("pAddressline3", objDCClientInfo.Addressline3 == string.Empty ? DBNull.Value : (object)objDCClientInfo.Addressline3);
                objDatabaseHelper.AddParameter("pCityOrProvince", objDCClientInfo.CityProvince == string.Empty ? DBNull.Value : (Object)objDCClientInfo.CityProvince);
                objDatabaseHelper.AddParameter("pCountry", objDCClientInfo.Country == string.Empty ? DBNull.Value : (object)objDCClientInfo.Country);
                objDatabaseHelper.AddParameter("pState", objDCClientInfo.State == string.Empty ? DBNull.Value : (object)objDCClientInfo.State);
                objDatabaseHelper.AddParameter("pZipCode", objDCClientInfo.ZipCode == string.Empty ? DBNull.Value : (object)objDCClientInfo.ZipCode);
                objDatabaseHelper.AddParameter("pOfficePhone", objDCClientInfo.OfficePhone == string.Empty ? DBNull.Value : (object)objDCClientInfo.OfficePhone);
                objDatabaseHelper.AddParameter("pMobilePhone", objDCClientInfo.MobilePhone == string.Empty ? DBNull.Value : (object)objDCClientInfo.MobilePhone);
                objDatabaseHelper.AddParameter("pUserID", objDCClientInfo.UserId == 0 ? DBNull.Value : (object)objDCClientInfo.UserId);

                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_ADDUPDATECLIENTINFO, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCClientInfo = new DCClientInfo();
                        objDCClientInfo.ClientId = reader.IsDBNull(reader.GetOrdinal("ClientId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientId"));
                        objDCClientInfo.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        objDCClientInfo.ContactTitle = reader.IsDBNull(reader.GetOrdinal("ContactTitle")) ? string.Empty : reader.GetString(reader.GetOrdinal("ContactTitle"));
                        objDCClientInfo.FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                        objDCClientInfo.LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                        objDCClientInfo.EmailAddress = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email"));
                        objDCClientInfo.CompanyName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader.GetString(reader.GetOrdinal("CompanyName"));
                        objDCClientInfo.Addressline1 = reader.IsDBNull(reader.GetOrdinal("Addressline1")) ? string.Empty : reader.GetString(reader.GetOrdinal("Addressline1"));
                        objDCClientInfo.Addressline2 = reader.IsDBNull(reader.GetOrdinal("Addressline2")) ? string.Empty : reader.GetString(reader.GetOrdinal("Addressline2"));
                        objDCClientInfo.Addressline3 = reader.IsDBNull(reader.GetOrdinal("Addressline3")) ? string.Empty : reader.GetString(reader.GetOrdinal("Addressline3"));
                        objDCClientInfo.CityProvince = reader.IsDBNull(reader.GetOrdinal("CityOrProvince")) ? string.Empty : reader.GetString(reader.GetOrdinal("CityOrProvince"));
                        objDCClientInfo.State = reader.IsDBNull(reader.GetOrdinal("State")) ? string.Empty : reader.GetString(reader.GetOrdinal("State"));
                        objDCClientInfo.ZipCode = reader.IsDBNull(reader.GetOrdinal("ZipCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("ZipCode"));
                        objDCClientInfo.OfficePhone = reader.IsDBNull(reader.GetOrdinal("OfficePhone")) ? string.Empty : reader.GetString(reader.GetOrdinal("OfficePhone"));
                        objDCClientInfo.MobilePhone = reader.IsDBNull(reader.GetOrdinal("MobilePhone")) ? string.Empty : reader.GetString(reader.GetOrdinal("MobilePhone"));
                        objDCClientInfo.Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? string.Empty : reader.GetString(reader.GetOrdinal("Country"));
                        lstDCClientInfo.Add(objDCClientInfo);
                    }
                }
                return lstDCClientInfo;
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

        //SP_GETCLIENTINFO
        public List<DCClientInfo> GetClientInfo(int intEmpId)
        {

            objDatabaseHelper = new DatabaseHelper();
            objDCClientInfo = new DCClientInfo();
            lstDCClientInfo = new List<DCClientInfo>();
            try
            {
                objDatabaseHelper.AddParameter("pUserId", intEmpId == 0 ? DBNull.Value : (object)intEmpId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETCLIENTINFO, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCClientInfo = new DCClientInfo();
                        objDCClientInfo.ClientId = reader.IsDBNull(reader.GetOrdinal("ClientId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientId"));
                        objDCClientInfo.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        objDCClientInfo.ContactTitle = reader.IsDBNull(reader.GetOrdinal("ContactTitle")) ? string.Empty : reader.GetString(reader.GetOrdinal("ContactTitle"));
                        objDCClientInfo.FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                        objDCClientInfo.LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                        objDCClientInfo.EmailAddress = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email"));
                        objDCClientInfo.CompanyName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader.GetString(reader.GetOrdinal("CompanyName"));
                        objDCClientInfo.Addressline1 = reader.IsDBNull(reader.GetOrdinal("Addressline1")) ? string.Empty : reader.GetString(reader.GetOrdinal("Addressline1"));
                        objDCClientInfo.Addressline2 = reader.IsDBNull(reader.GetOrdinal("Addressline2")) ? string.Empty : reader.GetString(reader.GetOrdinal("Addressline2"));
                        objDCClientInfo.Addressline3 = reader.IsDBNull(reader.GetOrdinal("Addressline3")) ? string.Empty : reader.GetString(reader.GetOrdinal("Addressline3"));
                        objDCClientInfo.CityProvince = reader.IsDBNull(reader.GetOrdinal("CityOrProvince")) ? string.Empty : reader.GetString(reader.GetOrdinal("CityOrProvince"));
                        objDCClientInfo.State = reader.IsDBNull(reader.GetOrdinal("State")) ? string.Empty : reader.GetString(reader.GetOrdinal("State"));
                        objDCClientInfo.ZipCode = reader.IsDBNull(reader.GetOrdinal("ZipCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("ZipCode"));
                        objDCClientInfo.OfficePhone = reader.IsDBNull(reader.GetOrdinal("OfficePhone")) ? string.Empty : reader.GetString(reader.GetOrdinal("OfficePhone"));
                        objDCClientInfo.MobilePhone = reader.IsDBNull(reader.GetOrdinal("MobilePhone")) ? string.Empty : reader.GetString(reader.GetOrdinal("MobilePhone"));
                        objDCClientInfo.Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? string.Empty : reader.GetString(reader.GetOrdinal("Country"));
                        objDCClientInfo.ClientUploadId = reader.IsDBNull(reader.GetOrdinal("ClientUploadId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientUploadId"));
                        objDCClientInfo.UploadLogo = reader.IsDBNull(reader.GetOrdinal("UploadedFile")) ? string.Empty : reader.GetString(reader.GetOrdinal("UploadedFile"));
                        lstDCClientInfo.Add(objDCClientInfo);
                    }
                }
                return lstDCClientInfo;
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

        public DataOperationResponse UploadLogo(DCClientInfo objdCClientInfo)
        {          
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponce = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pUserId", objdCClientInfo.UserId == 0 ? DBNull.Value : (object)objdCClientInfo.UserId);
                objDatabaseHelper.AddParameter("pDescription", string.IsNullOrEmpty(objdCClientInfo.Description) ? DBNull.Value : (object)objdCClientInfo.Description);
                objDatabaseHelper.AddParameter("pFileType", string.IsNullOrEmpty(objdCClientInfo.FileType) ? DBNull.Value : (object)objdCClientInfo.FileType);
                objDatabaseHelper.AddParameter("pUploadedFile", objdCClientInfo.UploadLogo == null ? DBNull.Value : (object)objdCClientInfo.UploadLogo);
                objDatabaseHelper.AddParameter("pClientUploadId", objdCClientInfo.ClientUploadId == 0 ? DBNull.Value : (object)objdCClientInfo.ClientUploadId);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_INSERTCLIENTUPLOADS, CommandType.StoredProcedure);
                if (result > 0)
                {
                    objDataOperationResponce.Code = GetSuccessCode;
                    objDataOperationResponce.Message = objdCClientInfo.ClientUploadId == 0 ? "File Uploaded Successfully" : "Description Updated Successfully";
                }
                else
                {
                    objDataOperationResponce.Code = GetErrorCode;
                    objDataOperationResponce.Message = "Unable to Upload File";
                }
                return objDataOperationResponce;
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

        public DataOperationResponse DeleteUploadLogo(int ClientUploadId)
        {
            objDatabaseHelper = new DatabaseHelper();
            objDataOperationResponce = new DataOperationResponse();
            objDatabaseHelper.AddParameter("pClientUploadId",ClientUploadId==0?DBNull.Value:(object)ClientUploadId);
            int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETEUPLOADFILE, CommandType.StoredProcedure);
            if (result > 0)
            {
                objDataOperationResponce.Code = GetSuccessCode;
                objDataOperationResponce.Message = "File Deleted Successfully";
            }
            else
            {
                objDataOperationResponce.Code = GetErrorCode;
                objDataOperationResponce.Message = "Unable to Delete File";
            }
            return objDataOperationResponce;
        }

        public List<DCClientInfo> GetClientUploadedFile(int UserId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCClientInfo = new List<DCClientInfo>();
                objDatabaseHelper.AddParameter("pUserId", UserId == 0 ? DBNull.Value : (object)UserId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETCLIENTUPLOADEDFILE, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCClientInfo = new DCClientInfo();
                        objDCClientInfo.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"));
                        objDCClientInfo.UploadLogo = reader.IsDBNull(reader.GetOrdinal("UploadedFile")) ? string.Empty : reader.GetString(reader.GetOrdinal("UploadedFile"));
                        objDCClientInfo.ClientUploadId = reader.IsDBNull(reader.GetOrdinal("ClientUploadId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientUploadId"));
                        objDCClientInfo.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        lstDCClientInfo.Add(objDCClientInfo);
                    }
                }
                return lstDCClientInfo;
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
        public List<DCClientInfo> GetUploadFileByClientUploadId(int ClientUploadId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                lstDCClientInfo = new List<DCClientInfo>();
                objDatabaseHelper.AddParameter("pClientUploadId", ClientUploadId == 0 ? DBNull.Value : (object)ClientUploadId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETUPLOADFILEBYCLIENTUPLOADID, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDCClientInfo = new DCClientInfo();
                        objDCClientInfo.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"));
                        objDCClientInfo.UploadLogo = reader.IsDBNull(reader.GetOrdinal("UploadedFile")) ? string.Empty : reader.GetString(reader.GetOrdinal("UploadedFile"));
                        objDCClientInfo.ClientUploadId = reader.IsDBNull(reader.GetOrdinal("ClientUploadId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientUploadId"));
                        objDCClientInfo.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        lstDCClientInfo.Add(objDCClientInfo);
                    }
                }
                return lstDCClientInfo;
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
