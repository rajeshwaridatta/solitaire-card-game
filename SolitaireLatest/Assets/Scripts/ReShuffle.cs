/*using UnityEngine;
using System.Collections;

public class ReShuffle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//void restarGame()
	{
		//SceneManager.LoadScene("finalSceneUpgradeVersion", LoadSceneMode.Additive);
		//Application.LoadLevel("finalSceneUpgradeVersion");
		//SceneManagement.SceneManager.LoadScene("finalSceneUpgradeVersion");
		//Time.timeScale = 1;
	}
}*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReShuffle : MonoBehaviour {

	public void loadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}
}
