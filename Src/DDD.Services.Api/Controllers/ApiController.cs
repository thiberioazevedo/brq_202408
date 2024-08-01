using DDD.Application.Interfaces;
using DDD.Application.ViewModels;
using DDD.Domain.Core.Models;
using DDD.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Services.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    private readonly DomainNotificationHandler _notifications;

    protected ApiController(INotificationHandler<DomainNotification> notifications)
    {
        _notifications = (DomainNotificationHandler)notifications;
    }

    protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

    protected bool IsValidOperation()
    {
        return (!_notifications.HasNotifications());
    }

    protected new IActionResult Response(object result = null)
    {
        if (IsValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifications.GetNotifications().Select(n => n.Message)
        });
    }

    protected new IActionResult ResponseCollection<T>(IEnumerable<BaseViewModel<T>> baseViewModelCollection) where T : Entity
    {
        var hasSuccess = false;
        var data = new List<object>();
        var notificationList = new List<DomainNotification>();

        foreach (var baseViewModel in baseViewModelCollection)
        {
            var notificationIdList = _notifications.GetNotifications().Where(i => i.Id == baseViewModel.Id).ToList();
            notificationList.AddRange(notificationIdList);
            data.Add(new
            {
                success = !notificationIdList.Any(),
                data = baseViewModel,
                erros = notificationIdList.Select(n => n.Message)
            });

            if (!hasSuccess && notificationIdList.Count() == 0)
                hasSuccess = true;
        }

        var otherNotificationList = _notifications.GetNotifications().Where(i => !notificationList.Any(n => n.DomainNotificationId == i.DomainNotificationId));

        var response = new
        {
            success = hasSuccess,
            data,
            erros = otherNotificationList.Select(n => n.Message)
        };

        if (hasSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    protected void NotifyModelStateErrors()
    {
        var erros = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var erro in erros)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(string.Empty, erroMsg);
        }
    }

    protected void NotifyError(string code, string message)
    {
    }

    protected void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            NotifyError(result.ToString(), error.Description);
        }
    }
}
