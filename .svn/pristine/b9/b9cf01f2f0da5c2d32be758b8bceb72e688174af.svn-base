using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreIRMA.BAL;
using PreIRMA.DataContract;
using System.Web.Security;
using PreIRMA.WebSite.Models;
using System.Net.Mail;
using System.Net.Mime;

namespace PreIRMA.WebSite.Controllers
{
    public class AccountController : BaseController
    {
        BLUsers objBLUsers = null;
        DataOperationResponse objDataOperationResponse = null;

        #region Login
        /// <summary>
        /// This method is used for Login
        /// </summary>
        /// <param name="frmColl"></param>
        /// <returns></returns> 

        public ActionResult Login(FormCollection frmColl)
        {
            if (!string.IsNullOrEmpty(frmColl["btnLogin"]) && (string.Compare(frmColl["btnLogin"].ToUpper(), "LOGIN") == 0))
            {
                objBLUsers = new BLUsers();
                DCUsers objDCUsers = objBLUsers.UserLogon(frmColl["txtEmailAddress"], frmColl["txtPassword"]);
                if (objDCUsers.Code > 0)
                {
                    Session["UserLogon"] = objDCUsers;
                    switch (objDCUsers.UserType)
                    {
                        case UserType.A:
                            return Redirect("RiskCriteria/ViewRiskCriteria");
                        case UserType.U:
                            return Redirect("ClientInfo/AddUpdateClientInfo");
                    }
                }
                else
                {
                    TempData["errorMessage"] = "The UserID/Password is Incorrect.";
                }
            }
            return View();
        }
        #endregion

        #region Logout
        /// <summary>
        /// This method is used for Logout
        /// </summary>
        /// <param name="frmColl"></param>
        /// <returns></returns>

        public ActionResult Logout(FormCollection frmColl)
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        #endregion

        #region Forgot Password
        public ActionResult ForgotPassword(string txtEmailAddress)
        {
            List<DCUsers> lstDCUsers = new List<DCUsers>();           
            
                string strMessage = string.Empty;
                if (!string.IsNullOrEmpty(txtEmailAddress))
                {
                    objBLUsers = new BLUsers();
                    objDataOperationResponse = new DataOperationResponse();

                    lstDCUsers = objBLUsers.ForgotPassword(txtEmailAddress);
                    if (lstDCUsers.Count>0)
                    {
                        EmailAttributesModel objEmailAttributes = new EmailAttributesModel();
                        objEmailAttributes.Subject = "IRMA™ Onboarding Portal Password";
                        string imagePath = Server.MapPath(@"~/Images/mail.png");

                        var linkedResource = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg);
                        linkedResource.ContentId = "logoImage";
                        string body = "Hello " + lstDCUsers[0].FirstName + "," + " <br/><br/>" + "We heard that you lost your IRMA™ Onboarding Portal password. Sorry about that!"
                            + "<br/><br/>" + "<b style='margin-left:30px;'>Your Password: </b>" + lstDCUsers[0].LastName + "<br/><br/>" + " Please contact the applicable support group for any questions or assistance:" + "<br/><br/>"
                            + "<li style='margin-left:30px;'>Adaptive Risk System (ARS™) support team for questions related to the use of the IRMA™ Onboarding and/or Live applications.</li>"
                            + "<li type='circle' style='margin:5px 60px'> <a href='mailto:" + AppConfig.SMTPEmailARS + "'>" + AppConfig.SMTPEmailARS + " </a></li> "
                            + "<li type='circle' style='margin:5px 60px'> Mobile: " + AppConfig.SMTPPHNNO + "</li>"

                            + "<li style='margin-left:30px;'>IRMA™ IT support team for any questions related to logon, password or other IT related issues.</li>"
                            + "<li type='circle' style='margin:5px 60px'> <a href='mailto:" + AppConfig.SMTPEmailIRMA + "'> " + AppConfig.SMTPEmailIRMA + " </a></li> "

                            + "<br/><br/>" + " We will respond to emails within 24 hours of receipt." + "<br/><br/>" + " Thank you and have a great day!" + "<br/><b> IRMA™ Support Team</b>" + "<br/> <img src='cid:logoImage' alt='Red dot' width='122' height='48' />";
                        var altView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                        altView.LinkedResources.Add(linkedResource);
                        objEmailAttributes.AlternateView = altView;
                        objEmailAttributes.MessageBody = body;
                        objEmailAttributes.From = AppConfig.SMTPEMAILFROM;
                        objEmailAttributes.To = lstDCUsers[0].EmailAddress;
                        objEmailAttributes.CC = "";
                        strMessage = SendMail(objEmailAttributes);                      
                        TempData["SuccessMessage"] = "Password has been sent to your mail";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Email-id does not exist";
                        return RedirectToAction("Login");
                    }
                }
                return View();
                      
        }
        #endregion

        #region Change Password
        /// <summary>
        /// This method is used for Change Password
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
        #endregion
    }
}
