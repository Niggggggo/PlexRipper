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
    
    public class GetAvailableClientsMediaContainer
    {

        [JsonProperty("size")]
        public double? Size { get; set; }

        [JsonProperty("Server")]
        public List<Models.Requests.Server>? Server { get; set; }
    }
}