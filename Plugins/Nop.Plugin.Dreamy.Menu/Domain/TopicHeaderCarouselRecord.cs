using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu.Domain
{
    public class TopicHeaderCarouselRecord : BaseEntity
    {
        public int Id { get; set; }
        public int TopicHeaderId { get; set; }
        public string ContentUrl { get; set; }
        public string ContentType { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string LinkUrl { get; set; }

        public TopicHeaderRecord TopicHeader {get; set;}
    }
}
