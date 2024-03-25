using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private Transform   playerTransform;
    [SerializeField]
    private GameObject  objText;

    [SerializeField]
    private float       interactionDistance;
    public bool         isActionDone;

    protected virtual void Start()
    {
        playerTransform = FindObjectOfType<PersonagemPrincipal>().GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        if(!isActionDone)
        {
            if(Vector2.Distance(playerTransform.position, transform.position) <= interactionDistance)
            {
                objText.SetActive(true);
                if(Input.GetButtonDown("Interact"))
                {
                    Action();
                }
            }else
            {
                objText.SetActive(false);
            }
        }
    }

    public virtual void Action()
    {

    }
}
