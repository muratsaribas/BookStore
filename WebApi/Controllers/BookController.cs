
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBookCommand;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBookCommand.CreateBookCommand;
using static WebApi.BookOperations.GetById.GetById;
using static WebApi.BookOperations.UpdateBook.UpdateBook;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context){
            _context = context;
        }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id){
        GetById command = new GetById(_context);
        BooksViewModelID result;
        try
        {
            command.Id = id;
            result = command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(result);
    }

    // [HttpGet]
    // public Book Get([FromQuery] string id){
    //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
    //     return book;
    // }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook){
        CreateBookCommand command = new CreateBookCommand(_context);
        try
        {
            command.Model = newBook;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();       
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel bookModel){
        UpdateBook command = new UpdateBook(_context);
        try
        {
            command.Model = bookModel;
            command.Id = id;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();        
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id){
        DeleteBookCommand command = new DeleteBookCommand(_context);
        try
        {
            command.Id = id;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }


    }
}