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
    
    public class Ratings
    {

        [JsonProperty("image")]
        public string? Image { get; set; }

        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}