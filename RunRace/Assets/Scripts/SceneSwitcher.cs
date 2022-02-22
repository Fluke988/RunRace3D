using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        Initiate.Fade("GameScene", Color.white, 1f);
    }

    public void OnRetryButtonClick()
    {
        Initiate.Fade("GameScene", Color.green, 1f);
    }

    public void OnBackButtonClick()
    {
        Initiate.Fade("HomeScene", Color.yellow, 1f);
    }
}
