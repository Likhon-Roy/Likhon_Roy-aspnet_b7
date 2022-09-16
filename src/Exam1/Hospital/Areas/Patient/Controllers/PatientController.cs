using Microsoft.AspNetCore.Mvc;
using Autofac;
using Hospital.Areas.Patient.Models;

namespace Hospital.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<PatientController> _logger;
        public PatientController(ILogger<PatientController> logger, ILifetimeScope scope)
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
            PatientCreateModel model = _scope.Resolve<PatientCreateModel>();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientCreateModel model)
        {
            if(ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.PatientCreate();
            }
            return View(model);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
