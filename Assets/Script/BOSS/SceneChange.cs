using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneChange : MonoBehaviour
{

    public GameObject _GameRestartLoadingScreen;

    public GameObject _SubCameras;

    public GameObject[] AvoidSomethings;


    public GameObject _LifeSlider;


    public void RestartScreen()
    {
        SceneManager.LoadScene("GameMain 2");



        if(_GameRestartLoadingScreen != null)
        _GameRestartLoadingScreen.SetActive(true);

        if (_SubCameras != null) _SubCameras.SetActive(false);
        
        if(AvoidSomethings != null)
        {
            foreach(GameObject av in AvoidSomethings)
            {
                av.SetActive(false);
            }


        }

        if(_LifeSlider != null)
        {
    //        Debug.Log("Life Slider Should be Dissapear");
            _LifeSlider.SetActive(false);
        }



    }

    public void GameStartWinStatus()
    {
        SceneManager.LoadScene("LoadingScreenEnd");
    }

    public void GameStartLoseStatus()
    {
        SceneManager.LoadScene("GameStart");
    }






}