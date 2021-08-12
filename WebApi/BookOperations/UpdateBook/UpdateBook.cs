using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook{

    
    public class UpdateBook{

        private readonly BookStoreDbContext _dbContext;
        public int Id;
        public CreateBookModelUpdate Model { get; set; }

        public UpdateBook(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(){
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);

            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }

        public class CreateBookModelUpdate{
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }

    }
    
    
}