using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IWordCounterServices
    {
		/// <summary>
		/// Method to get all Images
		/// </summary>
		/// <returns></returns>
		List<string> GetImages();

		/// <summary>
		/// Method to get total word count
		/// </summary>
		/// <returns></returns>
		int GetWordCount();

		/// <summary>
		/// Method to get Top 10 recurring words
		/// </summary>
		/// <returns></returns>
		IEnumerable<KeyValuePair<string, int>> GetTopRecurringWords();

		/// <summary>
		/// Builder method to set the url for Services
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		IWordCounterServices SetUrl(string url);
	}
}
