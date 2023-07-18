using FitCookieAI_Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_Data.Entities.UserRelated
{
	public class PasswordRecoveryToken : BaseEntity
	{
		[Required]
		[MaxLength(5)]
		public string? Code { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public DateTime Start { get; set; } = DateTime.Now;

		[Required]
		public DateTime End { get; set; } = DateTime.Now.AddMinutes(10);

		[ForeignKey("UserId")]
		public User? ParentUser { get; set; }
	}
}
