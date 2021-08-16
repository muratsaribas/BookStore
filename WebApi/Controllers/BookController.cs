
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
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
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id){
        GetById command = new GetById(_context, _mapper);
        BooksViewModelID result;
        try
        {
            command.Id = id;
            GetByIdValidator validator = new GetByIdValidator();
            validator.ValidateAndThrow(command);
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
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        try
        {
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
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
            UpdateBookValidator validator = new UpdateBookValidator();
            validator.ValidateAndThrow(command);
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
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
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