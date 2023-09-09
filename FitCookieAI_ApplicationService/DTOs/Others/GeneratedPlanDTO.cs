using FitCookieAI_Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_ApplicationService.DTOs.Others
{
	public class GeneratedPlanDTO : BaseEntity
	{
		public int UserId { get; set; }
		public string? Plan { get; set; }
		public DateTime? CreatedAt { get; set; } = DateTime.Now;
	}
}
