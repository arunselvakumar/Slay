namespace Slay.DalContracts.Options
{
	public sealed class PagingOptions
	{
		public PagingOptions()
		{
			
		}

		public PagingOptions(int? skip, int? limit)
		{
			this.Skip = skip;
			this.Limit = limit;
		}

		public int? Skip { get; set; }

		public int? Limit { get; set; }
	}
}