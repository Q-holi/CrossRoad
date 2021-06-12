# CrossRoad

Title : 
_CrossRoad_    
Game Genre : Deck Building (Single Play Game Client)   
Development Personnel : 3    
Source Coding : 윤승원  
Picture Source : 이강민  
  
Development Environment  
1. Unity Engine  
2. Unity 2020.2.1f1 License  
3. Visual Studio 2019
4. FireAlpaca 2.4.3ver 
5. DoTween
      

*Collection of cards* [Producers : 이강민]  
![CardCollection](https://github.com/Q-holi/CrossRoad/blob/master/img/Collection%20of%20cards.png)  
*Character*[Producers : 이강민]  
![Player](https://github.com/Q-holi/CrossRoad/blob/master/img/Player.png)
![Boss](https://github.com/Q-holi/CrossRoad/blob/master/img/BOSS.png)  
*Battle Screen*  [Producers : 윤승원]  
![Combat Screen](https://github.com/Q-holi/CrossRoad/blob/master/img/BattleStart.gif)  
[MyCards Handle Souce]
```C#
void CardAlignment(bool isMine){
        List<PRS> originCardPRSs = new List<PRS>();
        if (isMine == true){
            originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * 1.9f);
            var targetCards = myCards;
            for (int i = 0; i < targetCards.Count; i++){
                var targetCard = targetCards[i];
                targetCard.originPRS = originCardPRSs[i];
                targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
            }
        }
    }
```  
*Ending Screen*  [Producers : 윤승원]  
![ChooseEnding](https://github.com/Q-holi/CrossRoad/blob/master/img/ChooseEnding.gif)  
[Card Prefabs]
![CardInfo](https://github.com/Q-holi/CrossRoad/blob/master/img/CardInfo.png)  
```C#
[System.Serializable]
public class Item{
    public string name;//card name
    public int attack;//card attack damage
    public int cost;//card cost
    public Sprite sprite;//card img
    public int option; //0 == Def , 1 == Attack , 3 == Draw
    public float percent;//card drop%
}
[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject{
    public Item[] items;
}
```  

  
References  
Map : <https://www.youtube.com/watch?v=P9ogBkLWmPQ&ab_channel=GamedevJourney>  
Battle Screen : <https://www.youtube.com/channel/UCqzWomWZKZUKOdT0sQdWFPQ>  
Chat Panel : <https://www.youtube.com/user/GoldmetalYT>  
