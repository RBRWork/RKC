﻿using AppCache;
using BL.Helper;
using BL.Service;
using BL.Services;
using RKC.Extensions;
using BE.PersData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RKC.Controllers
{
    [Authorize]
    public class PersonalDataController : Controller
    {
        private readonly IPersonalData _personalData;
        private readonly Ilogger _logger;
        private readonly IGeneratorDescriptons _generatorDescriptons;
        private readonly ICacheApp _cacheApp;
        public readonly IFlagsAction _flagsAction;
        public readonly IReadFileBank _readFileBank;
        public PersonalDataController(IPersonalData personalData, Ilogger logger, IGeneratorDescriptons generatorDescriptons,
            ICacheApp cacheApp, IFlagsAction flagsAction, IReadFileBank readFileBank)
        {
            _personalData = personalData;
            _logger = logger;
            _generatorDescriptons = generatorDescriptons;
            _cacheApp = cacheApp;
            _flagsAction = flagsAction;
            _readFileBank = readFileBank;
        }
        // GET: PersonalData
        public ActionResult DetailedInformPersData(string FULL_LIC)
        {
            try
            {
                if (_cacheApp.Lock(User.Identity.GetFIO(), nameof(DetailedInformPersData) + FULL_LIC))
                {
                    ViewBag.User = _cacheApp.GetValue(nameof(DetailedInformPersData) + FULL_LIC);
                    ViewBag.IsLock = true;
                }
                else ViewBag.IsLock = false;
                if (ViewBag.IsLock == false)
                {
                    ViewBag.IsLock = _flagsAction.GetAction(nameof(DetailedInformPersData));
                }
                var Result = _personalData.GetInfoPersData(FULL_LIC);
                if (Result.Count() > 0)
                {
                    return View(Result);
                }
                else
                {
                    if (Result.Count() == 0)
                    {
                        ViewBag.FULL_LIC = FULL_LIC;
                    }
                    return View(Result);
                }
            }
            catch (Exception ex)
            {
                return Redirect("/Home/ResultEmpty?Message=" + ex.Message);
            }
        }
        [HttpGet]
        public ActionResult clearCache(string Page)
        {
            _cacheApp.Delete(User.Identity.GetFIO(), Page);
            return null;
        }
        [HttpPost]
        public ActionResult SaveFile(HttpPostedFileBase FileLoad, string NameFile,string Lic,int idPersData,string Fio)
        {
            return Json(new { result = _personalData.saveFile(ConverToBytes(FileLoad), idPersData, Fio, Lic, FileLoad.FileName.Split('.')[1], NameFile, User.Identity.GetFIO()),
                JsonRequestBehavior.AllowGet });
        }
        [HttpGet]
        public ActionResult DownLoadFile(int Id)
        {
            var Result = _personalData.DownLoadFile(Id);
            return File(Result.FileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Result.FileName);
        }
        [HttpGet]
        public ActionResult DeleteFile(int Id)
        {
            _personalData.DeleteFile(Id,User.Identity.GetFIO());
            return null;
        }
        public static byte[] ConverToBytes(HttpPostedFileBase file)
        {
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }
            return fileData;
        }
        public ActionResult SavePersonalData(PersDataModel persDataModel)
        {
            _personalData.SavePersonalData(persDataModel, User.Identity.GetFIO());
            return null;
        }
        public ActionResult AddPersData(PersDataModel persDataModel)
        {
            _personalData.AddPersData(persDataModel, User.Identity.GetFIO());
            return null;
        }
        public ActionResult HistoryEdit(int idPersData)
        {
            return PartialView(_personalData.GetHistory(idPersData));
        }
        [HttpGet]
        public ActionResult EditMain(int idPersData)
        {
            _personalData.MakeToMain(idPersData);
            return null;
        }
        public ActionResult FormAddPers(string FULL_LIC)
        {
            ViewBag.FULL_LIC = FULL_LIC;
            return PartialView();
        }
        public ActionResult DeletePersonalData(int IdPersData)
        {
            _personalData.DeletePers(IdPersData,User.Identity.GetFIO());
            return null;
        }
        public ActionResult DetailedInformPersDelete(string FULL_LIC)
        {
            ViewBag.FULL_LIC = FULL_LIC;
            return View(_personalData.GetInfoPersDataDelete(FULL_LIC));
        }
    }
}