using System;
using System.Collections.Generic;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace TestFramework.Common

{
    public abstract class AbstractPageContext
    {
        public abstract string pageurl {get;}
        private  IBrowserContext _context;
        public IPage _page {get;set;}

        public AbstractPageContext(IBrowserContext context)
        {
            _context = context;
            _context.SetDefaultTimeout(10000);
            _context.SetDefaultNavigationTimeout(10000);
            var t = Task.Run(async () => await Init());
            t.Wait();
        }

        //init POM with the page inside the context we can call it like new AbstractPageContext(context.Pages[nth])
        public AbstractPageContext(IPage page)
        {
            _page = page;
            _page.SetDefaultTimeout(10000);
            _page.SetDefaultNavigationTimeout(10000);
        }

        public async Task Init()
        {
            if (_context.Pages.Count < 1)
            {
                _page = await _context.NewPageAsync();
            }
            else
            {
                _page = _context.Pages[_context.Pages.Count - 1]; //init to last opened page
                // _page = await _context.NewPageAsync();
            }
            _page.SetDefaultTimeout(10000);
            _page.SetDefaultNavigationTimeout(10000);
        }

        

        public async Task GoToAsync(string url)
        {
            await _page.GotoAsync(url);
        }

        public async Task OpenPageAsync()
        {
            await _page.GotoAsync(pageurl);
        }

        public async Task<string> GetPageName()
        {
            string name = string.Empty;
            name = await _page.TitleAsync();
            return name;
        }

        public int CurrentTabIndex ()
        {
            //String currentTab = driver.CurrentWindowHandle;
            //List<string> tabs = new List<string>(driver.WindowHandles);
            //int index = tabs.IndexOf(currentTab);
            return 0;
        }

        public async Task Refresh() => await _page.ReloadAsync();

        public async Task Close() => await _page.CloseAsync();
        
    }
}