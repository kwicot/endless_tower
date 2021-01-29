using UnityEngine;

public class OnOnableElement : MonoBehaviour
{
    public GameObject[] show;
    public GameObject[] hide;

    public void OnEnable()
    {
        if(show != null)
            foreach (var e in show)
            {
                if (e == null) continue;
                e.SetActive(true);
            }
        
        if(hide != null)
            foreach (var e in hide)
            {
                if (e == null) continue;
                e.SetActive(false);
            }
    }
}
