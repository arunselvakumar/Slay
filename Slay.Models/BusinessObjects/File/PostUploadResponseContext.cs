namespace Slay.Models.BusinessObjects.File
{
    public sealed class PostUploadResponseContext
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string PrimaryUrl { get; set; }

        public string SecondaryUrl { get; set; }
    }
}