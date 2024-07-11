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
    
    /// <summary>
    /// Watchlist Data
    /// </summary>
    public class GetWatchlistResponseBody
    {

        [JsonProperty("librarySectionID")]
        public string? LibrarySectionID { get; set; }

        [JsonProperty("librarySectionTitle")]
        public string? LibrarySectionTitle { get; set; }

        [JsonProperty("offset")]
        public int? Offset { get; set; }

        [JsonProperty("totalSize")]
        public int? TotalSize { get; set; }

        [JsonProperty("identifier")]
        public string? Identifier { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("Metadata")]
        public List<Metadata>? Metadata { get; set; }
    }
}