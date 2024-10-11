using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using YGame.Scripts.Common;
using YGame.Scripts.Log;
using Object = UnityEngine.Object;

namespace YGame.Scripts.Addressable
{
    public class AddressableLoader : Singleton<AddressableLoader>
    {
        private Dictionary<string, object> loadedAssets = new Dictionary<string, object>();
        

        // Generic method to load an asset
        public void LoadAsset<T>(string address, Action<T> onLoaded) where T : UnityEngine.Object
        {
            if (loadedAssets.TryGetValue(address, out var asset))
            {
                onLoaded?.Invoke(asset as T);
                return;
            }

            Addressables.LoadAssetAsync<T>(address).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    loadedAssets[address] = handle.Result;
                    onLoaded?.Invoke(handle.Result);
                }
                else
                {
                    YLogger.LogError($"Failed to load asset at address: {address}");
                }
            };
        }

        // Method to instantiate a prefab
        public void InstantiatePrefab(string address, Action<GameObject> onInstantiated = null, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            LoadAsset<GameObject>(address, prefab =>
            {
                if (prefab != null)
                {
                    GameObject instance = Object.Instantiate(prefab, parent? parent.position+position:position, rotation, parent);
                    onInstantiated?.Invoke(instance);
                }
            });
        }
        

        // Method to release an asset
        public void ReleaseAsset(string address)
        {
            if (loadedAssets.ContainsKey(address))
            {
                Addressables.Release(loadedAssets[address]);
                loadedAssets.Remove(address);
            }
        }

    }
}