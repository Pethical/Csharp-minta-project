using Microsoft.Practices.Prism.Commands;
using Pethical.Framework.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Pethical.Framework.UI
{


    public interface IUICommand : ICommand
    {
        string Text { get; }
        string Description { get; }

        IList<IUICommand> Children { get; }

        void AddChild(IUICommand command);

    }

    public class DelegateUICommand : DelegateCommand, IUICommand
    {
        public string Text { get; set; }
        public string Description { get; set; }

        public DelegateUICommand(Action executeMethod) : base(executeMethod) 
        { 
            
        }
        public DelegateUICommand(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        { 
        }

        public DelegateUICommand(Action executeMethod, string text) : base(executeMethod)
        {
            Text = text;
        }

        public DelegateUICommand(Action executeMethod, string text, string description): base(executeMethod)
        {
            Text = text;
            Description = description;
        }

        public DelegateUICommand(Action executeMethod, Func<bool> canExecuteMethod, string text) : base(executeMethod)
        {
            Text = text;
        }

        public DelegateUICommand(Action executeMethod, Func<bool> canExecuteMethod, string text, string description) : base(executeMethod, canExecuteMethod)
        {
            Text = text;
            Description = description;
        }

        private IList<IUICommand> _children;
        public IList<IUICommand> Children
        {
            get
            {
                if (_children == null) _children = new List<IUICommand>();
                return _children;
            }
        }

        public void AddChild(IUICommand command)
        {
            Children.Add(command);            
        }
    }

    public class DelegateUICommand<T> : DelegateCommand<T>, IUICommand
    {
        public string Text { get; set; }
        public string Description { get; set; }
        
        public DelegateUICommand(Action<T> executeMethod) : base(executeMethod)
        {
        }
   
        public DelegateUICommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        { 
        
        }

        public DelegateUICommand(Action<T> executeMethod, string text) : base(executeMethod)
        {
            Text = text;
        }

        public DelegateUICommand(Action<T> executeMethod, string text, string description): base(executeMethod)
        {
            Text = text;
            Description = description;
        }

        public DelegateUICommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, string text) : base(executeMethod)
        {
            Text = text;
        }

        public DelegateUICommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, string text, string description) : base(executeMethod, canExecuteMethod)
        {
            Text = text;
            Description = description;
        }

        private IList<IUICommand> _children;
        public IList<IUICommand> Children
        {
            get
            {
                if (_children == null) _children = new List<IUICommand>();
                return _children;
            }
        }

        public void AddChild(IUICommand command)
        {
            Children.Add(command);
        }


    }

}
