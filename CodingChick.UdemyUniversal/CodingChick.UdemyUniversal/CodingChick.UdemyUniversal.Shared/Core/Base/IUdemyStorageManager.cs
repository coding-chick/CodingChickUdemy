using Windows.Storage;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public interface IUdemyStorageManager
    {
        void SaveValueToSettings(string name, object value);
        object GetValueFromSettings(string name);
    }

    class UdemyStorageManager : IUdemyStorageManager
    {
        ApplicationDataContainer _localSettings;

        public UdemyStorageManager()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }

        public void SaveValueToSettings(string name, object value)
        {
            _localSettings.Values[name] = value;
        }

        public object GetValueFromSettings(string name)
        {
            //if (_localSettings.Values.ContainsKey(name))
                return _localSettings.Values[name];
            //return null;
        }
    }
}