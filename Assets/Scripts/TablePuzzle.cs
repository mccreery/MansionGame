using System;
using UnityEngine;

public class TablePuzzle : MonoBehaviour
{
    // Table goes
    //
    //    2  3
    //  1      4
    //    6  5
    //
    //   '--' door

    public TablePlace[] places;

    public ItemPickup msHumphreys, mrsBaker, mrStewart, mrBaker, mrFrederick, drWilson;

    public ItemPickup[] People => new ItemPickup[] {
        msHumphreys, mrsBaker, mrStewart, mrBaker, mrFrederick, drWilson
    };

    public bool Check()
    {
        int humph = GetPlace(msHumphreys);
        int bake = GetPlace(mrsBaker);
        int stew = GetPlace(mrStewart);
        int mrBake = GetPlace(mrBaker);
        int fred = GetPlace(mrFrederick);
        int wil = GetPlace(drWilson);

        return Math.Abs(bake - stew) == 1 // Mr Baker and Mr Stewart are old friends
            && (humph == 5 || humph == 6) // Ms Humphreys is a good cook
            && (mrBake == 2 && bake == 3 || mrBake == 3 && bake == 2 || mrBake == 5 && bake == 6 || mrBake == 6 && bake == 5) // Mr and Mrs baker sit side by side
            && Math.Abs(wil - stew) > 1 // Mr Stewart is squeamish & Dr Wilson tells gory stories
            && fred == 1 // Life of the party
            && Math.Abs(fred - humph) > 1; // Mr Frederick & Ms Humphreys have fallen out
    }

    private int GetPlace(ItemPickup person)
    {
        return Array.IndexOf(places, person.transform.parent.GetComponent<TablePlace>());
    }
}
