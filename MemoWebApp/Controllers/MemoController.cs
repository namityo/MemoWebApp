using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MemoWebApp.Controllers
{
    public class MemoController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            // memoのリストを取得
            var di = new DirectoryInfo(".");
            var files = di.GetFiles("*.memo");

            return View(files);
        }

        public IActionResult New()
        {
            return View();
        }

        public IActionResult Create(string title, string text)
        {
            // ファイルを作成する
            using (var sw = new StreamWriter($"{title}.memo"))
            {
                sw.Write(text);
            }

            return View();
        }
    }
}
