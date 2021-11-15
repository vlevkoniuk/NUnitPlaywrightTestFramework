using System.Collections.Generic;
using System.Threading.Tasks;
using TestFramework.Common;
using Microsoft.Playwright;


namespace TestFramework.PageObjects
{
    public class GoogleSearchPageContext : AbstractPageContext
    {
        public override string pageurl 
        {
            get
            {
                return _url;
            }
        }
        //private IPage _page;
        private IBrowserContext _context;
        private string _url = "https://google.com.ua";

        public GoogleSearchPageContext(IBrowserContext context) : base(context)
        {
            _context = context;
            //Task.Run(async () => _page = await _context.NewPageAsync());
        }

        public ILocator SearchString 
        {
            get
            {
                return _page.Locator("input[name=q]");
            }
        }

        public ILocator SearchButton 
        {
            get
            {
                return _page.Locator(":nth-match(:text(\"Пошук Google\"), 2)"); //2nd match (:nth-match(what,2))
            }
        }

        public async Task SearchAsync (string searchstring)
        {
            await SearchString.FillAsync(searchstring);
            await SearchButton.ClickAsync();
        }
    }
}