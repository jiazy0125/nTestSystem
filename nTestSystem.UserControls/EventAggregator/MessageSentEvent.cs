﻿using Prism.Events;

namespace nTestSystem.UserControls.EventAggregator
{
	public class MessageSentEvent : PubSubEvent<string>
	{
	}

	public class NavigateEvent : PubSubEvent<string>
	{

	}

}
