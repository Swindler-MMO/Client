using System.Collections;
using System.Collections.Generic;
using Swindler.API;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLoadingManager : MonoBehaviour
{
    async void Start()
    {
        await ConfigAPI.Load();
        OnLoadingComplete();
    }

    private void OnLoadingComplete()
    {
        SceneManager.LoadScene("Scenes/Play");
    }
}
