using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetById{

    public class GetById{
        private readonly BookStoreDbContext _dbContext;
        public int Id { get; set; }

        public GetById(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BooksViewModelID Handle(){
            var book = _dbContext.Books.Where(book => book.Id == Id).SingleOrDefault();
            
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            
            return new BooksViewModelID(){
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                PageCount = book.PageCount
            };
        }

        public class BooksViewModelID{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        }
    }
}