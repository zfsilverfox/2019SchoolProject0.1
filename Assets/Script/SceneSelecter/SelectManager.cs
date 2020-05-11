using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SelectManager : MonoBehaviour {

    public AudioClip _SelectedSoundClip;


    public GameObject[] SelectBtn;


        public void Stage1Btn()
    {
        SceneManager.LoadScene("GameMain");


        if(SelectBtn[0] != null)
        {
            AudioSource.PlayClipAtPoint(_SelectedSoundClip, SelectBtn[0].transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(_SelectedSoundClip, this.transform.position);
        }
       
    }


    public void Stage2Select()
    {
        SceneManager.LoadScene("GameMain 1");

        if (SelectBtn[1] != null)
        {
            AudioSource.PlayClipAtPoint(_SelectedSoundClip, SelectBtn[1].transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(_SelectedSoundClip, this.transform.position);
        }
    }

    public void Stage3Select()
    {
        SceneManager.LoadScene("GameMain 2");

        if (SelectBtn[2] != null)
        {
            AudioSource.PlayClipAtPoint(_SelectedSoundClip, SelectBtn[2].transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(_SelectedSoundClip, this.transform.position);
        }
    }


    public void GoBackToMenu()
    {
        SceneManager.LoadScene("GameStart");
    }


}
