using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeNet_Models
{
    /// <summary>
    /// Helper class to send json response
    /// </summary>
    public class Message
    {
        public Guid MessageID
        {
            get { return Guid.NewGuid(); }
        }
        public string State { get; set; }
        public string Data { get; set; }
    }
}
