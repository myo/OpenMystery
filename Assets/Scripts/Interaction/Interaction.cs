using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using System;

/*
Interaction Class Lifespan

Wait for enter events to finish
Spawn Group members - Only if group
Wait for task to complete
Spawn leads to
Wait for leads to complete
Wait for exit events to finish
Return groupProgress to parent
*/

public abstract class Interaction : MonoBehaviour
{
    [SerializeField]
    public ConfigInteraction.Interaction config_interaction { get; set; }
    public InteractionGroup parent_group_interaction { get; set; }
    public InteractionAutotuneGroup parent_autotune_group_interaction { get; set; }
    public GameObject interaction_gameobject { get; protected set; }

    public System.Guid id = System.Guid.NewGuid();
    public System.Guid parent_group_id = System.Guid.Empty;
    public System.Guid parent_autotune_group_id = System.Guid.Empty;

    [SerializeField]
    public int group_progress = 0;
    public bool shouldShow = false;
    public bool destroyed = false;
    public bool should_onFinishedEnterEvents_when_respawned = true;

    public string[] toStringArray()
    {
        return new string[] {
            config_interaction.id,
            id.ToString(),
            parent_group_id.ToString(),
            parent_autotune_group_id.ToString(),
            group_progress.ToString(),
            shouldShow.ToString(),
            destroyed.ToString(),
            should_onFinishedEnterEvents_when_respawned.ToString()
        };
    }

    public virtual Interaction setup(ref ConfigInteraction.Interaction _interaction, bool should_add_enter_events)
    {
        config_interaction = _interaction;
        interaction_gameobject = gameObject;
        interaction_gameobject.name = _interaction.id;
        if (should_add_enter_events)
        {
            addEnterEvents();
        }
        return this;
    }
    protected void addEnterEvents()
    {
        if (config_interaction != null)
        {
            if (config_interaction.enterEvents != null)
            {
                GameStart.event_manager.main_event_player.event_stack.AddRange(config_interaction.enterEvents);
            }
        }
        GameStart.event_manager.all_script_events_finished_event += onFinishedEnterEvents;
    }

    public void respawnEnterEvents()
    {
        onFinishedEnterEvents();
    }

    protected virtual void onFinishedEnterEvents()
    {
        GameStart.event_manager.all_script_events_finished_event -= onFinishedEnterEvents;
    }

    public abstract void activate();
    public void interactionComplete()
    {
        if (!destroyed)
            addExitEvents();
    }
    protected void addExitEvents()
    {
        Assert.IsNotNull(config_interaction, "finished interaction was null");

        Debug.Log("Finished interaction " + config_interaction.id);

        if (config_interaction.exitEvents != null)
        {
            GameStart.event_manager.main_event_player.event_stack.AddRange(config_interaction.exitEvents);
        }

        if (config_interaction.successEvents != null) //I guess we can fail some stuff. Not for now though.
        {
            GameStart.event_manager.main_event_player.event_stack.AddRange(config_interaction.successEvents);
        }

        if (config_interaction.qteSuccessEvents != null) //I guess we can fail some stuff. Not for now though.
        {
            GameStart.event_manager.main_event_player.event_stack.AddRange(config_interaction.qteSuccessEvents);
        }
        GameStart.event_manager.all_script_events_finished_event += onFinishedExitEvents;

    }
    protected virtual void onFinishedExitEvents()
    {
        GameStart.event_manager.all_script_events_finished_event -= onFinishedExitEvents;

        Scenario.completeInteraction(config_interaction.id);

        if (config_interaction.leadsTo != null)
        {
            spawnLeadsTo();
        }
        else
        {
            finish();
        }
    }
    public void spawnLeadsTo()
    {
        Debug.Log("spawnLeadsTo");
        if (config_interaction.leadsToPredicate != null)
        {
            int best_match_leads_to = config_interaction.leadsTo.Length - 1;
            if (config_interaction.leadsToPredicate != null)
            {
                for (int i = config_interaction.leadsToPredicate.Length - 1; i >= 0; i--)
                {
                    if (Predicate.parsePredicate(config_interaction.leadsToPredicate[i]))
                    {
                        best_match_leads_to = i;
                    }
                }
            }
            if (config_interaction.leadsTo[best_match_leads_to] == config_interaction.id)
            {
                gameObject.transform.name = "old_" + gameObject.transform.name;
            }

            Debug.Log("Activating a leads to " + config_interaction.leadsTo[best_match_leads_to]);

            GameObject new_interaction = GameStart.interaction_manager.activateInteraction(config_interaction.leadsTo[best_match_leads_to]);

            if (new_interaction != null)
            {
                if (parent_group_interaction != null)
                {
                    Interaction leads_to_interaction = new_interaction.GetComponent<Interaction>();

                    parent_group_interaction.addMemberInteraction(leads_to_interaction);
                }
            }
            finish();
        }
        else
        {
            if (config_interaction.leadsTo[0] != "exit")
            {
                Debug.Log("Activating leads to " + config_interaction.leadsTo[0]);

                GameObject leads_to_gameobject = GameStart.interaction_manager.activateInteraction(config_interaction.leadsTo[0]);
                if (leads_to_gameobject == null)
                {
                    Debug.LogError(config_interaction.leadsTo[0] + " spawned a null gameobject ");
                }
                else {
                    Interaction leads_to_interaction = leads_to_gameobject.GetComponent<Interaction>();
                    if (parent_group_interaction != null)
                    {
                        parent_group_interaction.addMemberInteraction(leads_to_interaction);
                    }
                    finish();
                }
            }
            else
            {
                finish();
            }
        }
    }

    public void finish()
    {
        if (config_interaction == null) return;

        if (config_interaction.successReward != null)
        {
            Reward.getReward(config_interaction.successReward);
        }

        destroy();

        GameStart.interaction_manager.finishInteraction(this);

        if (parent_group_interaction != null)
            parent_group_interaction.memberInteractionFinished(this);
        if (parent_autotune_group_interaction != null)
            parent_autotune_group_interaction.memberInteractionFinished(config_interaction);
    }

    public virtual void destroy()
    {
        destroyed = true;
        InteractionManager.active_interactions.Remove(this);
        GameObject.DestroyImmediate(interaction_gameobject);
    }

    protected void setHotspot()
    {
        if (config_interaction.spot != null) {

            Scene.setGameObjectToHotspot(interaction_gameobject, config_interaction.spot);
        }
    }
}
