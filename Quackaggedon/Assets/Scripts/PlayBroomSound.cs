using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBroomSound : MonoBehaviour
{
    public void PlayBroom()
    {
        AudioController.Instance.PlayBroom();
    }
}
