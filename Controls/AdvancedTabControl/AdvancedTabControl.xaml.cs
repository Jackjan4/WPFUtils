using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace De.JanRoslan.WPFUtils.Controls
{



    /// <summary>
    ///     '''
    ///     ''' This control will NOT implement MVVM via a ViewModel because it is planned to be reusable, atomic like a Button (even outside of applications that use Prism).
    ///     ''' </summary>
    public partial class AdvancedTabControl : UserControl
    {



        // ' Properties
        public ObservableCollection<TabItem> TabCollection
        {
            get
            {
                return (ObservableCollection<TabItem>)GetValue(TabCollectionProperty);
            }
            set
            {
                SetValue(TabCollectionProperty, value);
            }
        }

        public static readonly DependencyProperty TabCollectionProperty = DependencyProperty.Register("TabCollection", typeof(ObservableCollection<TabItem>), typeof(AdvancedTabControl));

        public int SelectedTab
        {
            get
            {
                return (int)GetValue(SelectedTabProperty);
            }
            set
            {
                SetValue(SelectedTabProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedTabProperty = DependencyProperty.Register("SelectedTab", typeof(int), typeof(AdvancedTabControl), new FrameworkPropertyMetadata(OnSelectedTabPropertyChanged));

        public string NoTabsHereText
        {
            get => (string)GetValue(SelectedTabProperty);
            set => SetValue(SelectedTabProperty, value);
        }

        public static readonly DependencyProperty NoTabsHereTextProperty = DependencyProperty.Register("NoTabsHereText", typeof(string), typeof(AdvancedTabControl),new FrameworkPropertyMetadata(""));




        // ' Fields
        // Determines if this control got loaded once -> this Is used because Loaded() event can be called by XAML more than once
        private bool _loaded;



        // ' Events
        // Is raised when the user clicks on the Plus icon to add a New tab
        public event PlusSelectedEventHandler PlusSelected;

        public delegate void PlusSelectedEventHandler();



        /// <summary>
        ///         ''' Constructor
        ///         ''' </summary>
        public AdvancedTabControl()
        {
            // Init fields
            TabCollection = new ObservableCollection<TabItem>();
            _loaded = false;

            // Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent();
        }



        /// <summary>
        ///         ''' Is called by XAML GUI whenever the control is loaded
        ///         ''' </summary>
        ///         ''' <param name="sender"></param>
        ///         ''' <param name="e"></param>
        private void OnControlLoaded(Object sender, RoutedEventArgs e)
        {
            if (TabCollection != null && !_loaded)
            {
                TabCollection.CollectionChanged += (obj, evt) =>
                {
                    OnCollectionChanged(obj, evt);
                };
                _loaded = true;
            }
            e.Handled = true;
        }



        /// <summary>
        ///         ''' Is called by event handler if the
        ///         ''' </summary>
        ///         ''' <param name="sender"></param>
        ///         ''' <param name="e"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_loaded)
            {
                if (MainTabControl.Items.Count > 1)
                {
                    // Remove every Tab beside the Plus Button
                    for (int i = MainTabControl.Items.Count - 2; i >= 0; i += -1)
                    {
                        MainTabControl.Items.RemoveAt(i);
                    }
                }

                // TODO This Is actually a very dumb method of excersising changes in ObservableList. Instead we really should only touch elements that are affected by the change

                // Insert every Tab again
                foreach (object obj in TabCollection)
                {
                    MainTabControl.Items.Insert(MainTabControl.Items.Count - 1, obj);
                }

                // Check if "+"-Tab would be selected. If yes, select tab before
                if (MainTabControl.SelectedIndex == MainTabControl.Items.Count - 1)
                    MainTabControl.SelectedIndex = MainTabControl.SelectedIndex - 1;
            }
        }



        /// <summary>
        ///         ''' Is called by XAML binding when the source property of SelectedTab changed
        ///         ''' </summary>
        ///         ''' <param name="source"></param>
        ///         ''' <param name="e"></param>
        private static void OnSelectedTabPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (source is AdvancedTabControl)
            {
                TabControl tabCtrl = ((AdvancedTabControl)source).MainTabControl;
                tabCtrl.SelectedIndex = tabCtrl.Items.Count - 2;
            }
        }



        /// <summary>
        ///         ''' Is called by XAML when the user clicks on the Plus Tab
        ///         ''' </summary>
        ///         ''' <param name="sender"></param>
        ///         ''' <param name="e"></param>
        private void OnPlusClicked(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            PlusSelected?.Invoke();
        }



        /// <summary>
        ///         ''' Is called by XAML when the Selection of the internal TabControl has changed
        ///         ''' </summary>
        ///         ''' <param name="sender"></param>
        ///         ''' <param name="e"></param>
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs evt)
        {
            if (evt.AddedItems.Count > 0 && evt.AddedItems[0] is TabItem)
            {
                // Handle New Selection

                TabItem tab = (TabItem)evt.AddedItems[0];
                if ((string)tab.Header == "+")
                {
                    // + wants to be selected, we don't do that here! >:(

                    int pos = MainTabControl.Items.Count - 2;
                    if (pos < 0)
                        pos = 0;
                    MainTabControl.SelectedIndex = pos;
                }
            }
            else if (evt.AddedItems.Count == 0)
            {
                int pos = MainTabControl.Items.Count - 2;
                if (pos < 0)
                    pos = 0;
                MainTabControl.SelectedIndex = pos;
            }
        }
    }
}
