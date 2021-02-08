using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using DTO;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class SocialMediaController : BaseController
    {
        SocialMediaBLL bll = new SocialMediaBLL();

        // GET: Admin/SocialMedia
        public ActionResult AddSocialMedia()
        {
            SocialMediaDTO dto = new SocialMediaDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO model)
        {
            if (model.SocialImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.SocialImage;
                string ext = Path.GetExtension(postedFile.FileName);
                string fileName = string.Empty;
                if(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    Bitmap socialMedia = new Bitmap(postedFile.InputStream);
                    string uniqueNumber = Guid.NewGuid().ToString();
                    fileName = uniqueNumber + postedFile.FileName;
                    socialMedia.Save(Server.MapPath("~/Areas/Admin/Content/socialmedia/" + fileName));
                    model.ImagePath = fileName;

                    if (bll.AddSocialMedia(model))
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

            SocialMediaDTO newModel = new SocialMediaDTO();
            return View(newModel);
        }

        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> model = new List<SocialMediaDTO>();
            model = bll.GetSocialMediaData();
            return View(model);
        }

        public ActionResult UpdateSocialMedia(int id)
        {
            SocialMediaDTO model = new SocialMediaDTO();
            model = bll.GetSocialMediaDataWithID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.SocialImage != null)
                {
                    HttpPostedFileBase postedFile = model.SocialImage;
                    string ext = Path.GetExtension(postedFile.FileName);
                    string fileName = string.Empty;
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        Bitmap socialMedia = new Bitmap(postedFile.InputStream);
                        string uniqueNumber = Guid.NewGuid().ToString();
                        fileName = uniqueNumber + postedFile.FileName;
                        socialMedia.Save(Server.MapPath("~/Areas/Admin/Content/socialmedia/" + fileName));
                        model.ImagePath = fileName;
                    }
                }

                string oldImagePath = bll.UpdateSocialMedia(model);

                if(model.SocialImage != null)
                {
                    if(System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/socialmedia/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/socialmedia/" + oldImagePath));     
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }

            return View(model);
        }

        public JsonResult DeleteSocialMedia(int ID)
        {
            string imagePath = bll.DeleteSocialMedia(ID);

            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/socialmedia/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/socialmedia/" + imagePath));
                }
            }

            return Json("");
        }
    }
}