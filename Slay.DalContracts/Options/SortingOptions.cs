namespace Slay.DalContracts.Options
{
	public sealed class SortingOptions
	{
		public SortingOptions()
		{
			
		}

		public SortingOptions(string fieldName, SortingMode sortingMode = SortingMode.Ascending)
		{
			this.FieldName = fieldName;
			this.SortingMode = sortingMode;
		}

		public string FieldName { get; set; }

		public SortingMode SortingMode { get; set; }
	}
}