using System;

public interface ITapjoyEvent
{
	void ContentDidAppear(TapjoyEvent tapjoyEvent);

	void ContentDidDisappear(TapjoyEvent tapjoyEvent);

	void DidRequestAction(TapjoyEvent tapjoyEvent, TapjoyEventRequest tapjoyEventRequest);

	void SendEventFailed(TapjoyEvent tapjoyEvent, string error);

	void SendEventSucceeded(TapjoyEvent tapjoyEvent, bool contentIsAvailable);
}