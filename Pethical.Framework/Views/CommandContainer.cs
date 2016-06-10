using Pethical.Framework.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pethical.Framework.Views
{

    public class CommandList : ObservableCollection<IUICommand>
    {
    }

    public interface ICommandContainer
    {
        CommandList Commands { get; }
    }

    public class CommandContainer : ICommandContainer
    {
        public CommandList Commands { get; set; }        

        public CommandContainer()
        {
            Commands = new CommandList();
        }

        public bool Exists(string text)
        {
            return Commands.Count(p => p.Text == text) > 0;
        }

        public IUICommand Item(string text)
        {
            if (Exists(text)) return Commands.Where(p => p.Text == text).First();
            return null;
        }

    }

}
