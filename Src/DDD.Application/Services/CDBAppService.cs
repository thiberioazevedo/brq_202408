using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DDD.Application.Interfaces;
using DDD.Application.ViewModels;
using DDD.Domain.Models;

namespace DDD.Application.Services
{
    public class CDBAppService : ICDBAppService
    {
        IMapper _mapper;
        public CDBAppService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public CDBViewModel Calcular(decimal valorInicial, int meses)
        {
            var cDB = new CDB(valorInicial);

            cDB.SetTaxas();

            for (var i = 2; i <= meses; ++i)
            {
                cDB = new CDB(cDB);

                cDB.SetTaxas();
            }

            cDB.SetImposto();

            return _mapper.Map<CDBViewModel>(cDB);
        }

        public ICollection<CDBViewModel> CalcularCollection(decimal valorInicial, int meses)
        {
            var cDB = Calcular(valorInicial, meses);

            var cDBList = new List<CDBViewModel>();

            var cDB_ = cDB;

            while (cDB_ != null)
            {
                cDBList.Add(_mapper.Map<CDBViewModel>(cDB_));
                cDB_ = cDB_.CBDAnterior;
            }

            cDBList = cDBList.OrderBy(i => i.Mes).ToList();

            return cDBList;
        }
    }
}
