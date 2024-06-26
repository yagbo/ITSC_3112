using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/*
 
This class controls the PokemonSprite in the BattleSystem. We need its PokeBase and level to print out the 
PlayerHud and we need to know if isPlayerUnit so that we can show the front or back sprite. 

In this class we also have the sprites enter animation, attack animation, hit animation and faint animation.
 
 */
public class BattleUnit : MonoBehaviour
{

    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;


    public Pokemon Pokemon { get; set; }

    private Image image;
    private Vector3 originalPos;    // local position gives us the images coordinates in relation to the canvas, as opposed to the world
    Color originalColor;

    // when the battle starts we store the original sprite, position, and color in this method so that we can use it in future methods
    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }


    // sets up Pokemon sprite
    public void Setup()
    {
        Pokemon = new Pokemon(_base, level);
        new Pokemon(_base, level);
        if (isPlayerUnit)
            image.sprite = Pokemon.Base.backSprite;
        else
            image.sprite = Pokemon.Base.frontSprite;

        image.color = originalColor;                    // makes pokemon look normal when we encounter them again, when they die they lose color, this revives their color
        PlayEnterAnimation();
    }   

    // method that slides pokemon sprites into battle
    public void PlayEnterAnimation()
    {
        if (isPlayerUnit)
        {
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        }
        else
        {
            image.transform.localPosition = new Vector3(500f, originalPos.y);
        }

        image.transform.DOLocalMoveX(originalPos.x, 1f);    // this method takes parameters (position to move to, how long it takes to move)
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();  // lets us play animations 1 by 1, similar to a list data structure
                                            // A -> B -> A, in this method the object will move to position B, then back to position A
        if (isPlayerUnit)
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, .25f)); // if the player pokemon is attacking then we move to the right and move back
        
        else
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, .25f)); // if the player pokemon is attacking then we move to the right and move back

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, .25f));
    }

    // method that shows a pokemon was hit, we make it gray for a bit then make it the original color again
    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, .1f));
        sequence.Append(image.DOColor(originalColor, .1f));
    }

    // method that shows a pokemon faint. We send it down and have it fade away
    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, .5f));
        sequence.Join(image.DOFade(0f, .5f));
    }
}
