using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorView : MonoBehaviour {
    public ActorModel Model;
    public int movement = 0;
    protected SpriteRenderer Body;
    protected SpriteRenderer WallTop;
    protected AudioListener listener;
    protected AudioSource source;
    protected Animator animator;
    protected Animator walkingAnim;

    void Update() {
        ChangeView(this);
    }

    public void Setup(ActorModel m) {
        Model = m;
        m.View = this;
        Body = transform.Find("Body").GetComponent<SpriteRenderer>();
        gameObject.name = m.Type.ToString();
        if(m.Type == ThingTypes.Player) {
            listener = gameObject.AddComponent<AudioListener>();
            animator = gameObject.AddComponent<Animator>();
        }
        if(m.Type == ThingTypes.Monster) {
            if (gameObject.GetComponent<AudioSource>() == null) {
                //Debug.Log("Make new source");
                source = gameObject.AddComponent<AudioSource>();
            } else {
                //Debug.Log("Found Source");
                source = gameObject.GetComponent<AudioSource>();
            }
            //source = gameObject.AddComponent<AudioSource>();
            source.minDistance = 1f;
            source.maxDistance = 2.4f;
            source.spatialBlend = 1.0f;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.loop = true;
            source.clip = God.AM.monsterSnarl;
            source.Play();
        }
        if (Model.Type == ThingTypes.Wall) {
            GameObject wallTop;
            wallTop = new GameObject();
            wallTop.name = "WallTop";
            wallTop.transform.parent = gameObject.transform;
            wallTop.transform.localPosition = new Vector3(0, 0, 0);
            wallTop.AddComponent<SpriteRenderer>();
            wallTop.GetComponent<SpriteRenderer>().sprite = God.SM.wallTop;
            wallTop.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        Body.sprite = God.Library.GetSprite(m.Type);
        if (m.Species != MonsterType.Types.None)
            Body.sprite = God.Library.GetMonster(m.Species).S;
        SetLocation(m.GetLocation().View);
    }
    
    public void SetLocation(TileView tile) {
        transform.SetParent(tile.transform);
        transform.localPosition = Vector3.zero;
    }

    public void Despawn() {
        gameObject.SetActive(false);
    }

    public void ChangeView(ActorView a) {
        if (Model.Type == ThingTypes.Player) {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite = God.SM.lightgrey;
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            walkingAnim = gameObject.GetComponentInChildren<Animator>();
            walkingAnim.enabled = true;
        }
        if (Model.Type == ThingTypes.MagicDoor) {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite = God.SM.grey;
            a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (Model.Type == ThingTypes.RedKey) {
            walkingAnim = gameObject.GetComponentInChildren<Animator>();
            walkingAnim.enabled = true;
            walkingAnim.SetBool("Key", true);

            if (a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite.name == "Inverted Floor") {
                a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
                a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<Animator>().enabled = true;
                //God.GSM.keyIsVisible = false;
            } else {
                a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                //God.GSM.keyIsVisible = true;
            }
        }
        if (Model.Type == ThingTypes.Wall) {
            a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (Model.Type == ThingTypes.Monster) {
            if (a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite.name == "Inverted Floor") {
                a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
                a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<Animator>().enabled = true;
            } else {
                a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
        }
        if (Model.Type == ThingTypes.ScoreThing) {
            walkingAnim = gameObject.GetComponentInChildren<Animator>();
            walkingAnim.enabled = true;
            walkingAnim.SetBool("BlackSpider", true);
            if (a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().sprite.name == "Inverted Floor") {
                a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
                a.Model.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<Animator>().enabled = true;
                //God.GSM.scoreIsVisible = false;
            } else {
                a.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                //God.GSM.scoreIsVisible = true;
            }
        }
    }
}
