using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionsCounterText;
    private GameManager gameManager;

    [SerializeField] private Button strikeButton;
    [SerializeField] private Button sheildButton;
    [SerializeField] private Button strengthenButton;
    [SerializeField] private Button healButton;
    [SerializeField] private Button endTurnButton;

    [SerializeField] private float actionsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        strikeButton.onClick.AddListener(gameManager.DoStrike);
        sheildButton.onClick.AddListener(gameManager.DoShield);
        strengthenButton.onClick.AddListener(gameManager.DoStrengthen);
        healButton.onClick.AddListener(gameManager.DoHeal);
        endTurnButton.onClick.AddListener(gameManager.EndTurn);

        UpdateButtonInteractivity();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonInteractivity();

        int actionsRemaining = gameManager.ActionsRemaining;
        actionsCounterText.text = string.Format("Actions: {0}/3", actionsRemaining);
    }

    void UpdateButtonInteractivity()
    {
        int actionsRemaining = gameManager.ActionsRemaining;

        strikeButton.interactable = actionsRemaining > 0;
        sheildButton.interactable = actionsRemaining > 0;
        strengthenButton.interactable = actionsRemaining > 0;
        healButton.interactable = actionsRemaining > 0;
    }
}