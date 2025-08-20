using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SlotMachine : MonoBehaviour
{
    [Header("Buttons")]
    public Button menuButton;
    public Button settingsButton;
    public Button celenderButton;
    public Button leaderButton;
    public Button spinButton;
    
    [Header("Reels Object")]
    public Reel[] reels;
    
    [Header("Text")]
    public TMP_Text resultText;
    void Start()
    {
        leaderButton.onClick.AddListener(OnleaderClicked);
        menuButton.onClick.AddListener(OnmenuClicked);
        settingsButton.onClick.AddListener(OnsettingsClicked);
        celenderButton.onClick.AddListener(OncelenderClicked);
        spinButton.onClick.AddListener(() => StartCoroutine(SpinAll()));
    }

    public void OnleaderClicked()
    {
        Debug.Log("OnleaderClicked");
    }

    public void OnmenuClicked()
    {
        Debug.Log("OnmenuClicked");
    }

    public void OnsettingsClicked()
    {
        Debug.Log("OnsettingsClicked");
    }

    public void OncelenderClicked()
    {
        Debug.Log("OnleaderClicked");
    }
    IEnumerator SpinAll()
    {
        spinButton.interactable = false;
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].Spin();
            yield return new WaitForSeconds(0.3f); 
        }
        yield return new WaitForSeconds(reels[0].spinDuration + 0.5f);

        // CheckWin();
        spinButton.interactable = true;
    }


    // void CheckWin()
    // {
    //     int r1 = reels[0].GetFinalIndex();
    //     int r2 = reels[1].GetFinalIndex();
    //     int r3 = reels[2].GetFinalIndex();
    //
    //     if (r1 == r2 && r2 == r3)
    //     {
    //         resultText.text = "🎉 WIN! Same Symbol!";
    //     }
    //     else
    //     {
    //         resultText.text = "❌ Try Again!";
    //     }
    // }
}
