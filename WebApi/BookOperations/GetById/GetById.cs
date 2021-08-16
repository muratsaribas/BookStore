using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetById{

    public class GetById{
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int Id { get; set; }

        public GetById(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BooksViewModelID Handle(){
            var book = _dbContext.Books.Where(book => book.Id == Id).SingleOrDefault();
            
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            
            BooksViewModelID vm = _mapper.Map<BooksViewModelID>(book);
            return vm;
        }

        public class BooksViewModelID{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        }
    }
}