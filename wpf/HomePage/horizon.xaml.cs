using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace HomePage
{
    /// <summary>
    /// Interaction logic for horizon.xaml
    /// </summary>
    public partial class horizon : UserControl, INotifyPropertyChanged
    {
        private Storyboard sb;
        private Storyboard sbtranslate;
        private EasingDoubleKeyFrame keyFrametranslate;
        private EasingDoubleKeyFrame keyFrame;
        private EasingDoubleKeyFrame keyFrame1;
        private EasingDoubleKeyFrame keyFrame2;


        public horizon()
        {
            InitializeComponent();
            this.DataContext = this;
            sb = Grd.Resources["rotate"] as Storyboard;
            keyFrame = (sb.Children[0]
              as DoubleAnimationUsingKeyFrames).KeyFrames[0]
              as EasingDoubleKeyFrame;
            keyFrame1 = (sb.Children[1]
               as DoubleAnimationUsingKeyFrames).KeyFrames[0]
               as EasingDoubleKeyFrame;
            keyFrame2 = (sb.Children[2]
               as DoubleAnimationUsingKeyFrames).KeyFrames[0]
               as EasingDoubleKeyFrame;

            keyFrame.Value = 0;
            keyFrame1.Value = 0;
            keyFrame2.Value = 0;


            sbtranslate = Grd.Resources["translate"] as Storyboard;
            keyFrametranslate = (sbtranslate.Children[0]
              as DoubleAnimationUsingKeyFrames).KeyFrames[0]
              as EasingDoubleKeyFrame;

        }

        //public override void OnApplyTemplate()
        //{
        // }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            TanguageCalcul = Tanguage * 12;
            if (Tanguage > 30) { TanguageCalcul = 30 * 12; }
            if (Tanguage < -30) { TanguageCalcul = -30 * 12; }


            if (keyFrametranslate != null)
            {
                keyFrametranslate.Value = TanguageCalcul;
                sbtranslate.Begin();
            }




            RoulisCalcul = Roulis;
            if (Roulis > 100) { RoulisCalcul = 100; }
            if (Roulis < -100) { RoulisCalcul = -100; }


            if (keyFrame != null)
            {
                keyFrame.Value = RoulisCalcul;
                keyFrame1.Value = RoulisCalcul;
                keyFrame2.Value = RoulisCalcul;
                sb.Begin();
            }


            //NotifyPropertyChanged("");

        }



        [Category("Communes")]
        public double Roulis
        {
            get
            {

                return (double)GetValue(RoulisProperty);
            }
            set
            {

                SetValue(RoulisProperty, value);
            }
        }

        public static readonly DependencyProperty RoulisProperty =
         DependencyProperty.Register(
             "Roulis",
             typeof(double),
             typeof(horizon),
             new PropertyMetadata(0.0, OnRoulisChange));



        private static void OnRoulisChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


        }

        [Category("Communes")]
        public double Tanguage
        {
            get
            {

                return (double)GetValue(TanguageProperty);
            }
            set
            {

                SetValue(TanguageProperty, value);
            }
        }


        public double RoulisCalcul { get; set; }


        private static double tanguage_calcul;
        public double TanguageCalcul
        {
            get
            {

                return tanguage_calcul;
            }
            set
            {

                tanguage_calcul = value;

            }
        }

        public static readonly DependencyProperty TanguageProperty =
         DependencyProperty.Register(
             "Tanguage",
             typeof(double),
             typeof(horizon),
             new PropertyMetadata(0.0, OnTanguageChange));



        private static void OnTanguageChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            //tanguage_calcul=(double) d.GetValue(TanguageProperty);

            //     this.DataContext = null;
            //NotifyPropertyChanged("TanguageCalcul");

        }
        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }

        #endregion


    }
}
