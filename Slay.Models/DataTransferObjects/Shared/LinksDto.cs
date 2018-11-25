namespace Slay.Models.DataTransferObjects.Shared
{
    using Newtonsoft.Json;

    public sealed class LinksDto
    {
        public string Base { get; set; }

        public string Self { get; set; }

        public string Next { get; set; }

        public string Descendants { get; set; }
    }
}