using System;
using System.Collections.Generic;
using System.Text;

namespace EuromessageDotnetSample.Dto
{
    public class PostHtmlResponseDto
    {
        public string PostId { get; set; }
        public bool Success { get; set; }
        public Error[] Errors { get; set; }
        public string DetailedMessage { get; set; }
        public string TransactionId { get; set; }
    }

}
