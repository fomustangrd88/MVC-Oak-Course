using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
using System.IO;
using System.Drawing;

namespace UI.Areas.Admin.Controllers
{
    public class FavController : BaseController
    {
        FavBLL bll = new FavBLL();

        // GET: Admin/Fav
        public ActionResult UpdateFav()
        {
            FavDTO model = new FavDTO();
            model = bll.GetFav();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateFav(FavDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.FavImage != null)
                {
                    HttpPostedFileBase postedFileFav = model.FavImage;
                    string ext = Path.GetExtension(postedFileFav.FileName);
                    string fileNameFav = string.Empty;
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        Bitmap favImage = new Bitmap(postedFileFav.InputStream);
                        Bitmap resizedImageFav = new Bitmap(favImage, 100, 100);
                        string uniqueNumberFav = Guid.NewGuid().ToString();
                        fileNameFav = uniqueNumberFav + postedFileFav.FileName;
                        resizedImageFav.Save(Server.MapPath("~/Areas/Admin/Content/favimage/" + fileNameFav));
                        model.Fav = fileNameFav;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }

                if (model.LogoImage != null)
                {
                    HttpPostedFileBase postedFileLogo = model.LogoImage;
                    string ext = Path.GetExtension(postedFileLogo.FileName);
                    string fileNameLogo = string.Empty;
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        Bitmap logoImage = new Bitmap(postedFileLogo.InputStream);
                        Bitmap resizedImageLogo = new Bitmap(logoImage, 100, 100);
                        string uniqueNumberLogo = Guid.NewGuid().ToString();
                        fileNameLogo = uniqueNumberLogo + postedFileLogo.FileName;
                        resizedImageLogo.Save(Server.MapPath("~/Areas/Admin/Content/favimage/" + fileNameLogo));
                        model.Logo = fileNameLogo;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }
                FavDTO returnDTO = new FavDTO();
                returnDTO = bll.UpdateFav(model);

                if (model.FavImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/favimage/" + returnDTO.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/favimage/" + returnDTO.Fav));
                    }
                }

                if (model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/favimage/" + returnDTO.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/favimage/" + returnDTO.Logo));
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }

            return View(model);
        }
    }
}