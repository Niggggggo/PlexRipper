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
    
    public class GetMetadataChildrenMediaContainer
    {

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("allowSync")]
        public bool? AllowSync { get; set; }

        [JsonProperty("art")]
        public string? Art { get; set; }

        [JsonProperty("identifier")]
        public string? Identifier { get; set; }

        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("librarySectionID")]
        public int? LibrarySectionID { get; set; }

        [JsonProperty("librarySectionTitle")]
        public string? LibrarySectionTitle { get; set; }

        [JsonProperty("librarySectionUUID")]
        public string? LibrarySectionUUID { get; set; }

        [JsonProperty("mediaTagPrefix")]
        public string? MediaTagPrefix { get; set; }

        [JsonProperty("mediaTagVersion")]
        public int? MediaTagVersion { get; set; }

        [JsonProperty("nocache")]
        public bool? Nocache { get; set; }

        [JsonProperty("parentIndex")]
        public int? ParentIndex { get; set; }

        [JsonProperty("parentTitle")]
        public string? ParentTitle { get; set; }

        [JsonProperty("parentYear")]
        public int? ParentYear { get; set; }

        [JsonProperty("summary")]
        public string? Summary { get; set; }

        [JsonProperty("theme")]
        public string? Theme { get; set; }

        [JsonProperty("thumb")]
        public string? Thumb { get; set; }

        [JsonProperty("title1")]
        public string? Title1 { get; set; }

        [JsonProperty("title2")]
        public string? Title2 { get; set; }

        [JsonProperty("viewGroup")]
        public string? ViewGroup { get; set; }

        [JsonProperty("viewMode")]
        public int? ViewMode { get; set; }

        [JsonProperty("Directory")]
        public List<GetMetadataChildrenDirectory>? Directory { get; set; }

        [JsonProperty("Metadata")]
        public List<GetMetadataChildrenMetadata>? Metadata { get; set; }
    }
}