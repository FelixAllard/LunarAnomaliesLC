namespace LunarAnomalies;

using UnityEngine;

public class MonoBehaviourHelper : MonoBehaviour
{
    private static MonoBehaviourHelper instance;

    // Static property to get the instance of MonoBehaviourHelper
    public static MonoBehaviourHelper Instance
    {
        get
        {
            // If instance is null, find an existing instance in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<MonoBehaviourHelper>();

                // If no instance exists in the scene, create a new GameObject with MonoBehaviourHelper attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("MonoBehaviourHelper");
                    instance = obj.AddComponent<MonoBehaviourHelper>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure there's only one instance of MonoBehaviourHelper in the scene
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this GameObject
        instance = this;

        // Ensure that this GameObject persists across scenes
        DontDestroyOnLoad(gameObject);
    }
}
