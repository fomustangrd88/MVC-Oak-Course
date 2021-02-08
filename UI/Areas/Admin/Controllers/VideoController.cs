using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {
        VideoBLL bll = new VideoBLL();

        // GET: Admin/Video
        public ActionResult AddVideo()
        {
            VideoDTO model = new VideoDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddVideo(VideoDTO model)
        {
            //<iframe width="560" height="315" src="https://www.youtube.com/embed/E7Voso411Vs" frameborder="0" allowfullscreen></iframe>
            //https://www.youtube.com/watch?v=E7Voso411Vs&ab_channel=ProgrammingwithMosh
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergeLink = "https://www.youtube.com/embed/";
                mergeLink += path;
                model.VideoPath = string.Format(@"<iframe width=""300"" height=""200"" src=""{0}"" frameborder=""0"" allowfullscreen></iframe>", mergeLink);

                if (bll.AddVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
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

        public ActionResult VideoList()
        {
            List<VideoDTO> model = new List<VideoDTO>();
            model = bll.GetVideoList();
            return View(model);
        }

        public ActionResult UpdateVideo(int id)
        {
            VideoDTO model = new VideoDTO();
            model = bll.GetVideoById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergeLink = "https://www.youtube.com/embed/";
                mergeLink += path;
                model.VideoPath = string.Format(@"<iframe width=""300"" height=""200"" src=""{0}"" frameborder=""0"" allowfullscreen></iframe>", mergeLink);

                if (bll.UpdateVideo(model))
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

        public JsonResult DeleteVideo(int ID)
        {
            bll.DeleteVideo(ID);
            return Json("");
        }
    }
}