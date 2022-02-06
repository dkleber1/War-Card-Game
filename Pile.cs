// Author: Dana Kleber
// File Name: Card.cs
// Project Name: pass3v2-23
// Creation Date: Nov 15, 2020
// Modified Date: Nov. 18, 2020
//Description: An implementation of an object stack, in this case a pile of cards

using System;

class Pile
{
  //Maintain the cards in an array and its size
  Card [] cards;
  int size;

  public Pile()
  {
    //Instantiate the maximum size the pile can hold to 52 cards
    cards = new Card[52];
    size = 0;
  }

  //Pre: num is a valid integer
  //Post: None
  //Description: Add a card to the stack if there is room
  public void Push(Card card)
  {
    if (size < cards.Length)
    {
      cards[size] = card;
      size++;
    }
  }

  //Pre: None
  //Post: Returns the top element of the stack
  //Description: returns and removes the element on the top of the stack, null if none exists
  public Card Pop()
  {
    Card result = null;

    if (!IsEmpty())
    {
      result = cards[size - 1];
    }

    size--; 
    return result;
  }

  //Pre: None
  //Post: Returns the top element of the stack
  //Description: returns the element on the top of the stack, null if none exists
  public Card Top()
  {
    Card result = null;

    if (!IsEmpty())
    {
      result = cards[size - 1];
    }

    return result;
  }

  //Pre: None
  //Post: Returns the current size of the stack
  //Description: Returns the current number of elements on the stack
  public int Size()
  {
    return size;
  }

  //Pre: None
  //Post: Returns true if the size of the stack is 0, false otherwise
  //Description: Compare the size of the stack against 0 to determine its empty status
  public bool IsEmpty()
  {
    return size == 0;
  }

  //Pre: None
  //Post: Returns true if the size of the stack is 0, false otherwise
  //Description: Shuffle the cards currently exisiting in the stack, and then place them back in the stack
  public void Shuffle()
  {
    Random random = new Random();
    int rNum;
    Card t;

    // Shuffle cards and store back in stack
    for (int i = cards.Length - 1; i > 0; i--)
    {
      t = cards[i];
      rNum = random.Next(0, i + 1);
      cards[i] = cards[rNum];
      cards[rNum] = t;
    }
  }
}