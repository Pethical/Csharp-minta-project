using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pethical.Framework.UI
{
    public interface IMenuBuilder
    {
        IList<MenuItem> Menu { get; }

    }
}
