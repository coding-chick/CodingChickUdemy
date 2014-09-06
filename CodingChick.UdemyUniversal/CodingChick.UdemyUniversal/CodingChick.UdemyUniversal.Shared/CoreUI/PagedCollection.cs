using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using CodingChick.UdemyUniversal.ViewModels;

namespace CodingChick.UdemyUniversal.CoreUI
{
    public class PagedCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private Func<uint, int, Task<IEnumerable<T>>> _loadMoreItemsOperation;
        private readonly int _maxLoadedPages;
        public bool HasMoreItems { get; protected set; }

        public int PageNumber { get; set; }

        public PagedCollection(Func<uint, int, Task<IEnumerable<T>>> loadMoreItemsOperation, int maxLoadedPages = 1000)
        {
            HasMoreItems = true;
            this._loadMoreItemsOperation = loadMoreItemsOperation;
            _maxLoadedPages = maxLoadedPages;
            PageNumber = 1;

            if (DesignMode.DesignModeEnabled)
            {
                HasMoreItems = false;
            }
        }


        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            CoreDispatcher dispatcher = Window.Current == null ? null : Window.Current.Dispatcher;
            return Task.Run<LoadMoreItemsResult>(async () =>
            {
                var newItems = await _loadMoreItemsOperation(count, PageNumber);

                if (newItems == null || PageNumber > _maxLoadedPages)
                {
                    count = 0;
                    HasMoreItems = false;
                }
                else
                {
                    DispatchedHandler addItemsHandler = () =>
                    {
                        foreach (T item in newItems)
                        {
                            this.Add(item);
                        }
                    };

                    if (dispatcher != null)
                    {
                        await dispatcher.RunAsync(CoreDispatcherPriority.Normal, addItemsHandler);
                    }
                    else
                    {
                        addItemsHandler();
                    }
                }

                ++PageNumber;
                return new LoadMoreItemsResult() { Count = count };
            }).AsAsyncOperation<LoadMoreItemsResult>();
        }
    }
}
