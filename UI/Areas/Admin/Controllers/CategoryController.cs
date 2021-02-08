using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryBLL bll = new CategoryBLL();

        // GET: Admin/Category
        public ActionResult AddCategory()
        {
            CategoryDTO model = new CategoryDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddCategory(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new CategoryDTO();
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
            return View(model);
        }

        public ActionResult CategoryList()
        {
            List<CategoryDTO> model = new List<CategoryDTO>();
            model = bll.GetCategoryList();
            return View(model);
        }

        public ActionResult UpdateCategory(int ID)
        {
            CategoryDTO model = new CategoryDTO();
            model = bll.GetCategoryById(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateCategory(model))
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

            return View(model);
        }

        public JsonResult DeleteCategory(int ID)
        {
            List<PostImageDTO> imageList = bll.DeleteCategory(ID);

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
    }
}