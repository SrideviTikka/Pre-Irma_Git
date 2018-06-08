using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreIRMA.DataContract;
using PreIRMA.BAL;
using System.Text;

namespace PreIRMA.WebSite.Controllers
{
    public class CAPADetailsController : Controller
    {
        DataOperationResponse objDataOperationResponse = null;
        BLCAPADetails objBLCAPADetails = null;
        DCUsers objDCUsers = null;
        List<DCCAPADetails> lstDCCAPADetails = null;
        DCCAPADetails objDCCAPADetails = null;

        #region View Capa Details
        /// <summary>
        /// This method is used for View Capa Details
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewCapaDetails()
        {
            if (Session["UserLogon"] != null)
            {
                return View();
            }
            return Redirect("~/Account/Login");
        }
        #endregion

        #region Add Update CAPA Details
        /// <summary>
        /// This method is used for Add Update CAPA Details
        /// </summary>
        /// <param name="objDCCAPADetails"></param>
        /// <returns></returns>
        /// 
        public ActionResult AddUpdateCAPADetails(FormCollection frmColl, string mitigationData)
        {
            objDCUsers = (DCUsers)Session["UserLogon"];
            objDataOperationResponse = new DataOperationResponse();
            if (Session["UserLogon"] != null)
            {
                objDCCAPADetails = new DCCAPADetails();
                objDCCAPADetails.AssessmentTypeId = string.IsNullOrEmpty(frmColl["hdnAssessmentTypesId"]) ? 0 : Convert.ToInt32(frmColl["hdnAssessmentTypesId"]);
                objDCCAPADetails.RiskScoreId = string.IsNullOrEmpty(frmColl["hdnRiskScoreId"]) ? 0 : Convert.ToInt32(frmColl["hdnRiskScoreId"]);
                objDCCAPADetails.SectionId = string.IsNullOrEmpty(frmColl["hdnSectionId"]) ? 0 : Convert.ToInt32(frmColl["hdnSectionId"]);
                objDCCAPADetails.CAPADetailsId = string.IsNullOrEmpty(frmColl["hdnCAPADetailsId"]) ? 0 : Convert.ToInt32(frmColl["hdnCAPADetailsId"]);
                objDCCAPADetails.ConfirmationOfComplianceImprovement = frmColl["ddlConfirmationOfComplianceImprovement"];
                objDCCAPADetails.Impact = frmColl["ddlImpact"];

                StringBuilder objStrBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(frmColl["ddlMitigation"]))
                {
                    string[] strMitigationIds = frmColl["ddlMitigation"].Split(',');
                    objStrBuilder.Append("<MitigationIds>");
                    foreach (string MitigationId in strMitigationIds)
                    {
                        objStrBuilder.Append("<MitigationId>" + Convert.ToInt32(MitigationId) + "</MitigationId>");
                    }
                    objStrBuilder.Append("</MitigationIds>");
                }
                objDCCAPADetails.Mitigation = Convert.ToString(objStrBuilder);
                objDCCAPADetails.RiskCriteria = frmColl["ddlRiskCriteria"];
                objDCCAPADetails.RiskCriteriaDescription = frmColl["ddlRiskCriteriaDescription"];
                objBLCAPADetails = new BLCAPADetails();
                objDCCAPADetails.CreatedBy = objDCUsers.UserId;
                objDataOperationResponse = objBLCAPADetails.AddUpdateCAPADetails(objDCCAPADetails);
                if (objDataOperationResponse.Code > 0)
                {
                    TempData["successMessage"] = objDataOperationResponse.Message;
                    return Redirect("~/CAPADetails/ViewCapaDetails");
                }
                else
                {
                    TempData["errorMessage"] = objDataOperationResponse.Message;
                    return View("ViewCapaDetails");
                }
            }
            else
            {
                return Redirect("~/Account/Login");
            }
        }
        #endregion

        #region Get CAPA Details
        /// <summary>
        /// This method is used for Get CAPA Details
        /// </summary>
        /// <param name="strSectionId"></param>
        /// <param name="strRiskScoreId"></param>
        /// <returns></returns>
        public JsonResult GetCAPADetails(string strSectionId, string strRiskScoreId, string strAssessmentType)
        {
            lstDCCAPADetails = new List<DCCAPADetails>();
            objBLCAPADetails = new BLCAPADetails();
            objDCUsers = (DCUsers)Session["UserLogon"];
            lstDCCAPADetails = objBLCAPADetails.GetCAPADetails(Convert.ToInt32(strSectionId), Convert.ToInt32(strRiskScoreId), objDCUsers.UserId, Convert.ToInt32(strAssessmentType));
            return Json(lstDCCAPADetails, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete CAPADetails
        /// <summary>
        /// This method is used for Delete Sections 
        /// </summary>
        /// <param name="SectionID"></param>
        /// <returns></returns>
        public string DeleteCAPADetails(string CAPADetailsId)
        {
            objBLCAPADetails = new BLCAPADetails();
            objDataOperationResponse = objBLCAPADetails.DeleteCAPADetails(Convert.ToInt32(CAPADetailsId));
            return objDataOperationResponse.Message;
        }
        #endregion

        #region Get CAPA Details By CAPA Id
        /// <summary>
        /// This method is used to Get CAPA Details By CAPA Id
        /// </summary>
        /// <param name="CAPADetailsId"></param>
        /// <returns></returns>
        public JsonResult GetCAPADetailsByCAPAId(string CAPADetailsId)
        {
            lstDCCAPADetails = new List<DCCAPADetails>();
            objBLCAPADetails = new BLCAPADetails();

            lstDCCAPADetails = objBLCAPADetails.GetCAPADetailsByCAPAId(Convert.ToInt32(CAPADetailsId));
            return Json(lstDCCAPADetails, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}






