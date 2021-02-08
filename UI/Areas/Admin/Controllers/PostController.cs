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
    public class PostController : BaseController
    {
        PostBLL bll = new PostBLL();

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPost()
        {
            PostDTO model = new PostDTO();
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPost(PostDTO model)
        {
            if(model.PostImage[0] == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                List<PostImageDTO> imageList = new List<PostImageDTO>();

                foreach (var item in model.PostImage)
                {
                    HttpPostedFileBase postedFile = item;
                    string ext = Path.GetExtension(postedFile.FileName);
                    string fileName = string.Empty;
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".gif")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropdown();
                        return View(model);
                    }
                    else
                    {
                        Bitmap userImage = new Bitmap(postedFile.InputStream);
                        Bitmap resizeImage = new Bitmap(userImage, 750, 422);
                        string uniqueNumber = Guid.NewGuid().ToString();
                        fileName = uniqueNumber + postedFile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/postimage/" + fileName));
                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = fileName;
                        imageList.Add(dto);
                    }
                }

                model.PostImageList = imageList;

                if (bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new PostDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }

        public ActionResult PostList()
        {
            CountDTO countDTO = new CountDTO();
            countDTO = bll.GetAllCounts();
            ViewData["AllCounts"] = countDTO;

            List<PostDTO> postList = new List<PostDTO>();
            postList = bll.GetPosts();
            return View(postList);
        }

        public ActionResult UpdatePost(int ID)
        {
            PostDTO model = new PostDTO();
            model = bll.GetPostById(ID);
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            model.isUpdate = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdatePost(PostDTO model)
        {
            IEnumerable<SelectListItem> selectList = CategoryBLL.GetCategoriesForDropdown();

            if (ModelState.IsValid)
            {
                if (model.PostImage[0] != null)
                {
                    List<PostImageDTO> imageList = new List<PostImageDTO>();

                    foreach (var item in model.PostImage)
                    {
                        HttpPostedFileBase postedFile = item;
                        string ext = Path.GetExtension(postedFile.FileName);
                        string fileName = string.Empty;
                        if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".gif")
                        {
                            ViewBag.ProcessState = General.Messages.ExtensionError;
                            model.Categories = CategoryBLL.GetCategoriesForDropdown();
                            return View(model);
                        }
                        else
                        {
                            Bitmap userImage = new Bitmap(postedFile.InputStream);
                            Bitmap resizeImage = new Bitmap(userImage, 750, 422);
                            string uniqueNumber = Guid.NewGuid().ToString();
                            fileName = uniqueNumber + postedFile.FileName;
                            resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/postimage/" + fileName));
                            PostImageDTO dto = new PostImageDTO();
                            dto.ImagePath = fileName;
                            imageList.Add(dto);
                        }
                    }

                    model.PostImageList = imageList;
                }

                if(bll.UpdatePost(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            model = bll.GetPostById(model.ID);
            model.Categories = selectList;
            model.isUpdate = true;

            return View(model);
        }

        public JsonResult DeletePostImage(int ID)
        {
            string imagePath = bll.DeletePostImage(ID);

            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/postimage/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/postimage/" + imagePath));
                }
            }

            return Json("");
        }

        public JsonResult DeletePost(int ID)
        {
            List<PostImageDTO> imageList = bll.DeletePost(ID);

            foreach (var item in imageList)
            {
                if (item.ImagePath != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/postimage/" + item.ImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/postimage/" + item.ImagePath));
                    }
                }
            }

            return Json("");
        }

        public JsonResult GetCounts()
        {
            CountDTO dto = new CountDTO();
            dto = bll.GetCounts();
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
    }
}