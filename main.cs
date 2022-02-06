// Author: Dana Kleber
// File Name: main.cs
// Project Name: pass3v2-23
// Creation Date: Nov. 12, 2020
// Modified Date: Nov. 18, 2020
// Description: This program is built to play the card game war with 2 players

using System;
using System.Collections.Generic;

class MainClass
 {
   // Store the cards being displayed on screen as a list
   static List <Card> playAreaP1 = new List <Card>();
   static List <Card> playAreaP2 = new List <Card>();

   // Store the piles of all player cards
   static Pile pileDiscardP1 = new Pile();
   static Pile pileDiscardP2 = new Pile();
   static Pile pileP1 = new Pile();
   static Pile pileP2 = new Pile();

   // Store variables to continue or start game
   static bool isContinued;
   static int playAgain;
   static bool startGame = true;
   static int tryNum = 0;
   static int count2 = 1;
   
   public static void Main (string[] args) 
   {
     // Create a new main pile of cards
     Pile pileMain = new Pile();
     
     while (startGame == true || playAgain == 1)
     {
       // Clear console for readibility
       Console.Clear();

       // Empty all piles if they are not empty
       while(!pileDiscardP1.IsEmpty())
       {
         pileDiscardP1.Pop();
       }
       while(!pileDiscardP2.IsEmpty())
       {
         pileDiscardP2.Pop();
       }
       while(!pileP2.IsEmpty())
       {
         pileP2.Pop();
       }
       while(!pileP1.IsEmpty())
       {
         pileP1.Pop();
       }
       
       // Add 52 playing cards to the main deck
       for (int suit = 0; suit < 4; suit++)
       {
         for (int rank = 1; rank <= 13; rank++)
         {
           pileMain.Push(new Card(rank, suit));
         }
       }
       
       // Shuffle the main card pile
       pileMain.Shuffle();
       
       // Transfer main cards to players
       while(!pileMain.IsEmpty())
       {
         pileP2.Push(pileMain.Pop());
         pileP1.Push(pileMain.Pop());
       }
       
       // Keep the game going until both of one of players piles is empty
       while (!((pileP1.Size() <=0) && (pileDiscardP1.Size() <=0)) && !((pileP2.Size() <= 0) && (pileDiscardP2.Size() <= 0)))
       {
         // Clear console and display stats
         Console.Clear();
         DisplayHeader();
         DisplayPlayAreas();
         
         // Ask user to flip
         isContinued = false;
         AskUserFlip();
         
         // Transfer discard cards to player pile if one of their piles are empty
         if (pileP2.Size() <= 0)
         {
           while (!pileDiscardP2.IsEmpty())
           {
             pileP2.Push(pileDiscardP2.Pop());
           }
         }
         if (pileP1.Size() <= 0)
         { 
           while (!pileDiscardP1.IsEmpty())
           {
             pileP1.Push(pileDiscardP1.Pop());
           }
         }
         
         // Add the last pile card to the play area
         playAreaP1.Add(pileP1.Pop());
         playAreaP2.Add(pileP2.Pop());
         
         // Clear console and display stats
         Console.Clear();
         DisplayHeader();
         DisplayPlayAreas();
         
         //Inform player 1 they won if their card is higher in rank
         if (playAreaP1[playAreaP1.Count - 1].GetRank() > playAreaP2[playAreaP2.Count - 1].GetRank())
         {
           Console.ForegroundColor = ConsoleColor.Green;
           Console.WriteLine("Player 1 wins the flip!");
           tryNum++;
           count2 = 1;
           
           // Add player cards to discard player 1 pile
           while(tryNum != 0)
           {
             pileDiscardP1.Push(playAreaP1[playAreaP1.Count - count2]);
             pileDiscardP1.Push(playAreaP2[playAreaP2.Count - count2]);
             tryNum--;
             count2++;
           }
         }
      
         //Inform player 2 they won if their card is higher in rank
         if (playAreaP1[playAreaP1.Count - 1].GetRank() < playAreaP2[playAreaP2.Count - 1].GetRank())
         {
           Console.ForegroundColor = ConsoleColor.Green;
           Console.WriteLine("Player 2 wins the flip!");
           tryNum++;
           count2 = 1;

           // Add player cards to discard player 2 pile
           while(tryNum != 0)
           {
             pileDiscardP2.Push(playAreaP1[playAreaP1.Count - count2]);
             pileDiscardP2.Push(playAreaP2[playAreaP2.Count - count2]);
             
             tryNum--;
             count2++;
           }
         }
         
         // Inform there is a war if both cards are the same in rank
         if (playAreaP1[playAreaP1.Count - 1].GetRank() == playAreaP2[playAreaP2.Count - 1].GetRank())
         {
           Console.ForegroundColor = ConsoleColor.Red;
           Console.WriteLine("War!");
           
           // Inform player they cannot win the war if they do not have enough cards to play a war 
           if (pileP1.Size() < 3 && pileDiscardP1.IsEmpty())
           {
             Console.WriteLine("Not enough cards to win a war.");
             while (!pileP1.IsEmpty())
             {
               pileDiscardP2.Push(pileP1.Pop());   
             }       
           }
           else if (pileP2.Size() < 3 && pileDiscardP2.IsEmpty())
           {
             Console.WriteLine("Not enough cards to win a war.");
             while(!pileP2.IsEmpty())
             {
               pileDiscardP1.Push(pileP2.Pop());
             }
           }
           
           else
           {
             // If player ran out of cards during the war, transfe discard cards into their pile 
             if (pileP1.Size() == 0)
             { 
               while (!pileDiscardP1.IsEmpty())
               {
                 pileP1.Push(pileDiscardP1.Pop());
               }
             }
             if (pileP2.Size() == 0)
             {
               while (!pileDiscardP2.IsEmpty())
               {
                 pileP2.Push(pileDiscardP2.Pop());
               }
             }
             
             playAreaP1.Add(pileP1.Pop());
             playAreaP2.Add(pileP2.Pop());
             tryNum+=2;
           }
        }
        // Ask user to continue the game
        startGame = false;
        AskUserContinue();
     }
     // Display winner based on which player's piles are empty
     if (pileP1.Size() <= 0 && pileDiscardP1.Size() <= 0)
     {
       Console.WriteLine("Player 2 wins!");
     }
     if (pileP2.Size() <= 0 && pileDiscardP2.Size() <= 0)
     {
       Console.WriteLine("Player 1 wins!");
     }
     // Ask user to play again
     AskUserAgain();
   }
 }
 
 //Pre: None
 //Post: None
 //Description: Display the cards needed in the play area
 static public void DisplayPlayAreas()
 {
   // Output that this line is for player 1's cards
   Console.ForegroundColor = ConsoleColor.White;
   Console.Write("\nPlayer 1: ");
   
   // Display player areas if game is starting or continuing
   if (playAreaP1.Count == 0 || isContinued == true)
   {
     // Display the player area card based on if there's a war or not
     if (tryNum != 0)
     {
       int c = playAreaP1[playAreaP1.Count - 2].GetSuit();
       // Display colour of card based on suit
       if (c == 0 || c == 2)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write(playAreaP1[playAreaP1.Count - 2].DisplayText());
          Console.ForegroundColor = ConsoleColor.White;
          Console.WriteLine(" ** ");
        }
        if (c == 1 || c == 3)
        {
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write(playAreaP1[playAreaP1.Count - 2].DisplayText());
          Console.ForegroundColor = ConsoleColor.White;
          Console.WriteLine(" ** ");
        }  
      }
      else
      {
        Console.WriteLine("");
      }
    }
    else
    {
      if (tryNum != 0)
      {
        int c3 = playAreaP1[playAreaP1.Count - 3].GetSuit();
        int c2 = playAreaP1[playAreaP1.Count - 1].GetSuit();
        
        if (c3 == 0 || c3 == 2)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write(playAreaP1[playAreaP1.Count - 3].DisplayText());
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(" ** ");
          
          if (c2 == 0 || c2 == 2)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(playAreaP1[playAreaP1.Count - 1].DisplayText());
          }
          else if (c2 == 1 || c2 == 3)
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(playAreaP1[playAreaP1.Count - 1].DisplayText());
          }
        } 
        else if (c3 == 1 || c3 == 3)
        {
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write(playAreaP1[playAreaP1.Count - 3].DisplayText());
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(" ** ");

          if (c2 == 0 || c2 == 2)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(playAreaP1[playAreaP1.Count - 1].DisplayText());
          }
          else if(c3 == 1 || c3 == 3)
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(playAreaP1[playAreaP1.Count - 1].DisplayText());
          }
        }
      }
      else
      {
        int c4 = playAreaP1[playAreaP1.Count - 1].GetSuit();
        if (c4 == 0 || c4 == 2)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine(playAreaP1[playAreaP1.Count - 1].DisplayText());
        }
        else if (c4 == 1 || c4 == 3)
        {
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.WriteLine(playAreaP1[playAreaP1.Count - 1].DisplayText());
        }
      }
    }

    // Output that this line is for player 2's cards
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("Player 2: ");
    // Display player areas if game is starting or continuing
    if (playAreaP2.Count == 0 || isContinued == true)
    {
      // Display the player area card based on if there's a war or not
      if (tryNum != 0)
      {
        // Display colour of card based on suit
        int c5 = playAreaP2[playAreaP2.Count - 2].GetSuit();
        if (c5 == 0 || c5 == 2)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write(playAreaP2[playAreaP2.Count - 2].DisplayText());
          Console.ForegroundColor = ConsoleColor.White;
          Console.WriteLine(" ** ");
        }
        if (c5 == 1 || c5 == 3)
        {
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write(playAreaP2[playAreaP2.Count - 2].DisplayText());
          Console.ForegroundColor = ConsoleColor.White;
          Console.WriteLine(" ** ");
        }
      }
      else 
      {
        Console.WriteLine("");
      }
    }
    else
    {
      if (tryNum != 0)
      {
        int c6 = playAreaP2[playAreaP2.Count - 3].GetSuit();
        int c7 = playAreaP2[playAreaP2.Count - 1].GetSuit();

        if (c6 == 0 || c6 == 2)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write(playAreaP2[playAreaP2.Count - 3].DisplayText());
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(" ** ");
          if (c7 == 0 || c7 == 2)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(playAreaP2[playAreaP2.Count - 1].DisplayText());
          }
          else if (c7 == 1 || c7 == 3)
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(playAreaP2[playAreaP2.Count - 1].DisplayText());
          }
        }
        else if (c6 == 1 || c6 == 3)
        {
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write(playAreaP2[playAreaP2.Count - 3].DisplayText());
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(" ** ");
          if (c7 == 0 || c7 == 2)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(playAreaP2[playAreaP2.Count - 1].DisplayText());
          }
          if (c7 == 1 || c7 == 4)
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(playAreaP2[playAreaP2.Count - 1].DisplayText());
          }
        }
      }
      else
      {
        int c8 = playAreaP2[playAreaP2.Count - 1].GetSuit();
        if (c8 == 0 || c8 == 2)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine(playAreaP2[playAreaP2.Count - 1].DisplayText());
        }
        else if (c8 == 1 || c8 == 3)
        {
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.WriteLine(playAreaP2[playAreaP2.Count - 1].DisplayText());
        }
      }
    }
  }
  
  //Pre: None
  //Post: None
  //Description: Ask the user to flip the next card
  static public void AskUserFlip()
  {
    Console.WriteLine("Press enter to flip the next card.");
    Console.ReadLine();
  }

  //Pre: None
  //Post: None
  //Description: Ask the user to continue
  static public void AskUserContinue()
  {
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Press enter to continue.");
    Console.ReadLine();
    isContinued = true;
  }
  
  //Pre: None
  //Post: None
  //Description: Ask the user to play again
  static public void AskUserAgain()
  {
    try
    {
    Console.WriteLine("Would you like to play again? Press 1 for yes and 2 for no.");
    playAgain = Convert.ToInt32(Console.ReadLine());
    }
    catch (FormatException fe)
    {
      Console.WriteLine(fe.Message);
    }
  }
 
  //Pre: None
  //Post: None
  //Description: Display the player pile sizes
  static public void DisplayHeader()
  {
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write("P1-Library: " + pileP1.Size() + "\t P1-Discard: " + pileDiscardP1.Size());
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(" || "); 
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("P2-Library: " + pileP2.Size() + "\tP2-Discard: " + pileDiscardP2.Size());
  }
}
