using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TestSceneLoader : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.fKey.ReadValue() == 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
