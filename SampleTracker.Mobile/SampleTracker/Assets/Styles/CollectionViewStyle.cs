using Xamarin.Forms;

namespace SampleTracker.Assets.Styles
{
    public static class CollectionViewStyle
    {
        public static TCollectionView VerticalListStyle<TCollectionView>(this TCollectionView collection) where TCollectionView : CollectionView
        {
            collection.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
            {
                ItemSpacing = 5,
            };
            collection.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepLastItemInView;

            return collection;
        }
    }
}
