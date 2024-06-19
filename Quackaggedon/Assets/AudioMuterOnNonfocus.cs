using Unity.VisualScripting;
using UnityEngine;

public class AudioMuterOnNonfocus : MonoBehaviour
{
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
            AudioListener.pause = false;
        else 
            AudioListener.pause = true;
    }
}
