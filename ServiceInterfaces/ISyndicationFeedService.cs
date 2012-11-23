using System.ServiceModel.Syndication;

namespace ServiceInterfaces
{
    public interface ISyndicationFeedService
    {
        SyndicationFeed CreateSyndicationFeed(string feedType, string scheme, string host);
    }
}