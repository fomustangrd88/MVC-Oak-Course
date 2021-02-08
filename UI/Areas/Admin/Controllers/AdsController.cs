using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using DTO;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class AdsController : BaseController
    {
        AdsBLL bll = new AdsBLL();

        // GET: Admin/Ads
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAds()
        {
            AdsDTO model = new AdsDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddAds(AdsDTO model)
        {
            if (model.AdsImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.AdsImage;
                string ext = Path.GetExtension(postedFile.FileName);
                string fileName = string.Empty;
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    Bitmap userImage = new Bitmap(postedFile.InputStream);
                    Bitmap resizeImage = new Bitmap(userImage, 128, 128);
                    string uniqueNumber = Guid.NewGuid().ToString();
                    fileName = uniqueNumber + postedFile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/adsimage/" + fileName));
                    model.ImagePath = fileName;

                    if (bll.AddAds(model))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.GeneralError;
                    }
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.ExtensionError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            AdsDTO newModel = new AdsDTO();
            return View(newModel);
        }

        public ActionResult AdsList()
        {
            List<AdsDTO> model = new List<AdsDTO>();
            model = bll.GetAdsList();
            return View(model);
        }

        public ActionResult UpdateAds(int ID)
        {
            AdsDTO model = new AdsDTO();
            model = bll.GetAdsById(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateAds(AdsDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.AdsImage != null)
                {
                    HttpPostedFileBase postedFile = model.AdsImage;
                    string ext = Path.GetExtension(postedFile.FileName);
                    string fileName = string.Empty;
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        Bitmap userImage = new Bitmap(postedFile.InputStream);
                        Bitmap resizeImage = new Bitmap(userImage, 128, 128);
                        string uniqueNumber = Guid.NewGuid().ToString();
                        fileName = uniqueNumber + postedFile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/adsimage/" + fileName));
                        model.ImagePath = fileName;
                    }

                    string oldImagePath = bll.UpdateUser(model);

                    if (model.AdsImage != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/adsimage/" + oldImagePath)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/adsimage/" + oldImagePath));
                        }
                    }

                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
            }

            return View(model);
        }

        public JsonResult DeleteAds(int ID)
        {
            string imagePath = bll.DeleteAds(ID);

            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/adsimage/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/adsimage/" + imagePath));
                }
            }

            return Json("");
        }
    }
}