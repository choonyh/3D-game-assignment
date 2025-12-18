using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI tmp1;
    public TextMeshProUGUI tmp2;
    //public TextMeshProUGUI dialogueText;
    //public GameObject dialogueBox;
    public int money = 700;
    public int tree = 30;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
        tmp1.text = money.ToString();
        tmp2.text = tree.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
