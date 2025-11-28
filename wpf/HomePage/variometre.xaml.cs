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
using System.Windows.Media.Animation;


namespace HomePage
{
    /// <summary>
    /// Interaction logic for variometre.xaml
    /// </summary>
    public partial class variometre : UserControl
    {
        public double Angle { get; set; }

        private Storyboard sb;
        private EasingDoubleKeyFrame keyFrame;
        public variometre()
        {
            InitializeComponent();
            this.DataContext = this;

            sb = Grd.Resources["rotate"] as Storyboard;
            keyFrame = (sb.Children[0]
              as DoubleAnimationUsingKeyFrames).KeyFrames[0]
              as EasingDoubleKeyFrame;

            keyFrame.Value = 0;

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);



            Angle = DecifeetPm * 8.585;

            if (Angle > 176) { Angle = 176; }
            if (Angle < -176) { Angle = -176; }

            if (keyFrame != null)
            {
                keyFrame.Value = Angle;
                sb.Begin();
            }

        }

        [Category("Communes")]
        public double DecifeetPm
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
         DependencyProperty.Register("DecifeetPm", typeof(double),
        typeof(variometre), new PropertyMetadata(null));


    }
}
