using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : BaseController
    {
        AddressBLL bll = new AddressBLL();

        // GET: Admin/Address
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAddress()
        {
            AddressDTO model = new AddressDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddAddress(model))
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
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            AddressDTO newModel = new AddressDTO();
            return View(newModel);
        }

        public ActionResult AddressList()
        {
            List<AddressDTO> model = new List<AddressDTO>();
            model = bll.GetAddresses();
            return View(model);
        }

        public ActionResult UpdateAddress(int ID)
        {
            AddressDTO model = new AddressDTO();
            model = bll.GetAddressByID(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {

                if (bll.UpdateAddress(model))
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

        public JsonResult DeleteAddress(int ID)
        {
            bll.DeleteAddress(ID);
            return Json("");
        }
    }
}