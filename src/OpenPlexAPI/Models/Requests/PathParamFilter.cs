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
    using System;
    
    /// <summary>
    /// Filter
    /// </summary>
    public enum PathParamFilter
    {
        [JsonProperty("all")]
        All,
        [JsonProperty("available")]
        Available,
        [JsonProperty("released")]
        Released,
    }

    public static class PathParamFilterExtension
    {
        public static string Value(this PathParamFilter value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString())[0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static PathParamFilter ToEnum(this string value)
        {
            foreach(var field in typeof(PathParamFilter).GetFields())
            {
                var attributes = field.GetCustomAttributes(typeof(JsonPropertyAttribute), false);
                if (attributes.Length == 0)
                {
                    continue;
                }

                var attribute = attributes[0] as JsonPropertyAttribute;
                if (attribute != null && attribute.PropertyName == value)
                {
                    var enumVal = field.GetValue(null);

                    if (enumVal is PathParamFilter)
                    {
                        return (PathParamFilter)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum PathParamFilter");
        }
    }

}