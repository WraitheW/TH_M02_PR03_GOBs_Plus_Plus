using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalSeeker : MonoBehaviour
{
    Goals[] goals;
    Action[] actions;
    Action changeOverTime;
    const float TICK = 5.0f;
    public Text text;
    public Sprite[] images;
    public GameObject Image;
    int currentIndex;

    void Start()
    {
        goals = new Goals[3];
        goals[0] = new Goals("Work", 2f);
        goals[1] = new Goals("Eat", 4f);
        goals[2] = new Goals("Sleep", 4f);

        actions = new Action[6];
        actions[0] = new Action("sit at my desk");
        actions[0].goals.Add(new Goals("Work", -3f));
        actions[0].goals.Add(new Goals("Eat", +1f));
        actions[0].goals.Add(new Goals("Sleep", +1f));

        actions[1] = new Action("eat some junk food");
        actions[1].goals.Add(new Goals("Work", +2f));
        actions[1].goals.Add(new Goals("Eat", -2f));
        actions[1].goals.Add(new Goals("Sleep", +1f));

        actions[2] = new Action("lay my head on my keyboard");
        actions[2].goals.Add(new Goals("Work", +2f));
        actions[2].goals.Add(new Goals("Eat", +1f));
        actions[2].goals.Add(new Goals("Sleep", -2f));

        actions[3] = new Action("crack a cold one");
        actions[3].goals.Add(new Goals("Work", +1f));
        actions[3].goals.Add(new Goals("Eat", -2f));
        actions[3].goals.Add(new Goals("Sleep", +2f));

        actions[4] = new Action("apply a caffeine patch");
        actions[4].goals.Add(new Goals("Work", +2f));
        actions[4].goals.Add(new Goals("Eat", -1f));
        actions[4].goals.Add(new Goals("Sleep", -2f));

        actions[5] = new Action("crank out work like I'm on cocaine");
        actions[5].goals.Add(new Goals("Work", -3f));
        actions[5].goals.Add(new Goals("Eat", +1f));
        actions[5].goals.Add(new Goals("Sleep", +1f));

        changeOverTime = new Action("Tick");
        changeOverTime.goals.Add(new Goals("Work", +3f));
        changeOverTime.goals.Add(new Goals("Eat", 2f));
        changeOverTime.goals.Add(new Goals("Sleep", +2f));

        InvokeRepeating("Tick", 0f, TICK);
        Debug.Log("Hit A to try");
    }

    void Tick()
    {
        foreach (Goals g in goals)
        {
            g.value += changeOverTime.getChange(g);
            g.value = Mathf.Max(g.value, 0);
        }

        goalString();
    }

    void goalString()
    {
        string goalsString = "";
        foreach(Goals g in goals)
        {
            goalsString += g.name + ": " + g.value + "; ";
        }
        goalsString += "Discontentment: " + CurrentDiscontentment();
        Debug.Log(goalsString);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Action bestAction = ChooseAction(actions, goals);
            Debug.Log("I think I'll " + bestAction.name);

            //foreach(Goals g in goals)
            //{
            //    g.value += bestAction.getChange(g);
            //    g.value = Mathf.Max(g.value, 0);
            //}

            for (int i = 0; i < goals.Length; i++)
            {
                goals[i].value += bestAction.getChange(goals[i]);
                goals[i].value = Mathf.Max(goals[i].value, 0);
            }

            Image.GetComponent<Image>().sprite = images[currentIndex];

            goalString();
        }
    }

    Action ChooseAction(Action[] actions, Goals[] goals)
    {
        Action bestAction = null;
        float bestVal = float.PositiveInfinity;

        //foreach (Action action in actions)
        //{
        //    float val = Discontentment(action, goals);

        //    if (val < bestVal)
        //    {
        //        bestVal = val;
        //        bestAction = action;
        //    }
        //}

        for (int i = 0; i < actions.Length; i++)
        {
            float val = Discontentment(actions[i], goals);

            if (val < bestVal)
            {
                bestVal = val;
                bestAction = actions[i];
                currentIndex = i;
            }
        }

        return bestAction;
    }

    float Discontentment(Action action, Goals[] goals)
    {
        float discontentment = 0f;

        foreach (Goals g in goals)
        {
            float newVal = g.value + action.getChange(g);
            newVal = Mathf.Max(newVal, 0);

            discontentment += g.returnDiscontentment(newVal);
        }

        return discontentment;
    }

    float CurrentDiscontentment()
    {
        float tot = 0f;
        
        foreach (Goals g in goals)
        {
            tot += (g.value * g.value);
        }

        return tot;
    }
}
