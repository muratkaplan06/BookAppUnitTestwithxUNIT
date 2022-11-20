using BookApp.Data.Models;

namespace BookApp.Services
{
    public class BookService:IBookService
    {
        private readonly List<Book> _books;
        public BookService()
        {
            _books = new List<Book>()
            {
                new Book(){ 
                    Id=1,
                    Title ="Managing Oneself",
                    Description="We live in...",
                    Author="Peter Ducker"
                },new Book(){
                    Id=2,
                    Title ="Evolutionary Psychology",
                    Description="New Science",
                    Author="Davis Buss"
                },new Book(){
                    Id=3,
                    Title ="How to Win Friends",
                    Description="New Science",
                    Author="Davis Buss"
                },new Book(){
                    Id=4,
                    Title ="The Selfish Gene",
                    Description="Prof. Davkings Artuculates..",
                    Author="Richard Dawkins"
                },new Book(){
                    Id=5,
                    Title ="The Lossons of History",
                    Description="Will and Ariel Durant",
                    Author="Will & Ariel Durant"
                }


            };
        }

        public Book Add(Book newBook)
        {
            var book = newBook;
            _books.Add(book);
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            
            var result = _books.ToList();
            return result;
        }

        public Book GetById(int id)
        {
            var result=_books.Find(x => x.Id == id);
            return result;
        }

        public void Remove(int id)
        {
          var book = _books.Find(x => x.Id == id);
            _books.Remove(book);

        }
    }
}
