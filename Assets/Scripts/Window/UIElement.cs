using UnityEngine;


public class UIElement : MonoBehaviour
{
    public GameObject[] prefabs;
    
    protected bool HasPrefab(string nameElement)
    {
        if (prefabs != null)
            foreach (var p in prefabs)
            {
                if (p == null) continue;
                if (p.name == nameElement)
                    return true;
            }

        return false;
    }
    
    protected GameObject GetPrefab(string nameElement)
    {
        if (prefabs != null)
            foreach (var p in prefabs)
            {
                if (p == null) continue;
                if (p.name == nameElement)
                    return p;
            }

        return null;
    }
}
