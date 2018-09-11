using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IWordCounterServices
    {

		List<string> GetImages();
		int GetWordCount();
		IEnumerable<KeyValuePair<string, int>> GetTopRecurringWords();
		IWordCounterServices SetUrl(string url);
	}
}
