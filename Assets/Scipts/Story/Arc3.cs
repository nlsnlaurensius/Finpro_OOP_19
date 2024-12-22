using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arc3 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Level 3", LoadSceneMode.Single);
    }
}
