using System.Collections.Generic;

namespace De.JanRoslan.WPFUtils.Controls.Interfaces {
    public interface IAutoCompleteProvider
    {


        IList<string> ProvideCompletions(string text);
    }
}
