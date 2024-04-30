using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/*
 
This class controls the dialogBox. It has methods to show or hide all of the text options in the dialogbox.
 
 */
public class BattleDialogBox : MonoBehaviour
{

    [SerializeField] int lettersPerSecond;   // how fast text is shown
    [SerializeField] Color highlightedColor; // in Unity Editor make sure to set the opacity to full in order to see the color

    [SerializeField] TextMeshProUGUI dialogText; // text object for the dialogText ex: Charmander fainted
    [SerializeField] GameObject actionSelector;  // gameObject that holds a list of actions 
    [SerializeField] GameObject moveSelector;   // gameObject that holds a list of moves 
    [SerializeField] GameObject moveDetails;    // moveDetails (PP, Type) gameObject

    [SerializeField] List<TextMeshProUGUI> actionTexts; // list of actions
    [SerializeField] List<TextMeshProUGUI> moveTexts;   // list of moves

    [SerializeField] TextMeshProUGUI ppText;            // PP text for moveDetails
    [SerializeField] TextMeshProUGUI typeText;          // type Text for moveDetails


    // method that types dialog letter by letter
    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
    
    // method that enables the dialogtext Text Object
    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    // method that enables the Action Selector list of actions
    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    // method that enables the move selector list of moves
    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }
    
    // iterate through possible actions, highlight the selectedAction
    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; ++i)
        {
            if (i == selectedAction)
                actionTexts[i].color = highlightedColor;
            else
                actionTexts[i].color = Color.black;
        }
    }

    // highlights the move the user is hovering and shows the PP and type associated with given move
    public void UpdateMoveSelection(int selectedMove, Move move)
    {

        // figure out which move the player is hovering then highlight
        for (int i = 0; i < moveTexts.Count; ++i)
        {
            if (i == selectedMove)
                moveTexts[i].color = highlightedColor;
            else
                moveTexts[i].color = Color.black;
        }

        // show the PP and the type of the move
        ppText.text = $"PP {move.PP}/{move.Base.PP}";
        typeText.text = move.Base.Type.ToString();
    }

    // method that changes the name of the moves in the MoveSelector
    public void SetMoveNames(List<Move> moves)
    {
        for (int i = 0; i < moveTexts.Count; i = i+1)
        {
            if (i < moves.Count)
                moveTexts[i].text = moves[i].Base.Name;
            else
                moveTexts[i].text = "-";
        }
    }
    
}

