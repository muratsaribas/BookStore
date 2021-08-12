using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook{

    
    public class DeleteBookCommand{

        private readonly BookStoreDbContext _dbContext;
        public int Id { get; set; }

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(){
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
        
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();    
        }



    }
    
}