using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWithDapper.Features.Helper
{
	public class NotificationMessageContainer
	{
		#region "Book"
		public const string BookCreated = "Book Created successfully.";
		public const string BookUpdated = "Book Updated successfully.";
		public const string BookDeleted = "Book Deleted successfully.";
		public const string BookCreationFailed = "Operation Failed.";
		#endregion
	}
}
