using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class LogController : BaseController
    {
        LogBLL bll = new LogBLL();

        // GET: Admin/Log
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogList()
        {
            List<LogDTO> dto = new List<LogDTO>();
            dto = bll.GetLogs();
            return View(dto);
        }
    }
}