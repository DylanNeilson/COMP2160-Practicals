using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDisplay : MonoBehaviour
{
    // Pillars
    [SerializeField] private Transform[] pillars;
    private Transform selectedPillar;
    private Vector3 pillarStartPosition;
    [SerializeField] private float pillarLandHeight;

    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        gm.DoNewMove += NewMove;
    }

    void NewMove()
    {
        // Drop old command
        if (selectedPillar != null)
        {
            selectedPillar.localPosition = pillarStartPosition;
        }

        // Bring up new Pillar
        selectedPillar = pillars[gm.CurrentCommand];
        pillarStartPosition = selectedPillar.localPosition;
        selectedPillar.localPosition = new Vector3(pillarStartPosition.x, pillarLandHeight, pillarStartPosition.z);
    }
}
