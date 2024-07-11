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
    
    public class MyPlex
    {

        [JsonProperty("authToken")]
        public string? AuthToken { get; set; }

        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("mappingState")]
        public string? MappingState { get; set; }

        [JsonProperty("mappingError")]
        public string? MappingError { get; set; }

        [JsonProperty("signInState")]
        public string? SignInState { get; set; }

        [JsonProperty("publicAddress")]
        public string? PublicAddress { get; set; }

        [JsonProperty("publicPort")]
        public double? PublicPort { get; set; }

        [JsonProperty("privateAddress")]
        public string? PrivateAddress { get; set; }

        [JsonProperty("privatePort")]
        public double? PrivatePort { get; set; }

        [JsonProperty("subscriptionFeatures")]
        public string? SubscriptionFeatures { get; set; }

        [JsonProperty("subscriptionActive")]
        public bool? SubscriptionActive { get; set; }

        [JsonProperty("subscriptionState")]
        public string? SubscriptionState { get; set; }
    }
}