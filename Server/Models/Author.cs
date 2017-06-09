using System.Collections.Generic;

namespace Server.Models
{
    public class Author
    {
        public Author()
        {
            News = new List<News>(); 
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }

        public List<News> News { get; set; }
    }
}