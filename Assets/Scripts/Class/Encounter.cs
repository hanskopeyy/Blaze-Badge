using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter
{
    public Character ally, enemy;
    public Encounter(Character a, Character e){
        ally = a;
        enemy = e;
    }

}
