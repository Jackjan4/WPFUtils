using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using De.JanRoslan.WPFUtils.Controls;



namespace De.JanRoslan.WPFUtils.Controls
{



    /// <summary>
    ///     ''' ChangelogControl does not implements MVVM via a ViewModel since it is a simple Read-only control that does not execute any logic (with services e.g.) besides creating its own GUI
    ///     ''' It is intended to be reusable even outside of Prism applications
    ///     ''' </summary>
    public partial class ChangelogControl
    {



        // ' Properties
        public ObservableCollection<ChangelogEntry> Changelog
        {
            get
            {
                return (ObservableCollection < ChangelogEntry >)GetValue(ChangelogProperty);
            }
            set
            {
                SetValue(ChangelogProperty, value);
            }
        }
        public static readonly DependencyProperty ChangelogProperty = DependencyProperty.Register("Changelog", typeof(ObservableCollection<ChangelogEntry>), typeof(ChangelogControl));



        // ' Fields
        private bool _loaded;


        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        public ChangelogControl()
        {
            Changelog = new ObservableCollection<ChangelogEntry>();
            _loaded = false;

            // Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent();
        }



        /// <summary>
        ///         ''' This method does not break MVVM. It is needed for creating the Changelog UI visually by adding the needed visual components
        ///         ''' </summary>
        private void InitChangelog()
        {
            // Clear DockChangelog first
            DockChangelog.Children.Clear();

            if (Changelog!= null)
            {
                foreach (ChangelogEntry chg in Changelog)
                {

                    // ChangelogEntryControl
                    ChangelogEntryControl chgCtrl = new ChangelogEntryControl(chg);
                    DockPanel.SetDock(chgCtrl, Dock.Top);
                    DockChangelog.Children.Add(chgCtrl);

                    // Separator
                    Separator sep = new Separator();
                    sep.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC0050EF"));
                    DockPanel.SetDock(sep, Dock.Top);
                    DockChangelog.Children.Add(sep);
                }
            }
        }



        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        ///         ''' <param name="sender"></param>
        ///         ''' <param name="e"></param>
        private void OnControlLoaded(Object sender, RoutedEventArgs e)
        {
            if (!_loaded && Changelog!= null)
            {
                if (Changelog.Count > 0)
                    InitChangelog();
                Changelog.CollectionChanged += (obj,evt) =>
                {
                    InitChangelog();
                };
                _loaded = true;
            }
        }
    }
}