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
    
    /// <summary>
    /// The libraries available on the Server
    /// </summary>
    public class GetLibrariesResponseBody
    {

        [JsonProperty("MediaContainer")]
        public GetLibrariesMediaContainer? MediaContainer { get; set; }
    }
}