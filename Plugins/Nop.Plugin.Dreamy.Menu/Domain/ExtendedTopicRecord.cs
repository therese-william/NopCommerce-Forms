using Nop.Core;
using Nop.Core.Domain.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu.Domain
{
    public class ExtendedTopicRecord : BaseEntity
    {
        private ICollection<TopicHeaderRecord> _topicHeaders;

        public virtual ICollection<TopicHeaderRecord> TopicHeaders
        {
            get { return _topicHeaders ?? (_topicHeaders = new List<TopicHeaderRecord>()); }
            set { _topicHeaders = value; }
        }

        public int TopicId { get; set; }
        public int ParentTopicId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSection { get; set; }
    }
}
