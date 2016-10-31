using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Topics;

namespace Nop.Plugin.Dreamy.Menu.Domain
{
    public class FullTopicRecord
    {
        public Topic MainTopic { get; set; }
        public ExtendedTopicRecord ExtendedTopic { get; set; }
    }
}
