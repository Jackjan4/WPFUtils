using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPFUtils.Animations {
    public class SmoothHFadeOutManager : AnimationManager {

        private int distance;
        public int Distance {
            get { return distance; }
            set { distance = value; }
        }

        private DoubleAnimation movementAnimation; 
        private DoubleAnimation opacityAnimation;

        public SmoothHFadeOutManager(int distance, int time) {
            Distance = distance;
            Time = time;
            movementAnimation = new DoubleAnimation(Distance, new Duration(TimeSpan.FromMilliseconds(Time)));
            opacityAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(Time)));
            opacityAnimation.Completed += MovementAnimation_Completed;
        }

        


        /// <summary>
        /// Asynchronous: Starts an horizontal fade-out animation and immediately returns
        /// </summary>
        protected override void Start() {
            foreach (FrameworkElement e in Elements) {
                    e.RenderTransform.BeginAnimation(TranslateTransform.XProperty, movementAnimation);
                    e.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            }
        } 
    }
}
