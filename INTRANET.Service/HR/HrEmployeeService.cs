using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Common;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Model;
using INTRANET.Service.Interfaces;

namespace INTRANET.Service

{
    public class HrEmployeeService : BaseService<HrEmployee>, IHrEmployeeService
    {
        public HrEmployeeService(IHrEmployeeRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }

        public void Create(HrEmployee model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Update(HrEmployee model)
        {
            this._repository.Update(model);
            Save();
        }

        //use this method for index page
        //this will consume less memory 
        //as pagination + filtering + mapping to model will retrieve limited rows and columns, not all rows and columns       
        public IQueryable<HrEmployee> GetAllQueryable()
        {
            return this._repository.GetAny();
        }

        public void ChangeCvCompletionStatus(HrCvChangeCompletionStatusMode mode, int[] selectedEmployees)
        {
            //safety check
            if (mode == null) return;

            //no selection - do nothing
            if (mode == HrCvChangeCompletionStatusMode.SelectedEmployees
                && (selectedEmployees == null || !selectedEmployees.Any()))
                return;

            //initially all
            var employees = GetAllQueryable();

            //if needed - filter according to selection
            if (mode == HrCvChangeCompletionStatusMode.SelectedEmployees)
                employees = employees.Where(e => selectedEmployees.Contains(e.Id));

            foreach(var e in employees)
            {
                e.ComplietedRuCv = false;
                e.ComplietedUzCv = false;
                this._repository.Update(e);
            }

            Save();

        }

        public HrEmployee GetByEmail(string emailLogin)
        {
            return GetAllQueryable().FirstOrDefault(e => e.EmailLogin.Equals(emailLogin));
        }
    }
}
