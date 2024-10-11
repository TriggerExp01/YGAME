using System.Collections.Generic;
using UnityEngine;
using YGame.Scripts.Common;
using YGame.Scripts.Storage.Base;

namespace YGame.Scripts.Storage
{
    public class StorageManager : MonoSingleton<StorageManager>
    {
        #region LocalData

        public Dictionary<string, IStorage> LocalStorages = new Dictionary<string, IStorage>();

        public Dictionary<string,string> jsonData = new Dictionary<string, string>();
        public T GetLocalStorageData<T>() where T : IStorageData, new()
        {
            
            var storage = GetLocalStorage(typeof(T).Name, out bool isHasStorage);
            if (!isHasStorage)
            {
                storage = new BaseStorage();
                var data  = storage.GetStorageData<T>();
                storage.UpdateStorageData(data);
                LocalStorages.Add(typeof(T).Name, storage);
            }
            return (T)storage.GetStorageData<T>();
        }

        public void UpdateLocalStorageData<T>(T data) where T : IStorageData
        {
            
            if (data == null)
            {
                Debug.LogWarning("Storage data is null, can not save.");
                return;
            }
            var storage = GetLocalStorage(typeof(T).Name, out bool isHasStorage);
            if (!isHasStorage)
            {
                storage = new BaseStorage();
                LocalStorages.Add(typeof(T).Name, storage);
            }
            storage.UpdateStorageData(data);
        }
        
        public void SaveLocalStorageData<T>(T data) where T : IStorageData
        {
            if (data == null)
            {
                Debug.LogWarning("Storage data is null, can not save.");
                return;
            }
            UpdateLocalStorageData(data);
            var storage = GetLocalStorage(typeof(T).Name, out bool isHasStorage);
            storage.SaveStorageData(typeof(T).Name, data);
        }

        public IStorage GetLocalStorage(string storageName , out bool isHasStorage)
        {
            isHasStorage = LocalStorages.TryGetValue(storageName, out IStorage storage);
            if (isHasStorage)
                return storage;
            
            return null;

        }

        #endregion
    }
}