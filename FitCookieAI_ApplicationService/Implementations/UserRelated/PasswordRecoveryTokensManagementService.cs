using FitCookieAI_ApplicationService.DTOs.Others;
using FitCookieAI_ApplicationService.DTOs.UserRelated;
using FitCookieAI_Data.Entities.Others;
using FitCookieAI_Data.Entities.UserRelated;
using FitCookieAI_Repository.Implementations.Base;
using FitCookieAI_Repository.Implementations.Others;
using FitCookieAI_Repository.Implementations.UserRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_ApplicationService.Implementations.UserRelated
{
	public class PasswordRecoveryTokensManagementService
	{
		public static async Task<List<PasswordRecoveryTokenDTO>> GetAll()
		{
			using (MyUnitOfWork unitOfWork = new MyUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				PasswordRecoveryTokensRepository passwordRecoveryTokensRepo = new PasswordRecoveryTokensRepository(unitOfWork);
				List<PasswordRecoveryToken> PasswordRecoveryTokens = await passwordRecoveryTokensRepo.GetAll();

				List<PasswordRecoveryTokenDTO> paymentsDTO = new List<PasswordRecoveryTokenDTO>();

				if (PasswordRecoveryTokens != null)
				{
					foreach (var item in PasswordRecoveryTokens)
					{
						paymentsDTO.Add(new PasswordRecoveryTokenDTO
						{
							Id = item.Id,
							Code = item.Code,
							UserId = item.UserId,
							Start = item.Start,
							End = item.End
						});
					}

					unitOfWork.Commit();
				}
				else
				{
					unitOfWork.Rollback();
				}
				return paymentsDTO;
			}
		}

		public static async Task<PasswordRecoveryTokenDTO> GetById(int id)
		{
			using (MyUnitOfWork unitOfWork = new MyUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				PasswordRecoveryTokensRepository passwordRecoveryTokensRepo = new PasswordRecoveryTokensRepository(unitOfWork);
				PasswordRecoveryTokenDTO PasswordRecoveryTokenDTO = new PasswordRecoveryTokenDTO();

				PasswordRecoveryToken PasswordRecoveryToken = await passwordRecoveryTokensRepo.GetById(id);

				if (PasswordRecoveryToken != null)
				{
					PasswordRecoveryTokenDTO.Id = PasswordRecoveryToken.Id;
					PasswordRecoveryTokenDTO.Code = PasswordRecoveryToken.Code;
					PasswordRecoveryTokenDTO.UserId = PasswordRecoveryToken.UserId;
					PasswordRecoveryTokenDTO.Start = PasswordRecoveryToken.Start;
					PasswordRecoveryTokenDTO.End = PasswordRecoveryToken.End;

					unitOfWork.Commit();
				}
				else
				{
					unitOfWork.Rollback();
				}
				return PasswordRecoveryTokenDTO;
			}
		}

		public static async Task Save(PasswordRecoveryTokenDTO PasswordRecoveryTokenDTO)
		{
			using (MyUnitOfWork unitOfWork = new MyUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				PasswordRecoveryTokensRepository passwordRecoveryTokensRepo = new PasswordRecoveryTokensRepository(unitOfWork);
				PasswordRecoveryToken PasswordRecoveryToken = new PasswordRecoveryToken();

				if (PasswordRecoveryTokenDTO != null)
				{
					if (PasswordRecoveryTokenDTO.Id == 0)
					{
						PasswordRecoveryToken = new PasswordRecoveryToken
						{
							Code = PasswordRecoveryTokenDTO.Code,
							UserId = PasswordRecoveryTokenDTO.UserId,
							Start = PasswordRecoveryTokenDTO.Start,
							End = PasswordRecoveryTokenDTO.End
						};
					}
					else
					{
						PasswordRecoveryToken = new PasswordRecoveryToken
						{
							Id = PasswordRecoveryTokenDTO.Id,
							Code = PasswordRecoveryTokenDTO.Code,
							UserId = PasswordRecoveryTokenDTO.UserId,
							Start = PasswordRecoveryTokenDTO.Start,
							End = PasswordRecoveryTokenDTO.End
						};
					}

					await passwordRecoveryTokensRepo.Save(PasswordRecoveryToken);
					unitOfWork.Commit();
				}
				else
				{
					unitOfWork.Rollback();
				}
			}
		}

		public static async Task Delete(int id)
		{
			using (MyUnitOfWork unitOfWork = new MyUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				PasswordRecoveryTokensRepository passwordRecoveryTokensRepo = new PasswordRecoveryTokensRepository(unitOfWork);

				PasswordRecoveryToken PasswordRecoveryToken = await passwordRecoveryTokensRepo.GetById(id);

				if (PasswordRecoveryToken != null)
				{
					await passwordRecoveryTokensRepo.Delete(PasswordRecoveryToken);
					unitOfWork.Commit();
				}
				else
				{
					unitOfWork.Rollback();
				}
			}
		}
	}
}
