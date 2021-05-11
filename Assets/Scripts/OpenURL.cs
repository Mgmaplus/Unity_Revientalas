using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OpenUrl()
    {
        Application.OpenURL(url);
    }
}
