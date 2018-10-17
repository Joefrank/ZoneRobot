

namespace toyrobot.services.Interfaces
{
    public interface ICacheService
    {
        void Add<T>(T o, string key);

        void Clear(string key);

        bool Exists(string key);

        bool Get<T>(string key, out T value);

        T Get<T>(string key);

        void Update<T>(T o, string key);
    }
}


