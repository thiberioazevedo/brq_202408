using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DDD.Application.Interfaces;
using DDD.Application.ViewModels;
using DDD.Domain.Core.Models;
using DDD.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DDD.Application.Services
{
    public class BaseAppService<T> : IBaseAppService<T> where T : Entity
    {
        internal readonly IRepository<T> repository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public BaseAppService(IRepository<T> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.repository = repository;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public void Add(BaseViewModel<T> viewModel)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual BaseViewModel<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<BaseViewModel<T>> Update(IEnumerable<BaseViewModel<T>> viewModelEnumerable)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(BaseViewModel<T> viewModel)
        {
            throw new NotImplementedException();
        }

        Dictionary<string, List<string>> GetQueryParameterDictionary()
        {
            var queryString = httpContextAccessor.HttpContext.Request.QueryString.Value;

            if (!queryString.Contains("?"))
                return new Dictionary<string, List<string>>();

            queryString = queryString.Substring(queryString.IndexOf('?') + 1);

            return queryString.Split("&")
                              .Select(i => i.Split("="))
                              .Where(i => i.Count() == 2)
                              .Select(i => new { Key = i[0], Value = i[1] })
                              .GroupBy(i => i.Key)
                              .ToDictionary(i => i.Key, i => i.Select(s => s.Value).ToList());
        }

        public IEnumerable<BaseViewModel<T>> GetAll()
        {
            var queryable = repository.GetAll(GetQueryParameterDictionary());
            return ModelToViewModelEnumerable(queryable);
        }

        internal virtual IEnumerable<BaseViewModel<T>> ModelToViewModelEnumerable(IQueryable<T> queryable)
        {
            throw new NotImplementedException();
            //return mapper.Map<IEnumerable<BaseViewModel<T>>>(queryable);
        }
    }
}
