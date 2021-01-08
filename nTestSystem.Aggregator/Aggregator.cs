using nTestSystem.DataHelper.Class;
using Prism.Events;
using System.Collections.Generic;

namespace nTestSystem.Aggregator
{

    public class LoadedEvent : PubSubEvent<bool> { }

    public class MessageSentEvent : PubSubEvent<string> { }

    public class NavigateEvent : PubSubEvent<string> { }

    public class TitleChangedEvent : PubSubEvent<string> { }

    public class UserInfoTransmit : PubSubEvent<UserInfoModel> { }
    
    public class ValueUpdate : PubSubEvent<KeyValue> { }
    
}
