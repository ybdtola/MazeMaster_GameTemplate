using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI cheeseText;
    private void Awake()
    {
        instance = this;
    }
    public void UpdateCheeseCountText(int currentCheese)
    {
        cheeseText.text = currentCheese.ToString(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void UpdateUI()
    {
        TextMeshProUGUI cheeseText = null;
        while(cheeseText == null)
        {
            //find the countText
            cheeseText = GameObject.FindObjectOfType<TextMeshProUGUI>();
        }
        cheeseText.text = GameManager.instance.GetCurrentCheeseCount().ToString();
    }
}
 