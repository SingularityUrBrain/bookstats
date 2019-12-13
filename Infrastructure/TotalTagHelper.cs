using BookStats.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.Infrastructure
{
    public class TotalTagHelper : TagHelper
    {
        private readonly IRepository repository;
        public TotalTagHelper(IRepository repo)
        {
            repository = repo;
        }
        public string Name { get; set; }
        public int Count { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "total";
            output.TagMode = TagMode.StartTagAndEndTag;
            
            if (Name == "Books")
            {
                Count = repository.Books.Count();
            }
            else if(Name == "Authors")
            {
                Count = repository.Authors.Count();
            }
            output.PreContent.SetHtmlContent($"<span class=\"text-muted\">{Name} total: {Count}</span>");
        }
    }
}
