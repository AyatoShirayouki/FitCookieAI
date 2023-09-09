using FitCookieAI_ApplicationService.DTOs.Others;
using FitCookieAI_Data.Entities.Others;
using FitCookieAI_Repository.Implementations.Base;
using FitCookieAI_Repository.Implementations.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_ApplicationService.Implementations.Others
{
	public class GeneratedPlansManagementService
	{
		public static async Task<List<GeneratedPlanDTO>> GetAll()
		{
			using (MyUnitOfWork unitOfWork = new MyUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				GeneratedPlansRepository GeneratedPlansRepo = new GeneratedPlansRepository(unitOfWork);
				List<GeneratedPlan> GeneratedPlans = await GeneratedPlansRepo.GetAll();

				List<GeneratedPlanDTO> GeneratedPlansDTO = new List<GeneratedPlanDTO>();

				if (GeneratedPlans != null)
				{
					foreach (var item in GeneratedPlans)
					{
						GeneratedPlansDTO.Add(new GeneratedPlanDTO
						{
							Id = item.Id,
							Plan = item.Plan,
							CreatedAt = item.CreatedAt,
							UserId = item.UserId
						});
					}

					unitOfWork.Commit();
				}
				else
				{
					unitOfWork.Rollback();
				}
				return GeneratedPlansDTO;
			}
		}

		public static async Task<GeneratedPlanDTO> GetById(int id)
		{
			using (MyUnitOfWork unitOfWork = new MyUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				GeneratedPlansRepository GeneratedPlansRepo = new GeneratedPlansRepository(unitOfWork);
				GeneratedPlanDTO GeneratedPlanDTO = new GeneratedPlanDTO();

				GeneratedPlan GeneratedPlan = await GeneratedPlansRepo.GetById(id);

				if (GeneratedPlan != null)
				{
					GeneratedPlanDTO.Id = GeneratedPlan.Id;
					GeneratedPlanDTO.Plan = GeneratedPlan.Plan;
					GeneratedPlanDTO.CreatedAt = GeneratedPlan.CreatedAt;
					GeneratedPlanDTO.UserId = GeneratedPlan.UserId;

					unitOfWork.Commit();
				}
				else
				{
					unitOfWork.Rollback();
				}
				return GeneratedPlanDTO;
			}
		}

		public static async Task Save(GeneratedPlanDTO GeneratedPlanDTO)
		{
			using (MyUnitOfWork unitOfWork = new MyUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				GeneratedPlansRepository GeneratedPlansRepo = new GeneratedPlansRepository(unitOfWork);
				GeneratedPlan GeneratedPlan = new GeneratedPlan();

				if (GeneratedPlanDTO != null)
				{
					if (GeneratedPlanDTO.Id == 0)
					{
						GeneratedPlan = new GeneratedPlan
						{
							Plan = GeneratedPlanDTO.Plan,
							CreatedAt = GeneratedPlanDTO.CreatedAt,
							UserId = GeneratedPlanDTO.UserId
						};
					}
					else
					{
						GeneratedPlan = new GeneratedPlan
						{
							Id = GeneratedPlanDTO.Id,
							Plan = GeneratedPlanDTO.Plan,
							CreatedAt = GeneratedPlanDTO.CreatedAt,
							UserId = GeneratedPlanDTO.UserId
						};
					}

					await GeneratedPlansRepo.Save(GeneratedPlan);
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

				GeneratedPlansRepository GeneratedPlansRepo = new GeneratedPlansRepository(unitOfWork);

				GeneratedPlan GeneratedPlan = await GeneratedPlansRepo.GetById(id);

				if (GeneratedPlan != null)
				{

					await GeneratedPlansRepo.Delete(GeneratedPlan);
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
