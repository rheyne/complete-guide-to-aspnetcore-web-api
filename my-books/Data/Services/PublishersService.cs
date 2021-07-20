using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString)
        {
            var allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            return allPublishers;
        }

        public Publisher GetPublisherById(int publisherId)
        {
            return _context.Publishers.FirstOrDefault(n => n.Id == publisherId);
        }


        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData =
                _context.Publishers
                        .Where(n => n.Id == publisherId)
                        .Select(n => new PublisherWithBooksAndAuthorsVM()
                        {
                            Name = n.Name,
                            BookAuthors = n.Books.Select(n => new BookAuthorVM()
                            {
                                BookName = n.Title,
                                BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                            }).ToList()
                        }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
        }
    }
}
