using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    GameController instance;

    int playersAlive;

    public static string state = "playing";

    public MenuNavigation menuNav;

	void Start () {

        if (instance == null)
            instance = this;

        FindPlayers();

	}

    void Update() {

        if(Input.GetKeyDown(KeyCode.J)) {

            GameObject g = GameObject.FindGameObjectWithTag("Player");

            g.GetComponent<PlayerDeath>().Die();

        }

    }

    void FindPlayers() {

        playersAlive = GameObject.FindGameObjectsWithTag("Player").Length;

    }

    public void PlayerHasDied() {

        playersAlive--;

        if (playersAlive <= 1)
            GameOver();

    }

    void GameOver() {

        menuNav.GoToScreen("GameOverPanel");

    }

    void ResetValues() {

        state = "playing";

        FindPlayers();

    }

    public void RestartGame() {

        ResetValues();

        SceneManager.LoadScene(0);

    }

}
