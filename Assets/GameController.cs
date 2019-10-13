using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _vegetables = 100;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _killCountText;

    private int _killCount = 0;
    
    private void Start()
    {
        _slider.maxValue = _vegetables;
        UpdateKillCountText();
    }
    private void Update()
    {
        _slider.value = _vegetables;
    }
    public void DecreaseVegetableCount(int value)
    {
        _vegetables -= value;

        if (_vegetables <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        _gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void IncreaseKillCount()
    {
        _killCount++;
        UpdateKillCountText();
    }

    private void UpdateKillCountText()
    {
        _killCountText.text = _killCount.ToString();
    }
}
