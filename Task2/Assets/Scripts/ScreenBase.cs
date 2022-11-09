using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Screen base class designed for game screens
/// </summary>
public class ScreenBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // close function for all screens
    public void Close()
    {
        Destroy(gameObject);
    }

    // if we want to show animation then we can call from chil class
    protected void Animate()
    {
        // animation codes here
    }
}
