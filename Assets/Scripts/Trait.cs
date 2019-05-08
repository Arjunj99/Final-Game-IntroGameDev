using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Trait
{
    public Traits Type;
    public List<EventType> ListenFor = new List<EventType>();

    public void Setup()
    {
        God.Library.TraitDict.Add(Type,this);
    }

    public abstract void TakeMsg(ActorModel who, EventMsg msg);

}

public enum Traits
{
    None = 0,
    Key = 1,
    Door = 2,
    Monster = 3,
    Score = 4,
    Player = 5,
    Wall = 6,
    Torches = 7,
    FakeKey = 8
}

public class EventMsg
{
    public EventType Type;
    public ActorModel Source;
    public int Amount;
    public Inputs Dir;
    public string Text = "";

    public EventMsg(EventType type,ActorModel source,int amt=0,Inputs dir=Inputs.None)
    {
        Type = type;
        Source = source;
        Amount = amt;
        Dir = dir;
    }
    public EventMsg(Inputs dir)
    {
        Type = EventType.PlayerInput;
        Source = null;
        Amount = 0;
        Dir = dir;
    }
}

public enum EventType
{
    None,
    GetBumped,
    TakeDmg,
    PlayerInput,
    GetName,
    MonsterMove,
    WallStop,
    KeyMove,
    ScoreMove
}

public class KeyTrait : Trait
{
    public KeyTrait()
    {
        Type = Traits.Key;
        ListenFor.Add(EventType.GetBumped);
        ListenFor.Add(EventType.GetName);
        ListenFor.Add(EventType.KeyMove);
    }

    public override void TakeMsg(ActorModel who, EventMsg msg)
    {
        bool key = false;
        switch (msg.Type)
        {
            case EventType.GetBumped:
                //if (God.GSM.keyIsVisible) {
                    ActorModel bumper = msg.Source;
                    if (bumper.Type == ThingTypes.Player)
                    {
                        TileModel loc = who.GetLocation();
                        bumper.HasKey = true;

                        God.GSM.hasKey = true;
                        Debug.Log("KEY IS " + key);
                        who.LeaveTile(who.GetLocation());
                        bumper.SetLocation(loc);
                        who.View.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                        who.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                        God.C.AddAction(new GainKeyAction(bumper, who));
                    }
                //}
                return;
            case EventType.GetName:
                msg.Text += " KEY";
                return;
            case EventType.KeyMove:
                //Debug.Log("KEY IS MOVING");
                if (!God.GSM.hasKey)
                {

                    if (who.View.movement < God.GSM.monsterRound)
                    {
                        who.View.movement++;
                    }
                    else if (who.View.movement == God.GSM.monsterRound)
                    {
                        int rand = Random.Range(0, 4);
                        //Debug.Log(rand);
                        if (rand == 0)
                        {
                            who.Move(-1, 0);
                        } else if (rand == 1) {
                            who.Move(1, 0);
                        } else if (rand == 2) {
                            who.Move(0, -1);
                        } else if (rand == 3) {
                            who.Move(0, 1);
                        }
                        who.View.movement = 0;
                    }
                }
                return;
        }
        
    }
}

public class FakeKeyTrait : Trait
{
    public FakeKeyTrait()
    {
        Type = Traits.FakeKey;
        ListenFor.Add(EventType.GetBumped);
        ListenFor.Add(EventType.GetName);
        ListenFor.Add(EventType.KeyMove);
        ListenFor.Add(EventType.TakeDmg);
    }

    public override void TakeMsg(ActorModel who, EventMsg msg)
    {
        switch (msg.Type)
        {
            case EventType.GetBumped:
                God.C.AddAction(new BumpAction(msg.Source, who.Location.x, who.Location.y));
                who.View.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                who.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                msg.Source.TakeMsg(new EventMsg(EventType.TakeDmg, who, 2));
                return;
            case EventType.GetName:
                msg.Text += " " + "FakeKey";
                return;
            case EventType.KeyMove:
                //Debug.Log("Move");
                //who.Move()

                if (who.View.movement < God.GSM.monsterRound)
                {
                    //God.GSM.monsterMovement++;
                }
                else if (who.View.movement == God.GSM.monsterRound)
                {
                    int rand = Random.Range(0, 2);
                    Debug.Log(rand);
                    if (rand == 1)
                    {
                        if (who.Location.x > GameSettings.playerX)
                        {
                            who.Move(-1, 0);
                        }
                        else if (who.Location.x < GameSettings.playerX)
                        {
                            who.Move(1, 0);
                        }
                    }
                    else if (rand == 0)
                    {
                        if (who.Location.y > GameSettings.playerY)
                        {
                            who.Move(0, -1);
                        }
                        else if (who.Location.y < GameSettings.playerY)
                        {
                            who.Move(0, 1);
                        }
                    }
                    //God.GSM.AS.PlayOneShot(God.GSM.StompClip);
                    who.View.movement = 0;
                }
                return;
        }
    }
}

public class GainKeyAction : GameAction
{
    public ActorModel Player;
    public ActorModel Key;

    public GainKeyAction(ActorModel player, ActorModel key)
    {
        Player = player;
        Key = key;
    }
    
    public override IEnumerator Run()
    {
        Key.View.transform.SetParent(Player.View.transform);
        Debug.Log("Trait");
        Key.View.gameObject.SetActive(true);
        float timer = 0;
        Vector3 startPos = Key.View.transform.localPosition;
        Vector3 endPos = new Vector3(0.25f,0.25f,-0.1f);
        Vector3 startSize = Key.View.transform.localScale;
        Vector3 endSize = new Vector3(0.5f,0.5f,1);
        while (timer < 1)
        {
            timer += Time.deltaTime / 0.2f;
            float t = God.Ease (timer, true);
            Key.View.transform.localPosition = Vector3.Lerp(startPos,endPos,t);
            Key.View.transform.localScale = Vector3.Lerp(startSize,endSize,t);
            yield return null;
        }
        Key.View.transform.localPosition = endPos;
        Key.View.transform.localScale = endSize;
    }
}

public class DoorTrait : Trait
 {
     public DoorTrait()
     {
         Type = Traits.Door;
         ListenFor.Add(EventType.GetBumped);
         ListenFor.Add(EventType.GetName);
     }
 
     public override void TakeMsg(ActorModel who, EventMsg msg)
     {
         switch (msg.Type)
         {
             case EventType.GetBumped:
                ActorModel bumper = msg.Source;
                who.View.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                who.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                if (bumper.HasKey)
                 {
                     God.C.AddAction(new UseDoorAction(bumper,who));
                     //ModelManager.SaveGame();
                 }
                 return;
             case EventType.GetName:
                 msg.Text += " DOOR";
                 return;
         }
         
     }
 }

public class UseDoorAction : GameAction
{
    public ActorModel Player;
    public ActorModel Door;

    public UseDoorAction(ActorModel player, ActorModel door)
    {
        Player = player;
        Door = door;
    }
    
    public override IEnumerator Run()
    {
        Player.View.transform.SetParent(Door.View.transform);
        float timer = 0;
        Vector3 startPos = Player.View.transform.localPosition;
        Vector3 endPos = new Vector3(0,0,-0.1f);
        Vector3 startSize = Player.View.transform.localScale;
        Vector3 endSize = new Vector3(0,0,1);
        while (timer < 1)
        {
            timer += Time.deltaTime / 0.5f;
            float t = God.Ease (timer, true);
            Player.View.transform.localPosition = Vector3.Lerp(startPos,endPos,t);
            Player.View.transform.localScale = Vector3.Lerp(startSize,endSize,t);
            Player.View.transform.Rotate(0,0,10);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        God.Round++;
        //Debug.Log(God.Round);
        SceneManager.LoadScene("Game");
    }
}
 
public class MonsterTrait : Trait
{
    public MonsterTrait()
    {
        Type = Traits.Monster;
        ListenFor.Add(EventType.GetBumped);
        ListenFor.Add(EventType.GetName);
        ListenFor.Add(EventType.MonsterMove);
    }

    public override void TakeMsg(ActorModel who, EventMsg msg)
    {
        switch (msg.Type)
        {
            case EventType.GetBumped:
                God.C.AddAction(new BumpAction(msg.Source, who.Location.x, who.Location.y));
                who.View.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                who.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                msg.Source.TakeMsg(new EventMsg(EventType.TakeDmg, who, God.Library.GetMonster(who.Species).Vibration));
                //who.Despawn();
                return;
            case EventType.GetName:
                msg.Text += " " + God.Library.GetMonster(who.Species).Type;
                return;
            case EventType.MonsterMove:
                //Debug.Log("Move");
                //who.Move()

                if (who.View.movement < God.GSM.monsterRound) {
                    who.View.movement++;
                }
                else if (who.View.movement == God.GSM.monsterRound)
                {
                    int rand = Random.Range(0, 2);
                    Debug.Log(rand);
                    if (rand == 1)
                    {
                        if (who.Location.x > GameSettings.playerX)
                        {
                            who.Move(-1, 0);
                        }
                        else if (who.Location.x < GameSettings.playerX)
                        {
                            who.Move(1, 0);
                        }
                    }
                    else if (rand == 0)
                    {
                        if (who.Location.y > GameSettings.playerY)
                        {
                            who.Move(0, -1);
                        }
                        else if (who.Location.y < GameSettings.playerY)
                        {
                            who.Move(0, 1);
                        }
                    }
                    //One I changed just now (Tanavast)
                    //God.AM.AS.PlayOneShot(God.AM.StompClip);
                    who.View.movement = 0;
                }
                return;
        }



                //int rand = Random.Range(0, 3);
                //Debug.Log(rand);
                //if (rand == 1) {
                //    if (who.Location.x > GameSettings.playerX)
                //    {
                //        who.Move(-1, 0);
                //    }
                //    else if (who.Location.x < GameSettings.playerX)
                //    {
                //        who.Move(1, 0);
                //    }
                //}
                //else if (rand == 0) {
                //    if (who.Location.y > GameSettings.playerY)
                //    {
                //        who.Move(0, -1);
                //    }
                //    else if (who.Location.y < GameSettings.playerY)
                //    {
                //        who.Move(0, 1);
                //    }
                //}
                //else if (rand == 2)
                //{
                //    if (who.Location.y > GameSettings.playerY)
                //    {
                //        who.Move(0, 0);
                //    }
                //    else if (who.Location.y < GameSettings.playerY)
                //    {
                //        who.Move(0,0);
                //    }
                //}
                //who.Move(who.Location.x);
                //if(who.Location.x == -(GameSettings.MapSizeX / 2)) {
                //    who.Move((int)Random.Range(0, 1), 0);
                //}
                //if (who.Location.x == (GameSettings.MapSizeX / 2)) {
                //    who.Move((int)Random.Range(-1, 0), 0);
                //}
                //if (who.Location.y == -(GameSettings.MapSizeY / 2)) {
                //    who.Move(0, (int)Random.Range(0, 1));
                //}
                //if (who.Location.y == (GameSettings.MapSizeY / 2))
                //{
                //    who.Move(0, (int)Random.Range(-1, 0));
                //}
                //else {
                //    who.Move(0, 0);
                //} 

                //if (who.Location.y > -(GameSettings.MapSizeY / 2) &&
                //    who.Location.y < (GameSettings.MapSizeY / 2) &&
                //    who.Location.x > -(GameSettings.MapSizeX / 2) &&
                //    who.Location.x > (GameSettings.MapSizeX / 2)) {
                //    who.Move((int)Random.Range(-1, 1), (int)Random.Range(-1, 1));
                //}
        //        return;
        //}

    }
}

public class BumpAction : GameAction
{
    public ActorModel Player;
    public float DirX;
    public float DirY;

    public BumpAction(ActorModel player, float x, float y)
    {
        Player = player;
        DirX = x;
        DirY = y;
    }
    
    public override IEnumerator Run()
    {
        float timer = 0;
        Vector3 startPos = Player.View.transform.position;
        Vector3 endPos = new Vector3(DirX,DirY,-0.1f);
        while (timer < 1)
        {
            timer += Time.deltaTime / 0.1f;
            float t = Mathf.Sin(timer * Mathf.PI);
            Player.View.transform.position = Vector3.Lerp(startPos,endPos,t);
            yield return null;
        }
        Player.View.transform.localPosition = Vector3.zero;
        God.GSM.UpdateText();
    }
}

public class ScoreTrait : Trait
{
    public ScoreTrait()
    {
        Type = Traits.Score;
        ListenFor.Add(EventType.GetBumped);
        ListenFor.Add(EventType.GetName);
        ListenFor.Add(EventType.ScoreMove);
    }

    public override void TakeMsg(ActorModel who, EventMsg msg)
    {
        switch (msg.Type)
        {
            case EventType.GetBumped:
                ActorModel bumper = msg.Source;
                if (bumper.Type == ThingTypes.Player)
                {
                    //if (God.GSM.scoreIsVisible) {
                        TileModel loc = who.GetLocation();
                        who.View.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                        who.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                        God.C.AddAction(new GetScoreAction(bumper, who));
                        who.Despawn();
                        ModelManager.ChangeScore(1);
                        bumper.SetLocation(loc);
                    //}

                }
                return;
            case EventType.GetName:
                msg.Text += " SCORETHING";
                return;
            case EventType.ScoreMove:
                if (who.View.movement < God.GSM.monsterRound)
                {
                    who.View.movement++;
                }
                else if (who.View.movement == God.GSM.monsterRound)
                {
                    int rand = Random.Range(0, 4);
                    //Debug.Log(rand);
                    if (rand == 0)
                    {
                        who.Move(-1, 0);
                    }
                    else if (rand == 1)
                    {
                        who.Move(1, 0);
                    }
                    else if (rand == 2)
                    {
                        who.Move(0, -1);
                    }
                    else if (rand == 3)
                    {
                        who.Move(0, 1);
                    }


                    //int rand = Random.Range(0, 2);
                    //Debug.Log(rand);
                    //if (rand == 1)
                    //{
                    //    if (who.Location.x > GameSettings.playerX)
                    //    {
                    //        who.Move(-1, 0);
                    //    }
                    //    else if (who.Location.x < GameSettings.playerX)
                    //    {
                    //        who.Move(1, 0);
                    //    }
                    //}
                    //else if (rand == 0)
                    //{
                    //    if (who.Location.y > GameSettings.playerY)
                    //    {
                    //        who.Move(0, -1);
                    //    }
                    //    else if (who.Location.y < GameSettings.playerY)
                    //    {
                    //        who.Move(0, 1);
                    //    }
                    //}
                    //God.GSM.AS.PlayOneShot(God.GSM.StompClip);
                    who.View.movement = 0;
                }
                return;
        }
        
    }
}

public class GetScoreAction : GameAction
{
    public ActorModel Player;
    public ActorModel Score;

    public GetScoreAction(ActorModel player, ActorModel score)
    {
        Player = player;
        Score = score;
    }
    
    public override IEnumerator Run()
    {
        Score.View.transform.SetParent(Player.View.transform);
        float timer = 0;
        Vector3 startPos = Score.View.transform.localPosition;
        Vector3 endPos = new Vector3(0,0,-0.1f);
        Vector3 startSize = Score.View.transform.localScale;
        Vector3 endSize = new Vector3(0,0,1);
        while (timer < 1)
        {
            timer += Time.deltaTime / 0.2f;
            float t = God.Ease (timer, true);
            Score.View.transform.localPosition = Vector3.Lerp(startPos,endPos,t);
            Score.View.transform.localScale = Vector3.Lerp(startSize,endSize,t);
            Score.View.transform.Rotate(0,0,30);
            yield return null;
        }
        God.GSM.UpdateText();
    }
}

public class PlayerTrait : Trait
{
    public PlayerTrait()
    {
        Type = Traits.Player;
        ListenFor.Add(EventType.TakeDmg);
        ListenFor.Add(EventType.PlayerInput);
        ListenFor.Add(EventType.MonsterMove);
        ListenFor.Add(EventType.GetName);
    }

    public override void TakeMsg(ActorModel who, EventMsg msg)
    {
        switch (msg.Type)
        {
            case EventType.TakeDmg:
                int amount = msg.Amount;
                God.C.AddAction(new TakeDamageAction(who,amount));
                ModelManager.TakeDamage();
                return;
            case EventType.PlayerInput:
                if (msg.Dir == Inputs.Left && God.GSM.wallLimit != 0)
                {
                    //Debug.Log(ModelManager.GetActor(who.ID));
                    who.Move(-1,0);
                }
                else if (msg.Dir == Inputs.Right && God.GSM.wallLimit != 1)
                {
                    who.Move(1,0);
                }
                else if (msg.Dir == Inputs.Up && God.GSM.wallLimit != 2)
                {
                    who.Move(0,1);
                }
                else if (msg.Dir == Inputs.Down && God.GSM.wallLimit != 3)
                {
                    who.Move(0,-1);
                }
                foreach (TileModel tm in ModelManager.GetTiles()) {
                    tm.View.ControllerCheck(tm.View);
                }
                GameSettings.playerX = who.Location.x;
                GameSettings.playerY = who.Location.y;
                God.GSM.currentMoves++;
                return;
            case EventType.GetName:
                msg.Text += " PLAYER";
                return;
            //case EventType.MonsterMove:
                //who.Move((int)Random.Range(-1, 1), (int)Random.Range(-1, 1));
                //return;
        }
    }
}


//public class SpecialWallTrait : Trait
//{
    //public SpecialWallTrait()
    //{
    //    Type = Traits.SpecialWall;
    //    ListenFor.Add(EventType.GetBumped);
    //    ListenFor.Add(EventType.GetName);
    //    ListenFor.Add(EventType.WallStop);
    //}

    //public override void TakeMsg(ActorModel who, EventMsg msg)
    //{
    //    Debug.Log("trigger");
    //    switch (msg.Type)
    //    {
    //        case EventType.GetBumped:
    //            ActorModel bumper = msg.Source;
    //            if (bumper.Type == ThingTypes.Player)
    //            {
    //                Debug.Log("I touched player");
    //                TileModel loc = who.GetLocation();
    //                //bumper.HasKey = true;
    //                who.LeaveTile(who.GetLocation());
    //                bumper.SetLocation(loc);
    //                //who.View.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    //                //who.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    //                God.C.AddAction(new GainWallAction(bumper, who));
    //            }
    //            return;
    //        case EventType.GetName:
    //            msg.Text += " KEY";
    //            return;
    //        case EventType.WallStop:
    //            int WallFace = Random.Range(0, 4);
    //            switch (WallFace)
    //            {
    //                case (0):
    //                    God.GSM.wallLimit = 0;
    //                    return;
    //                case (1):
    //                    God.GSM.wallLimit = 1;
    //                    return;
    //                case (2):
    //                    God.GSM.wallLimit = 2;
    //                    return;
    //                case (3):
    //                    God.GSM.wallLimit = 3;
    //                    return;
    //            }
    //            return;
    //    }

    //}




    //public SpecialWallTrait()
    //{
    //    Type = Traits.Wall;
    //    ListenFor.Add(EventType.GetBumped);
    //    ListenFor.Add(EventType.GetName);
    //    ListenFor.Add(EventType.WallStop);
    //}

    //public override void TakeMsg(ActorModel who, EventMsg msg)
    //{
    //    switch (msg.Type)
    //    {
    //        case EventType.GetBumped:
    //            ActorModel bumper = msg.Source;
    //            if (bumper.Type == ThingTypes.Player)
    //            {
    //                TileModel loc = who.GetLocation();
    //                who.View.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    //                who.View.transform.GetComponentInParent<TileView>().GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    //                //God.C.AddAction(new GetScoreAction(bumper, who));
    //                who.Despawn();
    //                //ModelManager.ChangeScore(1);
    //                //bumper.SetLocation(loc);
    //            }
    //            return;
    //        case EventType.GetName:
    //            msg.Text += " SPECIAL WALL";
    //            return;
    //        case EventType.WallStop:
    //            int WallFace = Random.Range(0, 4);
    //            switch(WallFace) {
    //                case(0):
    //                    God.GSM.wallLimit = 0;
    //                    return;
    //                case (1):
    //                    God.GSM.wallLimit = 1;
    //                    return;
    //                case (2):
    //                    God.GSM.wallLimit = 2;
    //                    return;
    //                case (3):
    //                    God.GSM.wallLimit = 3;
    //                    return;
    //            }
    //            return;
    //    }
    //}
//}

public class GainWallAction : GameAction
{
    public ActorModel Player;
    public ActorModel Wall;

    public GainWallAction(ActorModel player, ActorModel wall)
    {
        Player = player;
        Wall = wall;
    }

    public override IEnumerator Run()
    {
        Wall.View.transform.SetParent(Player.View.transform);
        Debug.Log("Trait");
        Wall.View.gameObject.SetActive(true);
        float timer = 0;
        Vector3 startPos = Wall.View.transform.localPosition;
        Vector3 endPos = new Vector3(0.25f, 0.25f, -0.1f);
        Vector3 startSize = Wall.View.transform.localScale;
        Vector3 endSize = new Vector3(0.5f, 0.5f, 1);
        while (timer < 1)
        {
            timer += Time.deltaTime / 0.2f;
            float t = God.Ease(timer, true);
            Wall.View.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            Wall.View.transform.localScale = Vector3.Lerp(startSize, endSize, t);
            yield return null;
        }
        Wall.View.transform.localPosition = endPos;
        Wall.View.transform.localScale = endSize;
    }
}


public class TakeDamageAction : GameAction
{
    public ActorModel Player;
    public float Amount;

    public TakeDamageAction(ActorModel player, float amt)
    {
        Player = player;
        Amount = amt / 10;
    }
    
    public override IEnumerator Run()
    {
        float timer = 0;
        while (timer < Amount)
        {
            timer += Time.deltaTime;
            Player.View.transform.localPosition = new Vector3(Random.Range(-Amount,Amount),Random.Range(-Amount,Amount),0);
            yield return null;
        }
        Player.View.transform.localPosition = Vector3.zero;
        God.GSM.UpdateText();
    }
}

public class DeathAction : GameAction
{
    public ActorModel Who;
    
    public DeathAction(ActorModel who)
    {
        Who = who;
    }

    public override IEnumerator Run()
    {
        float timer = 0;
        float size = Who.View.transform.localScale.x;
        Who.View.GetComponentInChildren<SpriteRenderer>().sortingOrder = 10;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            size *= 1.2f;
            Who.View.transform.localScale = new Vector3(size,size,1);
            Who.View.transform.Rotate(0,0,30);
            yield return null;
        }
        God.GSM.SetText("The End is Imminent");
        timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            //size *= 1.2f;
            //Who.View.transform.localScale = new Vector3(size, size, 1);
            //Who.View.transform.Rotate(0, 0, 30);
            yield return null;
        }
        God.GSM.SetText("Space to Try Again \n Score: " + ModelManager.Score);
        God.GSM.tryAgain = true;

    }
}