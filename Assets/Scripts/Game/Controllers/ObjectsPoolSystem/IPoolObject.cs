using UnityEngine;

namespace testProjectTemplate
{
    public interface IPoolObject
    {
        bool IsPooledObject { get; set; }
        void Deactivate();
        void Activate(GameObject templatePrefab);
    }
}
