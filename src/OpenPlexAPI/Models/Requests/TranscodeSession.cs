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
    
    public class TranscodeSession
    {

        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("throttled")]
        public bool? Throttled { get; set; }

        [JsonProperty("complete")]
        public bool? Complete { get; set; }

        [JsonProperty("progress")]
        public double? Progress { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("speed")]
        public double? Speed { get; set; }

        [JsonProperty("error")]
        public bool? Error { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }

        [JsonProperty("remaining")]
        public int? Remaining { get; set; }

        [JsonProperty("context")]
        public string? Context { get; set; }

        [JsonProperty("sourceVideoCodec")]
        public string? SourceVideoCodec { get; set; }

        [JsonProperty("sourceAudioCodec")]
        public string? SourceAudioCodec { get; set; }

        [JsonProperty("videoDecision")]
        public string? VideoDecision { get; set; }

        [JsonProperty("audioDecision")]
        public string? AudioDecision { get; set; }

        [JsonProperty("subtitleDecision")]
        public string? SubtitleDecision { get; set; }

        [JsonProperty("protocol")]
        public string? Protocol { get; set; }

        [JsonProperty("container")]
        public string? Container { get; set; }

        [JsonProperty("videoCodec")]
        public string? VideoCodec { get; set; }

        [JsonProperty("audioCodec")]
        public string? AudioCodec { get; set; }

        [JsonProperty("audioChannels")]
        public int? AudioChannels { get; set; }

        [JsonProperty("transcodeHwRequested")]
        public bool? TranscodeHwRequested { get; set; }

        [JsonProperty("timeStamp")]
        public double? TimeStamp { get; set; }

        [JsonProperty("maxOffsetAvailable")]
        public double? MaxOffsetAvailable { get; set; }

        [JsonProperty("minOffsetAvailable")]
        public double? MinOffsetAvailable { get; set; }
    }
}