using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WPFUtils.Animations {


    /// <summary>
    /// 
    /// </summary>
    public abstract class AnimationManager {
        private HashSet<FrameworkElement> elements;
        public HashSet<FrameworkElement> Elements {
            get { return elements; }
        }


        private int time;
        public int Time {
            get { return time; }
            set { time = value; }
        }


        public AnimationManager() {
            elements = new HashSet<FrameworkElement>();

        }

        public event EventHandler AnimationCompleted;

        private Boolean CompletedOnce;


        /// <summary>
        /// Registers an element for being affected by this animation
        /// </summary>
        /// <param name="element"></param>
        public void RegisterElement(FrameworkElement element) {
            elements.Add(element);
            TranslateTransform tT = new TranslateTransform();
            element.RenderTransform = tT;

        }


        public void RegisterAllChildren(DependencyObject parent) {

            IEnumerable coll = LogicalTreeHelper.GetChildren(parent);

            foreach(object child in coll) {
                if (child is FrameworkElement) {
                    RegisterElement((FrameworkElement)child);
                }
            }
        }

        /// <summary>
        /// Removes an element for being affected by this animation
        /// </summary>
        /// <param name="element"></param>
        public void RemoveElement(FrameworkElement element) {
            elements.Remove(element);
        }


        protected void MovementAnimation_Completed(object sender, EventArgs e) {

            if (!CompletedOnce) {
                if (AnimationCompleted != null) {
                    AnimationCompleted.Invoke(null, null);
                }
                
                CompletedOnce = true;
            }
            
        }


        public void StartAnimation() {
            CompletedOnce = false;
            Start();
        }

        /// <summary>
        /// Starts the animation
        /// </summary>
        protected abstract void Start();


    }
}
