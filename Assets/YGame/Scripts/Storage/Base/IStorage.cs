namespace YGame.Scripts.Storage.Base
{
    public interface IStorage
    {
        public IStorageData GetStorageData<T>() where T : IStorageData;
        public void UpdateStorageData(IStorageData data);
        public void SaveStorageData(string key, IStorageData data = null);
    }
}