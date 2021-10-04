using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace MSSAProject.Services
{
    public class PaginationTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("aria-label", "Page navigation");
            output.Content.SetHtmlContent(AddPageContent());
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRange { get; set; }
        public string PageFirst { get; set; }
        public string PageLast { get; set; }
        public string PageTarget { get; set; }

        private string AddPageContent()
        {
            InitDefaults();
            var content = new StringBuilder();
            InitPaginationHtml(content);

            if (PageNumber <= PageRange)
            {
                PageNumberUnderPageRange(content);
            }
            else if (PageNumber > PageRange && PageNumber < PageCount - PageRange)
            {
                PageNumberBetweenPageRange(content);
            }
            else
            {
                PageNumberUpperPageRange(content);
            }

            ClosingHtml(content);

            return content.ToString();
        }

        private void ClosingHtml(StringBuilder content)
        {
            //378.  Create an if statement for similar to pagefirst
            if(PageNumber != PageCount)
            {
                content.Append($"<li class='page-item'><a class='page-link' href='{PageTarget}?p={PageCount}'>{PageLast}</a></li>");
            }
            content.Append(" </ul");
            //380.  Go back to ProductController and create a Detail method for products
        }

        private void PageNumberUpperPageRange(StringBuilder content)
        {
            for (int currentPage = PageCount - (2 * PageRange); currentPage < PageCount + 1; currentPage++)
            {
                if (currentPage < 1 || currentPage > PageCount)
                {
                    continue;
                }
                var active = currentPage == PageNumber ? "active" : "";
                content.Append($"<li class='page-item {active}'><a class='page-link'href='{PageTarget}?p={currentPage}'>{currentPage}</a></li>");
            }
        }

        private void PageNumberBetweenPageRange(StringBuilder content)
        {
            for (int currentPage = PageNumber - PageRange; currentPage < PageNumber + PageRange; currentPage++)
            {
                if (currentPage < 1 || currentPage > PageCount)
                {
                    continue;
                }
                var active = currentPage == PageNumber ? "active" : "";
                content.Append($"<li class='page-item {active}'><a class='page-link'href='{PageTarget}?p={currentPage}'>{currentPage}</a></li>");
            }
        }

        private void PageNumberUnderPageRange(StringBuilder content)
        {
            for (int currentPage = 1; currentPage < 2 * PageRange + 1; currentPage++)
            {
                if (currentPage < 1 || currentPage > PageCount)
                {
                    continue;
                }
                var active = currentPage == PageNumber ? "active" : "";
                content.Append($"<li class='page-item {active}'><a class='page-link'href='{PageTarget}?p={currentPage}'>{currentPage}</a></li>");
            }
        }

        private void InitPaginationHtml(StringBuilder content)
        {
            content.Append(" <ul class='pagination'>");
            //375.  Edit this to where if it's in the first page, the First button isn't displayed using if statement
            if(PageNumber != 1)
            {
                content.Append($"<li class='page-item'><a class='page-link' href='{PageTarget}'>{PageFirst}</a></li>");
            }
            //376.  Find all /{currentPage} and replace it with query string ?p={currentPage} to redirect our pages correctly
            //377.  Change url for Last page

        }

        private void InitDefaults()
        {
            if (PageRange <= 0)
            {
                PageRange = 5;
            }

            if (PageCount < PageRange)
            {
                PageRange = PageCount;
            }
            if (PageSize <= 0)
            {
                PageSize = 10;
            }

            if (string.IsNullOrEmpty(PageFirst))
            {
                PageFirst = "First";
            }

            if (string.IsNullOrEmpty(PageLast))
            {
                PageLast = "Last";
            }
        }
    }
}