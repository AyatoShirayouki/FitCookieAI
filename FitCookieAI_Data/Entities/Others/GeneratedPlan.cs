using FitCookieAI_Data.Entities.Base;
using FitCookieAI_Data.Entities.UserRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_Data.Entities.Others
{
	public class GeneratedPlan : BaseEntity
	{
		[Required]
		public int UserId { get; set; }

		[Required]
		[MaxLength(100)]
		public string? Plan { get; set; }

		[Required]
		public DateTime? CreatedAt { get; set; } = DateTime.Now;

		[ForeignKey("UserId")]
		public User? ParentUser { get; set; }
	}
}
