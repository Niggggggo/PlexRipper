//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by Speakeasy (https://speakeasyapi.dev). DO NOT EDIT.
//
// Changes to this file may cause incorrect behavior and will be lost when
// the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
namespace OpenPlexAPI.Models.Requests
{
    using Newtonsoft.Json;
    using OpenPlexAPI.Models.Requests;
    using OpenPlexAPI.Utils;
    using System.Collections.Generic;
    
    public class GetTopWatchedContentMediaContainer
    {

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("allowSync")]
        public bool? AllowSync { get; set; }

        [JsonProperty("identifier")]
        public string? Identifier { get; set; }

        [JsonProperty("mediaTagPrefix")]
        public string? MediaTagPrefix { get; set; }

        [JsonProperty("mediaTagVersion")]
        public int? MediaTagVersion { get; set; }

        [JsonProperty("Metadata")]
        public List<GetTopWatchedContentMetadata>? Metadata { get; set; }
    }
}