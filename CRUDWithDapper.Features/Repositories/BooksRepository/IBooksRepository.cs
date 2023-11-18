using CRUDWithDapper.Views.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWithDapper.Features.Repositories.BooksRepository
{
	public interface IBooksRepository
	{
		Task<string> CreateBooks(Book book);

		Task<string> UpdateBook(int id, Book book);

		Task<IReadOnlyList<Book>> GetAllBookListAsync();
		Task<Book> GetBookById(int id);
	}
}
