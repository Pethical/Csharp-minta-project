using Microsoft.Practices.Prism.Events;
using Pethical.Framework.Messaging;
using Pethical.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pethical.Framework.UI
{

    public class EventAction<TEvent, TPayload> where TEvent : CompositePresentationEvent<TPayload>, new()
    {
        public Action Action
        {
            get
            {
                return () => { ServiceFinder.GetInstance<IEventAggregator>().GetEvent<TEvent>().Publish(default(TPayload)); };
            }
        }

        public Action<TPayload> PayloadAction
        {
            get
            {
                return (payload) => { 
                    ServiceFinder.GetInstance<IEventAggregator>().GetEvent<TEvent>().Publish(_payload); 
                };
            }
        }

        public EventAction()
        {

        }

        private TPayload _payload;
        public EventAction(TPayload payload) : this()
        {
            _payload = payload;
        }
    }

    public class EventCommand<TEvent, TPayload> : DelegateUICommand where TEvent : CompositePresentationEvent<TPayload>, new()
    {
        public EventCommand(string text) : base(new EventAction<TEvent, TPayload>().Action, text)
        {            
            
        }
    }


    public class EventPayloadCommand<TEvent, TPayload> : DelegateUICommand<TPayload> where TEvent : CompositePresentationEvent<TPayload>, new()
    {
        public EventPayloadCommand(TPayload payload, string text) : base(new EventAction<TEvent, TPayload>(payload).PayloadAction, text)
        {
           
        }
    }


    public class EventCommand<TEvent> : EventCommand<TEvent, object> where TEvent : CompositePresentationEvent<object>, new()
    {
        public EventCommand(string text) : base(text)
        {
            this.Text = text;
        }
    }

    public class SimpleEvent : RoutedEvent<object>
    {

    }

    public class EventCommand : EventPayloadCommand<SimpleEvent, object>
    {
        public EventCommand(object sender, string text) : base(sender, text)
        {

        }
    }



}
