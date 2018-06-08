using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreIRMA.BAL;
using PreIRMA.DataContract;
using System.Text;

namespace PreIRMA.WebSite.Controllers
{
    public class SDVDetailsController : Controller
    {
        DCUsers objDCUsers = null;
        DataOperationResponse objDataOperationResponse = null;
        BLSDVDetails objBLSDVDetails = null;
        List<DCSDVDetails> lstDCSDVDetails = null;
        DCSDVDetails objDCSDVDetails = null;
        List<DCSections> lstDCSections = null;
        List<DCQuestions> lstDCQuestions = null;

        #region View SDV Details
        /// <summary>
        /// This method is used for View SDV Details 
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewSDVDetails()
        {
            if (Session["UserLogon"] != null)
            {
                return View();
            }
            return Redirect("~/Account/Login");
        }
        #endregion

        #region Add Update SDV Details
        /// <summary>
        /// This method is used for Add Update SDV Details
        /// </summary>
        /// <param name="objDCSDVDetails"></param>
        /// <param name="frmColl"></param>
        /// <returns></returns>
        public ActionResult AddUpdateSDVDetails(FormCollection frmColl)
        {
            objDCUsers = (DCUsers)Session["UserLogon"];
            objDataOperationResponse = new DataOperationResponse();
            objDCSDVDetails = new DCSDVDetails();

            if (Session["UserLogon"] != null)
            {
                objBLSDVDetails = new BLSDVDetails();

                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("<SDVDetails>");


                //if (!string.IsNullOrEmpty(frmColl["hdnRiskScoreId"]))
                //{
                strBuilder.Append("<RiskLevels>");
                if (frmColl["hdnRisk_0_ScoreId"] == "0")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_0_ScoreId"] + "</RiskId>");
                    //strBuilder.Append("<WAC>" + frmColl["txtWAC"] + "</WAC>");
                    //strBuilder.Append("<DOCReview>" + frmColl["txtDocReview"] + "</DOCReview>");
                    //strBuilder.Append("<MAT>" + frmColl["ddlRisk0MAT"] + "</MAT>");
                    //strBuilder.Append("<SDV>" + frmColl["ddlRisk0SDRSDV"] + "</SDV>");
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_0_Id"] + "</SDVDetailsId>");

                    string[] questionIds = frmColl["hdn_risk0_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }


                if (frmColl["hdnRisk_1_ScoreId"] == "1")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_1_ScoreId"] + "</RiskId>");
                    //strBuilder.Append("<WAC>" + frmColl["txtWAC"] + "</WAC>");
                    //strBuilder.Append("<DOCReview>" + frmColl["txtDocReview"] + "</DOCReview>");
                    //strBuilder.Append("<MAT>" + frmColl["ddlRisk1MAT"] + "</MAT>");
                    //strBuilder.Append("<SDV>" + frmColl["ddlRisk1SDRSDV"] + "</SDV>");
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_1_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk1_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }

                if (frmColl["hdnRisk_2_ScoreId"] == "2")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_2_ScoreId"] + "</RiskId>");
                    //strBuilder.Append("<WAC>" + frmColl["txtWAC"] + "</WAC>");
                    //strBuilder.Append("<DOCReview>" + frmColl["txtDocReview"] + "</DOCReview>");
                    //strBuilder.Append("<MAT>" + frmColl["ddlRisk2MAT"] + "</MAT>");
                    //strBuilder.Append("<SDV>" + frmColl["ddlRisk2SDRSDV"] + "</SDV>");
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_2_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk2_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }

                if (frmColl["hdnRisk_3_ScoreId"] == "3")
                {
                    strBuilder.Append("<Risk>");

                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_3_ScoreId"] + "</RiskId>");
                    //strBuilder.Append("<WAC>" + frmColl["txtWAC"] + "</WAC>");
                    //strBuilder.Append("<DOCReview>" + frmColl["txtDocReview"] + "</DOCReview>");
                    //strBuilder.Append("<MAT>" + frmColl["ddlRisk3MAT"] + "</MAT>");
                    //strBuilder.Append("<SDV>" + frmColl["ddlRisk3SDRSDV"] + "</SDV>");
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_3_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk3_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }

                if (frmColl["hdnRisk_4_ScoreId"] == "4")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_4_ScoreId"] + "</RiskId>");
                    //strBuilder.Append("<WAC>" + frmColl["txtWAC"] + "</WAC>");
                    //strBuilder.Append("<DOCReview>" + frmColl["txtDocReview"] + "</DOCReview>");
                    //strBuilder.Append("<MAT>" + frmColl["ddlRisk4MAT"] + "</MAT>");
                    //strBuilder.Append("<SDV>" + frmColl["ddlRisk4SDRSDV"] + "</SDV>");
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_4_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk4_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }

                if (frmColl["hdnRisk_5_ScoreId"] == "5")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_5_ScoreId"] + "</RiskId>");
                    //strBuilder.Append("<WAC>" + frmColl["txtWAC"] + "</WAC>");
                    //strBuilder.Append("<DOCReview>" + frmColl["txtDocReview"] + "</DOCReview>");
                    //strBuilder.Append("<MAT>" + frmColl["ddlRisk5MAT"] + "</MAT>");
                    //strBuilder.Append("<SDV>" + frmColl["ddlRisk5SDRSDV"] + "</SDV>");
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_5_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk5_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }
                //22/03/2018 start
                //First visit
                if (frmColl["hdnRisk_6_ScoreId"] == "First Visit")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_6_ScoreId"] + "</RiskId>");                    
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_6_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk6_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }

                //Last Visit
                if (frmColl["hdnRisk_7_ScoreId"] == "Last Visit")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_7_ScoreId"] + "</RiskId>"); 
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_7_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk7_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }

                //QUA
                if (frmColl["hdnRisk_8_ScoreId"] == "QUA")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_8_ScoreId"] + "</RiskId>");                    
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_8_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk8_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }

                //QOV
                if (frmColl["hdnRisk_9_ScoreId"] == "QOV")
                {
                    strBuilder.Append("<Risk>");
                    strBuilder.Append("<SectionId>" + Convert.ToInt32(frmColl["hdnSectionId"]) + "</SectionId>");
                    strBuilder.Append("<AssessmentTypeId>" + frmColl["ddlAssessmentType"] + "</AssessmentTypeId>");
                    strBuilder.Append("<RiskId>" + frmColl["hdnRisk_9_ScoreId"] + "</RiskId>");
                    strBuilder.Append("<SDVDetailsId>" + frmColl["hdnSDVDetails_9_Id"] + "</SDVDetailsId>");
                    string[] questionIds = frmColl["hdn_risk9_QustIds[]"].Split(',');
                    strBuilder.Append("<Questions>");
                    foreach (var questionId in questionIds)
                    {
                        strBuilder.Append("<QuestionId>" + questionId + "</QuestionId>");
                    }
                    strBuilder.Append("</Questions>");
                    strBuilder.Append("</Risk>");
                }
                //22/03/2018 end
                strBuilder.Append("</RiskLevels>");
                //}
                strBuilder.Append("</SDVDetails>");

                string XMLData = strBuilder.ToString();

                objDataOperationResponse = objBLSDVDetails.AddUpdateSDVDetails(XMLData, objDCUsers.UserId);
                if (objDataOperationResponse.Code > 0)
                {
                    TempData["successMessage"] = objDataOperationResponse.Message;
                    //return View("ViewSDVDetails");
                    return Redirect("~/SDVDetails/ViewSDVDetails");
                    
                }
                else
                {
                    TempData["errorMessage"] = objDataOperationResponse.Message;
                    //return View("ViewSDVDetails");
                    return Redirect("~/SDVDetails/ViewSDVDetails");
                }

            }
            return Redirect("~/SDVDetails/ViewSDVDetails");
        }
        #endregion

        #region Get SDV Details
        /// <summary>
        /// This method is used for Get SDV Details 
        /// </summary>
        /// <param name="strSectionId"></param>
        /// <param name="strRiskScoreId"></param>
        /// <returns></returns>
        public JsonResult GetSDVDetails(string strSectionId, string strRefKey)
        {
            lstDCSDVDetails = new List<DCSDVDetails>();
            objBLSDVDetails = new BLSDVDetails();
            objDCUsers = (DCUsers)Session["UserLogon"];
            lstDCSDVDetails = objBLSDVDetails.GetSDVDetails(Convert.ToInt32(strSectionId), objDCUsers.UserId, Convert.ToInt32(strRefKey));
            return Json(lstDCSDVDetails, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete SDV
        /// <summary>
        /// This method is used for Delete Sections 
        /// </summary>
        /// <param name="RefKey"></param>
        /// <returns></returns>
        public string DeleteSDVDetails(string RefKey)
        {
            objBLSDVDetails = new BLSDVDetails();
            objDCUsers = (DCUsers)Session["UserLogon"];
            objDataOperationResponse = objBLSDVDetails.DeleteSDVDetails(Convert.ToInt32(RefKey), objDCUsers.UserId);
            return objDataOperationResponse.Message;
        }
        #endregion

        #region Get SDV Details By SDVDetails Id
        /// <summary>
        /// This method is used for Get CAPA Details By CAPA Id
        /// </summary>
        /// <param name="SDVADetailsId"></param>
        /// <returns></returns>
        public JsonResult GetSDVQuestionsBySDVDetailsId(string RiskScoreId, int RefKey)
        {
            lstDCQuestions = new List<DCQuestions>();
            objBLSDVDetails = new BLSDVDetails();
            lstDCQuestions = objBLSDVDetails.GetSDVQuestionsBySDVDetailsId(RiskScoreId, RefKey);
            return Json(lstDCQuestions, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get SDV Sections
        /// <summary>
        /// This function is Used to Get SDV_Sections Details
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSDV_Sections(string strAssessmentTypeId, string strSectionId)
        {
            objBLSDVDetails = new BLSDVDetails();
            lstDCSections = new List<DCSections>();
            objDCUsers = (DCUsers)Session["UserLogon"];
            lstDCSections = objBLSDVDetails.GetSDV_Sections(objDCUsers.UserId, Convert.ToInt32(strAssessmentTypeId), Convert.ToInt32(strSectionId));
            return Json(lstDCSections, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
