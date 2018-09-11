using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace Services
{
	public class WordCounterServices : IWordCounterServices
	{
		
		private readonly ILogger _logger;
		private readonly IHTMLParserManager _manager;
		public WordCounterServices(ILogger<IWordCounterServices> logger,IHTMLParserManager manager)
		{
			_manager = manager;
			_logger = logger;
		}
		public IWordCounterServices SetUrl(string url)
		{
			this._manager.SetUrl(url);
			return this;
		}
		
		public List<string> GetImages()
		{
			//throw new NotImplementedException();
			var imageTags = _manager.ParseImagesFromTags();
			return imageTags.ToList();
		}
		private IEnumerable<string> GetWords()
		{
			IEnumerable<string> words = null;
			var text = _manager.ParseTextFromTags();
			if (text != null && text.Count() != 0)
			{
				//Get word count from text
				 words = _manager.TextToWords(text);

			}
			return words;
		}
		public IEnumerable<KeyValuePair<string,int>> GetTopRecurringWords()
		{
			Dictionary<string, int> freq = null;
			var words = GetWords();

			//Do LINQ manipulation to get frequency of words and return dictionary
			freq= words.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

			return (from pair in freq
				   orderby pair.Value descending
				   select pair).Take(10) ;
		}

		public int GetWordCount()
		{
			var words = GetWords();
			if (words!=null && words.Count()!=0)
			{
			
				return words.Count();
			}
			return 0;
		}
	}
}
