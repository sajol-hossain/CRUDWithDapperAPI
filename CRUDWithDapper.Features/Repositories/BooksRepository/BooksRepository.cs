using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDWithDapper.Views.Book;
using CRUDWithDapper.Features.Helper;

namespace CRUDWithDapper.Features.Repositories.BooksRepository
{
	public class BooksRepository:IBooksRepository
	{
		private readonly string _connectionstring;

		public BooksRepository(IConfiguration config)
		{
			_connectionstring = config.GetSection("Api")["ConnectionString"];
		}



		public async Task<string> CreateBooks(Book book)
		{
			try
			{
				using (IDbConnection _dbConnection = new SqlConnection(_connectionstring))
				{
					DynamicParameters parameters = new();
					parameters.Add("@Name",book.Name, DbType.String);
					parameters.Add("@Description", book.Description, DbType.String);
					parameters.Add("@PublishedBy", book.PublishedBy, DbType.String);
					
					var results = await _dbConnection.QueryFirstAsync<int>("[dbo].[SP_InsertBooks]", parameters, commandType: CommandType.StoredProcedure);
					return (results > 0) ? NotificationMessageContainer.BookCreated : NotificationMessageContainer.BookCreationFailed;

				}
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<string> UpdateBook(int id, Book book)
		{
			try
			{
				using (IDbConnection _dbConnection = new SqlConnection(_connectionstring))
				{
					DynamicParameters parameters = new();
					parameters.Add("@updateId", id, DbType.Int32);
					parameters.Add("@Name", book.Name, DbType.String);
					parameters.Add("@Description", book.Description, DbType.String);
					parameters.Add("@PublishedBy", book.PublishedBy, DbType.String);
					parameters.Add("@PublishedOn", book.PublishedOn, DbType.Date);


					var results = await _dbConnection.QueryFirstAsync<int>("[dbo].[SP_UpdateBook]", parameters, commandType: CommandType.StoredProcedure);
					return (results > 0) ? NotificationMessageContainer.BookUpdated : NotificationMessageContainer.BookCreationFailed;

				}
			}
			catch (Exception)
			{

				throw;
			}
		}





		public async Task<IReadOnlyList<Book>> GetAllBookListAsync()
		{
			try
			{
				using (IDbConnection _dbConnection = new SqlConnection(_connectionstring))
				{
					var results = await _dbConnection.QueryAsync<Book>("[dbo].[SP_GetBookAll]", commandType: CommandType.StoredProcedure);
					return results.ToList();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<Book> GetBookById(int id)
		{
			try
			{
				using (IDbConnection _dbConnection = new SqlConnection(_connectionstring))
				{
					DynamicParameters parameters = new();
					parameters.Add("@Id", id, DbType.Int32);

					var result = await _dbConnection.QueryFirstAsync<Book>("[dbo].[SP_GetBookById]", parameters, commandType: CommandType.StoredProcedure);
					return result;
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}






	}
}
