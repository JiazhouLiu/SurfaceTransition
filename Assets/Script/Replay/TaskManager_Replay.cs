﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class TaskManager_Replay : MonoBehaviour
{
    public DataPlayback DP;
    public Transform StartBoard;
    public Text StartTitle;
    public Transform StartButton;
    public Transform AnswerButton;
    public Transform QuestionBoard;
    public Transform ConfirmBoard;
    public TextAsset QuestionFile;
    public Text TitleText;
    public Text BodyText;
    public bool TrainingScene = false;
    [HideInInspector]
    public List<string> questions;

    private int questionID;
    private int prevQuestionID;
    [HideInInspector]
    public bool initialised = false;

    private char lineSeperater = '\n'; // It defines line seperate character
    private char AlineSeperater = '&';

    // Start is called before the first frame update
    void Awake()
    {
        questions = new List<string>();

        prevQuestionID = 0;
        ReadQuestionsFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        questionID = DP.currentQuestionID;
        if (questionID != prevQuestionID) {
            prevQuestionID = questionID;
            UpdateUI(questionID);
        }

        if (!initialised && Camera.main != null) {
            initialised = true;

            Vector3 oldAngle = Camera.main.transform.eulerAngles;
            Camera.main.transform.eulerAngles = new Vector3(0, oldAngle.y, oldAngle.z);
            Vector3 forward = Camera.main.transform.forward;
            Camera.main.transform.eulerAngles = oldAngle;

            transform.position = Camera.main.transform.TransformPoint(Vector3.zero) + forward * 0.5f;
            transform.position -= Vector3.up * 0.1f;

            transform.LookAt(Camera.main.transform);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - 90, transform.localEulerAngles.y, transform.localEulerAngles.z + 180);
        }
    }

    private void UpdateUI(int questionID) {
        DisplayQuestionOnBoard(questions[questionID - 1]);
        if (TrainingScene)
            TitleText.text = "Training Question " + questionID + "/4";
        else {

            TitleText.text = "Replay";
            StartTitle.text = "Are you ready?";
        }
    }

    private void ReadQuestionsFromFile()
    {
        string[] lines = QuestionFile.text.Split(lineSeperater);

        questions.AddRange(lines);
    }

    private void DisplayQuestionOnBoard(string question)
    {
        string[] lines = question.Split(AlineSeperater);
        string final = "";
        foreach (string s in lines)
            final += s + "\n";
        BodyText.text = final;
    }
}
