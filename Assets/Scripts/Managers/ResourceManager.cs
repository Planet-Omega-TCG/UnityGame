using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public int instantsAvailable;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        instantsAvailable = ThrowDice(4); // Four dice are thrown at the beginning of each sequence
        UIManager.instance.UpdateInstantsValues(instantsAvailable);
    }

    // Returns the outcome of throwing n dice
    private int ThrowDice(int n) {
        int nb = 0;
        for (int i = 0; i < n; ++i) nb += Random.Range(1, 6);
        return nb;
    }

    // Discounts used instants and calls to update the counter
    public void DiscountInstants (int instants) {
        instantsAvailable -= instants;
        UIManager.instance.UpdateInstantsValues(instantsAvailable);
    }

}
