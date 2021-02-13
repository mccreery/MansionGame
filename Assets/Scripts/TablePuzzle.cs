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

    public GameObject key;

    private void Awake()
    {
        key.SetActive(false);
    }

    public void UpdateCheck()
    {
        if (Check())
        {
            key.SetActive(true);
        }
    }

    public bool Check()
    {
        int humph = GetPlace(msHumphreys);
        int bake = GetPlace(mrsBaker);
        int stew = GetPlace(mrStewart);
        int mrBake = GetPlace(mrBaker);
        int fred = GetPlace(mrFrederick);
        int wil = GetPlace(drWilson);

        return humph != 0 && bake != 0 && stew != 0 && mrBake != 0 && fred != 0 && wil != 0
            && Adjacent(mrBake, stew) // Mr Baker and Mr Stewart are old friends
            && (humph == 5 || humph == 6) // Ms Humphreys is a good cook
            && SideBySide(mrBake, bake) // Mr and Mrs baker sit side by side
            && !Adjacent(wil, stew) // Mr Stewart is squeamish & Dr Wilson tells gory stories
            && (fred == 1 || fred == 4) // Wants to feel important
            && !Adjacent(fred, humph); // Mr Frederick & Ms Humphreys have fallen out
    }

    private int GetPlace(ItemPickup person)
    {
        Transform transform = person.transform;
        do
        {
            if (transform.GetComponent<TablePlace>() is TablePlace place)
            {
                return Array.IndexOf(places, place) + 1;
            }
            else
            {
                transform = transform.parent;
            }
        } while (transform != null);

        return 0;
    }

    private static bool Adjacent(int seatA, int seatB)
    {
        MakeAscending(ref seatA, ref seatB);
        return seatB - seatA == 1 || seatA == 1 && seatB == 6;
    }

    private static bool SideBySide(int seatA, int seatB)
    {
        MakeAscending(ref seatA, ref seatB);
        return seatA == 2 && seatB == 3 || seatA == 5 && seatB == 6;
    }

    private static void MakeAscending<T>(ref T a, ref T b) where T : IComparable<T>
    {
        if (a.CompareTo(b) > 0)
        {
            Swap(ref a, ref b);
        }
    }

    private static void Swap<T>(ref T a, ref T b)
    {
        T oldA = a;
        a = b;
        b = oldA;
    }
}
