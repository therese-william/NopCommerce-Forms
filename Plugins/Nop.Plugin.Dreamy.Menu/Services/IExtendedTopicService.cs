using Nop.Plugin.Dreamy.Menu.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu.Services
{
    public interface IExtendedTopicService
    {
        /// <summary>
        /// Save more data to topic
        /// </summary>
        /// <param name="ExtendedTopic">data to be saved</param>
        void SaveExtendedTopic(ExtendedTopicRecord ExtendedTopic);
        /// <summary>
        /// Update topic extended data
        /// </summary>
        /// <param name="ExtendedTopic">data to be updated</param>
        void UpdateExtendedTopic(ExtendedTopicRecord ExtendedTopic);
        /// <summary>
        /// delete extended topic data
        /// </summary>
        /// <param name="ExtendedTopic">data to be deleted</param>
        void DeleteExtendedTopic(ExtendedTopicRecord ExtendedTopic);
        /// <summary>
        /// get topic extra data
        /// </summary>
        /// <param name="TopicId">Topic Identification</param>
        /// <returns></returns>
        ExtendedTopicRecord GetExtendedTopicByTopicId(int TopicId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TopicId"></param>
        /// <returns></returns>
        IList<FullTopicRecord> GetTopicSections(int TopicId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TopicHeader"></param>
        void AddTopicHeader(TopicHeaderRecord TopicHeader);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TopicHeader"></param>
        void UpdateTopicHeader(TopicHeaderRecord TopicHeader);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TopicHeader"></param>
        void DeleteTopicHeader(TopicHeaderRecord TopicHeader);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TopicId"></param>
        /// <returns></returns>
        TopicHeaderRecord GetTopicHeaderByTopicId(int TopicId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HeaderContent"></param>
        void AddTopicHeaderContent(TopicHeaderCarouselRecord HeaderContent);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HeaderContent"></param>
        void UpdateTopicHeaderContent(TopicHeaderCarouselRecord HeaderContent);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HeaderContent"></param>
        void DeleteTopicHeaderContent(TopicHeaderCarouselRecord HeaderContent);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TopicHeaderId"></param>
        /// <returns></returns>
        IList<TopicHeaderCarouselRecord> GetCarouselContents(int TopicHeaderId);
    }
}
