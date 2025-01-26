namespace IPAddressManagement.Models
{
    public class BreadcrumbItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        
        public BreadcrumbItem(string title, string url)
        {
            Title = title;
            Url = url;
        }
    }
}