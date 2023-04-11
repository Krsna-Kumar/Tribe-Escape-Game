using UnityEngine;
using TMPro;

public class PickupUI : MonoBehaviour
{
    public TextMeshProUGUI itemLeftText;
    public PlayerDrop playerDrop;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }


    private void LateUpdate()
    {
        RotateTowardsCam();

        UpdateLogText();
    }

    private void RotateTowardsCam()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    private void UpdateLogText()
    {
        itemLeftText.text = "" + playerDrop.requiredLogText;

        if(playerDrop.requiredLogText == 0)
        {
            //Animate and Destroy this gameobject
            this.gameObject.SetActive(false);
        }
    }
}
