using System;
using System.Collections.Generic;
using DDD.Application.ViewModels;
using DDD.Domain.Core.Models;

namespace DDD.Application.Interfaces
{
    public interface IBaseAppService<T> : IDisposable where T : Entity
    {
        void Add(BaseViewModel<T> viewModel);
        void Update(BaseViewModel<T> viewModel);
        BaseViewModel<T> GetById(int id);
        IEnumerable<BaseViewModel<T>> Update(IEnumerable<BaseViewModel<T>> viewModelEnumerable);
        IEnumerable<BaseViewModel<T>> GetAll();

    }
}
