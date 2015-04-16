# Unity-GameObjectPool

<h3>Overview</h3>
Object pooling is a great way to improve your games performance. Pooling is typically used when you need to create and destroy several same type objects in a small amount of time to reduce memory usage.

# <h3>Methods</h3>
<h5>PreloadGameObject()</h5>
Allows you to preload your GameObjects into the pool. These objects will be instantiated, disabled, and stored in the inactive pool for use. This method can only be called during runtime.

```
// Load the resource, and preload 5 instances of the object.
GameObject obj = Resources.Load<GameObject>("object") as GameObject;
GameObjectPool.m_Instance.PreloadGameObject(obj, 5);
```
<h5>AddGameObject()</h5>
Allows you to add new GameObjects into the pool. If a same type is found within the inactive pool, the pool will avoid creating a new instance, and use the stale object instead.

```
// Two ways. You can create a instance of the object and carry on,
// or you can grab the created instance of the object for use.
GameObjectPool.m_Instance.AddGameObject(obj, Vector3.zero, Quaternion.identity);
GameObject objInst = GameObjectPool.m_Instance.AddGameObject(obj, Vector3.zero, Quaternion.identity);
```

<h5>Cleaner()</h5>
The cleaner will remove any stale (disabled) objects from the active pool. This is typically used for longer game sessions, where the inactive pool may need to be replemish to avoid high memory usage. Avoid high amounts of cleaning as constant loops can deminish performance. Increase the preload count if the inactive pool dries up much sooner than expected.
