using System;
using System.Collections.Generic;
using System.Text;

namespace EuromessageDotnetSample.Dto
{

    public class PostHtmlRequestDto
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string ReplyAddress { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string Charset { get; set; }
        public string ToName { get; set; }
        public string ToEmailAddress { get; set; }
        public Attachment[] Attachments { get; set; }
        public string PostType { get; set; }
        public string KeyId { get; set; }
        public string CustomParams { get; set; }
    }

    public class Attachment
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }

}
