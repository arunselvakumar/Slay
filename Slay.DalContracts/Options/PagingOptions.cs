namespace Slay.DalContracts.Options
{
	public sealed class PagingOptions
	{
		public int? Skip { get; private set; }

		public int? Limit { get; private set; }

		public PagingOptions SkipItems(int? skip)
		{
			this.Skip = skip;

			return this;
		}

		public PagingOptions LimitItems(int? limit)
		{
			this.Limit = limit;

			return this;
		}
	}
}