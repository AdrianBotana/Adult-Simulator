using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InputBehavior : MonoBehaviour
{
	[Header("TMP")]
	public TMP_InputField input;
	public TMP_Text terminal;
	public TMP_Text days;

	[Header("Sliders")]
	public Slider work;
	public Slider selfcare;
	public Slider friends;
	public Slider family;

	[Header("Terminal parametters")]
	public float letterRate;
	public Scrollbar scroll;

	private float nextLetter;
	private string sentence;
	private bool write;

	private int dayCount;

	public void Start()
	{
		dayCount = 1;

		//Start sentence
		string startText = "Welcome to Adult Simulator! \n" +
			"To complete the game, you will need to survive 15 days on an adult life simulation! \n" +
			"Every day is focused on a certain task like work, hang out with friends, spend time with your family or care about your mental sanity! \n" +
			"Type the words on your right to performe differents actions but remember, every action has consequences so be aware and have fun! \n";
		WriteText(startText);
	}

	public void Update()
	{
		if (Time.time > nextLetter && write)
		{
			nextLetter = Time.time + letterRate;
			terminal.text += sentence[0];
			sentence = sentence.Substring(1);
			scroll.value = 0;
			if (sentence.Length == 0)
			{
				write = false;
				input.interactable = true;
			}
		}
	}

	public void OnPressEnter()
	{
		//Check input
		switch (input.text.ToLower())
		{
			case "work":
				work.value += 0.1f;
				selfcare.value -= 0.1f;
				family.value -= 0.1f;
				terminal.text += "<b>--work--</b>\n";
				WriteText("You worked the hole day to get some money and get back to home exhausted. \nWhen you arrived, your family was already asleep.\n");
				break;
			case "family":
				work.value -= 0.1f;
				friends.value -= 0.1f;
				family.value += 0.1f;
				terminal.text += "<b>--family--</b>\n";
				WriteText("Exped the day with your family, your son is happier than ever cause you are happy too.\n");
				break;
			case "friends":
				family.value -= 0.1f;
				friends.value += 0.1f;
				work.value -= 0.1f;
				terminal.text += "<b>--friends--</b>\n";
				WriteText("You go out to take a drink and have fun with some old friends.\nYou feel yought, like old times.\n");
				break;
			case "selfcare":
				selfcare.value += 0.1f;
				friends.value -= 0.1f;
				terminal.text += "<b>--selfcare--</b>\n";
				WriteText("Sometimes is needed to relax and care about yourself.\nRead a book, meditate or even drive without destination. Just take a day for yourself.\n");
				break;
			default:
				WriteText("I don't know what you want to do, please type again.\n");
				break;

		}
		//Update text and day
		input.text = string.Empty;
		if (dayCount > 10)
		{
			days.text = days.text.Substring(0, days.text.Length - 2) + dayCount++;
		}
		else
		{
			days.text = days.text.Substring(0, days.text.Length - 1) + dayCount++;
		}
		if (dayCount == 16)
		{
			//WinGame
			WriteText("<b>Congratulations!\n</b>You have survived 15 days in the simulation. \nAs you see life is not easy, it is needed to maintain a balance among thousands of things. \nWhether you have fail in something or not, life goes on and does not stop.\n So please go out and enjoy it, your life is yours.\nIf you enjoyed this game please leave a comment!\n");
		}

		//Check fail game
		if (work.value < 0.05f)
		{
			WriteText("<b>Work Fail:\n</b>");
			WriteText("By not working, the company kick you out and you are bankrupt. You cannot pay for the house or food. Luckily you still having people by your side and you can move on. \n");
			work.value = 0.06f;
		}
		if (family.value < 0.05f)
		{
			WriteText("<b>Family Fail:\n</b>");
			WriteText("Your family doesn't stand you anymore. Your couple wants the divorce, at least your children continue loving you.\n");
			family.value = 0.06f;
		}
		if (friends.value < 0.05f)
		{
			WriteText("<b>Friends Fail:\n</b>");
			WriteText("Not having a social life pays a bill, you've run out of friends who support you. Equally, life goes on.\n");
			friends.value = 0.06f;
		}
		if (selfcare.value < 0.05f)
		{
			WriteText("<b>Selfcare Fail:\n</b>");
			WriteText("You have self-destroyed; your self-esteem is on the floor and even consider suicide. In spite of that the world continues and you have to put up with it.\n");
			selfcare.value = 0.06f;
		}
	}

	public void WriteText(string text)
	{
		sentence += text;
		write = true;
		input.interactable = false;
	}
}

