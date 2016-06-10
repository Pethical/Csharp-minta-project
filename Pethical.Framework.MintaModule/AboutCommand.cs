using Pethical.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pethical.Framework.TestModule
{
    public class AboutCommand : EventPayloadCommand<AboutEvent, string>
    {
        public AboutCommand(string text, string payload) : base(payload, text)
        {
        }

    }
}
