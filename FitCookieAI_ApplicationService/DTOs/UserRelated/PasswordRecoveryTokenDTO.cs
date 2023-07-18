using FitCookieAI_Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_ApplicationService.DTOs.UserRelated
{
	public class PasswordRecoveryTokenDTO : BaseEntity
	{
		public string? Code { get; set; }
		public int UserId { get; set; }
		public DateTime Start { get; set; } = DateTime.Now;
		public DateTime End { get; set; } = DateTime.Now.AddMinutes(10);
	}
}
