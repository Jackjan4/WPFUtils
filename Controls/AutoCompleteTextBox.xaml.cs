using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using WPFUtils.Controls.Interfaces;
using WPFUtils.Enums;

namespace WPFUtils.Controls {



    /// <inheritdoc>
    /// 
    /// </inheritdoc>
    ///
    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>
    public partial class AutoCompleteTextBox {



        /// <summary>
        /// Text Property of TxtBox
        /// </summary>
        public string Text {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AutoCompleteTextBox));


        public TextWrapping TextWrapping
        {
            get => TxtBox.TextWrapping;
            set => TxtBox.TextWrapping = value;
        }


        public bool AcceptsReturn
        {
            get => TxtBox.AcceptsReturn;
            set => TxtBox.AcceptsReturn = value;
        }

        /// <summary>
        /// Gets or sets the ReplaceMode
        /// </summary>
        public ReplaceMode ReplaceMode { get; set; }



        /// <summary>
        /// Gets or sets the AutoCompleteProvider
        /// </summary>
        public IAutoCompleteProvider AutoCompleteProvider { get; set; }



        /// <summary>
        /// Gets or sets the maximum of shown items at once. Other items can be seen by scrolling.
        /// </summary>
        public int MaxShownItems { get; set; }



        /// <summary>
        /// Default constructor
        /// </summary>
        public AutoCompleteTextBox() {
            InitializeComponent();
            MaxShownItems = -1;
        }



        /// <summary>
        /// Registers this control correctly for parent window changes
        /// This is needed to react on Window move events since the PopUp is not moving automatically
        /// </summary>
        private void AttachToParentWindow() {
            DependencyObject obj = VisualTreeHelper.GetParent(this);

            if (obj == null) return;

            while (!(obj is Window)) {
                if (obj == null) return;
                obj = VisualTreeHelper.GetParent(obj);
            }
            ((Window)obj).LocationChanged += ParentWindow_LocationChanged;
        }



        /// <summary>
        /// Is called when the parent Window of this control changed its location.
        /// The PopUp is moved when this event occurs since it cannot move correctly on its own.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentWindow_LocationChanged(object sender, EventArgs e) {
            if (LstBox.Visibility != Visibility.Visible) return;
            var mode = PopUp.Placement;
            PopUp.Placement = PlacementMode.Relative;
            PopUp.Placement = mode;
        }




        /// <summary>
        /// Is called when the TextBox text is changed by the user.
        /// Notifies the AutoCompleteProvider to show AutoComplete suggestions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {

            if (AutoCompleteProvider == null) return;

            var lst = AutoCompleteProvider.ProvideCompletions(TxtBox.Text);

            if (lst.Count == 0) {
                LstBox.Visibility = Visibility.Collapsed;
                return;
            }

            // Only show defined range
            if (MaxShownItems > 0) {
                // lst = lst.GetRange(0, MaxShownItems);
                LstBox.Height = MaxShownItems * 25;
            }
            LstBox.ItemsSource = lst;
            LstBox.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Is called when the user selects an item in the ListBox
        /// Depending on the ReplaceMode the item is appended/replaced in the TxtBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count == 0) return;

            string str = e.AddedItems[0].ToString();
            switch (ReplaceMode) {
                case ReplaceMode.Append:
                    TxtBox.Text += str;
                    break;
                default:
                case ReplaceMode.Replace:
                    TxtBox.Text = str;
                    break;
                case ReplaceMode.LastReplace:
                    string substr;
                    for (int i = str.Length; i > 0; i--) {
                        substr = str.Substring(0, i);
                        if (TxtBox.Text.EndsWith(substr)) {
                            TxtBox.Text = TxtBox.Text.Substring(0, TxtBox.Text.Length - substr.Length) + str;
                            break;
                        }
                    }
                    break;
            }

            LstBox.UnselectAll();
            LstBox.Visibility = Visibility.Collapsed;
            TxtBox.Focus();
            TxtBox.CaretIndex = TxtBox.Text.Length;
        }



        /// <summary>
        /// Is called when this control losts focus and should be collapsed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_LostFocus(object sender, RoutedEventArgs e) {
            LstBox.Visibility = Visibility.Collapsed;
        }



        /// <summary>
        /// Is called when this control is loaded and ready to be rendered.
        /// Attaches this control to its parent window.
        /// (This cannot happen in constructor since this control is not attached to a parent during constructing)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            AttachToParentWindow();
        }
    }
}
