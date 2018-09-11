using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class HTMLParserManager : IHTMLParserManager
	{
		private ILogger _logger;
		private IDocument _document;
		
		public HTMLParserManager(ILogger<HTMLParserManager> logger)
		{
			
			_logger = logger;

			
		}

		public IHTMLParserManager SetUrl(string url = null)
		{
			
			if (!String.IsNullOrEmpty(url))
				this._document = GetDocumentFromUrl(url).Result;
			else
				_document = null;
			return this;
		}

		/// <summary>
		/// Utility method to extract all text from a default set of tags(paragraph,span,H1,H2,H3,H4 )
		/// </summary>
		/// <returns>Enumberable collection of words</returns>
		public IEnumerable<string> ParseTextFromTags()
		{
			IEnumerable<string> text = null;

			//Extract all text from paragraph,span,H1,H2,H3,H4 

			if (_document != null)
			{
				//Get all text from designated tags
				text = _document.QuerySelectorAll("p,span,h1,h2,h3,h4").Select(d => d.TextContent);


			}

			return text;
		}

		/// <summary>
		/// Utility method to extract all image tags
		/// </summary>
		/// <returns>Enumberable collection of image tags</returns>
		public IEnumerable<string> ParseImagesFromTags()
		{
			IEnumerable<string> imageHtml = null;

			if (_document != null)
			{
				imageHtml = _document.QuerySelectorAll("img").Select(d => d.OuterHtml);
			}

			return imageHtml;
		}

		/// <summary>
		/// Validate URL
		/// </summary>
		/// <param name="url"></param>
		/// <returns>True/False - Valid URL </returns>
		public bool ValidateUrl(string url)
		{
			bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
				&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
			return result;
		}

		/// <summary>
		/// Method to get DOM Document from the url
		/// </summary>
		/// <param name="url"></param>
		/// <returns>Task of type IDocument</returns>
		private async Task<IDocument> GetDocumentFromUrl(string url)
		{
			if (ValidateUrl(url))
			{
				
				var config = Configuration.Default.WithDefaultLoader();
				
				var document = await BrowsingContext.New(config).OpenAsync(url);
				return document;
			}
			else
			{
				throw new Exception("Invalid Url");
			}
			
			
		}
		/// <summary>
		/// Borrowed from https://stackoverflow.com/questions/16725848/how-to-split-text-into-words
		/// </summary>
		/// <param name="text"></param>
		/// <returns>IEnumerable of words</returns>
		public IEnumerable<string> TextToWords(string text)
		{
			if(String.IsNullOrEmpty(text)) throw new ArgumentNullException("text");

			var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();

			//Split sentences/combined text into a word array (trimming all blanks and \n characters)
			var words = text.Split().Select(x => x.Trim(punctuation)).Select(y=>y.Trim('\n')).Where(t => !String.IsNullOrEmpty(t));
			return words;
		}

		/// <summary>
		/// Overload that accepts an enumerable of strings
		/// which is flattened to produce a string
		/// </summary>
		/// <param name="text"></param>
		/// <returns>IEnumerable of string </returns>
		public IEnumerable<string> TextToWords(IEnumerable<string> text)
		{
			if (text==null) throw new ArgumentNullException("text");
			List<string> flatString = new List<string>();
			foreach (var textItem in text.Where(t=>!String.IsNullOrEmpty(t)))
			{
				flatString.AddRange(textItem.Split(' ').Where(t => !String.IsNullOrEmpty(t)));
			}
			
			return TextToWords(string.Join(" ", flatString));
		}
	}
}
