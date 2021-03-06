﻿using AngleSharp;
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

		
		public IEnumerable<string> ParseTextFromTags()
		{
			IEnumerable<string> text = null;

			//Extract all text from paragraph,span,H1,H2,H3,H4 

			if (_document != null)
			{
				//Get all text from designated tags
				text = _document.QuerySelectorAll("p,span,h1,h2,h3,h4,title").Select(d => d.TextContent);


			}

			return text;
		}

	
		public IEnumerable<string> ParseImagesFromTags()
		{
			IEnumerable<string> imageHtml = null;

			if (_document != null)
			{
				imageHtml = _document.QuerySelectorAll("img").Select(d => d.OuterHtml);
			}

			return imageHtml;
		}

		
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
				if (String.IsNullOrEmpty(document.Body.InnerHtml )) throw new Exception("No Data");
				return document;
			}
			else
			{
				throw new Exception("Invalid Url");
			}
			
			
		}
		
		public IEnumerable<string> TextToWords(string text)
		{
			if(String.IsNullOrEmpty(text)) throw new ArgumentNullException("text");

			var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();

			//Split sentences/combined text into a word array (trimming all blanks and \n characters)
			var words = text.Split().Select(x => x.Trim(punctuation)).Select(y=>y.Trim('\n')).Where(t => !String.IsNullOrEmpty(t));
			return words;
		}

		
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
