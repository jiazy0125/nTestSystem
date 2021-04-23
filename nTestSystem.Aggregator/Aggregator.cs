using nTestSystem.DataHelper.Class;
using nTestSystem.DataHelper.Models;
using Prism.Events;

namespace nTestSystem.Aggregator
{

    public class LoadedEvent : PubSubEvent<bool> { }

    public class MessageSentEvent : PubSubEvent<string> { }

    public class NavigateEvent : PubSubEvent<string> { }

    public class ProfileUpdate : PubSubEvent<ProfileInfo> { }

    public class ResourceChanged : PubSubEvent<bool> { }
    
}
