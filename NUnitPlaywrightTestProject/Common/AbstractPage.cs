using System;
using System.Collections.Generic;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Framework.Common

{
    public abstract class AbstractPage
    {
        public abstract string pageurl {get;}
        private readonly IPage _page;

        public AbstractPage(IPage page)
        {
            _page = page;
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