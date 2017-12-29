using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPFUtils.Controls.Interfaces;
using WPFUtils.Enums;

namespace WPFUtils.Controls {



    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>
    public partial class AutoCompleteTextBox : UserControl {



        /// <summary>
        /// 
        /// </summary>
        public string Text {
            get { return TxtBox.Text; }
            set {
                TxtBox.Text = value;
                SetValue(TextProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AutoCompleteTextBox));




        /// <summary>
        /// 
        /// </summary>
        public ReplaceMode ReplaceMode { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public IAutoCompleteProvider AutoCompleteProvider { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public int MaxShownItems { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public AutoCompleteTextBox() {
            InitializeComponent();
            MaxShownItems = -1;


        }

        private void AttachToParentWindow() {
            var obj = VisualTreeHelper.GetParent(this);

            if (obj == null) return;

            while (!(obj is Window)) {
                obj = VisualTreeHelper.GetParent(obj);
            }
            ((Window)obj).LocationChanged += ParentWindow_LocationChanged;
        }



        private void ParentWindow_LocationChanged(object sender, EventArgs e) {
            if (LstBox.Visibility != Visibility.Visible) return;

            var mode = PopUp.Placement;
            PopUp.Placement = PlacementMode.Relative;
            PopUp.Placement = mode;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, TextChangedEventArgs e) {
            var lst = AutoCompleteProvider.ProvideCompletions(TxtBox.Text);

            // Only show defined range
            if (MaxShownItems > 0) {
                // lst = lst.GetRange(0, MaxShownItems);
                LstBox.Height = MaxShownItems * 25;
            }
            LstBox.ItemsSource = lst;
            LstBox.Visibility = Visibility.Visible;
        }

        private void LstBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (e.AddedItems.Count == 0) return;

            string str = e.AddedItems[0].ToString();
            switch (ReplaceMode) {
                case ReplaceMode.Append:
                    TxtBox.Text += str;
                    break;
                case ReplaceMode.Replace:
                    TxtBox.Text = str;
                    break;
                case ReplaceMode.LastReplace:
                    string substr = str;
                    for (int i = str.Length; i > 0; i--) {
                        substr = str.Substring(0, i);
                        if (TxtBox.Text.EndsWith(substr)) {
                            TxtBox.Text = TxtBox.Text.Substring(0, TxtBox.Text.Length - substr.Length) + str;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }

            LstBox.UnselectAll();
            LstBox.Visibility = Visibility.Collapsed;
            TxtBox.Focus();
            TxtBox.CaretIndex = TxtBox.Text.Length;
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e) {
            LstBox.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            AttachToParentWindow();
        }
    }
}
