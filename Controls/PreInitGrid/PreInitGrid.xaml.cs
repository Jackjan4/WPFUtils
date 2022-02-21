using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace De.JanRoslan.WPFUtils.Controls
{
    /// <summary>
    ///     ''' Code-behind file of PreInitGrid.xaml
    ///     ''' This control does not implement MVVM via ViewModel by design so it is reusable, portable and atomic like a Button (even outside Prism applications)
    ///     ''' </summary>
    public partial class PreInitGrid : UserControl
    {
        // ' Fields
        private bool _loaded;


        // ' Properties
        public ObservableCollection<PreInitGridProperty> PreInitItems
        {
            get { return (ObservableCollection<PreInitGridProperty>) GetValue(PreInitItemsProperty); }
            set { SetValue(PreInitItemsProperty, value); }
        }

        public static readonly DependencyProperty PreInitItemsProperty = DependencyProperty.Register("PreInitItems",
            typeof(ObservableCollection<PreInitGridProperty>), typeof(PreInitGrid));


        // ' Events
        public event PreInitChangedEventHandler PreInitChanged;

        public delegate void PreInitChangedEventHandler(PreInitGridProperty prop, object newValue);


        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        public PreInitGrid()
        {
            _loaded = false;

            if (PreInitItems == null)
            {
                PreInitItems = new ObservableCollection<PreInitGridProperty>();
            }

            // Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent();
        }


        /// <summary>
        ///         ''' Initializes the pre init grid controls for the properties
        ///         ''' The Pre-Init Grid shows properties of the calculation that are very important and so should be entered now in advance before the real CalcControls
        ///         ''' </summary>
        private void InitPreInitGrid(IEnumerable<PreInitGridProperty> preInits)
        {
            Grid grid = new Grid();

            grid.BeginInit();

            for (int i = 0; i <= preInits.Count() - 1; i++)
            {
                RowDefinition lblRowDef = new() {Height = GridLength.Auto};
                RowDefinition rowDef = new() {Height = GridLength.Auto};

                grid.RowDefinitions.Add(lblRowDef);
                grid.RowDefinitions.Add(rowDef);

                // Create property label
                Label lbl = new()
                {
                    Content = preInits.ElementAt(i).DisplayName,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 40, 0, 0),
                    FontWeight = FontWeights.Bold
                };
                Grid.SetRow(lbl, i * 2);
                grid.Children.Add(lbl);

                // Create property editor
                FrameworkElement uiElement = GetEditorForPreInit(preInits.ElementAt(i));
                uiElement.HorizontalAlignment = HorizontalAlignment.Center;
                uiElement.MinWidth = 70;
                Grid.SetRow(uiElement, i * 2 + 1);
                grid.Children.Add(uiElement);
            }

            grid.EndInit();
            MainDock.Children.Add(grid);
        }


        private FrameworkElement GetEditorForPreInit(PreInitGridProperty preInit) { 
            FrameworkElement result = null;


            Binding bind = new("Value")
            {
                Source = preInit
            };

        // Select type of the Property
        switch (preInit.Type)
        {
                case PreInitGridProperty.DataType.Bool:
                    CheckBox cb = new();
                    cb.SetBinding(CheckBox.IsCheckedProperty, bind);
                    //AddHandler cb.Checked, Sub() RaiseEvent PreInitChanged(preInit, cb.IsChecked)
                    //AddHandler cb.Unchecked, Sub() RaiseEvent PreInitChanged(preInit, cb.IsChecked)
                    result = cb;
                    break;
        
        
            case PreInitGridProperty.DataType.Text:
            TextBox textBox = new();
            textBox.SetBinding(TextBox.TextProperty, bind);
                //AddHandler textBox.TextChanged, Sub() RaiseEvent PreInitChanged(preInit, textBox.Text)
            result = textBox;
            break;
                case PreInitGridProperty.DataType.List:
                    // TODO:
                //Dim ext As ListElements = DirectCast(preInit.Extension, ListElements)
                    WrapPanel wrapPanel = new();
                    string groupId = new Random().Next().ToString();
                // Create a random string as a RadioButton Group identifier
        //For Each ele As String In ext.ListElements
        //    Dim radioBtn As New RadioButton With {
        //    .GroupName = groupId,
        //        .Content = ele,
        //        .Margin = New Thickness(4, 0, 4, 0)
        //}

        //radioBtn.SetBinding(RadioButton.IsCheckedProperty, bind)
            //AddHandler radioBtn.Checked, Sub() RaiseEvent PreInitChanged(preInit, radioBtn.Content)


           //         wrapPanel.Children.Add(radioBtn);
           // Next


           //         result = wrapPanel;
            break;
           //     case PreInitGridPropertyDataType.Int:
           // IntegerUpDown intUp = new();
       // intUp.SetBinding(IntegerUpDown.ValueProperty, bind)
            //AddHandler intUp.ValueChanged, Sub(sender, args) RaiseEvent PreInitChanged(preInit, args.NewValue)
        //result = intUp
           // Exit Select
           // Case DataType.Dec
           // Dim doubleUp As New DoubleUpDown()
        //doubleUp.SetBinding(DoubleUpDown.ValueProperty, bind)
            //AddHandler doubleUp.ValueChanged, Sub(sender, args) RaiseEvent PreInitChanged(preInit, args.NewValue)
        //result = doubleUp
            //Exit Select
            //End Select

            

            }
        return result;
        }


        /// <summary> 
        ///         ''' Is always called when the Control is loaded 
        ///         ''' </summary> 
        ///         ''' <param name="sender"></param> 
        ///         ''' <param name="e"></param>
        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            // We only want to execute this method once so we use a boolean 
            if (!_loaded)
            {
                // If the PreInitItems already got filled up by a source then initialize all editor controls 
                if (PreInitItems != null && PreInitItems.Count > 0)
                    InitPreInitGrid(PreInitItems);

                _loaded = true;
            }
        }
    }
}