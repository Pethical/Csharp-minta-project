using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pethical.Framework.UI
{
    public class MenuBuilder : IMenuBuilder
    {
        public IList<MenuItem> Menu
        {
            get 
            {
                List<MenuItem> menu = new List<MenuItem>();
                foreach(IUICommand command in _commands){
                    menu.Add(BuildItem(command));
                }
                return menu;
            }
        }

        private MenuItem BuildItem(IUICommand command)
        {
            MenuItem item = new MenuItem();
            item.Header = command.Text;
            item.Command = command;
            //item.CommandParameter = command.Text;
            if (command.Children.Count() > 0)
            {
                foreach (IUICommand child in command.Children)
                {
                    item.Items.Add(BuildItem(child));
                }
            }
            return item;
        }

        private IEnumerable<IUICommand> _commands;

        public MenuBuilder(IEnumerable<IUICommand> commands)
        {
            _commands = commands;
        }

    }
}
