using Nop.Core;
using Nop.Core.Domain.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu.Domain
{
    public class TopicHeaderRecord : BaseEntity
    {

        private ICollection<TopicHeaderCarouselRecord> _topicHeaderCarousels;

        public ICollection<TopicHeaderCarouselRecord> TopicHeaderCarousels
        {
            get { return _topicHeaderCarousels ?? (_topicHeaderCarousels = new List<TopicHeaderCarouselRecord>()); }
            set { _topicHeaderCarousels = value; }
        }
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string  HeaderType { get; set; }
        public string ContentUrl { get; set; }
        public string LinkUrl { get; set; }

        public virtual ExtendedTopicRecord Topic { get; set; }
    }
}
