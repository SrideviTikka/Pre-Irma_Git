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
    public class QuestionsController : Controller
    {
        List<DCQuestions> lstDCQuestions = null;
        BLQuestions objBLQuestions = null;
        DataOperationResponse objDataOperationResponse = null;
        DCSections objDCSections = null;

        #region View Questions
        /// <summary>
        /// This is for View Questions
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewQuestions()
        {
            if (Session["Userlogon"] != null)
                return View();
            else
                return Redirect("~/Account/Login");
        }
        #endregion

        #region Add Update Question
        /// <summary>
        /// This method is used for Add Update Question
        /// </summary>
        /// <param name="frmColl"></param>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        public string AddUpdateQuestion(DCQuestions objDCQuestions, string UserId)
        {
            if (Session["UserLogon"] != null)
            {
                objBLQuestions = new BLQuestions();
                if (objDCQuestions.QuestionId == 0)
                    objDCQuestions.QuestionId = 0;
                else
                    objDCQuestions.QuestionId = Convert.ToInt32(objDCQuestions.QuestionId);
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                objDCQuestions.CreatedBy = objDCUsers.UserId;
                //string ActionItems = objBLQuestions.ConvertStringArrayToStringJoin(objDCQuestions.ActionItems);

                StringBuilder objStrBuilder = new StringBuilder();
                //if (!string.IsNullOrEmpty(objDCQuestions.ActionItems))
                
                    //string[] separatingChars = { "$$" };
                    //string[] strQueActionItems = objDCQuestions.ActionItems.Split(separatingChars, System.StringSplitOptions.None);
                    List<string> strQueActionItems = objDCQuestions.ActionItems;
                    objStrBuilder.Append("<QueActionItems>");
                    foreach (string actionItems in strQueActionItems)
                    {
                        objStrBuilder.Append("<ActionItem>" + actionItems + "</ActionItem>");
                    }
                    objStrBuilder.Append("</QueActionItems>");
                
                string XMLData = objStrBuilder.ToString();

                StringBuilder objStrMitigation = new StringBuilder();
                List<string> strQueMitigation = objDCQuestions.Mitigation;
                objStrMitigation.Append("<QueMitigations>");
                foreach (string mitigation in strQueMitigation)
                {
                    objStrMitigation.Append("<Mitigation>" + mitigation + "</Mitigation>");
                }
                objStrMitigation.Append("</QueMitigations>");

                string XMLQueMitigation = objStrMitigation.ToString();

                objDataOperationResponse = objBLQuestions.AddUpdateQuestions(objDCQuestions, XMLData, XMLQueMitigation);
                if (objDataOperationResponse.Code > 0)
                {
                    return objDataOperationResponse.Message;
                }
            }
            return objDataOperationResponse.Message;
        }
        #endregion

        #region Get Questions
        /// <summary>
        ///This method is used for Get Questions 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetQuestions(string strQuestionID)
        {
            lstDCQuestions = new List<DCQuestions>();
            objBLQuestions = new BLQuestions();
            objDCSections = new DCSections();
            if (Session["UserLogon"] != null)
            {
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                lstDCQuestions = objBLQuestions.GetQuestions(Convert.ToInt32(strQuestionID));
            }
            var result = Json(lstDCQuestions, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        public JsonResult GetActionItemsbyQuestionId(string strQuestionID)
        {
            List<string> lstString = new List<string>();
            objBLQuestions = new BLQuestions();
            objDCSections = new DCSections();
            if (Session["UserLogon"] != null)
            {
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                lstString = objBLQuestions.GetActionItemsbyQuestionId(Convert.ToInt32(strQuestionID));
            }
            var result = Json(lstString, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        public JsonResult GetMitigationbyQuestionId(string strQuestionID)
        {
            List<string> lstString = new List<string>();
            objBLQuestions = new BLQuestions();
            objDCSections = new DCSections();
            if (Session["UserLogon"] != null)
            {
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                lstString = objBLQuestions.GetMitigationbyQuestionId(Convert.ToInt32(strQuestionID));
            }
            var result = Json(lstString, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }


        #region Get Questions By SectionId
        /// <summary>
        ///This method is used for Get Questions 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetQuestionsBySectionId(int SectionId)
        {
            lstDCQuestions = new List<DCQuestions>();
            objBLQuestions = new BLQuestions();
            objDCSections = new DCSections();
            if (Session["UserLogon"] != null)
            {
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                lstDCQuestions = objBLQuestions.GetQuestionsBySectionId(SectionId, objDCUsers.UserId);
            }
            var result = Json(lstDCQuestions, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        #region Delete Question
        /// <summary>
        /// This method is used for Delete Question 
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        public string DeleteQuestion(string QuestionID)
        {
            objBLQuestions = new BLQuestions();
            objDataOperationResponse = objBLQuestions.DeleteQuestion(Convert.ToInt32(QuestionID));
            return objDataOperationResponse.Message;
        }
        #endregion

        public JsonResult QuestionsSortingBySortKey(int QuestionId,int SectionId, string Arrow, string AssessmentTypeId)
        {
            lstDCQuestions = new List<DCQuestions>();
            objBLQuestions = new BLQuestions();
            objDCSections = new DCSections();
            if (Session["UserLogon"] != null)
            {
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                lstDCQuestions = objBLQuestions.QuestionsSortingBySortKey(QuestionId,SectionId, Arrow, Convert.ToInt32(AssessmentTypeId), objDCUsers.UserId);
            }
            var result = Json(lstDCQuestions, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        
        }

    }
}
