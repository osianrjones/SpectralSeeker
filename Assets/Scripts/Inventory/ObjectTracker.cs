using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public static ObjectTracker Instance;

    private Dictionary<string, List<GameObject>> taggedObjects = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterObject(GameObject obj, string tag)
    {
        if (!taggedObjects.ContainsKey(tag))
        {
            taggedObjects[tag] = new List<GameObject>();
        }

        taggedObjects[tag].Add(obj);
    }

    public GameObject FindInactiveObjectByTag(string tag)
    {
        if (taggedObjects.ContainsKey(tag))
        {
            foreach (GameObject obj in taggedObjects[tag])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }
        }

        Debug.LogWarning("No inactive GameObject with tag '" + tag + "' found.");
        return null;
    }
}