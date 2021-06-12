# CrossRoad

Title : 
_CrossRoad_    
Game Genre : Deck Building (Single Play Game Client)   
Development Personnel : 3    

Development Environment  
1. Unity Engine  
2. Unity 2020.2.1f1 License  
3. Visual Studio 2019
4. FireAlpaca 2.4.3ver 
      

*Collection of cards* [Producers : 이강민]  
![CardCollection](https://github.com/Q-holi/CrossRoad/blob/master/img/Collection%20of%20cards.png)  
*Battle Screen*  
![Combat Screen](https://github.com/Q-holi/CrossRoad/blob/master/img/BattleStart.gif)  
[MyCards Handle]
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

  
References  
Map : <https://www.youtube.com/watch?v=P9ogBkLWmPQ&ab_channel=GamedevJourney>  
Battle Screen : <https://www.youtube.com/channel/UCqzWomWZKZUKOdT0sQdWFPQ>  
Chat Panel : <https://www.youtube.com/user/GoldmetalYT>  
