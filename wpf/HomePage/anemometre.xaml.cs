using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Animation;


namespace HomePage
{
    /// <summary>
    /// Interaction logic for anemometre.xaml
    /// </summary>
    public partial class anemometre : UserControl
    {

        // 1,852 km/h = 1 Kt
        private Storyboard sb;
        private EasingDoubleKeyFrame keyFrame;

        public anemometre()
        {
            InitializeComponent();
            this.DataContext = this;

            sb = Grd.Resources["rotate"] as Storyboard;
            keyFrame = (sb.Children[0]
              as DoubleAnimationUsingKeyFrames).KeyFrames[0]
              as EasingDoubleKeyFrame;

            keyFrame.Value = 0;
        }
        public double Angle { get; set; }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);



            //Angle = Vitesse * 8.585;

            if (Vitesse < 40)
            {
                Angle = 36;
            }
            if (Vitesse > 210)
            {
                Angle = 324;
            }
            if (Vitesse > 40 && Vitesse < 61)
            {

                double coeff = 35d / 20d;
                Angle = ((Vitesse - 40) * coeff) + 36;
            }
            if (Vitesse > 60 && Vitesse < 81)
            {

                double coeff = 38d / 20d;
                Angle = ((Vitesse - 60) * coeff) + 71;
            }
            if (Vitesse > 80 && Vitesse < 101)
            {

                double coeff = 41d / 20d;
                Angle = ((Vitesse - 80) * coeff) + 109;
            }
            if (Vitesse > 100 && Vitesse < 121)
            {

                double coeff = 40d / 20d;
                Angle = ((Vitesse - 100) * coeff) + 150;
            }
            if (Vitesse > 120 && Vitesse < 141)
            {

                double coeff = 38d / 20d;
                Angle = ((Vitesse - 120) * coeff) + 190;
            }

            if (Vitesse > 140 && Vitesse < 161)
            {

                double coeff = 28d / 20d;
                Angle = ((Vitesse - 140) * coeff) + 228;
            }
            if (Vitesse > 160 && Vitesse < 181)
            {

                double coeff = 23d / 20d;
                Angle = ((Vitesse - 160) * coeff) + 265;
            }
            if (Vitesse > 180 && Vitesse < 211)
            {

                double coeff = 24d / 20d;
                Angle = ((Vitesse - 180) * coeff) + 288;
            }



            //if (Angle > 176) { Angle = 176; }
            //if (Angle < -176) { Angle = -176; }

            if (keyFrame != null)
            {
                keyFrame.Value = Angle;
                sb.Begin();
            }

        }


        [Category("Communes")]
        public double Vitesse
        {
            get
            {

                return (double)GetValue(ValueProperty);
            }
            set
            {

                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty =
         DependencyProperty.Register("Vitesse", typeof(double),
        typeof(anemometre), new PropertyMetadata(null));
    }

}
