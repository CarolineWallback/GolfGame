using System;
using System.Collections.Generic;

namespace GolfGame
{

    class Swing
    {
        public double angle {get; set;}
        public double force {get; set;}
        public int distance {get; set;}
    }
   
    class Program
    {
        static Random random = new Random();

        static double currentLocation = 0;
        static double cupLocation;
        static double distToCup;
        static double angle;
        static double force;

        static double angleInRadians;
        static int distance;

        static float gravity = 9.8f;

        static List <Swing> allSwings = new List<Swing>();

        static int par = 0;
        

        static void Main(string[]args)
        {
            Console.WriteLine("Welcome to the Golf Tournament");
            Console.WriteLine("Enter you name and press enter to begin.");

            var player = Console.ReadLine();

            //Lots of information about game


            // "par tre"-hole is from c:a 75 - c:a 225 metres.
            // "par fyra"-hole is from c:a 250 - c:a 450 metres.
            // "par fem"-hole is from c:a 400 - c:a 610 metres.

            while (par < 3 || par > 5)
            {
                Console.WriteLine("What would you like to play; par 3, 4 or 5?");
                int.TryParse(Console.ReadLine(), out par);

                switch (par)
                {
                    case 3 :
                    cupLocation = random.Next(75,226);
                    break;

                    case 4 :
                    cupLocation = random.Next(250,451);
                    break;

                    case 5 :
                    cupLocation = random.Next(400,611);
                    break;
                }
            }
            
            
            distToCup = cupLocation - currentLocation;
            Console.WriteLine($"You chose a par {par} hole, and the distance to the hole is {distToCup}.");

            Console.ReadKey();

            while(distToCup > 0 )
            {
                if(distToCup >= cupLocation *2)
                    LostGame();
                
                Console.WriteLine("Press enter to continue");
                Console.ReadKey();
                NewSwing();

            }

            WonGame();
            
        }

        static void NewSwing()
        {
            Console.Clear();

            Swing swing = new Swing();

            Console.WriteLine($"Swing no.{allSwings.Count+1}");
            Console.WriteLine($"Distance to hole is {distToCup} metres.\n");
            Console.WriteLine("Which angle would you like to have? Between 1-90");
            double.TryParse(Console.ReadLine(), out angle);
            Console.WriteLine("How much force do you want in your swing? Between 1-90");
            double.TryParse(Console.ReadLine(), out force);

            angleInRadians = (Math.PI / 180) * angle;
            distance = Convert.ToInt32(Math.Pow(force, 2) / gravity * Math.Sin(2 * angleInRadians));

            Console.WriteLine($"At {angle} degrees and {force} m/s, the ball travels {distance} meters.");

            currentLocation += distance;
            distToCup = Math.Abs(cupLocation - currentLocation);
            Console.WriteLine($"Distance to cup is {distToCup} metres.");
            currentLocation = cupLocation - distToCup;

            swing.distance = distance;
            swing.angle = angle;
            swing.force = force;
            allSwings.Add(swing);
        }

        static void LostGame()
        {
            Console.WriteLine("You are too far away, no idea in keep going. Better luck next time.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void WonGame()
        {
            Console.Clear();

            if(allSwings.Count == 1)
            {
                Console.WriteLine("HOLE IN ONE!");
            }
            else
            {
                Console.WriteLine("Congratulations! You got the ball in the hole.");
            }

            Console.WriteLine($"\nIt took you {allSwings.Count} swings.");
            

            for(int i = 0; i < allSwings.Count; i++)
            {
                Console.WriteLine($"Round no. {i+1}. ");
                Console.WriteLine($"Angle: {allSwings[i].angle} degrees.");
                Console.WriteLine($"Force: {allSwings[i].force} m/s.");
                Console.WriteLine($"Distance: {allSwings[i].distance}");
                Console.WriteLine("--------------------");
            }

            Console.ReadKey();
        }
    }
}