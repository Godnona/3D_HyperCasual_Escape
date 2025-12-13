using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool m_isFirstUpdate = true;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(m_isFirstUpdate)
        {
            m_isFirstUpdate=false;
            Loader.LoaderCallback();
        }
    }
}
