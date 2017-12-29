using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPFUtils.Animations {
    public class SmoothHFadeInManager : AnimationManager {

        private int distance;
        public int Distance {
            get { return distance; }
            set { distance = value; }
        }

        private DoubleAnimation movementAnimation;
        private DoubleAnimation opacityAnimation;

        public SmoothHFadeInManager(int distance, int time) {
            Distance = distance;
            Time = time;
            movementAnimation = new DoubleAnimation(Distance,0, new Duration(TimeSpan.FromMilliseconds(Time)));
            opacityAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(Time)));
            opacityAnimation.Completed += MovementAnimation_Completed;
        }




        /// <summary>
        /// Starts an horizontal fade-in animation
        /// </summary>
        protected override void Start() {
            foreach (FrameworkElement e in Elements) {
                e.RenderTransform.BeginAnimation(TranslateTransform.XProperty, movementAnimation);
                e.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            }
        }
    }
}
