using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using Library.Areas.Admin.Models;
using Library.Models;

namespace Library.Areas.Member.Controllers
{
    [Area("Member")]
    public class BookController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<BookController> _logger;
        public BookController(ILogger<BookController> logger, ILifetimeScope scope)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetBookData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<BookListModel>();
            return Json(model.GetPagedBooks(dataTableModel));
        }

    }
}
