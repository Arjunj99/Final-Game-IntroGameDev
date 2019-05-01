using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActorView : MonoBehaviour
{
    public ActorModel Model;
    
    protected SpriteRenderer Body;
    protected AudioListener listener;
    protected AudioSource source;
    protected AudioClip clip1;
    protected AudioClip clip2;
    protected AudioClip clip3;
    protected AudioClip clip4;
    protected Animator animator;

    
    
    void Update()
    {
        IncreaseVision(this);
        //        if (Input.GetKeyDown(KeyCode.Space))
        //            transform.Dance();
    }

    //When I first spawn a world thing I need to run setup on it so that it does basic stuff like place itself
    public void Setup(ActorModel m)
    {
        Model = m;
        m.View = this;
        Body = transform.Find("Body").GetComponent<SpriteRenderer>();
        gameObject.name = m.Type.ToString();
//        if (!God.GSM.AllThings.Contains(this))
//            God.GSM.AllThings.Add(this);
        if(m.Type == ThingTypes.Player) {
            //Debug.Log("X is " + m.Location.x);
            //Body = transform.Find("Body").GetComponent<SpriteRenderer>();
            listener = gameObject.AddComponent<AudioListener>();
            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = God.GSM.animator;
            //listener = transform.Find("Body").GetComponent<AudioListener>();
        }
        if(m.Type == ThingTypes.Skeleton) {
            source = gameObject.AddComponent<AudioSource>();
            source.minDistance = 1f;
            source.maxDistance = 2.4f;
            source.spatialBlend = 1.0f;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.loop = true;
            source.clip = God.GSM.clip;
            source.Play();
        }
        //if (m.Type == ThingTypes.MagicDoor)
        //{
        //    source = gameObject.AddComponent<AudioSource>();
        //    source.minDistance = 1f;
        //    source.maxDistance = 1.7f;
        //    source.spatialBlend = 1.0f;
        //    source.rolloffMode = AudioRolloffMode.Linear;
        //    clip2 = God.GSM.clip2;
        //    source.PlayOneShot(clip2);
        //}
        //if (m.Type == ThingTypes.RedKey)
        //{
        //    source = gameObject.AddComponent<AudioSource>();
        //    source.minDistance = 1f;
        //    source.maxDistance = 1.7f;
        //    source.spatialBlend = 1.0f;
        //    source.rolloffMode = AudioRolloffMode.Linear;
        //    clip3 = God.GSM.clip3;
        //    source.PlayOneShot(clip3);
        //}
        //if (m.Type == ThingTypes.Wall)
        //{
        //    source = gameObject.AddComponent<AudioSource>();
        //    source.minDistance = 1f;
        //    source.maxDistance = 1.7f;
        //    source.spatialBlend = 1.0f;
        //    source.rolloffMode = AudioRolloffMode.Linear;
        //    clip4 = God.GSM.clip4;
        //    source.PlayOneShot(clip4);
        //}
        Body.sprite = God.Library.GetSprite(m.Type);
        if (m.Species != MonsterType.Types.None)
            Body.sprite = God.Library.GetMonster(m.Species).S;
        SetLocation(m.GetLocation().View);
//        transform.Rotate2D(50);
    }
    
    public void SetLocation(TileView tile)
    {
        
        transform.SetParent(tile.transform);

        transform.localPosition = Vector3.zero;
    }

    //When I'm destroyed make sure I destroy myself safely
    public void Despawn()
    {
        Debug.Log("despawn");
        gameObject.SetActive(false);
//        God.GSM.AllThings.Remove(this);
    }

    public void IncreaseVision(ActorView a)
    {
        if (Model.Type == ThingTypes.Player) {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite = God.GSM.lightgrey;
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            a.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
        }
        //else if ()
        //{
        //    a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
        //}
        if (Model.Type == ThingTypes.MagicDoor) {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite = God.GSM.grey;
            //a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite = God.GSM.grey;
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (Model.Type == ThingTypes.RedKey)
        {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite = God.GSM.grey;
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (Model.Type == ThingTypes.Wall) {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (Model.Type == ThingTypes.SpecialWall) {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (Model.Type == ThingTypes.Skeleton) {
            Debug.Log(a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite.name);

            if (a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite.name == "Inverted Floor") {
                //Debug.Log("THIS AINT WORKIN");
                a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
            }
            else {
                a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
            //else if (a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite.name == "Inverted Floor")
            //{
            //    a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
            //}


            //Debug.Log(a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite.name);
            //if (a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().name = "Grey")
        }
        if (Model.Type == ThingTypes.ScoreThing) {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite = God.GSM.grey;
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }


        //if (Model.Type == ThingTypes.Skeleton) {
        //    ModelManager.AllTiles();

        //    Debug.Log(a.Model.Location.x + ":" + a.Model.Location.y);
        //    //if()
        //}
        //Debug.Log(a.Model.Location.x);

        //if (Model.Type == ThingTypes.Skeleton.)



        //    gameObject.transform.Find("Player") == true)
        //{
        //    t.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        //    Debug.Log(gameObject.transform.localPosition);
        //}
    }

}
