using System.Collections.Generic;
using TestFramework.Common;
using Microsoft.Playwright;

namespace TestFramework.PageObjects
{
    public class GoogleSearchResult : AbstractPage
    {
        private IPage _page;

        public GoogleSearchResult(IPage page) : base(page)
        {
            _page = page;
        }

        public override string pageurl 
        {
            get
            {
                return _page.Url;
            }
        }
        public ILocator SearchResultContainer
        {
            get
            {
                return _page.Locator("#search");
            }
        }

        public ILocator Links 
        {
            get
            {
                return SearchResultContainer.Locator("a");
            }
        }

        public ILocator GetLink(int i)
        {
            return Links.Locator($"nth={i}"); //narrow down to the n-th element
        }
    }
}