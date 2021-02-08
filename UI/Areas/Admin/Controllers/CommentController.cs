using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        PostBLL bll = new PostBLL();

        // GET: Admin/Comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllComments()
        {
            List<CommentDTO> commentList = new List<CommentDTO>();
            commentList = bll.GetAllComments();
            return View(commentList);
        }

        public ActionResult UnapprovedComments()
        {
            List<CommentDTO> commentList = new List<CommentDTO>();
            commentList = bll.GetComments(); 
            return View(commentList);
        }

        public ActionResult ApproveCommentAll(int ID)
        {
            bll.ApproveComment(ID);
            return RedirectToAction("AllComments", "Comment");
        }

        public ActionResult ApproveComment(int ID)
        {
            bll.ApproveComment(ID);
            return RedirectToAction("UnapprovedComments", "Comment");
        }

        public JsonResult DeleteComment(int ID)
        {
            bll.DeleteComment(ID);
            return Json("");
        }
    }
}