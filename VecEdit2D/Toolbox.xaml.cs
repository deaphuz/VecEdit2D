using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VecEdit2D
{
    public partial class Toolbox : Window
    {
        //Singleton
        private static Toolbox _instance;
        public string Shape { get; set; }
        public double StrokeThickness { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Radius { get; set; }
        private Toolbox()
        {
            InitializeComponent();
            SetControlsVisibility(false, false, false);
        }
        public static Toolbox Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Toolbox();
                }
                return _instance;
            }
        }

        private void StraightLine_Click(object sender, RoutedEventArgs e)
        {
            Shape = "straightLine";
        }

        private void Triangle_Click(object sender, RoutedEventArgs e)
        {
            Shape = "triangle";
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            Shape = "circle";
            SetControlsVisibility(false, false, true);
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            Shape = "rectangle";
            SetControlsVisibility(true, true, false);
        }

        private void TextBox_Click(object sender, RoutedEventArgs e)
        {
            Shape = "textBox";
        }

        private void Polygon_Click(object sender, RoutedEventArgs e)
        {
            Shape = "polygon";
        }
        private void StrokeThicknessEventHandler(object sender, TextChangedEventArgs args)
        {
            StrokeThickness = double.Parse(StrokeThicknessTextBox.Text);
        }
        private void WidthEventHandler(object sender, TextChangedEventArgs args)
        {
            Width = double.Parse(WidthTextBox.Text);
        }
        private void HeightEventHandler(object sender, TextChangedEventArgs args)
        {
            Height = double.Parse(HeightTextBox.Text);
        }
        private void RadiusEventHandler(object sender, TextChangedEventArgs args)
        {
            Radius = double.Parse(RadiusTextBox.Text);
        }
        private void SetControlsVisibility(bool widthVisible, bool heightVisible, bool radiusVisible)
        {
            WidthTextBlock.Visibility = widthVisible ? Visibility.Visible : Visibility.Collapsed;
            HeightTextBlock.Visibility = heightVisible ? Visibility.Visible : Visibility.Collapsed;
            RadiusTextBlock.Visibility = radiusVisible ? Visibility.Visible : Visibility.Collapsed;

            WidthTextBox.Visibility = widthVisible ? Visibility.Visible : Visibility.Collapsed;
            HeightTextBox.Visibility = heightVisible ? Visibility.Visible : Visibility.Collapsed;
            RadiusTextBox.Visibility = radiusVisible ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}