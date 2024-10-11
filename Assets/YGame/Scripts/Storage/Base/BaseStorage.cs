using UnityEngine;

namespace YGame.Scripts.Storage.Base
{
    public class BaseStorage : IStorage
    {
        private IStorageData _data;
        public bool UseUosSave = true;
        public void UpdateStorageData(IStorageData data) 
        {
            this._data = data;
        }


        public IStorageData GetStorageData<T>() where T : IStorageData
        {
            if (_data == null)
            {
                if (UseUosSave)
                {
                    if (StorageManager.Instance.jsonData.TryGetValue(typeof(T).Name, out var jsonData))
                    {
                        if (!string.IsNullOrEmpty(jsonData))
                        {
                            _data = JsonUtility.FromJson<T>(jsonData);
                        }
                    }
                }
                else
                {
                    var jsonData = PlayerPrefs.GetString(typeof(T).Name);
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        _data = JsonUtility.FromJson<T>(jsonData);
                    }
                }
                
            }

            return _data;
        }

        public void SaveStorageData(string key, IStorageData data = null)
        {
            if (data != null)
                this._data = data;
            
            var jsonData = JsonUtility.ToJson(this._data);
            if (UseUosSave)
            {
                StorageManager.Instance.jsonData[key] = jsonData;
            }
            else
            {
                PlayerPrefs.SetString(key, jsonData);
                PlayerPrefs.Save();
            }
   
        }
    }
}