using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckClickFeedbackHandler : MonoBehaviour
{
    private static DuckClickFeedbackHandler _instance;

    public static DuckClickFeedbackHandler Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField]
    private GameObject duckClickUiFxs, duckClickCursorFx, uiParent;

    public void DisplayDuckClick(float amountReceived)
    {
        var worldPositionOfDuck = References.Instance.mouseWorldPos;

        Vector3 screenPosition = References.Instance.mouseScreenPos; // Camera.main.WorldToScreenPoint(worldPositionOfDuck);
        var inst = Instantiate(duckClickUiFxs, screenPosition, duckClickUiFxs.transform.rotation, uiParent.transform);
        var inst2 = Instantiate(duckClickCursorFx, worldPositionOfDuck, duckClickCursorFx.transform.rotation);
        inst.GetComponent<ClickDuckUiPopup>().SetQuacksReceievedOnClick(amountReceived);

    }
}
