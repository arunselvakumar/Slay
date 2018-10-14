namespace Slay.Models.DataTransferObjects.Category
{
    public sealed class CreatePostCategoryRequestDto
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsEnabled { get; set; }
    }
}