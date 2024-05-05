using AdvancedShapes;
using BasicShapes;
using Paint.App.Models;
using Paint.WpfCommon;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Controls;
using Button = Wpf.Ui.Controls.Button;

namespace Paint.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        private ShapeSpecifications _shapeSpecs;
        private IShape _shapeManager;
        private UIElement? _curShape;
        private RunningMode _runningMode;

        private List<Item> _store;


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<IShape> _prototypes =
            [
                new LineShape(),
                new RectangleShape(),
                new EllipseShape(),
                new StarShape(),
                new ArrowShape(),
            ];

            //foreach (var prototype in _prototypes)
            //{
            //    var radioButton = new RadioButton()
            //    {
            //        GroupName = "ShapeSelect",
            //        Content = prototype.Name,
            //        Tag = prototype,
            //    };
            //    radioButton.Click += ShapeRadioButton_Clicked;

            //    shapesToolbar.Items.Add(radioButton);
            //}

            shapesToolbar.ItemsSource = _prototypes;
            var firstButton = GetVisualChild<RadioButton>(shapesToolbar, _prototypes[0], "radioButton");
            firstButton.IsChecked = true;
            _shapeManager = _prototypes[0];

            _shapeSpecs = new ShapeSpecifications(
               Brushes.Black,
               Brushes.Transparent,
               2,
               Strokes.Solid
           );
            strokeRectangle.Fill = _shapeSpecs.StrokeBrush;
            fillRectangle.Fill = _shapeSpecs.FillBrush;

            colorList.ItemsSource = new List<Brush>()
            {
                Brushes.Black,
                Brushes.White,
                Brushes.Red,
                Brushes.Green,
                Brushes.Blue,
                Brushes.Yellow,
                Brushes.Purple,
                Brushes.Transparent,
            };
            UpdateColorSelection();

            widthComboBox.ItemsSource = new List<int>()
            {
                2, 4, 8
            };

            strokeTypeComboBox.ItemsSource = new List<DoubleCollection>()
            {
                Strokes.Solid,
                Strokes.Dot,
                Strokes.Dash,
                Strokes.DashDotDot,
            };

            _runningMode = RunningMode.Default;
        }

        private T GetVisualChild<T>(
            ItemsControl itemsControl, object item, string controlName
        )
        {
            var cp = (ContentPresenter)itemsControl.ItemContainerGenerator.ContainerFromItem(item);
            var dt = cp.ContentTemplate;
            cp.ApplyTemplate();
            var control = (T)dt.FindName(controlName, cp);

            return control;
        }

        private void UpdateColorSelection()
        {
            if (_shapeManager.IsFillable)
            {
                fillRectangle.Visibility = Visibility.Visible;
                var button = GetVisualChild<Button>(colorList, Brushes.Transparent, "button");
                button.Visibility = Visibility.Visible;
            }
            else
            {
                fillRectangle.Visibility = Visibility.Collapsed;
                var button = GetVisualChild<Button>(colorList, Brushes.Transparent, "button");
                button.Visibility = Visibility.Collapsed;
            }
        }

        public void ShapeRadioButton_Clicked(object sender, RoutedEventArgs e)
        {
            var item = ((FrameworkElement)sender).DataContext;

            _shapeManager = (IShape)item;

            UpdateColorSelection();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_runningMode == RunningMode.Default)
            {
                _runningMode = RunningMode.Drawing;
                _shapeSpecs.StartPoint = e.GetPosition(myCanvas);
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_runningMode == RunningMode.Drawing)
            {
                _runningMode = RunningMode.Default;
                _curShape = null;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_runningMode != RunningMode.Drawing || e.LeftButton == MouseButtonState.Released) return;

            _shapeSpecs.IsConstrained = (Keyboard.Modifiers == ModifierKeys.Shift);
            _shapeSpecs.EndPoint = e.GetPosition(myCanvas);

            if (_curShape != null)
            {
                _shapeManager.Transform(_curShape, _shapeSpecs);
            }
            else
            {
                _curShape = _shapeManager.Create(_shapeSpecs);
                _curShape.MouseLeftButtonDown += Shape_MouseLeftButtonDown;

                var item = new Item(_shapeManager.GetType(), _shapeSpecs);


                myCanvas.Children.Add(_curShape);
            }
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            var item = ((FrameworkElement)sender).DataContext;

            _shapeSpecs.StrokeBrush = (Brush)item;
            strokeRectangle.Fill = _shapeSpecs.StrokeBrush;
        }

        private void ColorButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)sender).DataContext;

            _shapeSpecs.FillBrush = (Brush)item;
            fillRectangle.Fill = _shapeSpecs.FillBrush;
        }

        private void WidthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = ((ComboBox)sender).SelectedValue;
            _shapeSpecs.StrokeThickness = (int)value;
        }

        private void StrokeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = ((ComboBox)sender).SelectedValue;
            _shapeSpecs.StrokeDashArray = (DoubleCollection)value;
        }

        private void Shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var shape = (UIElement)sender;

                _runningMode = RunningMode.Editing;
                defaultToolbarTray.Visibility = Visibility.Collapsed;
                editingToolbarTray.Visibility = Visibility.Visible;

            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _runningMode = RunningMode.Default;

            defaultToolbarTray.Visibility = Visibility.Visible;
            editingToolbarTray.Visibility = Visibility.Collapsed;
        }
    }
}
