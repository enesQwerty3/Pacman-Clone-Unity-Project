using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost {get; private set;}
    public float duration;
    // Start is called before the first frame update
    void Awake() 
    {
        ghost = GetComponent<Ghost>();
    }


    public void Enable()
    {
        Enable(duration);
    }
    
    public virtual void Enable(float duration)
    {
        enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();
    }
}
