using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using De.JanRoslan.WPFUtils.Annotations;

namespace De.JanRoslan.WPFUtils.Controls {
    /// <summary>
    /// Interaction logic for ListSelection.xaml
    /// </summary>
    public partial class ListSelection : UserControl, INotifyPropertyChanged {

        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        public event RoutedEventHandler LeftClick;
        public event RoutedEventHandler RightClick;
        public event RoutedEventHandler LeftAllClick;
        public event RoutedEventHandler RightAllClick;


        // Properties
        private string _sourceText;
        public string SourceText {
            get => _sourceText;
            set {
                _sourceText = value;
                OnSourceTextChanged();
            }
        }

        private string _targetText;

        public string TargetText {
            get => _targetText;
            set {
                _targetText = value;
                OnTargetTextChanged();
            }
        }


        /// <summary>
        /// SourceItems(ItemsSource) of the left ListView
        /// </summary>
        public ObservableCollection<object> SourceItems {
            get => (ObservableCollection<object>) GetValue(SourceItemsProperty);
            set => SetValue(SourceItemsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceItemsProperty =
            DependencyProperty.Register("SourceItems", typeof(ObservableCollection<object>), typeof(ListSelection), new PropertyMetadata(null, OnSourceItemsChanged));


        /// <summary>
        /// SourceItems(ItemsSource) of the left ListView
        /// </summary>
        public ObservableCollection<object> TargetItems {
            get => (ObservableCollection<object>) GetValue(TargetItemsProperty);
            set => SetValue(TargetItemsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetItemsProperty =
            DependencyProperty.Register("TargetItems", typeof(ObservableCollection<object>), typeof(ListSelection), new PropertyMetadata(null, OnTargetItemsChanged));



        /// <summary>
        /// SourceItems(ItemsSource) of the left ListView
        /// </summary>
        public object LeftSelectedItem {
            get => (object) GetValue(LeftSelectedItemProperty);
            set => SetValue(LeftSelectedItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftSelectedItemProperty =
            DependencyProperty.Register("LeftSelectedItem", typeof(object), typeof(ListSelection), new PropertyMetadata(null, OnLeftSelectedItemChanged));




        /// <summary>
        /// SourceItems(ItemsSource) of the left ListView
        /// </summary>
        public object RightSelectedItem {
            get => (object) GetValue(RightSelectedItemProperty);
            set => SetValue(RightSelectedItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSelectedItemProperty =
            DependencyProperty.Register("RightSelectedItem", typeof(object), typeof(ListSelection), new PropertyMetadata(null, OnRightSelectedItemChanged));




        /// <summary>
        /// Constructor
        /// </summary>
        public ListSelection() {
            InitializeComponent();
        }



        /// <summary>
        /// TODO: Insert same comment from BeProX
        /// </summary>
        /// <param name="name"></param>
        private void RaisePropChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void OnBtnRightClick(object sender, RoutedEventArgs e) {

            if (LeftListView.SelectedIndex < 0) {
                return;
            }

            object selItem = LeftListView.SelectedItem;
            SourceItems.Remove(selItem);
            TargetItems.Add(selItem);


            RightClick?.Invoke(sender, e);
        }

        private void OnBtnLeftClick(object sender, RoutedEventArgs e) {

            if (RightListView.SelectedIndex < 0) {
                return;
            }

            object selItem = RightListView.SelectedItem;
            TargetItems.Remove(selItem);
            SourceItems.Add(selItem);

            LeftClick?.Invoke(sender, e);
        }

        private void OnBtnRightAllClick(object sender, RoutedEventArgs e) {

            // Copy all elements from LeftListView to RightListView
            foreach (object obj in SourceItems) {
                TargetItems.Add(obj);
            }

            // Clear LeftListView
            SourceItems.Clear();

            RightAllClick?.Invoke(sender, e);
        }

        private void OnBtnLeftAllClick(object sender, RoutedEventArgs e) {

            // Copy all elements from RightListView to LeftListView
            foreach (object obj in TargetItems) {
                SourceItems.Add(obj);
            }

            // Clear RightListView
            TargetItems.Clear();

            LeftAllClick?.Invoke(sender, e);
        }

        private void OnSourceTextChanged() {
            SourceLabel.Content = SourceText;
        }


        private void OnTargetTextChanged() {
            TargetLabel.Content = TargetText;
        }

        private void OnSourceItemsChanged() {
            LeftListView.ItemsSource = SourceItems;
        }


        private void OnTargetItemsChanged() {
            RightListView.ItemsSource = TargetItems;
        }

        private void OnLeftSelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void OnRightSelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void OnRightSelectedItemChanged() {

        }

        private void OnLeftSelectedItemChanged() {
            LeftListView.SelectedItem = LeftSelectedItem;
        }



        /// ===== Static functions

        ///
        ///
        /// 
        private static void OnSourceItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ListSelection origin = (ListSelection) d;

            origin.OnSourceItemsChanged();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTargetItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ListSelection origin = (ListSelection) d;

            origin.OnTargetItemsChanged();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnRightSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ListSelection origin = (ListSelection) d;

            origin.OnRightSelectedItemChanged();
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnLeftSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ListSelection origin = (ListSelection) d;

            origin.OnLeftSelectedItemChanged();
        }


    }
}
