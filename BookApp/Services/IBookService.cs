using BookApp.Data.Models;

namespace BookApp.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Add(Book newBook);
        Book GetById(int id);
        void Remove(int id);
    }
}
