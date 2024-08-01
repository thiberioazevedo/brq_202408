using DDD.Application.ViewModels;
using System.Collections.Generic;
using DDD.Domain.Models;

namespace DDD.Application.Interfaces
{
    public interface ICDBAppService
    {
        CDBViewModel Calcular(decimal valorInicial, int meses);
        ICollection<CDBViewModel> CalcularCollection(decimal valorInicial, int meses);
    }
}
