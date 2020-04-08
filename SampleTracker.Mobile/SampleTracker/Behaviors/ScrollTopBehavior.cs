using System;
using System.Collections;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SampleTracker.Behaviors
{
    public class ScrollTopBehavior<T> : Behavior<CollectionView>
    {
        public CollectionView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(CollectionView bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;
            var source = bindable.ItemsSource;
            if (source != null)
            {
                ((ObservableCollection<T>)source).CollectionChanged += ScrollToTopBehavior_CollectionChanged;
            }
        }

        protected override void OnDetachingFrom(CollectionView bindable)
        {
            base.OnDetachingFrom(bindable);

            var source = bindable.ItemsSource;
            if (source != null)
                ((ObservableCollection<T>)source).CollectionChanged -= ScrollToTopBehavior_CollectionChanged;
        }

        private void ScrollToTopBehavior_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var collection = (ICollection)(AssociatedObject.ItemsSource);
            if (collection != null)
                if (collection.Count != 0)
                    try
                    {
                        AssociatedObject.ScrollTo(0, position: ScrollToPosition.MakeVisible, animate: true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
        }
    }
}
