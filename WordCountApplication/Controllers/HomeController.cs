using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using WordCountApplication.Models;

namespace WordCountApplication.Controllers
{
    public class HomeController : Controller
    {
		private ILogger _logger;
		private IWordCounterServices _wordSvc;
		public HomeController(ILogger<HomeController> logger, IWordCounterServices wordSvc)
		{
			_logger = logger;
			_wordSvc = wordSvc;
		}

		/// <summary>
		/// Default Index Action
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// Index Action, that take HttpPost and populates model with data.
		/// Action Can also be called via Ajax 
		/// </summary>
		/// <param name="param"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Index(IndexURLParameter param)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var man = _wordSvc.SetUrl(param.url);
					var images = man.GetImages();
					var wordCount = man.GetWordCount();
					var recurringWords = man.GetTopRecurringWords();

					param.ImageCarouselItems = images;
					param.WordCount = wordCount;
					param.TopTenRecurringWords = recurringWords.ToList();

					return View(param);
				}
				else
				{
					return View();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return BadRequest(ex.Message);
			
			}
		}

		

    }
}
