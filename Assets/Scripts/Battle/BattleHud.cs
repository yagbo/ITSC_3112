using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// we inport TMPro so that we can use the Text class
using TMPro;

// this is the class that controls the floating rectangle during battle that shows us a Pokemons level and health 
public class BattleHud : MonoBehaviour
{
    // nameText and levelText are of type TextMeshProUGUI, to treat them as strings we must use .text
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] TextMeshProUGUI hpText;

    Pokemon _pokemon; // we use this local Pokemon object so that we can update the HP without having to use a Pokemon parameter
    public void SetData(Pokemon pokemon)
    {
        _pokemon = pokemon;
        nameText.text = pokemon.Base.Name;
        levelText.text = "Lvl " + pokemon.Level;
        hpBar.SetHP((float) pokemon.HP / pokemon.MaxHP);
        hpText.text = pokemon.HP + "/" + pokemon.MaxHP;
    }

    public IEnumerator UpdateHP()
    {
        yield return hpBar.SetHPSmooth((float)_pokemon.HP / _pokemon.MaxHP);
        yield return hpBar.SetHPTextSmooth(_pokemon, _pokemon.Damage);
    }


}
