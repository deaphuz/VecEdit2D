using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        public string currentShape { get; set; }
        public System.Windows.Media.Color primaryColor { get; set; }
        public System.Windows.Media.Color secondaryColor { get; set; }
        public double StrokeThickness { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Radius { get; set; }

        //others
        public double scaleFactor { get; set; }
        public double rotAngle { get; set; }
        public double trX { get; set; }
        public double trY { get; set; }


        private Toolbox()
        {
            InitializeComponent();
            primaryColor = Colors.White;
            secondaryColor = Colors.Black;
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

        private void Group_Click(object sender, RoutedEventArgs e)
        {
            currentShape = "group";
        }
        private void StraightLine_Click(object sender, RoutedEventArgs e)
        {
            currentShape = "straightLine";
        }

        private void Polygon_Click(object sender, RoutedEventArgs e)
        {
            currentShape = "polygon";
        }
        private void MultiLine_Click(object sender, RoutedEventArgs e)
        {
            currentShape = "polyline";
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            currentShape = "circle";
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            currentShape = "rectangle";
        }

        private void TextArea_Click(object sender, RoutedEventArgs e)
        {
            currentShape = "textarea";
        }

        private void StrokeThicknessEventHandler(object sender, TextChangedEventArgs args)
        {
            StrokeThickness = double.Parse(StrokeThicknessTextBox.Text);
        }
/*
        private void RadiusEventHandler(object sender, TextChangedEventArgs args)
        {
            Radius = double.Parse(RadiusTextBox.Text);
        }
      */
        private void PrimaryColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if(colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color SWDColor = colorDialog.Color;
                primaryColor = System.Windows.Media.Color.FromArgb(SWDColor.A, SWDColor.R, SWDColor.G, SWDColor.B);
                primaryColorRect.Fill = new SolidColorBrush(primaryColor);
            }
        }
        private void SecondaryColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color SWDColor = colorDialog.Color;
                secondaryColor = System.Windows.Media.Color.FromArgb(SWDColor.A, SWDColor.R, SWDColor.G, SWDColor.B);
                secondaryColorRect.Fill = new SolidColorBrush(secondaryColor);
            }
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;
            double value = 0.0;
            if (double.TryParse(textBox.Text, out value))
            {
                switch(textBox.Name)
                {
                    case "ScaleBox":
                        scaleFactor = value;
                        break;
                    case "RotationBox":
                        rotAngle = value;
                        break;
                    case "TranslationXBox":
                        trX = value;
                        break;
                    case "TranslationYBox":
                        trY = value;
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            System.Windows.Controls.Slider slider = (System.Windows.Controls.Slider)sender;

            double newValue = e.NewValue;

            switch(slider.Name)
            {
                case "scaleSlider":
                    ScaleBox.Text = newValue.ToString();
                    scaleFactor = newValue;
                    break;
                case "rotationSlider":
                    RotationBox.Text = newValue.ToString();
                    rotAngle = newValue;
                    break;
                case "trSliderX":
                    TranslationXBox.Text = newValue.ToString();
                    trX = newValue;
                    break;
                case "trSliderY":
                    TranslationYBox.Text = newValue.ToString();
                    trY = newValue;
                    break;
                default:
                    break;
            }
        }
    }
}