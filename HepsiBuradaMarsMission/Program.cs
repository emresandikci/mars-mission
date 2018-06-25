using HepsiBuradaMarsMission.Helpers;
using System;

namespace HepsiBuradaMarsMission
{
    class Program
    {
        #region Fields
        private static string _Answer { get; set; }
        private static string _PlateauSize { get; set; }
        private static string _Coordinate { get; set; }
        private static string _Instructions { get; set; }
        #endregion

        static void Main(string[] args)
        {
            _Answer = "y";
            MarsRover marsRover = new MarsRover();
            Console.WriteLine("\tMars Misson\n");
            Console.WriteLine(" Before start of MARS mission, you have to create new mission...\n Please assign information for MARS mission.\n\n");

            do
            {
                Console.Write("\nPlateau Size:");
                _PlateauSize = Console.ReadLine();
                if (string.IsNullOrEmpty(_PlateauSize))
                {
                    Console.WriteLine("\nThe rover's plateau size is required!");
                    continue;
                }
                marsRover.SetPlateauSize(_PlateauSize);

                do
                {
                    Console.Write("Coordinate:");
                    _Coordinate = Console.ReadLine().ToUpper();
                    if (string.IsNullOrEmpty(_Coordinate))
                    {
                        Console.WriteLine("\nThe rover's cordinate is required!");
                        continue;
                    }

                    marsRover.SetCoordinate(_Coordinate);
                    Console.Write("Instructions:");
                    _Instructions = Console.ReadLine().ToUpper();
                    if (string.IsNullOrEmpty(_Instructions))
                    {
                        Console.WriteLine("\nThe rover's instruction is required!");
                        continue;
                    }
                    marsRover.SetInstruction(_Instructions);
                    Console.Write("\nWould like to add new mission (y/n):");
                    _Answer = Console.ReadLine();
                } while (_Answer.ToString().ToLower() == "y");

                marsRover.SendMission();
                Console.Write("\nWould like to keep going to Mars Mission (y/n):");
                _Answer = Console.ReadLine();
            } while (_Answer.ToString().ToLower() == "y");
        }
    }
}
