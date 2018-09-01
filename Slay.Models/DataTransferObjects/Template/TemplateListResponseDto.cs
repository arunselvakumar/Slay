namespace Slay.Models.DataTransferObjects.Template
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Slay.Models.DataTransferObjects.Post.Response;
    using Slay.Models.DataTransferObjects.Shared;

    public sealed class TemplateListResponseDto
    {
        [JsonProperty(PropertyName = "_links")]
        public LinksDto Links { get; set; }

        [JsonProperty(PropertyName = "_data")]
        public IEnumerable<TemplateResponseDto> Data { get; set; }
    }
}