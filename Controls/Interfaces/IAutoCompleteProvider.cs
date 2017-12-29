using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtils.Controls.Interfaces {
    public interface IAutoCompleteProvider
    {


        List<string> ProvideCompletions(string text);
    }
}
