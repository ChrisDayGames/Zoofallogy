using UnityEngine;
using System.Collections;

public class MenuNavigation : MonoBehaviour {

	public GameObject menuContainer;
	public GameObject[] menuScreens;
	public GameObject[] modes;


	public void CloseAll (GameObject[] arrayToClose) {

		for (int i = 0; i < arrayToClose.Length; i++) {

			if (arrayToClose [i].activeSelf) {

				arrayToClose [i].SetActive(false);

			}

		}

	}


	public void GoToScreen (string screenName) {

		OpenMenu ();

		GameController.state = "menu";

		CloseAll (menuScreens);

		for (int i = 0; i < menuScreens.Length; i++) {

			if (menuScreens[i].name == screenName) {

				menuScreens [i].SetActive (true);
				return;

			}

		}
			
	}

	public void OpenMenu () {

		if (!menuContainer.activeSelf)
			menuContainer.SetActive (true);

	}

	public void CloseMenu () {

		menuContainer.SetActive (false);

	}

	public void StartGame (string mode) {

		CloseMenu ();
	
	}

	public int ModeToInt (string mode) {

		switch (mode) {

			default:
			break;
		}

		return 0;

	}

	public void QuitGame () {

		Application.Quit ();

	}

}
