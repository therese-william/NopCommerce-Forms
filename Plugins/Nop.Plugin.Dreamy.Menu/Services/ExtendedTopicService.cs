using Nop.Core.Data;
using Nop.Core.Domain.Topics;
using Nop.Plugin.Dreamy.Menu.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu.Services
{
    public class ExtendedTopicService : IExtendedTopicService
    {
        private readonly IRepository<Topic> _topicRepository;
        private readonly IRepository<ExtendedTopicRecord> _extendedTopicRepository;
        private readonly IRepository<TopicHeaderRecord> _topicHeaderRepository;
        private readonly IRepository<TopicHeaderCarouselRecord> _topicHeaderContentRepository;

        public ExtendedTopicService(IRepository<Topic> topicRepository,IRepository<ExtendedTopicRecord> extendedTopicRepository,
                                    IRepository<TopicHeaderRecord> topicHeaderRepository,
                                    IRepository<TopicHeaderCarouselRecord> topicHeaderContentRepository)
        {
            _topicRepository = topicRepository;
            _extendedTopicRepository = extendedTopicRepository;
            _topicHeaderRepository = topicHeaderRepository;
            _topicHeaderContentRepository = topicHeaderContentRepository;
        }

        public void SaveExtendedTopic(Domain.ExtendedTopicRecord ExtendedTopic)
        {
            _extendedTopicRepository.Insert(ExtendedTopic);
        }

        public void UpdateExtendedTopic(Domain.ExtendedTopicRecord ExtendedTopic)
        {
            _extendedTopicRepository.Update(ExtendedTopic);
        }

        public void DeleteExtendedTopic(Domain.ExtendedTopicRecord ExtendedTopic)
        {
            _extendedTopicRepository.Delete(ExtendedTopic);
        }

        public Domain.ExtendedTopicRecord GetExtendedTopicByTopicId(int TopicId)
        {
            try
            {
                var query = _extendedTopicRepository.Table;
                return (query.Where(t => t.TopicId == TopicId).ToList().FirstOrDefault());
            }
            catch
            {
                return null;
            }
        }

        public IList<Domain.FullTopicRecord> GetTopicSections(int TopicId)
        {
            List<FullTopicRecord> AllSections = new List<FullTopicRecord>();
            var query = _extendedTopicRepository.Table;
            try
            {
                var extendedSections = query.Where(t => t.ParentTopicId == TopicId && t.IsSection).ToList();
                foreach (var extendedSection in extendedSections)
                {
                    try
                    {
                        FullTopicRecord FullTopic = new FullTopicRecord();
                        FullTopic.ExtendedTopic = extendedSection;
                        FullTopic.MainTopic = _topicRepository.GetById(extendedSection.TopicId);
                        AllSections.Add(FullTopic);
                    }
                    catch { }                    
                }
            }
            catch{}
            return AllSections;
        }

        public void AddTopicHeader(Domain.TopicHeaderRecord TopicHeader)
        {
            _topicHeaderRepository.Insert(TopicHeader);
        }

        public void UpdateTopicHeader(Domain.TopicHeaderRecord TopicHeader)
        {
            _topicHeaderRepository.Update(TopicHeader);
        }

        public void DeleteTopicHeader(Domain.TopicHeaderRecord TopicHeader)
        {
            _topicHeaderRepository.Delete(TopicHeader);
        }

        public Domain.TopicHeaderRecord GetTopicHeaderByTopicId(int TopicId)
        {
            try
            {
                return (_topicHeaderRepository.Table.Where(th => th.TopicId == TopicId).ToList().FirstOrDefault());
            }
            catch
            {
                return null;
            }
        }

        public void AddTopicHeaderContent(Domain.TopicHeaderCarouselRecord HeaderContent)
        {
            _topicHeaderContentRepository.Insert(HeaderContent);
        }

        public void UpdateTopicHeaderContent(Domain.TopicHeaderCarouselRecord HeaderContent)
        {
            _topicHeaderContentRepository.Update(HeaderContent);
        }

        public void DeleteTopicHeaderContent(Domain.TopicHeaderCarouselRecord HeaderContent)
        {
            _topicHeaderContentRepository.Delete(HeaderContent);
        }

        public IList<Domain.TopicHeaderCarouselRecord> GetCarouselContents(int TopicHeaderId)
        {
            try
            {
                return (_topicHeaderContentRepository.Table.Where(c => c.TopicHeaderId == TopicHeaderId).ToList());
            }
            catch
            {
                return new List<TopicHeaderCarouselRecord>();
            }
        }
    }
}
