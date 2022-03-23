using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfferingDialogueManager : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;

	private Queue<string> sentences;

	public Dialogue offeringDialogue;

	public float timeBetweenLetters = 0.1f;
	public float timeBetweenSentences = 2f;


	OfferingNPC NPC;

	private void Awake()
	{
		NPC = GetComponentInChildren<OfferingNPC>();
		sentences = new Queue<string>();
		StartDialogue(offeringDialogue);
	}

	public void StartDialogue(Dialogue dialogue)
	{
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	IEnumerator DialogueDelay(float time)
	{
		yield return new WaitForSeconds(time);
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 1)
		{
			EndDialogue();
			return;
		}
		else if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		else
		{
			string sentence = sentences.Dequeue();
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
		}
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(timeBetweenLetters);
		}
		StartCoroutine(DialogueDelay(timeBetweenSentences));
	}

	void EndDialogue()
	{
		Debug.Log("Dialogue Over");
		NPC.speaking = false;
	}

	public void Farewell()
    {
		if(sentences.Count == 1)
			StartCoroutine(TypeSentence(sentences.Dequeue()));
	}

}
