using System.Collections.Generic;
using System;

namespace Client.Core.Models
{
    public class Response
    {
        public long StatusCode { get; set; }

        public string Error { get; set; }

        public string Data { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public string intelligence { get; set; }

        public string strength { get; set; }

        public string speed { get; set; }

        public string durability { get; set; }

        public string power { get; set; }

        public string combat { get; set; }

        public Dictionary<string, string> Headers { get; set; }

    }



}

