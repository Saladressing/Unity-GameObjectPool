using UnityEngine;
using System.Collections;

public class Preloader : MonoBehaviour {

    /// <summary>
    /// Preload our required Resources.
    /// This is called after the Pool has
    /// been created.
    /// </summary>
    [ContextMenu("Preload")]
    private void Preload()
    {
        GameObject obj = Resources.Load<GameObject>("object") as GameObject;
        GameObjectPool.m_Instance.PreloadGameObject(obj, 5);

        GameObject obj2 = Resources.Load<GameObject>("anotherObject") as GameObject;
        GameObjectPool.m_Instance.PreloadGameObject(obj2, 5);
    }
}
