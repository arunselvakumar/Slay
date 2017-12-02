using System.Reflection.Metadata.Ecma335;

namespace Slay.Models.DataTransferObjects.Link
{
	public sealed class LinksDto
	{
		public string Base { get; set; }

		public string Self { get; set; }

		public string Previous { get; set; }

		public string Next { get; set; }
	}
}