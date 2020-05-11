using UnityEngine;

public class SecondStageCalled : MonoBehaviour {

	public void ThirdStageFunctionCalled()
    {
        FadeManager.Instance.LoadScene("GameMain 2", 0.5f);
    }





}
