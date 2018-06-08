using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreIRMA.DataContract;
using PreIRMA.BAL;
using PreIRMA.WebSite.Models;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;

namespace PreIRMA.WebSite.Controllers
{
    public class UserController : BaseController
    {
        DCUsers objDCUsers = null;
        BLUsers objBLUsers = null;
        List<DCUsers> lstDCUsers = null;
        List<DCAssessment> lstDCAssessment = null;
        BLAssessment objBLAssessment = null;
        DataOperationResponse objDataOperationResponse = null;

        #region View Users
        /// <summary>
        /// This method is for View Users 
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewUsers()
        {
            if (Session["Userlogon"] != null)
                return View();
            else
                return Redirect("~/Account/Login");
        }
        #endregion

        public ActionResult ViewAssesments()
        {
            if (Session["Userlogon"] != null)
                return View();
            else
                return Redirect("~/Account/Login");
        }

        #region Add Update Users
        /// <summary>
        /// This method is used for Add Update Users
        /// </summary>
        /// <param name="frmColl"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ActionResult AddUpdateUsers(FormCollection frmColl)
        {
            if (Session["UserLogon"] != null)
            {
                objDCUsers = new DCUsers();
                //if (!string.IsNullOrEmpty(frmColl["btnSave"]) && string.Compare(frmColl["btnSave"].ToUpper(), "SAVE") == 0)

                objDataOperationResponse = new DataOperationResponse();
                objBLUsers = new BLUsers();

                StringBuilder objStrBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(frmColl["ddlAssessmentType"]))
                {
                    string[] strUserAssessmentTypeIds = frmColl["ddlAssessmentType"].Split(',');
                    objStrBuilder.Append("<UserAssessmentTypeIds>");
                    foreach (string UserAssessmentTypeId in strUserAssessmentTypeIds)
                    {
                        objStrBuilder.Append("<UserAssessmentTypeId>" + Convert.ToInt32(UserAssessmentTypeId) + "</UserAssessmentTypeId>");
                    }
                    objStrBuilder.Append("</UserAssessmentTypeIds>");
                }
                objDCUsers.UserId = string.IsNullOrEmpty(frmColl["hdnUserId"]) ? 0 : Convert.ToInt32(frmColl["hdnUserId"]);
                objDCUsers.FirstName = frmColl["txtFirstName"];
                objDCUsers.LastName = frmColl["txtLastName"];
                objDCUsers.EmailAddress = frmColl["txtEmail"];
                objDCUsers.SponsorApproval = frmColl["ddlSponserApproval"];
                objDCUsers.ExpiryDate = Convert.ToDateTime(frmColl["txtExpiryDate"]);
                if (Convert.ToInt32(frmColl["ddlClientName"]) == 0)
                {
                    objDCUsers.ClientName = frmColl["txtNewClientName"];
                    objDCUsers.ClientNameAbbr = frmColl["txtClientNameABBR"];
                }
                else
                    objDCUsers.ClientName = frmColl["hdnClientName"];
                if (Convert.ToInt32(frmColl["ddlSponserName"]) == 0)
                {
                    objDCUsers.SponserName = frmColl["txtNewSponserName"];
                    objDCUsers.SponserNameAbbr = frmColl["txtSponserNameABBR"];
                }
                else
                    objDCUsers.SponserName = frmColl["hdnSponserName"];
                if (Convert.ToInt32(frmColl["ddlProtocalName"]) == 0)
                {
                    objDCUsers.ProtocalName = frmColl["txtNewProtocalName"];
                    objDCUsers.ProtocolNameAbbr = frmColl["txtProtocolNameABBR"];
                }
                else
                    objDCUsers.ProtocalName = frmColl["hdnProtocalName"];

                string XMLData = objStrBuilder.ToString();
                objDataOperationResponse = objBLUsers.AddUpdateUser(objDCUsers, XMLData);
                if (objDataOperationResponse.Code > 0 )
                {
                    if (objDCUsers.UserId == 0)
                    {
                        string strMessage = string.Empty;
                        string strPassword = string.Empty;
                        objDCUsers = (DCUsers)Session["UserLogon"];
                        EmailAttributesModel objEmailAttributes = new EmailAttributesModel();
                        objEmailAttributes.Subject = "Welcome to the IRMA™ Onboarding Portal";
                        string imagePath = Server.MapPath(@"~/Images/mail.png");

                        var linkedResource = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg);
                        linkedResource.ContentId = "logoImage";
                        string body = "Hello " + frmColl["txtFirstName"] + "," + " <br/><br/>" + "Welcome to Adaptive Risk Systems (ARS™) Intelligent Risk Monitoring Assessment ( IRMA™) Onboarding application [" + AppConfig.IRMAVERSION + "]. The web-link to login to the IRMA™ Onboarding portal and user name details are provided below. Password will be provided in a separate email."
                            + "<br/><br/>" + "<b style='margin-left:30px;'> IRMA™ Onboarding web-link:</b> <a href='http://onboarding.besymple.com/'> http://onboarding.besymple.com/ </a>" + "<br/><b style='margin-left:30px;'>User Name: </b>" + frmColl["txtEmail"] + "<br/><b style='margin-left:30px;'> Password:</b> to be provided in a separate email<br/><br/>" + " Please contact the applicable support group for any questions or assistance:" + "<br/><br/>"
                            + "<li style='margin-left:30px;'>Adaptive Risk System (ARS™) support team for questions related to the use of the IRMA™ Onboarding and/or Live applications.</li>"
                            + "<li type='circle' style='margin:5px 60px'> <a href='mailto:" + AppConfig.SMTPEmailARS + "'>" + AppConfig.SMTPEmailARS + " </a></li> "
                            + "<li type='circle' style='margin:5px 60px'> Mobile: "+AppConfig.SMTPPHNNO+"</li>"

                            + "<li style='margin-left:30px;'>IRMA™ IT support team for any questions related to logon, password or other IT related issues.</li>"
                            + "<li type='circle' style='margin:5px 60px'> <a href='mailto:" + AppConfig.SMTPEmailIRMA + "'> " + AppConfig.SMTPEmailIRMA + " </a></li> "

                            + "<br/><br/>" + " We will respond to emails within 24 hours of receipt." + "<br/><br/>" + " Thank you and have a great day!" + "<br/><b> IRMA™ Support Team</b>" + "<br/> <img src='cid:logoImage' alt='Red dot' width='122' height='48' />";
                        var altView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                        altView.LinkedResources.Add(linkedResource);
                        objEmailAttributes.AlternateView = altView;
                        objEmailAttributes.MessageBody = body;                       
                        objEmailAttributes.From = AppConfig.SMTPEMAILFROM;
                        objEmailAttributes.To = frmColl["txtEmail"];
                        objEmailAttributes.CC = AppConfig.SMTPEmailCC;
                        strMessage = SendMail(objEmailAttributes);

                        EmailAttributesModel objEmailAttributesForPassword = new EmailAttributesModel();
                        objEmailAttributesForPassword.Subject = "";
                        string imagePathPwd = Server.MapPath(@"~/Images/mail.png");

                        var linkedResourcePwd = new LinkedResource(imagePathPwd, MediaTypeNames.Image.Jpeg);
                        linkedResourcePwd.ContentId = "logoImagePwd";
                        string bodyPwd = "Hello " + frmColl["txtFirstName"] + "," + " <br/><br/>" + "Welcome to Adaptive Risk Systems (ARS™) Intelligent Risk Monitoring Assessment ( IRMA™) Onboarding application [" + AppConfig.IRMAVERSION + "]."
                           + "<br/><br/>" + frmColl["txtLastName"] + "<br/><br/>" + " Please contact the applicable support group for any questions or assistance:" + "<br/><br/>"
                           + "<li style='margin-left:30px;'>Adaptive Risk System (ARS™) support team for questions related to the use of the IRMA™ Onboarding and/or Live applications.</li>"
                           + "<li type='circle' style='margin:5px 60px'> <a href='mailto:" + AppConfig.SMTPEmailARS + "'>" + AppConfig.SMTPEmailARS + " </a></li> "
                           + "<li type='circle' style='margin:5px 60px'> Mobile: " + AppConfig.SMTPPHNNO + "</li>"

                           + "<li style='margin-left:30px;'>IRMA™ IT support team for any questions related to logon, password or other IT related issues.</li>"
                           + "<li type='circle' style='margin:5px 60px'> <a href='mailto:" + AppConfig.SMTPEmailIRMA + "'> " + AppConfig.SMTPEmailIRMA + " </a></li> "

                           + "<br/><br/>" + " We will respond to emails within 24 hours of receipt." + "<br/><br/>" + " Thank you and have a great day!" + "<br/><b> IRMA™ Support Team</b>" + "<br/> <img src='cid:logoImagePwd' alt='Red dot' width='122' height='48' />";
                        var altViewPwd = AlternateView.CreateAlternateViewFromString(bodyPwd, null, "text/html");
                        altViewPwd.LinkedResources.Add(linkedResourcePwd);
                        objEmailAttributesForPassword.AlternateView = altViewPwd;
                        objEmailAttributesForPassword.MessageBody = bodyPwd;
                        objEmailAttributesForPassword.From = AppConfig.SMTPEMAILFROM;
                        objEmailAttributesForPassword.To = frmColl["txtEmail"];
                        objEmailAttributesForPassword.CC = "";
                        strPassword = SendMail(objEmailAttributesForPassword);
                    }
                    objDCUsers.Activetab = frmColl["hdnActivetab"];
                    if (objDCUsers.Activetab == "1")
                    {
                        if (objDataOperationResponse.Code > 0)
                        {
                            TempData["successMessage"] = objDataOperationResponse.Message;
                        }
                        else
                        {
                            TempData["errorMessage"] = objDataOperationResponse.Message;
                        }
                        TempData["hdnActivetab"] = "1";
                        return Redirect("~/User/ViewUsers");
                    }
                    else
                    {
                        TempData["hdnActivetab"] = "2";
                        return Json(objDataOperationResponse, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    objDCUsers.Activetab = frmColl["hdnActivetab"];
                    if (objDCUsers.Activetab == "1")
                    {
                        TempData["hdnActivetab"] = "1";
                        TempData["errorMessage"] = objDataOperationResponse.Message;
                        return Redirect("~/User/ViewUsers");
                    }
                    else
                    {
                        TempData["hdnActivetab"] = "2";                       
                        return Json(objDataOperationResponse.Message, JsonRequestBehavior.AllowGet);
                    }                   
                    
                }

                //return View("ViewUsers");
            }
            else
                return Redirect("~/Account/Login");
        }
        #endregion

        #region Get Users
        /// <summary>
        /// This method is used for Get Users 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUsers(string UserId)
        {
            lstDCUsers = new List<DCUsers>();
            objBLUsers = new BLUsers();
            lstDCUsers = objBLUsers.GetUsers(Convert.ToInt32(UserId));
            var result = Json(lstDCUsers, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        #region Delete User
        /// <summary>
        /// This method is used for Delete Uers
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string DeleteUers(string UserId)
        {
            objBLUsers = new BLUsers();
            objDataOperationResponse = objBLUsers.DeleteUser(Convert.ToInt32(UserId));
            return objDataOperationResponse.Message;
        }
        #endregion

        #region Get Admin AssessmentTypes
        /// <summary>
        ///This method is used for Get Questions 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAdminAssessmentTypes()
        {
            lstDCAssessment = new List<DCAssessment>();
            objBLAssessment = new BLAssessment();
            lstDCAssessment = objBLAssessment.GetAdminAssessmentTypes();
            var result = Json(lstDCAssessment, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        #region Get User AssessmentTypes
        /// <summary>
        ///This method is used for Get Questions 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserAssessmentTypes()
        {
            lstDCAssessment = new List<DCAssessment>();
            objBLAssessment = new BLAssessment();
            objDCUsers = (DCUsers)Session["UserLogon"];
            lstDCAssessment = objBLAssessment.GetUserAssessmentTypes(objDCUsers.UserId);
            var result = Json(lstDCAssessment, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }
        #endregion

        public JsonResult GetClientDetails()
        {
            lstDCUsers = new List<DCUsers>();
            objBLUsers = new BLUsers();
            lstDCUsers = objBLUsers.GetClientDetails();
            var result = Json(lstDCUsers, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        public JsonResult GetSponserDetails(int ClientDetailsId)
        {
            lstDCUsers = new List<DCUsers>();
            objBLUsers = new BLUsers();
            lstDCUsers = objBLUsers.GetSponserDetails(ClientDetailsId);
            var result = Json(lstDCUsers, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        public JsonResult GetProtocolDetails(int SponserDetailsId)
        {
            lstDCUsers = new List<DCUsers>();
            objBLUsers = new BLUsers();
            lstDCUsers = objBLUsers.GetProtocolDetails(SponserDetailsId);
            var result = Json(lstDCUsers, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        public JsonResult GetVersionsByProtocolId(int ClientDetailsId, int SponserDetailsId, int ProtocalDetailsId)
        {
            lstDCUsers = new List<DCUsers>();
            objBLUsers = new BLUsers();
            lstDCUsers = objBLUsers.GetVersionsByProtocolId(ClientDetailsId, SponserDetailsId, ProtocalDetailsId);
            var result = Json(lstDCUsers, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;

        }
    }
}
