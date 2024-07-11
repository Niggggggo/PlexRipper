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
    
    public class GetOnDeckPart
    {

        [JsonProperty("id")]
        public double? Id { get; set; }

        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("duration")]
        public double? Duration { get; set; }

        [JsonProperty("file")]
        public string? File { get; set; }

        [JsonProperty("size")]
        public double? Size { get; set; }

        [JsonProperty("audioProfile")]
        public string? AudioProfile { get; set; }

        [JsonProperty("container")]
        public string? Container { get; set; }

        [JsonProperty("videoProfile")]
        public string? VideoProfile { get; set; }

        [JsonProperty("Stream")]
        public List<GetOnDeckStream>? Stream { get; set; }
    }
}