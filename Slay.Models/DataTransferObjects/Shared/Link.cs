namespace Slay.Models.DataTransferObjects.Shared
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    /// <summary>
    /// HATEOAS Implementation as per Ion, https://www.slideshare.net/stormpath/beautiful-restjson-apis-with-ion (Reference)
    /// </summary>
    public sealed class Link
    {
        [JsonProperty(Order = -4)]
        public string Href { get; set; }

        [JsonProperty(Order = -3, NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("GET")]
        public string Method { get; set; }

        [JsonProperty(Order = -2, PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }
    }
}