using FitCookieAI_Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_Data.Entities.UserRelated
{
	public class User : Person
	{
        [Required]
        public DateTime DOJ { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}
