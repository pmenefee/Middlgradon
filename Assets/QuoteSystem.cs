using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuoteSystem : MonoBehaviour
{
    // UI Text GameObjects
    public GameObject tmp_Quote;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        int index = rand.Next(Quotes().Length);

        tmp_Quote.GetComponent<TextMeshProUGUI>().text = Quotes()[index];
    }

    private static string[] Quotes(){

        string[] quotes = {
            "Let no man glory in the greatness of his mind, but rather keep watch over his wits. Cautious and silent let him enter a dwelling; to the heedful comes seldom harm, for none can find a more faithful friend than the wealth of mother wit.",
            "Wealth dies. Friends die. One day you too will die. But, the thing that never dies is the judgement on how you have spent your life.",
            "It is fortunate to be favored with praise and popularity. It is dire luck to be dependent on the feelings of your fellow man."
            };
        return quotes;
    }

}
