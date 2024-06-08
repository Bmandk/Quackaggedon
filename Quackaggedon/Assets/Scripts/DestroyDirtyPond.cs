using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDirtyPond : MonoBehaviour
{
    public GameObject dirtyHolder;
    public GameObject triggerToDestroy;
    public GameObject trasgTriggerToDestroy;
    public void DestroyAllDirty()
    {
        Destroy(triggerToDestroy.gameObject);
        Destroy(trasgTriggerToDestroy.gameObject);
        Destroy(dirtyHolder.gameObject);
        AudioController.Instance.EnableBackgroundMusic();
        References.Instance.menuController.EnabbleUiForDirtInteraction();
    }

    public void PlayCleanShimmer()
    {
        AudioController.Instance.PlayCleanShimmer();
    }
}
