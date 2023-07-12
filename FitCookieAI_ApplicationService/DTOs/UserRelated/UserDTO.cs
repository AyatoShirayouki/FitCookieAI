using FitCookieAI_Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_ApplicationService.DTOs.UserRelated
{
	public class UserDTO : Person
	{
        public DateTime DOJ { get; set; } = DateTime.Now;
        public string? PhoneNumber { get; set; }
    }
}
