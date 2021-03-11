using JobSearch.Models.Core;

namespace JobSearch.DTO.Core
{
	public class PagedRequestDTO : PagedRequestModel
	{
		public PagedRequestDTO() : base()
		{
		}
	}

	public class PagedResponseDTO<T> : PagedResponseModel<T>
	{
		public PagedResponseDTO()
		{
		}
	}
}
