using System;
using System.Collections.Generic;
using Slay.Models.Entities;

namespace Slay.Models.BusinessObjects.Comment
{
	public sealed class CommentItemBo
	{
		public string Id { get; set; }

		public string PostId { get; set; }

		public string ParentId { get; set; }

		public string Comment { get; set; }

		public long ChildrensCount { get; set; }

		public string CommentedBy { get; set; }

		public DateTime CreatedOn { get; set; }
	}
}