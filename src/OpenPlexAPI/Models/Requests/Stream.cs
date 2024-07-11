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
    
    public class Stream
    {

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("streamType")]
        public int? StreamType { get; set; }

        [JsonProperty("default")]
        public bool? Default { get; set; }

        [JsonProperty("codec")]
        public string? Codec { get; set; }

        [JsonProperty("index")]
        public int? Index { get; set; }

        [JsonProperty("bitrate")]
        public int? Bitrate { get; set; }

        [JsonProperty("bitDepth")]
        public int? BitDepth { get; set; }

        [JsonProperty("chromaLocation")]
        public string? ChromaLocation { get; set; }

        [JsonProperty("chromaSubsampling")]
        public string? ChromaSubsampling { get; set; }

        [JsonProperty("codedHeight")]
        public int? CodedHeight { get; set; }

        [JsonProperty("codedWidth")]
        public int? CodedWidth { get; set; }

        [JsonProperty("colorPrimaries")]
        public string? ColorPrimaries { get; set; }

        [JsonProperty("colorRange")]
        public string? ColorRange { get; set; }

        [JsonProperty("colorSpace")]
        public string? ColorSpace { get; set; }

        [JsonProperty("colorTrc")]
        public string? ColorTrc { get; set; }

        [JsonProperty("frameRate")]
        public int? FrameRate { get; set; }

        [JsonProperty("hasScalingMatrix")]
        public bool? HasScalingMatrix { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("level")]
        public int? Level { get; set; }

        [JsonProperty("profile")]
        public string? Profile { get; set; }

        [JsonProperty("refFrames")]
        public int? RefFrames { get; set; }

        [JsonProperty("scanType")]
        public string? ScanType { get; set; }

        [JsonProperty("streamIdentifier")]
        public string? StreamIdentifier { get; set; }

        [JsonProperty("width")]
        public int? Width { get; set; }

        [JsonProperty("displayTitle")]
        public string? DisplayTitle { get; set; }

        [JsonProperty("extendedDisplayTitle")]
        public string? ExtendedDisplayTitle { get; set; }

        [JsonProperty("selected")]
        public bool? Selected { get; set; }

        [JsonProperty("channels")]
        public int? Channels { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }

        [JsonProperty("languageTag")]
        public string? LanguageTag { get; set; }

        [JsonProperty("languageCode")]
        public string? LanguageCode { get; set; }

        [JsonProperty("samplingRate")]
        public int? SamplingRate { get; set; }
    }
}