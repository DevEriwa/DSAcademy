using Logic.IHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSAcademy.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly INotificationHelper _notificationHelper;
        private readonly IUserHelper _userHelper;

        public NotificationsController(INotificationHelper notificationHelper, IUserHelper userHelper)
        {
            _notificationHelper = notificationHelper;
            _userHelper = userHelper;
        }

        // Called by the notification bell via AJAX to load the dropdown list
        [HttpGet]
        public JsonResult GetUnread()
        {
            var user = _userHelper.FindByEmailAsync(User.Identity!.Name!).Result;
            if (user == null) return Json(new { count = 0, items = new object[] { } });

            var items = _notificationHelper.GetUnread(user.Id)
                .Select(n => new
                {
                    n.Id,
                    n.Title,
                    n.Message,
                    n.Icon,
                    n.ActionUrl,
                    n.IsRead,
                    Date = n.DateCreated.ToString("dd MMM HH:mm")
                });

            return Json(new { count = _notificationHelper.GetUnreadCount(user.Id), items });
        }

        // Mark a single notification as read
        [HttpPost]
        public async Task<JsonResult> MarkRead(int id)
        {
            await _notificationHelper.MarkReadAsync(id);
            return Json(new { ok = true });
        }

        // Mark all as read
        [HttpPost]
        public async Task<JsonResult> MarkAllRead()
        {
            var user = _userHelper.FindByEmailAsync(User.Identity!.Name!).Result;
            if (user != null) await _notificationHelper.MarkAllReadAsync(user.Id);
            return Json(new { ok = true });
        }
    }
}
