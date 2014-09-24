using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CodingChick.UdemyUniversal.Controls.Custom
{
    [TemplateVisualState(GroupName = ITEMS_STATE_NAME, Name = HAS_ITEMS_STATE_NAME)]
    [TemplateVisualState(GroupName = ITEMS_STATE_NAME, Name = NO_ITEMS_STATE_NAME)]
    public class Loader : Control
    {
        public const string ITEMS_STATE_NAME = "ItemsState";
        public const string HAS_ITEMS_STATE_NAME = "HasItems";
        public const string NO_ITEMS_STATE_NAME = "NoItems";

        public Loader()
        {
            this.DefaultStyleKey = typeof(Loader);
            this.Unloaded += Loader_Unloaded;
        }


        #region public object ItemSource
        /// <summary>
        /// Gets or sets an ItemSource based on which we'll hide or show the loader
        /// </summary>
        public object ItemSource
        {
            get { return GetValue(ItemSourceProperty) as object; }
            set { SetValue(ItemSourceProperty, value); }
        }

        /// <summary>
        /// Identifies the ItemSource dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
                "ItemSource",
                typeof(object),
                typeof(Loader),
                new PropertyMetadata(null, OnItemSourcePropertyChanged));

        /// <summary>
        /// ItemSourceProperty property changed handler.
        /// </summary>
        /// <param name="d">Loader that changed its ItemSource.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnItemSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Loader source = d as Loader;
            object value = e.NewValue as object;

            if (value != null && value is INotifyCollectionChanged && value is IList && source != null)
            {
                source.InitializeCollectionChanges(((INotifyCollectionChanged)value));
                source.UpdateState();
            }
        }

        private void InitializeCollectionChanges(INotifyCollectionChanged notifyCollectionChanged)
        {
            notifyCollectionChanged.CollectionChanged += Loader_CollectionChanged;
        }

        void Loader_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.ItemSource != null && this.ItemSource is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)ItemSource).CollectionChanged -= Loader_CollectionChanged;
            }
        }

        void Loader_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateState();
        }

        private void UpdateState()
        {
            if (this.ItemSource == null || (!(this.ItemSource is IList)))
            {
                VisualStateManager.GoToState(this, NO_ITEMS_STATE_NAME, false);
                return;
            }

            var itemsSource = (IList)this.ItemSource;
            if (itemsSource.Count == 0)
            {
                VisualStateManager.GoToState(this, NO_ITEMS_STATE_NAME, false);
            }
            else
            {
                VisualStateManager.GoToState(this, HAS_ITEMS_STATE_NAME, false);
            }
        }

        #endregion public object ItemSource

    }
}
