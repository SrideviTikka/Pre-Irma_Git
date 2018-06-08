using PreIRMA.BAL;
using PreIRMA.DataContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PreIRMA.WebSite.Controllers
{
    public class ClientInfoController : Controller
    {
        DCUsers objDCUsers = null;
        DCClientInfo objDCClientInfo = null;
        DataOperationResponse objDataOperationResponse = null;
        BLClientInfo objBLClientInfo = null;
        List<DCClientInfo> lstDCClientInfo = null;

        #region Add Update Client Info
        /// <summary>
        /// This method is used for Add Update Client Info 
        /// </summary>
        /// <param name="frmColl"></param>
        /// <returns></returns>
        public ActionResult AddUpdateClientInfo(FormCollection frmColl)
        {
            if (Session["Userlogon"] != null)
            {
                objDCClientInfo = new DCClientInfo();
                lstDCClientInfo = new List<DCClientInfo>();
                if (!string.IsNullOrEmpty(frmColl["btnSave"]))
                {
                    objDataOperationResponse = new DataOperationResponse();
                    objBLClientInfo = new BLClientInfo();
                    objDCUsers = (DCUsers)Session["UserLogon"];
                    objDCClientInfo.ClientId = string.IsNullOrEmpty(frmColl["hdnClientId"]) ? 0 : Convert.ToInt32(frmColl["hdnClientId"]);                    
                    objDCClientInfo.FirstName = frmColl["txtCCFirstName"];
                    objDCClientInfo.LastName = frmColl["txtCCLastName"];
                    objDCClientInfo.ContactTitle = frmColl["txtContactTitle"];
                    objDCClientInfo.EmailAddress = frmColl["txtEmail"];
                    objDCClientInfo.CompanyName = frmColl["txtCompanyName"];
                    objDCClientInfo.Addressline1 = frmColl["txtAddress1"];
                    objDCClientInfo.Addressline2 = frmColl["txtAddress2"];
                    objDCClientInfo.Addressline3 = frmColl["txtAddress3"];
                    objDCClientInfo.CityProvince = frmColl["txtCityProvince"];
                    objDCClientInfo.Country = frmColl["txtCountry"];
                    objDCClientInfo.State = frmColl["txtState"];
                    objDCClientInfo.ZipCode = frmColl["txtZipCode"];
                    objDCClientInfo.OfficePhone = frmColl["txtOfficePhone"];
                    objDCClientInfo.MobilePhone = frmColl["txtMobilePhone"];
                    objDCClientInfo.UserId = objDCUsers.UserId;
                    lstDCClientInfo = objBLClientInfo.AddUpdateClientInfo(objDCClientInfo);
                    TempData["activetab"] = "1";
                    if (lstDCClientInfo.Count > 0)
                    {
                        TempData["successMessage"] = objDCClientInfo.ClientId == 0 ? "Client Details Added Succesfully" : "Client Details Updated Successfully";
                    }
                    return View(lstDCClientInfo[0]);
                }
            }
            else
            {
                return Redirect("~/Account/Login");
            }
            return View(new DCClientInfo());
        }
        #endregion
        public JsonResult GetClientInfo()
        {
            lstDCClientInfo = new List<DCClientInfo>();
            objBLClientInfo = new BLClientInfo();
            objDCUsers = (DCUsers)Session["UserLogon"];
            lstDCClientInfo = objBLClientInfo.GetClientInfo(objDCUsers.UserId);
            var result = Json(lstDCClientInfo, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        public ActionResult UploadLogo(FormCollection frmcol, HttpPostedFileBase fleUpload)
        {
            objDCClientInfo = new DCClientInfo();
            objBLClientInfo = new BLClientInfo();
            objDCUsers = (DCUsers)Session["UserLogon"];
            objDataOperationResponse = new DataOperationResponse();
            objDCClientInfo.UserId = objDCUsers.UserId;
            objDCClientInfo.ClientUploadId = string.IsNullOrEmpty(frmcol["hdnClientUploadId"]) ? 0 : Convert.ToInt32(frmcol["hdnClientUploadId"]);    
            objDCClientInfo.Description = frmcol["txtDescription"];
            if (frmcol["btnLogoSave"] == "Save")
            {
                objDCClientInfo.FileType = "Logo";
            }
            else
                objDCClientInfo.FileType = "Document";
            if (fleUpload != null && fleUpload.ContentLength > 0)
            {
                //Save to folder
                string UniqueFileName = Guid.NewGuid() + "-" + Path.GetFileName(fleUpload.FileName);
                string path = Path.Combine(Server.MapPath("~/Uploads/LogoUpload/"), UniqueFileName);
                var FileVirtualPath = "/Uploads/LogoUpload/" + UniqueFileName;
                fleUpload.SaveAs(path);
                //Save to database
                
                objDCClientInfo.UploadLogo = FileVirtualPath;                
            }
            objDataOperationResponse = objBLClientInfo.UploadLogo(objDCClientInfo);
            TempData["activetab"] = "2";
            if (objDataOperationResponse.Code > 0)
            {
                TempData["successMessage"] = objDataOperationResponse.Message;
                return RedirectToAction ("AddUpdateClientInfo");
            }
            else
            {
                TempData["errorMessage"] = objDataOperationResponse.Message;
                return RedirectToAction("AddUpdateClientInfo");
            }           
        }

        public string DeleteUploadLogo(int ClientUploadId)
        {
            objBLClientInfo = new BLClientInfo();
            objDataOperationResponse = objBLClientInfo.DeleteUploadLogo(ClientUploadId);
            return objDataOperationResponse.Message;        
        }

        public JsonResult GetClientUploadedFile()
        {
            lstDCClientInfo = new List<DCClientInfo>();
            objBLClientInfo = new BLClientInfo();
            objDCUsers = (DCUsers)Session["UserLogon"];
            lstDCClientInfo = objBLClientInfo.GetClientUploadedFile(objDCUsers.UserId);
            var result = Json(lstDCClientInfo, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        public JsonResult GetUploadFileByClientUploadId(int ClientUploadId)
        {
            lstDCClientInfo = new List<DCClientInfo>();
            objBLClientInfo = new BLClientInfo();
            objDCUsers = (DCUsers)Session["UserLogon"];
            lstDCClientInfo = objBLClientInfo.GetUploadFileByClientUploadId(ClientUploadId);
            var result = Json(lstDCClientInfo, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        
        }
    }
}