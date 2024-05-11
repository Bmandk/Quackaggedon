using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSoundsIfSpawnedOnSceneLoad : MonoBehaviour
{
    public AudioSource[] soundsToMute;
    // Start is called before the first frame update
    void Awake()
    {
        if (Time.timeSinceLevelLoad < 1f)
        {
            StartCoroutine(MuteThenUnmuteAfterDelay());
        }
    }

    IEnumerator MuteThenUnmuteAfterDelay()
    {
        foreach (var sound in soundsToMute)
        {
            sound.mute = true;
        }
        yield return new WaitForSeconds(1f);
    }
}
