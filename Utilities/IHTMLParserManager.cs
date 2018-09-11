using System.Collections.Generic;

namespace Utilities
{
	public interface IHTMLParserManager
	{
		IEnumerable<string> ParseImagesFromTags();
		IEnumerable<string> ParseTextFromTags();
		IEnumerable<string> TextToWords(string text);
		IEnumerable<string> TextToWords(IEnumerable<string> text);

		bool ValidateUrl(string url);
		IHTMLParserManager SetUrl(string url);
	}
}