using System;

namespace Amazon.Contexts
{
    public class SearchContext
    {
        public SearchContext()
        {
            this.SearchTitle = String.Empty;
            this.SearchType = String.Empty;
            this.SearchPrice = 0.0;
        }

        public string SearchTitle { get; set; }
        public string SearchType { get; set; }
        public double SearchPrice { get; set; }
    }
}
