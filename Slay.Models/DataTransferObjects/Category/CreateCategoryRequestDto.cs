namespace Slay.Models.DataTransferObjects.Category
{
    public sealed class CreateCategoryRequestDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsEnabled { get; set; }
    }
}