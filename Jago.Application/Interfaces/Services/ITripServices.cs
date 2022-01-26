using FluentValidation.Results;
using Jago.Application.ViewModel;
using Jago.domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jago.Application.Services
{
    public interface ITripServices : IDisposable
    {
        IEnumerable<TripViewModel> GetAll();
        TripViewModel GetById(Guid id);
        IEnumerable<TripViewModel> GetAllBy(Func<Trip, bool> exp);
        ValidationResult Add(TripViewModel vm);
        ValidationResult Update(TripViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<PassengerViewModel> GetPax();
        IEnumerable<PaxListModel> GetPaxList();
        IEnumerable<TripViewModel> GetOrder();
    }
}
