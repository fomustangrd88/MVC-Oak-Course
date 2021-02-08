using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
using System.Drawing;
using System.IO;

namespace UI.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        UserBLL bll = new UserBLL();

        // GET: Admin/User
        public ActionResult AddUser()
        {
            UserDTO model = new UserDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO model)
        {
            if(model.UserImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.UserImage;
                string ext = Path.GetExtension(postedFile.FileName);
                string fileName = string.Empty;
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    Bitmap userImage = new Bitmap(postedFile.InputStream);
                    Bitmap resizeImage = new Bitmap(userImage, 128, 128);
                    string uniqueNumber = Guid.NewGuid().ToString();
                    fileName = uniqueNumber + postedFile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/userimage/" + fileName));
                    model.ImagePath = fileName;

                    if (bll.AddUser(model))
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

            UserDTO newModel = new UserDTO();
            return View(newModel);
        }

        public ActionResult UserList()
        {
            List<UserDTO> model = new List<UserDTO>();
            model = bll.GetUserData();
            return View(model);
        }

        public ActionResult UpdateUser(int ID)
        {
            UserDTO model = new UserDTO();
            model = bll.GetUserDataWithID(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.UserImage != null)
                {
                    HttpPostedFileBase postedFile = model.UserImage;
                    string ext = Path.GetExtension(postedFile.FileName);
                    string fileName = string.Empty;
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        Bitmap userImage = new Bitmap(postedFile.InputStream);
                        Bitmap resizeImage = new Bitmap(userImage, 128, 128);
                        string uniqueNumber = Guid.NewGuid().ToString();
                        fileName = uniqueNumber + postedFile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/userimage/" + fileName));
                        model.ImagePath = fileName;
                    }
                }

                string oldImagePath = bll.UpdateUser(model);

                if (model.UserImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/userimage/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/userimage/" + oldImagePath));
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }

            return View(model);
        }

        public JsonResult DeleteUser(int ID)
        {
            string imagePath = bll.DeleteUser(ID);

            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/userimage/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/userimage/" + imagePath));
                }
            }

            return Json("");
        }
    }
}