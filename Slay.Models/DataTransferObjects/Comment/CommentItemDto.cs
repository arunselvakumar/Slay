namespace Slay.Models.DataTransferObjects.Comment
{
    public sealed class CommentItemDto
    {
        public string Id { get; set; }

        public string PostId { get; set; }

        public string ParentId { get; set; }

        public string Comment { get; set; }

        public long Descendants { get; set; }

        public string CommentedBy { get; set; }

        public string CreatedOn { get; set; }
    }
}