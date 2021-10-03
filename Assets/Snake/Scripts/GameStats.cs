using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    static private int crystalCount;
    public Text crystalCounterText;

    void Start()
    {
        crystalCount = 0;
    }

    void FixedUpdate()
    {
        crystalCounterText.text = crystalCount.ToString();
    }

    static public void AddCrystalCounter()
    {
        crystalCount++;
    }

    static public void ClearCrystalCounter()
    {
        crystalCount = 0;
    }
}

