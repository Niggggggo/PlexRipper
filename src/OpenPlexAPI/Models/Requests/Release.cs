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
    using OpenPlexAPI.Utils;
    
    public class Release
    {

        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("added")]
        public string? Added { get; set; }

        [JsonProperty("fixed")]
        public string? Fixed { get; set; }

        [JsonProperty("downloadURL")]
        public string? DownloadURL { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }
    }
}