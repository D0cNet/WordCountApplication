using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WordCountApplication.Models
{
    public class IndexURLParameter
    {
		[Required]
		[DataType(DataType.Url)]
		public string url { get; set; }

		public List<string> ImageCarouselItems { get; set; }

		public int WordCount { get; set; }

		public List<KeyValuePair<string, int>> TopTenRecurringWords { set; get; }
	}
}
