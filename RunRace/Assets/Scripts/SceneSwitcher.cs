using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        Initiate.Fade("Level1", Color.white, 1f);
    }

    public void OnRetryButtonClick()
    {
        Initiate.Fade("Level1", Color.green, 1f);
    }

    public void OnBackButtonClick()
    {
        Initiate.Fade("HomeScene", Color.yellow, 1f);
    }
}
