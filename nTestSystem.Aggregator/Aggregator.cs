using nTestSystem.DataHelper.Class;
using Prism.Events;

namespace nTestSystem.Aggregator
{

    public class LoadedEvent : PubSubEvent<bool> { }

    public class MessageSentEvent : PubSubEvent<string> { }

    public class NavigateEvent : PubSubEvent<string> { }

    public class TitleChangedEvent : PubSubEvent<string> { }

    public class UserInfoTransmit : PubSubEvent<UserInfoModel> { }

}
