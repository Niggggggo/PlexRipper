//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by Speakeasy (https://speakeasyapi.dev). DO NOT EDIT.
//
// Changes to this file may cause incorrect behavior and will be lost when
// the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
namespace OpenPlexAPI.Models.Errors
{
    using Newtonsoft.Json;
    using OpenPlexAPI.Utils;
    
    public class GetDevicesErrors
    {

        [JsonProperty("code")]
        public double? Code { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("status")]
        public double? Status { get; set; }
    }
}