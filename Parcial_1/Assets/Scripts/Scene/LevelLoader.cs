using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _transitionAnimator;

    [SerializeField][Range(1,5)] private float transitionTime = 1f;
    private readonly int Start = Animator.StringToHash("Start");

    private void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadNextLevel("TestLevelTransitions");
        }
    }

    public void LoadNextLevel(string level)
    {
        StartCoroutine(LoadLevel(level));
    }

    private IEnumerator LoadLevel(string level)
    {
        _transitionAnimator.SetTrigger(Start);

        yield return new WaitForSeconds(transitionTime);

        SceneLoaderParams.sceneToLoad = level;
        SceneManager.LoadScene("LoadScene");
        
    }
}
