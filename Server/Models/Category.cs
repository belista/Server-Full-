using System.Collections.Generic;

namespace Server.Models
{
    public class Category
    {
        public Category()
        {
            News = new List<News>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<News> News { get; set; }
    }
}