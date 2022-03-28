using System;
using System.Text.Json.Serialization;

namespace ServerSlutprojekt
{
    public class PersonIn
    {
        public string Name { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}