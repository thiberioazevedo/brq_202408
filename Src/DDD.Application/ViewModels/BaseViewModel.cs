using System.ComponentModel.DataAnnotations;
using DDD.Domain.Core.Models;

namespace DDD.Application.ViewModels
{
    public class BaseViewModel<Model> where Model : Entity
    {
        [Key]
        public long? Id { get; set; }
    }
}
