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
    public class BLAssessment : BLBaseClass
    {

        DatabaseHelper objDatabaseHelper = null;
        List<DCAssessment> lstDCAssessment = null;

        #region Get Admin Assessment Type
        /// <summary>
        /// This method is used to Get SDV Details
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="RiskScoreId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        /// 
        public List<DCAssessment> GetAdminAssessmentTypes()
        {
            lstDCAssessment = new List<DCAssessment>();
            objDatabaseHelper = new DatabaseHelper();

            try
            {
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETADMINASSESSMENTTYPES, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DCAssessment objDCAssessment = new DCAssessment();
                        objDCAssessment.AssessmentTpyeId = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AssessmentTypeId"));
                        objDCAssessment.AssessmentType = reader.IsDBNull(reader.GetOrdinal("AssessmentType")) ? string.Empty : reader.GetString(reader.GetOrdinal("AssessmentType"));
                        //objDCAssessment.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? string.Empty : reader.GetString(reader.GetOrdinal("Status"));
                        lstDCAssessment.Add(objDCAssessment);
                    }
                }
                return lstDCAssessment;
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

        #region Get User Assessment Type
        /// <summary>
        /// This method is used to Get SDV Details
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="RiskScoreId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        /// 
        public List<DCAssessment> GetUserAssessmentTypes(int UserId)
        {
            lstDCAssessment = new List<DCAssessment>();
            objDatabaseHelper = new DatabaseHelper();

            try
            {
                objDatabaseHelper.AddParameter("pUserId", UserId == 0 ? DBNull.Value : (object)UserId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BLDBRoutines.SP_GETUSERASSESSMENTTYPES, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DCAssessment objDCAssessment = new DCAssessment();
                        objDCAssessment.AssessmentTpyeId = reader.IsDBNull(reader.GetOrdinal("AssessmentTypeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AssessmentTypeId"));
                        objDCAssessment.AssessmentType = reader.IsDBNull(reader.GetOrdinal("AssessmentType")) ? string.Empty : reader.GetString(reader.GetOrdinal("AssessmentType"));
                        lstDCAssessment.Add(objDCAssessment);
                    }
                }
                return lstDCAssessment;
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

    }
}
