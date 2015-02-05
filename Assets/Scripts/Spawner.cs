using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject m_Object = null; // Object that will be spawned
    public float m_Rate = 1.0f; // Rate per second

    /// <summary>
    /// Start the Spawner
    /// </summary>
    [ContextMenu("Start")]
    private void StartSpawner()
    {
        StartCoroutine("Spawn", m_Rate);
    }

    /// <summary>
    /// Spawn the object with a
    /// set rate.
    /// </summary>
    /// <param name="rate"></param>
    /// <returns></returns>
    private IEnumerator Spawn(float rate)
    {
        yield return new WaitForSeconds(rate);

        GameObjectPool.m_Instance.AddGameObject(m_Object, 
                                                transform.position, 
                                                transform.rotation);

        StartCoroutine("Spawn", rate);
    }
}
