using Prism.Events;

namespace nTestSystem.Class
{
	public class InitializingEvent : PubSubEvent<bool>
	{
	}

	public class NavigateTransform : PubSubEvent<string>
	{ 
	
	}
}
