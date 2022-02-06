// Author: Dana Kleber
// File Name: Card.cs
// Project Name: pass3v2-23
// Creation Date: Nov 15, 2020
// Modified Date: Nov. 18, 2020
//Description: An object class that implements a single playing card

using System;

class Card
{
  //Public constants to denote suit types, in order
  public const int HEARTS = 0;
  public const int SPADES = 1;
  public const int DIAMONDS = 2;
  public const int CLUBS = 3;

  //Maintain the key attributes of a card for comparison and display purposes
  private bool isVisible;
  private int rank;
  private int suit;
  private string displayRank;
  private string displaySuit;

  //A simplified listing of suit types for display purposes
  private string [] suits = new string[]{"♥","♤","♢","♣"};

  public Card(int rank, int suit)
  {
    this.rank = rank;
    this.suit = suit;
    isVisible = true;

    //Convert the card data to string form for display purposes
    SetDisplayRank(rank);
    displaySuit = suits[suit]; 
  }

  //Pre: None
  //Post: Returns the card's rank in integer format
  //Description: Retrieve the rank of the card to determine its value
  public int GetRank()
  {
    return rank;
  }

  //Pre: None
  //Post: Returns the card's visibility in boolean format
  //Description: Retrieve the visibility of the card
  public bool GetIsVisible()
  {
    return isVisible;
  }

  //Pre: None
  //Post: Returns the string representation of the card's rank
  //Description: Retrieve the rank of the card for display
  public string GetDisplayRank()
  {
    return displayRank;
  }

  //Pre: None
  //Post: Returns the card's suit in integer format
  //Description: Retrieve the suit of the card
  public int GetSuit()
  {
    return suit;
  }

  //Pre: None
  //Post: Returns the string representation of the card's suit
  //Description: Retrieve the suit of the card for display
  public string GetDisplaySuit()
  {
    return displaySuit;
  }

  //Pre: None
  //Post: Returns a formatted string of the card details
  //Description: Retrieve a formatted string of the card for display purposes
  public string DisplayText()
  {
    return displayRank + "" + displaySuit;
  }

  //Pre: rank is an integer inclusively between 1 and 13 
  //Post: Sets the cards suit to its string representation
  //Description: Determines the string representation of a card's rank
  private void SetDisplayRank(int rank)
  { 
    switch (rank)
    {
      case 1:
        displayRank = "Ace";
        break;
      case 11:
        displayRank = "Jack";
        break;
      case 12:
        displayRank = "Queen";
        break;
      case 13:
        displayRank = "King";
        break;
      default:
        
      displayRank = Convert.ToString(rank);
      break;
    }
  }
}