using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CodingChick.UdemyUniversal.CoreUI
{
    public class PagedCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private Func<uint, Task<IEnumerable>> _loadMoreItems;
        public bool HasMoreItems { get; protected set; }

        public PagedCollection(Func<uint, Task<IEnumerable>> loadMoreItems)
        {
            HasMoreItems = true;
            this._loadMoreItems = loadMoreItems;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            throw new NotImplementedException();
            //CoreDispatcher dispatcher = Window.Current.Dispatcher;
            //return AsyncInfo.Run(token =>
            //{

            //});
        }
    }
}
