using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CinematicManager : MonoBehaviour
{
    public PlayableDirector timelineOnDefendFriend;
    public PlayableDirector timelineOnHide;

    public float changeTimeForDefend;
    public string SceneToLoadForDefend;
    private bool defending = false;

    public float changeTimeForHide;
    public string SceneToLoadForHide;
    private bool hiding = false;

    private bool activated = false;

    public void Defend() {
        timelineOnDefendFriend.Play();
        StoryEngine.current.TriggerEvent("ViolentChoice");
        StoryEngine.current.CheckPoint();
        activated = true;
        defending = true;
    }

    public void Hide() {
        timelineOnHide.Play();
        StoryEngine.current.TriggerEvent("PacifistChoice");
        StoryEngine.current.CheckPoint();
        activated = true;
        hiding = true;
    }

    public void Update() {
        if (activated) {

            if (defending) {
                changeTimeForDefend -= Time.deltaTime;
                if (changeTimeForDefend <= 0f) {
                    SceneManager.LoadScene(SceneToLoadForDefend);
                }
            } else if (hiding) {
                changeTimeForHide -= Time.deltaTime;
                if (changeTimeForHide <= 0f) {
                    SceneManager.LoadScene(SceneToLoadForHide);
                }
            }
        }
    }
}
