using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    int score = 0;
    [SerializeField] int nbreOfCars = 0;
    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI endScoreTxt;
    public static GameManager instance;

    bool gameOverBool;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (nbreOfCars <= 0 && !gameOverBool)
        {
            gameOverBool = true;
            Invoke(nameof(GameOver), 2f);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreTxt.SetText(score.ToString());
    }

    public void SetNbreCar()
    {
        nbreOfCars--;
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        PlayerMovement.instance.GetComponent<PlayerInput>().DeactivateInput();
        endScoreTxt.SetText(score.ToString());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
