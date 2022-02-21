using System.ComponentModel;
using System.Windows;


namespace De.JanRoslan.WPFUtils.Controls
{


    /// <summary>
    ///     ''' This is the code-behind class for ChangelogEntryControl
    ///     ''' This UserControl does not implement MVVM via ViewModel by design so it is reusable, atomic and abstract and can be used in all different kinds of application (even outside of Prism)
    ///     ''' </summary>
    public partial class ChangelogEntryControl : INotifyPropertyChanged
    {



        // ' Properties
        public ChangelogEntry ChangelogEntry
        {
            get
            {
                return (ChangelogEntry)GetValue(ChangelogEntryProperty);
            }
            set
            {
                SetValue(ChangelogEntryProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChangelogEntry"));
            }
        }
        public static readonly DependencyProperty ChangelogEntryProperty = DependencyProperty.Register("ChangelogEntry", typeof(ChangelogEntry), typeof(ChangelogEntryControl));



        // ' Events
        public event PropertyChangedEventHandler PropertyChanged;



        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        public ChangelogEntryControl()
        {
            // Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent();
        }



        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        public ChangelogEntryControl(ChangelogEntry entry)
        {
            ChangelogEntry = entry;

            // Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent();
        }
    }
}
