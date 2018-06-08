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
    public class BLSections : BLBaseClass
    {
        DCSections objDCSections = null;
        DatabaseHelper objDatabaseHelper = null;
        List<DCSections> lstDCSections = null;
        DataOperationResponse objDataOperationResponse = null;

        #region Get Sections
        /// <summary>
        /// This method is used for Get Sections
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="intUserId"></param>
        /// <returns></returns>
        public List<DCSections> GetSections(int AssessmentTypeId, int intUserId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDCSections = new List<DCSections>();
            objDatabaseHelper.AddParameter("pUserId", intUserId == 0 ? DBNull.Value : (object)intUserId);
            objDatabaseHelper.AddParameter("pAssessmentTypeId", AssessmentTypeId == 0 ? DBNull.Value : (object)AssessmentTypeId);
            DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETSECTIONSBYASSESSMENTTYPEID, CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    objDCSections = new DCSections();
                    objDCSections.SectionId = reader.IsDBNull(reader.GetOrdinal("SectionId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SectionId"));
                    objDCSections.SectionName = reader.IsDBNull(reader.GetOrdinal("SectionName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SectionName"));
                    objDCSections.SectionNumber = reader.IsDBNull(reader.GetOrdinal("SectionNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("SectionNumber"));
                    objDCSections.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"));
                    objDCSections.AssessmentTypeId = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AssessmentTypeId"));
                    objDCSections.Critical = reader.IsDBNull(reader.GetOrdinal("Critical")) ? string.Empty : reader.GetString(reader.GetOrdinal("Critical"));
                    lstDCSections.Add(objDCSections);
                }
                objDCSections.Code = GetSuccessCode;
            }
            return lstDCSections;
        }
        #endregion

        #region Add Update Sections
        /// <summary>
        /// Author : Satish
        /// Description : Add Update Sections this method used to add or update the Sections 
        /// Date : 12/10/2016
        /// </summary>
        /// <returns></returns>
        public DataOperationResponse AddUpdateSections(DCSections objDCSections)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pSectionId", objDCSections.SectionId == 0 ? DBNull.Value : (object)objDCSections.SectionId);
                objDatabaseHelper.AddParameter("pSectionName", objDCSections.SectionName == string.Empty ? DBNull.Value : (object)objDCSections.SectionName);
                objDatabaseHelper.AddParameter("pUserId", objDCSections.CreatedBy == 0 ? DBNull.Value : (object)objDCSections.CreatedBy);
                objDatabaseHelper.AddParameter("pSectionNumber", objDCSections.SectionNumber == string.Empty ? DBNull.Value : (object)objDCSections.SectionNumber);
                objDatabaseHelper.AddParameter("pAssessmentTypeId", objDCSections.AssessmentTypeId == 0 ? DBNull.Value : (object)objDCSections.AssessmentTypeId);
                objDatabaseHelper.AddParameter("pDescription", objDCSections.Description == string.Empty ? DBNull.Value : (object)objDCSections.Description);
                objDatabaseHelper.AddParameter("pCritical", objDCSections.Critical == string.Empty ? DBNull.Value : (object)objDCSections.Critical);
                objDatabaseHelper.AddParameter("oRegisterMessage", string.Empty, ParameterDirection.Output);
                int result = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_ADDUPDATESECTIONS, CommandType.StoredProcedure);
                string strMessage = Convert.ToString(objDatabaseHelper.Command.Parameters["oRegisterMessage"].Value);
                if (result > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    if (strMessage == "Maximum critical Count is Six")
                    {
                        objDataOperationResponse.Message = objDCSections.SectionId == 0 ? "Section Added Successfully,Unable to Mark as Critical" : "Section Updated Successfully,Unable to Mark as Critical";
                    }
                    else
                    {
                        objDataOperationResponse.Message = objDCSections.SectionId == 0 ? "Section Added Successfully" : "Section Updated Successfully";
                    }
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

        #region DeleteSection

        public DataOperationResponse DeleteSection(int intSectionId)
        {
            try
            {
                objDataOperationResponse = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pSectionid", intSectionId == 0 ? DBNull.Value : (object)intSectionId);
                int intResult = objDatabaseHelper.ExecuteNonQuery(BLDBRoutines.SP_DELETESECTIONS, CommandType.StoredProcedure);
                if (intResult > 0)
                {
                    objDataOperationResponse.Code = GetSuccessCode;
                    objDataOperationResponse.Message = "Section Deleted Successfully";
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

        #region Get Section Name By SectionId
        /// <summary>
        /// This method is used for Get Section Name By SectionId
        /// </summary>
        /// <param name="intSectionId"></param>
        /// <param name="intUserId"></param>
        /// <returns></returns>
        public List<DCSections> GetSectionNameBySectionId(int intSectionId, int intUserId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDCSections = new List<DCSections>();
            objDatabaseHelper.AddParameter("pSectionId", intSectionId == 0 ? DBNull.Value : (object)intSectionId);
            objDatabaseHelper.AddParameter("pUserId", intUserId == 0 ? DBNull.Value : (object)intUserId);
            DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETSECTIONDETAILSBYSECTIONID, CommandType.StoredProcedure);
            string SectionName = string.Empty;
            string SectionNumber = string.Empty;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    objDCSections = new DCSections();
                    lstDCSections = new List<DCSections>();
                    objDCSections.SectionName = reader.IsDBNull(reader.GetOrdinal("SectionName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SectionName"));
                    objDCSections.SectionNumber = reader.IsDBNull(reader.GetOrdinal("SectionNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("SectionNumber"));
                    objDCSections.AssessmentTypeId = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AssessmentTypeId"));
                    objDCSections.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"));
                    objDCSections.Critical = reader.IsDBNull(reader.GetOrdinal("Critical")) ? string.Empty : reader.GetString(reader.GetOrdinal("Critical"));
                    lstDCSections.Add(objDCSections);
                }

            }
            return lstDCSections;
        }
        #endregion

        public List<DCSections> SectionSortingBySortkey(int SectionId, string Arrow, int AssessmentTypeId, int UserId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDCSections = new List<DCSections>();
            objDCSections = new DCSections();
            objDatabaseHelper.AddParameter("pSectionId", SectionId == 0 ? DBNull.Value : (object)SectionId);
            objDatabaseHelper.AddParameter("pUserId", UserId == 0 ? DBNull.Value : (object)UserId);
            objDatabaseHelper.AddParameter("pArrow", Arrow == string.Empty ? DBNull.Value : (object)Arrow);
            objDatabaseHelper.AddParameter("pAssessmentTypeId", AssessmentTypeId == 0 ? DBNull.Value : (object)AssessmentTypeId);
            DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_SECTIONSORTINGBYSORTKEY, CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                objDCSections.Code = GetSuccessCode;
            }
            else
            {
                objDCSections.Code = GetErrorCode;
            }
            lstDCSections.Add(objDCSections);
            return lstDCSections; 
        }
    }
}
