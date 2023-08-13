using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class SearchRepository:ISearchRepository
    {
        private readonly Context context;

        public SearchRepository(Context context)
        {
            this.context = context;
        }
        public List<Book>? FindBookByNameOrByISBN(string searchText)
        {
            //Book book = context.Books.Include(x=>x.Category).Include(x=>x.author).Include(x=>x.Publisher).FirstOrDefault(x =>  x.Title == searchText);
            List<Book> book = context.Books.Include(x => x.Category).Include(x => x.author).Include(x => x.Publisher).Where(x => x.Title.Contains(searchText)).ToList();
            if(book.Count>0)
            {
                return book;
            }
            else
            {
                int isbn = 0;
                bool parseResult = int.TryParse(searchText, out isbn);
                if (parseResult)
                {
                    book = context.Books.Where(x => x.ISBN == isbn).Include(x => x.Category).Include(x => x.author).Include(x => x.Publisher).ToList();
                    return book;
                }
                
                return new List<Book>();
                //throw new NullReferenceException("No Books Found !");
            }
        }

        public List<Book>? SearchByAuthor(int id)
        {
            List<Book> books = context.Books.Include(x=>x.author).Include(x=>x.Category).Include(x=>x.Publisher).Where(x => x.AuthorId == id).ToList();
            if(books.Count > 0)
            {
                return books;
            }
            return null;
        }

        public List<Book> SearchByCategory(int id)
        {
            List<Book> books = context.Books.Include(x => x.author).Include(x => x.Category).Include(x => x.Publisher).Where(x => x.CategoryId == id).ToList();
            if (books.Count > 0)
            {
                return books;
            }
            return null;
        }
    }
}
