using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    /// <summary>
    /// In-app notification for a specific user within a tenant.
    /// </summary>
    public class AppNotification
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Target user (null = broadcast to whole company)</summary>
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        /// <summary>Tenant this notification belongs to (null = platform-wide / SuperAdmin)</summary>
        public Guid? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        /// <summary>Icon class e.g. "bi bi-person-plus", "bi bi-credit-card"</summary>
        [MaxLength(60)]
        public string Icon { get; set; } = "bi bi-bell";

        /// <summary>Optional link to navigate to when notification is clicked</summary>
        [MaxLength(300)]
        public string? ActionUrl { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
