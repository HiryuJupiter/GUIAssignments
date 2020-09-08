using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public delegate void eventHandler();
    public event eventHandler MyEvent;

    private void Start()
    {
        SpellProcessor.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Subscribe");
            MyEvent += PrintMe;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            Delegate[] ds = MyEvent.GetInvocationList();
            Debug.Log("Unsubcribe. Subscribers " + ds.Length);
            foreach (var item in ds)
            {
                MyEvent -= (eventHandler)item;
            }

            //MyEvent -= PrintMe;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Invoke");
            MyEvent?.Invoke();
        }
    }

    void PrintMe ()
    {
        Debug.Log("Print me");
    }
}



public enum SpellTypes
{
    Heal,
    Attack,
    Defend,
    Defend2,
}



public static class SpellProcessor
{
    static Dictionary<SpellTypes, Spell> spells = new Dictionary<SpellTypes, Spell>();
    
    public static void Initialize () {
        var assembly = Assembly.GetAssembly(typeof(Spell));
        var allSpells = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Spell)));

        Debug.Log("All spell types ----------");
        foreach (var s in allSpells)
        {
            Spell spell = Activator.CreateInstance(s) as Spell;
            spells.Add(spell.spellType, spell);
        }
    }

    public static void UseSpell(SpellTypes spell) {
        spells[spell].Effect();
    }
}

public abstract class Spell {
    public abstract SpellTypes spellType { get; }
    public abstract void Effect();
}

public class Heal : Spell
{
    public override SpellTypes spellType => SpellTypes.Heal;

    public override void Effect()
    {
        Debug.Log("Heal Effect");
    }
}

public class Attack : Spell
{
    public override SpellTypes spellType => SpellTypes.Attack;

    public override void Effect()
    {
        Debug.Log("Attack Effect");
    }
}

public class Defend : Spell
{
    public override SpellTypes spellType => SpellTypes.Defend;

    public override void Effect()
    {
        Debug.Log("Defend Effect");
    }
}

public class Defend2 : Spell
{
    public override SpellTypes spellType => SpellTypes.Defend2;

    public override void Effect()
    {
        Debug.Log("Defend2 Effect");
    }
}