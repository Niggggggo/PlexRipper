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
    
    public class GetBandwidthStatisticsAccount
    {

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("defaultAudioLanguage")]
        public string? DefaultAudioLanguage { get; set; }

        [JsonProperty("autoSelectAudio")]
        public bool? AutoSelectAudio { get; set; }

        [JsonProperty("defaultSubtitleLanguage")]
        public string? DefaultSubtitleLanguage { get; set; }

        [JsonProperty("subtitleMode")]
        public int? SubtitleMode { get; set; }

        [JsonProperty("thumb")]
        public string? Thumb { get; set; }
    }
}