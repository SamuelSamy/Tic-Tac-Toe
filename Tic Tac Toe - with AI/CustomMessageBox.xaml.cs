using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tic_Tac_Toe___with_AI
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        #region Vars

        public Color red = Color.FromRgb(255, 100, 100);
        public Color green = Color.FromRgb(100, 255, 100);
        public Color yellow = Color.FromRgb(255, 255, 100);

        public string message = "";
        public SolidColorBrush color = new SolidColorBrush();

        #endregion

        public CustomMessageBox(string stage) // stage -> win (green), lose (red), tie (yellow) 
        {
            
            if (stage == "win")
            {
                color = new SolidColorBrush(green);
                message = "You won! Do you want to play again?";
            }
            else if (stage == "lose")
            {
                color = new SolidColorBrush(red);
                message = "You lost! Do you want to play again?";
            }
            else
            {
                color = new SolidColorBrush(yellow);
                message = "It's a tie! Do you want to play again?";
            }
            InitializeComponent();
        }

        #region SelectAnswer

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            Border.Background = color;
            TextBlock.Text = message;

            this.Height = Owner.ActualHeight / 1.5;
            this.Width = Owner.ActualWidth / 1.5;


            if (Owner.WindowState == WindowState.Maximized)
            {
                this.Left = (Owner.ActualWidth - this.ActualWidth) / 2;
                this.Top = (Owner.ActualHeight - this.ActualHeight) / 2;
            }
            else
            {
                this.Left = Owner.Left + (Owner.ActualWidth - this.ActualWidth) / 2;
                this.Top = Owner.Top + (Owner.ActualHeight - this.ActualHeight) / 2;
            }

            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = 0.75;
            anim.Duration = (Duration)TimeSpan.FromSeconds(.3);

            this.BeginAnimation(UIElement.OpacityProperty, anim);            
        }

        #region ResizeFunctions

        private void btnYes_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size n = e.NewSize;
            Size o = e.PreviousSize;

            double l = n.Width / o.Width;

            if (l != double.PositiveInfinity)
            {
                btnYes.FontSize *= l;
            }
        }

        private void btnNo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size n = e.NewSize;
            Size o = e.PreviousSize;

            double l = n.Width / o.Width;

            if (l != double.PositiveInfinity)
            {
                btnNo.FontSize *= l;
            }
        }

        private void TextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size n = e.NewSize;
            Size o = e.PreviousSize;

            double l = n.Width / o.Width;

            if (l != double.PositiveInfinity)
            {
                TextBlock.FontSize *= l;
            }
        }

        #endregion
    }
}
