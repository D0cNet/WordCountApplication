using System.Collections.Generic;

namespace Utilities
{
	public interface IHTMLParserManager
	{
		/// <summary>
		/// Utility method to extract all image tags
		/// </summary>
		/// <returns>Enumberable collection of image tags</returns>
		IEnumerable<string> ParseImagesFromTags();

		/// <summary>
		/// Utility method to extract all text from a default set of tags(paragraph,span,H1,H2,H3,H4 )
		/// </summary>
		/// <returns>Enumberable collection of words</returns>
		IEnumerable<string> ParseTextFromTags();

		/// <summary>
		/// Borrowed from https://stackoverflow.com/questions/16725848/how-to-split-text-into-words
		/// </summary>
		/// <param name="text"></param>
		/// <returns>IEnumerable of words</returns>
		IEnumerable<string> TextToWords(string text);

		/// <summary>
		/// Overload that accepts an enumerable of strings
		/// which is flattened to produce a string
		/// </summary>
		/// <param name="text"></param>
		/// <returns>IEnumerable of string </returns>
		IEnumerable<string> TextToWords(IEnumerable<string> text);

		/// <summary>
		/// Validate URL
		/// </summary>
		/// <param name="url"></param>
		/// <returns>True/False - Valid URL </returns>
		bool ValidateUrl(string url);


		IHTMLParserManager SetUrl(string url);
	}
}