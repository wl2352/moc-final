using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class TextTyper : MonoBehaviour 
{	
	[Header("Settings")]
	[SerializeField] private float typeSpeed		= 0.1f;
	[SerializeField] private float startDelay 		= 0.5f;
	[SerializeField] private float volumeVariation 	= 0.1f;
	[SerializeField] private bool startOnStart		= false;

	[Header("Components")]
	[SerializeField] private AudioSource mainAudioSource;

	private bool typing;
	private int counter;
	private string textToType;
	private string previousText;
	private Text textComponent;

	private void Awake()
	{
		textComponent = GetComponent<Text>();

		if(!mainAudioSource)
		{
			Debug.Log("No AudioSource has been set. Set one if you wish you use audio features.");
		}
		
		counter = 0;
		textToType = textComponent.text;
		textComponent.text = "";
	}

	private void Start()
	{
		if(startOnStart)
		{
            StartTyping();
		}
	}

	public void StartTyping()
	{	
		if(!typing)
		{
			InvokeRepeating("Type", startDelay, typeSpeed);
		}
		else
		{
			Debug.LogWarning(gameObject.name + " : Is already typing!");
		}
	}

	public void StopTyping()
	{
        counter = 0;
        typing = false;
		CancelInvoke("Type");
	}

    public void UpdateText(string newText)
    {   
        StopTyping();
        textComponent.text = "";
        textToType = newText;
        StartTyping();
    }
	 public void ReplayPreviousText()
    {
        if (!typing && !string.IsNullOrEmpty(previousText))
        {
            StopTyping();
            textComponent.text = previousText; // Output the previous text
        }
    }

	public void QuickSkip()
	{
		if(typing)
		{
			StopTyping();
			textComponent.text = textToType;
		}
	}

	private void Type()
	{	
		typing = true;
		textComponent.text = textComponent.text + textToType[counter];
		counter++;

		if(mainAudioSource)
		{	
			mainAudioSource.Play();
			RandomiseVolume();
		}

		if(counter == textToType.Length)
		{	
			typing = false;
			CancelInvoke("Type");
		}
	}

	private void RandomiseVolume()
	{
		mainAudioSource.volume = Random.Range(1 - volumeVariation, volumeVariation + 1);
	}

    public bool IsTyping() { return typing; }
}