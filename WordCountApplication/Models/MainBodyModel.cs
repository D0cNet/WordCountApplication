using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCountApplication.Models
{
    public class MainBodyModel
    {
		public List<string> ImageCarouselItems { get; set; }

		public int WordCount { get; set; }

		public List<KeyValuePair<string, int>> TopTenRecurringWords { set; get; }
	}
}
