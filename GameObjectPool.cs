/// <summary>
/// Title: Unity GameObject Pooling.
/// Description: Keeps track of all Active and
///              Inactive GameObjects within the
///              Unity Scene.
/// </summary>
#region Using Statements
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
#endregion

public class GameObjectPool : MonoBehaviour
{

    #region Variables

    //Pool Instance
    public static GameObjectPool m_Instance = null;

    //Clean up stale objects
    public bool m_CleanUp = false;
    public float m_CleanUpTimer = 600.0f;

    //Object container
    private static List<GameObject> m_InactivePool = null;
    private static List<GameObject> m_ActivePool = null;

#if UNITY_EDITOR

    //Log our pools activity
    public bool m_Debugger = false;
#endif

    #endregion

    #region Pool Methods

    /// <summary>
    /// Add GameObject to our pool.
    /// </summary>
    /// <param name="gameObject"></param>
    public GameObject AddGameObject(GameObject newObj, Vector3 pos)
    {

        //Check our inactive pool for a similar object.
        GameObject temp = m_InactivePool.Find(x => x.name == newObj.name);

        //Check if we need to create a new instance
        // of the game object.
        if(temp == null)
        {

            temp = Instantiate(newObj, pos, Quaternion.identity) as GameObject;
            temp.name = newObj.name;
            m_ActivePool.Add(temp);

#if UNITY_EDITOR
            PoolDebugger("Added new " + temp.name + " to active pool.");
#endif
            return temp;
        }

        //Take our stale object, and reactivate it.
        GameObject stale = temp;
        m_InactivePool.Remove(temp);

        stale.transform.position = pos;
        stale.SetActive(true);
        m_ActivePool.Add(stale);

#if UNITY_EDITOR
        PoolDebugger("Added stale " + stale.name + " to active pool.");
#endif

        return stale;
    }

    /// <summary>
    /// Removes any stale objects from the pool.
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    private IEnumerator Cleaner(float timer)
    {
        yield return new WaitForSeconds(timer);

        //Gather a list of our stale objects
        // within the active pool.
        List<GameObject> staleObjects = m_ActivePool.FindAll(x => x.activeSelf == false);

        //Loop through our stale objects, removing them from
        // the active pool, and adding to inactive pool
        foreach(GameObject stale in staleObjects)
        {
            GameObject temp = stale;
            m_ActivePool.Remove(stale);
            m_InactivePool.Add(temp);

#if UNITY_EDITOR
            PoolDebugger("Removed " + temp.name + " from active pool, and added to inactive pool.");
#endif
        }

        //Recall our coroutine.
        StartCoroutine("Cleaner", m_CleanUpTimer);
    }

#if UNITY_EDITOR

    /// <summary>
    /// Log our pools status to the unity editor.
    /// </summary>
    /// <param name="msg"></param>
    private void PoolDebugger(string msg)
    {

        //Check if our debugger is active.
        if (m_Debugger == true)
        {

            Debug.Log("GameObjectPool: " + msg);
        }
    }
#endif
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        m_Instance = this;
        m_InactivePool = new List<GameObject>();
        m_ActivePool = new List<GameObject>();

        //Check if our pool has a clean up
        if (m_CleanUp == true)
        {

            //Start our pools cleaner
            StartCoroutine("Cleaner", m_CleanUpTimer);
        }
    }

    #endregion
}
