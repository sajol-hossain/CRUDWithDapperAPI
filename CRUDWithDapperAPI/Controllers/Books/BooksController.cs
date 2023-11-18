using CRUDWithDapper.Features.Repositories.BooksRepository;
using CRUDWithDapper.Views.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithDapperAPI.Controllers.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBooksRepository _booksRepository;
        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }


		[HttpGet("GetAllBookList")]
		public async Task<IReadOnlyList<Book>> GetAllBookListAsync()
		{
			try
			{
				return await _booksRepository.GetAllBookListAsync();
			}
			catch
			{
				throw;
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetBookById(int id)
		{
			var book = await _booksRepository.GetBookById(id);
			return book == null ? NotFound() : Ok(book);
		}


		[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<string> Create(Book book)
        {
            if (book == null)
            {
                return "Invalid Object";
            }
           

            return await _booksRepository.CreateBooks(book);
        }



		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public async Task<string> Update(int id, Book book)
		{
			

			return await _booksRepository.UpdateBook(id, book);
		}
	}
}
