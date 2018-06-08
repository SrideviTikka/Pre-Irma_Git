using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace PreIRMA.WebSite.Models
{
    public class EmailAttributesModel
    {
        public string MessageBody { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string CC { get; set; }
        public AlternateView AlternateView { get; set; }
    }
}