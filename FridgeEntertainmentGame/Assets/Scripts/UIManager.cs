﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VIDE_Data;

public class UIManager : MonoBehaviour
{

    public GameObject dialogueContainer;
    public GameObject NPC_Container;
    public GameObject playerContainer;

    public Text Item_text;
    public Text Beans_text;
    public Text NPC_Text;
    public Text NPC_label;
    public Image NPCSprite;
    public GameObject playerChoicePrefab;
    public Image playerSprite;
    public Text playerLabel;

    public TankController player;

    bool dialoguePaused = false; //Custom variable to prevent the manager from calling VD.Next
    bool animatingText = false;  //Will help us know when text is currently being animated

    //We'll be using this to store references of the current player choices
    private List<Text> currentChoices = new List<Text>();

    //With this we can start a coroutine and stop it. Used to animate text
    IEnumerator NPC_TextAnimator;

    void Awake()
    {
        // VD.LoadDialogues(); //Load all dialogues to memory so that we dont spend time doing so later
        //An alternative to this can be preloading dialogues from the VIDE_Assign component!

        //Loads the saved state of VIDE_Assigns and dialogues.
        VD.LoadState("VIDEDEMOScene1", true);
    }

    //This begins the dialogue and progresses through it (Called by VIDEDemoPlayer.cs)
    public void Interact(VIDE_Assign dialogue)
    {

        if (!VD.isActive)
        {
            Begin(dialogue);
        }
        else
        {
            CallNext();
        }
    }

    //This begins the conversation
    void Begin(VIDE_Assign dialogue)
    {
        //Let's reset the NPC text variables
        NPC_Text.text = "";
        NPC_label.text = "";
        playerLabel.text = "";

        //First step is to call BeginDialogue, passing the required VIDE_Assign component 
        //This will store the first Node data in VD.nodeData
        //But before we do so, let's subscribe to certain events that will allow us to easily
        //Handle the node-changes
        VD.OnActionNode += ActionHandler;
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += EndDialogue;

        VD.BeginDialogue(dialogue); //Begins dialogue, will call the first OnNodeChange

        dialogueContainer.SetActive(true); //Let's make our dialogue container visible
    }

    //Calls next node in the dialogue
    public void CallNext()
    {
        //Let's not go forward if text is currently being animated, but let's speed it up.
        if (animatingText) { CutTextAnim(); return; }

        if (!dialoguePaused) //Only if
        {
            VD.Next(); //We call the next node and populate nodeData with new data. Will fire OnNodeChange.
        }

    }


    void Start()
    {
        
    }

    //Input related stuff (scroll through player choices and update highlight)
    void Update()
    {
        //Lets just store the Node Data variable for the sake of fewer words
        var data = VD.nodeData;

        if (VD.isActive) //If there is a dialogue active
        {
            //Scroll through Player dialogue options if dialogue is not paused and we are on a player node
            //For player nodes, NodeData.commentIndex is the index of the picked choice
            if (!data.pausedAction && data.isPlayer)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (data.commentIndex < currentChoices.Count - 1)
                        data.commentIndex++;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (data.commentIndex > 0)
                        data.commentIndex--;
                }

                //Color the Player options. Blue for the selected one
                for (int i = 0; i < currentChoices.Count; i++)
                {
                    currentChoices[i].color = Color.white;
                    if (i == data.commentIndex) currentChoices[i].color = Color.yellow;
                }
            }
        }

        //Note you could also use Unity's Navi system
    }

    //When we call VD.Next, nodeData will change. When it changes, OnNodeChange event will fire
    //We subscribed our UpdateUI method to the event in the Begin method
    //Here's where we update our UI
    void UpdateUI(VD.NodeData data)
    {
        //Reset some variables
        //Destroy the current choices
        foreach (Text op in currentChoices)
            Destroy(op.gameObject);
        currentChoices = new List<UnityEngine.UI.Text>();
        NPC_Text.text = "";
        NPC_Container.SetActive(false);
        playerContainer.SetActive(false);
        playerSprite.sprite = null;
        NPCSprite.sprite = null;

        //Look for dynamic text change in extraData
        PostConditions(data);

        //If this new Node is a Player Node, set the player choices offered by the node
        if (data.isPlayer)
        {
            //Set node sprite if there's any, otherwise try to use default sprite
            if (data.sprite != null)
                playerSprite.sprite = data.sprite;
            else if (VD.assigned.defaultPlayerSprite != null)
                playerSprite.sprite = VD.assigned.defaultPlayerSprite;

            SetOptions(data.comments);

            //If it has a tag, show it, otherwise let's use the alias we set in the VIDE Assign
            if (data.tag.Length > 0)
                playerLabel.text = data.tag;
            else
                playerLabel.text = player.playerName;

            //Sets the player container on
            playerContainer.SetActive(true);

        }
        else  //If it's an NPC Node, let's just update NPC's text and sprite
        {
            //Set node sprite if there's any, otherwise try to use default sprite
            if (data.sprite != null)
            {
                //For NPC sprite, we'll first check if there's any "sprite" key
                //Such key is being used to apply the sprite only when at a certain comment index
                //Check CrazyCap dialogue for reference
                if (data.extraVars.ContainsKey("sprite"))
                {
                    if (data.commentIndex == (int)data.extraVars["sprite"])
                        NPCSprite.sprite = data.sprite;
                    else
                        NPCSprite.sprite = VD.assigned.defaultNPCSprite; //If not there yet, set default dialogue sprite
                }
                else //Otherwise use the node sprites
                {
                    NPCSprite.sprite = data.sprite;
                }
            } //or use the default sprite if there isnt a node sprite at all
            else if (VD.assigned.defaultNPCSprite != null)
                NPCSprite.sprite = VD.assigned.defaultNPCSprite;

            //This coroutine animates the NPC text instead of displaying it all at once
            NPC_TextAnimator = DrawText(data.comments[data.commentIndex], 0.02f);
            StartCoroutine(NPC_TextAnimator);

            //If it has a tag, show it, otherwise let's use the alias we set in the VIDE Assign
            if (data.tag.Length > 0)
                NPC_label.text = data.tag;
            else
                NPC_label.text = VD.assigned.alias;

            //Sets the NPC container on
            NPC_Container.SetActive(true);
        }
    }

    //This uses the returned string[] from nodeData.comments to create the UIs for each comment
    //It first cleans, then it instantiates new choices
    public void SetOptions(string[] choices)
    {
        //Create the choices. The prefab comes from a dummy gameobject in the scene
        //This is a generic way of doing it. You could instead have a fixed number of choices referenced.
        for (int i = 0; i < choices.Length; i++)
        {
            GameObject newOp = Instantiate(playerChoicePrefab.gameObject, playerChoicePrefab.transform.position, Quaternion.identity) as GameObject;
            newOp.transform.SetParent(playerChoicePrefab.transform.parent, true);
            newOp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 20 - (20 * i));
            newOp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newOp.GetComponent<UnityEngine.UI.Text>().text = choices[i];
            newOp.SetActive(true);

            currentChoices.Add(newOp.GetComponent<UnityEngine.UI.Text>());
        }
    }

    //Unsuscribe from everything, disable UI, and end dialogue
    //Called automatically because we subscribed to the OnEnd event
    void EndDialogue(VD.NodeData data)
    {
        VD.OnActionNode -= ActionHandler;
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= EndDialogue;
        dialogueContainer.SetActive(false);
        VD.EndDialogue();

        VD.SaveState("VIDEDEMOScene1", true); //Saves VIDE stuff related to EVs and override start nodes
        QuestChartDemo.SaveProgress(); //saves OUR custom game data
    }

    void OnDisable()
    {
        //If the script gets destroyed, let's make sure we force-end the dialogue to prevent errors
        //We do not save changes
        VD.OnActionNode -= ActionHandler;
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= EndDialogue;
        if (dialogueContainer != null)
            dialogueContainer.SetActive(false);
        VD.EndDialogue();
    }

    //Conditions we check after VD.Next was called but before we update the UI
    void PostConditions(VD.NodeData data)
    {
        //Don't conduct extra variable actions if we are waiting on a paused action
        if (data.pausedAction) return;

        if (!data.isPlayer) //For player nodes
        {
            //Replace [WORDS]
            ReplaceWord(data);

            //Checks for extraData that concerns font size (CrazyCap node 2)
            if (data.extraData[data.commentIndex].Contains("fs"))
            {
                int fSize = 14;

                string[] fontSize = data.extraData[data.commentIndex].Split(","[0]);
                int.TryParse(fontSize[1], out fSize);
                NPC_Text.fontSize = fSize;
            }
            else
            {
                NPC_Text.fontSize = 14;
            }
        }
    }

    //This will replace any "[NAME]" with the name of the gameobject holding the VIDE_Assign
    //Will also replace [WEAPON] with a different variable
    void ReplaceWord(VD.NodeData data)
    {
        if (data.comments[data.commentIndex].Contains("[NAME]"))
            data.comments[data.commentIndex] = data.comments[data.commentIndex].Replace("[NAME]", VD.assigned.gameObject.name);

        if (data.comments[data.commentIndex].Contains("[WEAPON]"))
        {
        }

    }

    #region EVENTS AND HANDLERS

    //Just so we know when we finished loading all dialogues, then we unsubscribe
    void OnLoadedAction()
    {
        Debug.Log("Finished loading all dialogues");
        VD.OnLoaded -= OnLoadedAction;
    }

    //Another way to handle Action Nodes is to listen to the OnActionNode event, which sends the ID of the action node
    void ActionHandler(int actionNodeID)
    {
        //Debug.Log("ACTION TRIGGERED: " + actionNodeID.ToString());
    }

    IEnumerator DrawText(string text, float time)
    {
        animatingText = true;

        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (i != words.Length - 1) word += " ";

            string previousText = NPC_Text.text;

            float lastHeight = NPC_Text.preferredHeight;
            NPC_Text.text += word;
            if (NPC_Text.preferredHeight > lastHeight)
            {
                previousText += System.Environment.NewLine;
            }

            for (int j = 0; j < word.Length; j++)
            {
                NPC_Text.text = previousText + word.Substring(0, j + 1);
                yield return new WaitForSeconds(time);
            }
        }
        NPC_Text.text = text;
        animatingText = false;
    }

    void CutTextAnim()
    {
        StopCoroutine(NPC_TextAnimator);
        NPC_Text.text = VD.nodeData.comments[VD.nodeData.commentIndex]; //Now just copy full text		
        animatingText = false;
    }

    #endregion

    //Utility note: If you're on MonoDevelop. Go to Tools > Options > General and enable code folding.
    //That way you can exapnd and collapse the regions and method
}
