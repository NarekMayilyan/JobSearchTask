using JobSearch.Common.Enums;
using System.Collections.Generic;

namespace JobSearch.Models.Core
{
    public class PagedRequestModel
    {
		public PagedRequestModel()
		{
			Page = new PageModel();
			Sort = new SortModel();
		}

		public PageModel Page { get; set; }
		public SortModel Sort { get; set; }
	}

	public class PageModel
	{
		public int Number { get; set; }
		public int Count { get; set; }
	}

	public class SortModel
	{
		public string Member { get; set; }
		public Sorting? Direction { get; set; }
	}

	public class PagedResponseModel<T>
	{
		public PagedResponseModel()
		{
			Data = new List<T>();
		}

		public IEnumerable<T> Data { get; set; }
		public int Count { get; set; }
	}
}
