using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	//IMPORTANT: Attach this script to a Dialogue Box/Canvas!! Look at the EndDialogue() method to see why. (Destruction and Chaos)

	public Text nameText;
	public Text dialogueText;

	private Queue<string> sentences;

	public Dialogue offeringDialogue;

	public float timeBetweenLetters = 0.1f;
	public float timeBetweenSentences = 2f;


    private void Awake()
    {
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
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
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
		Destroy(gameObject);
	}

}
