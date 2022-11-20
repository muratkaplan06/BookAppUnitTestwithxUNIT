using BookApp.Controllers;
using BookApp.Data.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.TEST
{
    public class BooksControllerTest
    {
        private readonly BooksController _controller;
        private readonly IBookService _service;

        public BooksControllerTest()
        {
            _service = new BookService();
            _controller=new BooksController(_service);
        }
        [Fact]
        public void GetAllTest()
        {
            //arrange
            //act
            var result = _controller.Get();
            //assert
            Assert.IsType<OkObjectResult>(result.Result);
            var list=result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(list.Value);
            var listBooks=list.Value as List<Book>;
            Assert.Equal(5, listBooks.Count);
        }
        [Theory]
        [InlineData(1,7)]
        public void GetBookByIdTest(int id1,int id2)
        {
            //arrange
            var validId=id1;
            var inValidId=id2;
            //act
            var okResult = _controller.Get(validId);
            var notFoundResult=_controller.Get(inValidId);
            //assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.IsType<OkObjectResult>(okResult.Result);
            var item=okResult.Result as OkObjectResult;

            Assert.IsType<Book>(item.Value);
            var bookItem=item.Value as Book;
            Assert.Equal(validId, bookItem.Id);
            Assert.Equal("Managing Oneself",bookItem.Title);
        }
        [Fact]
        public void AddBookTest()
        {
            //arrange
            var completeBook = new Book()
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"
            };
            //act
            var createdResponse = _controller.Save(completeBook);
            //assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

            var item = createdResponse as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var bookItem= item.Value as Book;
            Assert.Equal(completeBook.Author, bookItem.Author);
            Assert.Equal(completeBook.Title, bookItem.Title);
            Assert.Equal(completeBook.Description, bookItem.Description);

            //arrange
            var inCompleteBook = new Book()
            {
                Author = "Author",
                Description = "Description"
            };
            //act
            _controller.ModelState.AddModelError("Title", "Title is a required field");
            var badResponse = _controller.Save(inCompleteBook);
            //assert
            Assert.IsType<BadRequestObjectResult>(badResponse);

        }

        [Theory]
        [InlineData(1, 7)]
        public void RemoveByIdTest(int id1, int id2)
        {
            //arrange
            var validId = id1;
            var inValidId = id2;
            //act
            var notFoundResult = _controller.Remove(inValidId);
            //assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(5,_service.GetAll().Count());

            //act
            var okResult=_controller.Remove(validId);
            //assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(4, _service.GetAll().Count());
        }
    }
}