using Autofac;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace Hospital.Areas.Patient.Models
{
    public class PatientCreateModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        private IPatientService _patientService;
        private ILifetimeScope _scope;


        public PatientCreateModel()
        {

        }

        public PatientCreateModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _patientService = _scope.Resolve<IPatientService>();
        }

        internal async Task PatientCreate()
        {
            var pA = new PatientAdmition();
            pA.Name = Name;
            pA.Address = Address;

            //_patientService.PatientCreate(pA);
        }
    }
}
