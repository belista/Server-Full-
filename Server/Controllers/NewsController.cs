using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        // GET: api/values

        NewsContext db;


        public NewsController(NewsContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {

                
          //     var category = new Category
          //     {
          //       Name = "Some category 3"
          //   };
          //   db.Categorys.Add(category);
          //  var author = new Author
          //  {
          //     FullName = "Калганов Терентий Изяславович",
          //     Address = "ул. Куйбышева дом 1234 кв. 12345",
          //     Phone = 5555555
          //  };
          //   db.Authors.Add(author);

          //   db.Add(new News { Date = DateTimeOffset.Now.Date, Name = "Мирная Делегация", CategoryId = category.Id, AuthorId = author.Id });

          //  db.SaveChanges();

         

            var news = db.News.Include(n => n.Author)
                .Include(n => n.Category).Select(n => new { id = n.Id, Name = n.Name, Date = n.Date, Author = new { Name = n.Author.FullName, Phone = n.Author.Phone }, Category = n.Category.Name });
            return Json(news);
        }

        [Route("category")]
        [HttpGet]

        public IActionResult GetCategory()
        {
            return new OkObjectResult(db.Categorys);
        }

        [Route("author")]
        [HttpGet]
        public IActionResult GetAuthor()
        {
            return new OkObjectResult(db.Authors);
        } 

        [HttpGet("{id}")]
        public IActionResult GetNews(int id)
        {
            News news = db.News.FirstOrDefault(newsId => newsId.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            return new OkObjectResult(news);
        }

        [Route("author/{name}")]
        [HttpGet]
        public IActionResult GetAuthorName(string name)
        {
            var count = db.Authors.FirstOrDefault(authorName => authorName.FullName == name);
            if (name == null)
            {
                return NotFound();
            }
            return new OkObjectResult(count);
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult DeleteNews(int id)
        {
            News news = db.News.FirstOrDefault(newsId => newsId.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            db.News.Remove(news);
            db.SaveChanges();
            return Ok(news);
        }

        [Route("author/delete/{id}")]
        [HttpGet]
        public IActionResult DeleteAuthor(int id)
        {
            Author author = db.Authors.FirstOrDefault(authorId => authorId.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            db.Authors.Remove(author);
            db.SaveChanges();
            return Ok(author);
        }

        [Route("category/delete/{id}")]
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            Category category = db.Categorys.FirstOrDefault(categoryId => categoryId.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            db.Categorys.Remove(category);
            db.SaveChanges();
            return Ok(category);
        }



        [Route("author/post/{FullName}/{Address}/{Phone}")]
        [HttpGet]
        public IActionResult PostAuthor(string fullname, string address, int phone)
        {
          var author = db.Authors.Add(new Author { FullName = fullname, Address = address, Phone = phone });
            db.SaveChanges();
            return Ok(author);
        }

        [Route("category/post/{name}")]
        [HttpGet]
        public IActionResult PostCategory(string name)
        {
            var category = db.Categorys.Add(new Category { Name = name });
            db.SaveChanges();
            return Ok(category);
        }

        [Route("author/put/{id}/{FullName}/{Address}/{Phone}")]
        [HttpGet]
        public IActionResult PutAuthor(int id, string fullname, string address, int phone)
        {
            foreach (var item in db.Authors)
            {
                if (item.Id == id)
                {
                    item.FullName = fullname;
                    item.Address = address;
                    item.Phone = phone;
                }
            }
            db.SaveChanges();
            return Ok(db.Authors);
        }
        [Route("category/put/{id}/{name}")]
        [HttpGet]
        public IActionResult PutCategory(int id, string name)
        {
            foreach (var item in db.Categorys)
            {
                if (item.Id == id)
                {
                    item.Name = name;
                }
            }
            db.SaveChanges();
            return Ok(db.Authors);
        }
        [Route("get/count/{count}/offset/{offset}")]
        [HttpGet]
        public IActionResult GetNewsOffset(int count, int offset)
        {
            List<News> news = new List<News>();
            foreach (var item in db.News.Include(n => n.Author).Include(n => n.Category))
            {
                    if (item.Id >= offset && item.Id <= (count+offset))
                    {
                    news.Add(item);
                    }
            }
            if (news == null)
            {
                return NotFound();
            }
            return new OkObjectResult(news);
        }
    }
}
