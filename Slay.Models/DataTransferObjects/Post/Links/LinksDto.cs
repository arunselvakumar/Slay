namespace Slay.Models.DataTransferObjects.Post.Links
{
    using Newtonsoft.Json;

    public sealed class LinksDto
    {
        [JsonProperty(PropertyName = "_base")]
        public string Base { get; set; }

        [JsonProperty(PropertyName = "_self")]
        public string Self { get; set; }

        [JsonProperty(PropertyName = "_next")]
        public string Next { get; set; }

        [JsonProperty(PropertyName = "_descendants")]
        public string Descendants { get; set; }
    }
}