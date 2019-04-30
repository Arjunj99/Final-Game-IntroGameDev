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
        //if(m.Type == ThingTypes.Player) {
        //    //Debug.Log("X is " + m.Location.x);
        //    //Body = transform.Find("Body").GetComponent<SpriteRenderer>();
        //    listener = gameObject.AddComponent<AudioListener>();
        //    //listener = transform.Find("Body").GetComponent<AudioListener>();
        //}
        //if(m.Type == ThingTypes.Skeleton) {
        //    source = gameObject.AddComponent<AudioSource>();
        //    source.minDistance = 1f;
        //    source.maxDistance = 1.7f;
        //    source.spatialBlend = 1.0f;
        //    source.rolloffMode = AudioRolloffMode.Linear;
        //    clip1 = God.GSM.clip;
        //    source.PlayOneShot(clip1);
        //}
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
