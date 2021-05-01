using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FillStatusBar : MonoBehaviour
{
    public Player player;

    public Image fillImage;

    private Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>(); 
    }

    // Update is called once per frame
    void Update()
    {
        ThrusterUpdate();
    }

    public void ThrusterUpdate()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if (slider.value > 0 && fillImage.enabled == false)
        {
            fillImage.enabled = true;
        }

        float fillValue = player.thrustCurrent / player.thrustMax;
        slider.value = fillValue;
    }
}
