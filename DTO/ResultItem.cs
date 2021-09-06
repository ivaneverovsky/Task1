using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.DTO
{
    class ResultItem
    {
        [JsonProperty("text")]
        public string Content { get; set; }
    }
}
