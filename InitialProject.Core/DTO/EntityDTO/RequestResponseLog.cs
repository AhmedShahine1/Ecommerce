﻿namespace Ecommerce.Core.DTO.EntityDTO
{
    public class RequestResponseLog
    {
        public string RequestUrl { get; set; }
        public string HttpMethod { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}