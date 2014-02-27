using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace GuessANumber
{
    class GuessingGame
    {
        int randomNumber;
        int tries;
        LinkedList<int> previousGuesses;
        Random rng;
        int lowerBound;
        int higherBound;
        public GuessingGame()
        {
            lowerBound = 1;
            higherBound = 100;
            rng = new Random(5); //5 used as a random seed to get constant results for testing
            randomNumber = rng.Next(lowerBound, higherBound);
            tries = 0;
            previousGuesses = new LinkedList<int>();
            guessLoop();
        }

        private void cycleList(int guesses) //this method adds to the end of a list and removes from the start
        {          
            previousGuesses.AddLast(guesses); //replace previousGuesses with lists' name
            if (previousGuesses.Count >= 5) //replace 5 with the upper limit on objects in the list
                previousGuesses.RemoveFirst();
        }

        private void guessLoop()
        {
            int guess = 0;
            while (guess != randomNumber)
            {
                tries++;
                Console.Out.WriteLine("Guess a number between " + lowerBound + " and " + higherBound + "!");
                string userInput = Console.ReadLine();
                if (Int32.TryParse(userInput, out guess))
                {
                    if (guess == randomNumber)
                    {
                        success();
                        previousTries();
                        Console.ReadLine();
                    }
                    else
                        fail();
                }
                else
                {
                    userInput.ToLower();
                    switch (userInput)
                    {
                        case "?":
                        case "help":
                            displayHelp();
                            break;
                        case "numbers":
                            changeNumbers();
                            break;
                        default:
                            displayError();
                            break;
                    }
                }
            }
        }


        private void changeNumbers()
        {
            Console.WriteLine("Ok, we'll change the number range! Enter the lower number");
            int firstNumber;
            if (Int32.TryParse(Console.ReadLine(), out firstNumber))
            {
                Console.WriteLine("And now the higher number");
                int secondNumber;
                if (Int32.TryParse(Console.ReadLine(), out secondNumber))
                {
                    if (secondNumber > firstNumber)
                    {
                        randomNumber = rng.Next(firstNumber, secondNumber);
                        lowerBound = firstNumber;
                        higherBound = secondNumber;
                    }
                    else
                    {
                        Console.WriteLine("The lower number is higher (or equal) than the higher number. You can't explain that!");
                    }
                }
                else
                {
                    displayError();
                }
            }
            else
            {
                displayError();
            }
        }

        private void displayHelp()
        {
            Console.WriteLine(" -Type ? or help for help\n -Type numbers to change the range of numbers");
        }

        private void displayError()
        {
            Console.WriteLine("Something went wrong! Type ? for help or try to guess again");
        }

        private void success()
        {
            Console.Out.WriteLine("Congratulations, you correctly guessed the number " + randomNumber + "!");
            Console.Out.WriteLine("And it only took you " + tries + " tries!");
        }
        private void fail()
        {
            Console.Out.WriteLine("Bad luck! That was the wrong number");
        }
        private void previousTries()
        {
            if (previousGuesses.Count != 0)
            {
                Console.WriteLine("Your previous guesses include: ");
                foreach (int guess in previousGuesses)
                {
                    Console.Out.WriteLine("\t" + guess);
                }
            }
        }
    }
}
