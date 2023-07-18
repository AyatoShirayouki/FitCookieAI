using FitCookieAI_Data.Entities.UserRelated;
using FitCookieAI_Repository.Implementations.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_Repository.Implementations.UserRelated
{
	public class PasswordRecoveryTokensRepository : BaseRepository<PasswordRecoveryToken>
	{
		public PasswordRecoveryTokensRepository(MyUnitOfWork uow) : base(uow)
		{

		}
		public PasswordRecoveryTokensRepository() : base()
		{

		}
		protected override IQueryable<PasswordRecoveryToken> CascadeInclude(IQueryable<PasswordRecoveryToken> query)
		{
			return query.Include(c => c.ParentUser);
		}
	}
}
