using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        LayoutBLL layoutBLL = new LayoutBLL();
        GeneralBLL generalBLL = new GeneralBLL();
        PostBLL postBLL = new PostBLL();
        ContactBLL contactBLL = new ContactBLL();

        // GET: Home
        public ActionResult Index()
        {
            HomeLayoutDTO layoutDTO = new HomeLayoutDTO();
            layoutDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layoutDTO;
            GeneralDTO generalDTO = new GeneralDTO();
            generalDTO = generalBLL.GetAllPosts();
            return View(generalDTO);
        }

        public ActionResult CategoryPostList(string CategoryName)
        {
            HomeLayoutDTO layoutDTO = new HomeLayoutDTO();
            layoutDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layoutDTO;
            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetCategoryPostList(CategoryName);
            return View(dto);
        }

        public ActionResult PostDetail(int ID)
        {
            HomeLayoutDTO layoutDTO = new HomeLayoutDTO();
            layoutDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layoutDTO;
            GeneralDTO generalDTO = new GeneralDTO();
            generalDTO = generalBLL.GetPostDetailPageItemsWithID(ID);
            return View(generalDTO);
        }

        [HttpPost]
        public ActionResult PostDetail(GeneralDTO model)
        {
            if(model.Name != null && model.Email != null && model.Message != null)
            {
                if (postBLL.AddComment(model))
                {
                    ViewData["CommentState"] = "Success";
                    ModelState.Clear();
                }else
                {
                    ViewData["CommentState"] = "Error";
                }
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }

            HomeLayoutDTO layoutDTO = new HomeLayoutDTO();
            layoutDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layoutDTO;
            GeneralDTO generalDTO = new GeneralDTO();
            model = generalBLL.GetPostDetailPageItemsWithID(model.PostID);

            return View(model);
        }

        [Route("contactus")]
        public ActionResult ContactUs()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetContactPageItems();
            return View(dto);
        }

        [Route("contactus")]
        [HttpPost]
        public ActionResult ContactUs(GeneralDTO model)
        {
            if (model.Name != null && model.Subject != null && model.Email != null && model.Message != null)
            {
                if (contactBLL.AddContact(model))
                {
                    ViewData["CommentState"] = "Success";
                }
                else
                {
                    ViewData["CommentState"] = "Error";
                }
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetContactPageItems();
            return View(dto);
        }

        [Route("search")]
        [HttpPost]
        public ActionResult Search(GeneralDTO model)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetSearchPosts(model.SearchText);
            return View(dto);
        }
    }
}