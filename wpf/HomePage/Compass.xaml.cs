using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomePage
{
    /// <summary>
    /// Interaction logic for Compass.xaml
    /// </summary>
    public partial class Compass : UserControl
    {
        public double Angle { get; set; }

        private Storyboard sb;
        private Storyboard sbraz;
        private Storyboard sb360;
        private EasingDoubleKeyFrame keyFrame;
        private double angleantecedent = 0;


        public Compass()
        {
            InitializeComponent();

            this.DataContext = this;
            sb = Grd.Resources["rotate"] as Storyboard;
            sbraz = Grd.Resources["Raz"] as Storyboard;
            sb360 = Grd.Resources["Raz360"] as Storyboard;
            keyFrame = (sb.Children[0]
              as DoubleAnimationUsingKeyFrames).KeyFrames[0]
              as EasingDoubleKeyFrame;

            keyFrame.Value = 0;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);



            Angle = Heading * -1;

            if (Angle - angleantecedent > 180)
            {
                //jouer raz de 350 à 0
                sbraz.Begin();
                Angle = 0;
                angleantecedent = Angle;
                return;
            }
            if (Angle - angleantecedent < -180)
            {
                sb360.Begin();
                Angle = -359.99;
                angleantecedent = Angle;
                return;
            }
            angleantecedent = Angle;

            if (keyFrame != null)
            {
                keyFrame.Value = Angle;
                sb.Begin();
            }

        }

        [Category("Communes")]
        public double Heading
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
        DependencyProperty.Register("Heading", typeof(double),
        typeof(Compass), new PropertyMetadata(null));






    }
}
