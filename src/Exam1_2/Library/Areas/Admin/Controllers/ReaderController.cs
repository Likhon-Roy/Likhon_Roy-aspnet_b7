using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using Library.Areas.Admin.Models;
using Library.Models;

namespace Library.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReaderController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<ReaderController> _logger;
        public ReaderController(ILogger<ReaderController> logger, ILifetimeScope scope)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ReaderCreateModel model = _scope.Resolve<ReaderCreateModel>();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReaderCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.CreateReader();
            }
            return View(model);
        }

        public JsonResult GetReaderData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<ReaderListModel>();
            return Json(model.GetPagedBooks(dataTableModel));
        }

    }
}
