using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreIRMA.BAL;
using PreIRMA.DataContract;

namespace PreIRMA.WebSite.Controllers
{
    public class SectionsController : Controller
    {
        BLSections objBLSections = null;
        DCSections objDCSections = null;
        DataOperationResponse objDataOperationResponse = null;
        List<DCSections> lstDCSections = null;

        #region View Sections
        /// <summary>
        /// This method is used for View Sections 
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewSections()
        {
            if (Session["Userlogon"] != null)
                return View();
            else
                return Redirect("~/Account/Login");
        }
        #endregion

        #region Add Update Sections
        /// <summary>
        /// This method is used for Add Update Sections 
        /// </summary>
        /// <param name="strSectionName"></param>
        /// <param name="hdnSectionId"></param>
        /// <param name="strSectionNumber"></param>
        /// <returns></returns>
        public string AddUpdateSections(string strSectionName, string hdnSectionId, string strSectionNumber, string strAssessmentType,string strDescription,string strCritical)
        {
            objBLSections = new BLSections();
            objDCSections = new DCSections();

            if (Session["UserLogon"] != null)
            {
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                objDCSections.SectionName = strSectionName;
                objDCSections.CreatedBy = objDCUsers.UserId;
                objDCSections.SectionNumber = strSectionNumber;
                objDCSections.AssessmentTypeId = Convert.ToInt32(strAssessmentType);
                objDCSections.SectionId = string.IsNullOrEmpty(hdnSectionId) ? 0 : Convert.ToInt32(hdnSectionId);
                objDCSections.Description = strDescription;
                objDCSections.Critical = strCritical;
                objDataOperationResponse = objBLSections.AddUpdateSections(objDCSections);
            }
            return objDataOperationResponse.Message;
        }
        #endregion

        #region Delete Sections
        /// <summary>
        /// This method is used for Delete Sections 
        /// </summary>
        /// <param name="SectionID"></param>
        /// <returns></returns>
        public string DeleteSections(string SectionID)
        {
            objBLSections = new BLSections();
            objDataOperationResponse = objBLSections.DeleteSection(Convert.ToInt32(SectionID));
            return objDataOperationResponse.Message;
        }
        #endregion

        #region Get Sections
        /// <summary>
        /// This method is used for Get Sections 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSections(string strAssessmentTypeId)
        {
            if (Session["UserLogon"] != null)
            {
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                lstDCSections = new List<DCSections>();
                objBLSections = new BLSections();
                lstDCSections = objBLSections.GetSections(Convert.ToInt32(strAssessmentTypeId), objDCUsers.UserId);
            }
            var result = Json(lstDCSections, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        #region Get Section Name By SectionId
        /// <summary>
        /// This method is used for Get Section Name By SectionId
        /// </summary>
        /// <param name="strSectionId"></param>
        /// <returns></returns>
        public JsonResult GetSectionNameBySectionId(string strSectionId)
        {
            objBLSections = new BLSections();
            DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
            if (Session["UserLogon"] != null)
            {
                lstDCSections = objBLSections.GetSectionNameBySectionId(Convert.ToInt32(strSectionId), objDCUsers.UserId);
            }
            var result = Json(lstDCSections, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;

        }
        #endregion

        public JsonResult SectionSortingBySortkey(int SectionId, string Arrow, string AssessmentTypeId)
        {
            if (Session["UserLogon"] != null)
            {
                DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                objDCSections = new DCSections();
                lstDCSections = new List<DCSections>();
                objBLSections = new BLSections();
                lstDCSections = objBLSections.SectionSortingBySortkey(SectionId, Arrow, Convert.ToInt32(AssessmentTypeId), objDCUsers.UserId);           
            }
            var result = Json(lstDCSections, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;

        }

    }
}
