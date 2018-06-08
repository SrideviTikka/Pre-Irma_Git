using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreIRMA.DataContract;
using PreIRMA.BAL;

namespace PreIRMA.WebSite.Controllers
{
    public class RiskCriteriaController : Controller
    {
        BLRiskCriteria objBLRiskCriteria = null;
        DCRiskCriteria objDCRiskCriteria = null;
        List<DCRiskCriteria> lstDCRiskCriteria = null;
        DataOperationResponse objDataOperationResponse = null;

        #region View Risk Criteria
        /// <summary>
        /// View Risk Criteria
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult ViewRiskCriteria(string activeTab)
        {
            if (Session["Userlogon"] != null)
            {
                TempData["activetab"] = activeTab;
                return View();
            }
            else
                return Redirect("~/Account/Login");
        }
        #endregion

        #region  Add Risk Criteria
        /// <summary>
        /// Add Risk Criteria
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult AddRiskCriteria(FormCollection frmColl)
        {
            if (Session["UserLogon"] != null)
            {
                if (!string.IsNullOrEmpty(frmColl["btnAddRiskCriteria"]) || !string.IsNullOrEmpty(frmColl["btnAddAttribute"]))
                {
                    DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                    objBLRiskCriteria = new BLRiskCriteria();
                    objDCRiskCriteria = new DCRiskCriteria();

                    if (string.IsNullOrEmpty(frmColl["ddlAttribute"]) && string.IsNullOrEmpty(frmColl["ddlRiskCriteria"]))
                    {
                        objDCRiskCriteria.Attribute = "RC";
                        objDCRiskCriteria.DataValue = frmColl["txtRiskCriteria"];
                        objDCRiskCriteria.RiskCriteria = frmColl["txtRiskCriteria"];
                    }
                    else
                    {
                        objDCRiskCriteria.Attribute = frmColl["ddlAttribute"];
                        objDCRiskCriteria.DataValue = frmColl["txtAttributeDescription"];
                        objDCRiskCriteria.RiskCriteria = frmColl["hdnRiskCriteria"];
                    }
                    objDCRiskCriteria.CreatedBy = objDCUsers.UserId;
                    objDataOperationResponse = objBLRiskCriteria.AddRiskCriteria(objDCRiskCriteria);
                    if (objDataOperationResponse.Code > 0)
                    {
                        if (string.IsNullOrEmpty(frmColl["ddlAttribute"]) && string.IsNullOrEmpty(frmColl["ddlRiskCriteria"]))
                        {
                            TempData["successMessage"] = "Risk Criteria Added Successfully";
                            TempData["activetab"] = "1";
                        }
                        else
                        {
                            TempData["successMessage"] = "Risk Criteria Attribute Added Successfully";
                            TempData["activetab"] = "2";
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(frmColl["ddlAttribute"]) && string.IsNullOrEmpty(frmColl["ddlRiskCriteria"]))
                        {
                            TempData["errorMessage"] = objDataOperationResponse.Message;
                            TempData["activetab"] = "1";
                        }
                        else
                        {
                            TempData["errorMessage"] = objDataOperationResponse.Message;
                            TempData["activetab"] = "2";
                        }
                    }
                }
            }
            return View();
        }
        #endregion

        public ActionResult AddRCActionItems(FormCollection frmcll)
        {
            if (Session["UserLogon"] != null)
            {
                if (!string.IsNullOrEmpty(frmcll["btnAddActionItem"]))
                {
                    DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                    objBLRiskCriteria = new BLRiskCriteria();
                    objDCRiskCriteria = new DCRiskCriteria();
                    objDCRiskCriteria.RiskCriteria = frmcll["hdnRiskCriteria"];
                    objDCRiskCriteria.RiskCriteriaDescription = frmcll["ddlRiskCriteriaDescription"];
                    objDCRiskCriteria.ActionItem = frmcll["txtActionItem"];
                    objDataOperationResponse = objBLRiskCriteria.AddRCActionItems(objDCRiskCriteria);
                    if (objDataOperationResponse.Code > 0)
                    {
                        TempData["successMessage"] = objDataOperationResponse.Message;                        
                    }
                    else {
                        TempData["errorMessage"] = objDataOperationResponse.Message;
                    }
                    TempData["activetab"] = "2";
                   // return Redirect("/RiskCriteria/ViewRiskCriteria");
                    return Redirect("/RiskCriteria/AddRCActionItems");                   
                }
            }
            return View();
        }

        public ActionResult AddRCMitigation(FormCollection frmcol)
        {
            if (Session["UserLogon"] != null)
            {
                if (!string.IsNullOrEmpty(frmcol["btnAddMitigation"]))
                {
                    DCUsers objDCUsers = (DCUsers)Session["UserLogon"];
                    objBLRiskCriteria = new BLRiskCriteria();
                    objDCRiskCriteria = new DCRiskCriteria();
                    objDCRiskCriteria.RiskCriteria = frmcol["hdnRiskCriteria"];
                    objDCRiskCriteria.RiskCriteriaDescription = frmcol["ddlRiskCriteriaDescription"];
                    objDCRiskCriteria.Mitigation = frmcol["txtMitigation"];
                    objDataOperationResponse = objBLRiskCriteria.AddRCMitigation(objDCRiskCriteria);
                    if (objDataOperationResponse.Code > 0)
                    {
                        TempData["successMessage"] = objDataOperationResponse.Message;
                    }
                    else
                    {
                        TempData["errorMessage"] = objDataOperationResponse.Message;
                    }
                    TempData["activetab"] = "3";
                    // return Redirect("/RiskCriteria/ViewRiskCriteria");
                    return Redirect("/RiskCriteria/AddRCMitigation");
                }
            }
            return View();
        }

        #region Get Risk Criteria
        /// <summary>
        /// Get Risk Criteria
        /// </summary>
        /// <returns></returns>
        /// 

        public JsonResult GetRiskCriteria()
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objBLRiskCriteria = new BLRiskCriteria();
            lstDCRiskCriteria = objBLRiskCriteria.GetRiskCriteria();
            var result = Json(lstDCRiskCriteria, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        public JsonResult GetRCActionItems()
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objBLRiskCriteria = new BLRiskCriteria();
            lstDCRiskCriteria = objBLRiskCriteria.GetRCActionItems();
            var result = Json(lstDCRiskCriteria, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;        
        }

        public JsonResult GetRCMitigation()
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objBLRiskCriteria = new BLRiskCriteria();
            lstDCRiskCriteria = objBLRiskCriteria.GetRCMitigation();
            var result = Json(lstDCRiskCriteria, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        #region Delete Risk Criteria
        /// <summary>
        /// This method is used for Delete Sections 
        /// </summary>
        /// <param name="SectionID"></param>
        /// <returns></returns>
        public string DeleteRiskCriteria(string RiskCriteriaId)
        {
            objBLRiskCriteria = new BLRiskCriteria();
            objDataOperationResponse = objBLRiskCriteria.DeleteRiskCriteria(Convert.ToInt32(RiskCriteriaId));
            return objDataOperationResponse.Message;
        }
        #endregion

        public string DeleteRCActionItems(string RCActionItemId)
        {
            objBLRiskCriteria = new BLRiskCriteria();
            objDataOperationResponse = objBLRiskCriteria.DeleteRCActionItems(Convert.ToInt32(RCActionItemId));
            return objDataOperationResponse.Message;
        }

        public string DeleteRCMitigation(string RCMitigationId)
        {
            objBLRiskCriteria = new BLRiskCriteria();
            objDataOperationResponse = objBLRiskCriteria.DeleteRCMitigation(Convert.ToInt32(RCMitigationId));
            return objDataOperationResponse.Message;
        }

        #region Get Risk Criteria By Attribute Type
        /// <summary>
        /// Get Risk Criteria
        /// </summary>
        /// <returns></returns>
        /// 

        public JsonResult GetRiskCriteriaByAttributeType()
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objBLRiskCriteria = new BLRiskCriteria();
            lstDCRiskCriteria = objBLRiskCriteria.GetRiskCriteriaByAttributeType("RC");
            var result = Json(lstDCRiskCriteria, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        #region Get Risk Criteria Details By Risk Criteria
        /// <summary>
        /// Get Risk Criteria Details By Risk Criteria
        /// </summary>
        /// <returns></returns>
        /// 

        public JsonResult GetRiskCriteriaDetailsByRiskCriteria(string strRiskCriteria, string strAttribute)
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objBLRiskCriteria = new BLRiskCriteria();
            lstDCRiskCriteria = objBLRiskCriteria.GetRiskCriteriaDetailsByRiskCriteria(strRiskCriteria, strAttribute);
            var result = Json(lstDCRiskCriteria, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        public JsonResult GetActionItemsByRCDescription(string RiskCriteriaDescription)
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objBLRiskCriteria = new BLRiskCriteria();
            lstDCRiskCriteria = objBLRiskCriteria.GetActionItemsByRCDescription(RiskCriteriaDescription);
            var result = Json(lstDCRiskCriteria, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        public JsonResult GetMitigationByRCDescription(string RiskCriteriaDescription)
        {
            lstDCRiskCriteria = new List<DCRiskCriteria>();
            objBLRiskCriteria = new BLRiskCriteria();
            lstDCRiskCriteria = objBLRiskCriteria.GetMitigationByRCDescription(RiskCriteriaDescription);
            var result = Json(lstDCRiskCriteria, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
    }
}
