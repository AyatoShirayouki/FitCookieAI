using FitCookieAI_Data.Entities.Others;
using FitCookieAI_Repository.Implementations.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_Repository.Implementations.Others
{
	public class GeneratedPlansRepository : BaseRepository<GeneratedPlan>
	{
		public GeneratedPlansRepository(MyUnitOfWork uow) : base(uow)
		{

		}
		public GeneratedPlansRepository() : base()
		{

		}
		protected override IQueryable<GeneratedPlan> CascadeInclude(IQueryable<GeneratedPlan> query)
		{
			return query.Include(c => c.ParentUser);
		}
	}
}
