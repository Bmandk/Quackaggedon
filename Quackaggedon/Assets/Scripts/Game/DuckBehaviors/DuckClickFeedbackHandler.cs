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

    public void DisplayDuckClick(double amountReceived)
    {
        var worldPositionOfDuck = References.Instance.mouseWorldPos;

        Vector3 screenPosition = References.Instance.mouseScreenPos; // Camera.main.WorldToScreenPoint(worldPositionOfDuck);
        var inst = Instantiate(duckClickUiFxs, screenPosition - new Vector3(0, -10,0), duckClickUiFxs.transform.rotation, uiParent.transform);
        var inst2 = Instantiate(duckClickCursorFx, worldPositionOfDuck - new Vector3(0, -0.3f, 0), duckClickCursorFx.transform.rotation);
        inst.GetComponent<ClickDuckUiPopup>().SetQuacksReceievedOnClick(amountReceived);

    }
}
