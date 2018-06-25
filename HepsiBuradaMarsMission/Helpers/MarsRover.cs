using System;
using System.Collections.Generic;

namespace HepsiBuradaMarsMission.Helpers
{
    #region  MarsRover Manager
    /// <summary>
    /// All application logic is executing inside this class
    /// </summary>
    public class MarsRover
    {
        #region Fields
        private Rover _rover;
        private List<Mission> _missionList;
        private Mission _mission;

        public MarsRover()
        {
            this._rover = new Rover();
            this._missionList = new List<Mission>();
            this._mission = new Mission();
        }
        #endregion

        #region Methods
        /// <summary>
        /// To assign plateau's size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public MarsRover SetPlateauSize(string size)
        {
            this._mission.PlateauSize = size;
            return this;
        }

        /// <summary>
        /// To assign Rover's coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public MarsRover SetCoordinate(string coordinate)
        {
            this._mission.Coordinate = coordinate;
            return this;
        }

        /// <summary>
        /// To assign Rover's instruction
        /// </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public MarsRover SetInstruction(string inst)
        {
            this._mission.Instruction = inst;
            _missionList.Add(new Mission
            {
                Coordinate = this._mission.Coordinate,
                Instruction = this._mission.Instruction,
                PlateauSize = this._mission.PlateauSize
            });
            return this;
        }

        /// <summary>
        /// To send Rover's mission(s)
        /// </summary>
        /// <returns></returns>
        public MarsRover SendMission()
        {
            var results = this.StartMission();
            Console.WriteLine("Mission Completed");

            foreach (var item in results)
            {
                Console.WriteLine($"\nresult : {item}");
            }
            _missionList.Clear();
            return this;
        }

        /// <summary>
        /// To start Rover's mission(s)
        /// </summary>
        /// <returns>List of the Rover misson(s) results</returns>
        private List<string> StartMission()
        {
            List<string> results = new List<string>();
            foreach (var item in _missionList)
            {
                _rover.AssignRoverMission(item);
                _rover.StartRoverMarsMission();
                results.Add(_rover.GetLastLocationInformation());
            }
            return results;
        }
        #endregion
    }
    #endregion

    #region Rover Manager
    /// <summary>
    /// To manage All of Rover's functions
    /// </summary>
    public class Rover
    {
        private int X { get; set; }
        private int Y { get; set; }
        private Tools.Compass Direction { get; set; }
        private char[] Instruction { get; set; }

        /// <summary>
        /// This provide to see last location of Rover when the Rover finish mission(s)
        /// </summary>
        /// <returns>Rover's last location result</returns>
        public string GetLastLocationInformation()
        {
            return $"{this.X} {this.Y} {((char)this.Direction).ToString()}";
        }

        /// <summary>
        /// To assign Rover's current mission
        /// </summary>
        /// <param name="_mission"></param>
        public void AssignRoverMission(Mission _mission)
        {
            string[] coordinate = _mission.Coordinate.Split(' ');
            char[] instraction = _mission.Instruction.ToCharArray();

            this.X = Convert.ToInt32(coordinate[0].ToString());
            this.Y = Convert.ToInt32(coordinate[1].ToString());
            this.Instruction = instraction;
            this.Direction = (Tools.Compass)Convert.ToChar(coordinate[2]);
        }

        /// <summary>
        /// To start Rover's current mission 
        /// </summary>
        public void StartRoverMarsMission()
        {
            for (int i = 0; i < this.Instruction.Length; i++)
            {
                switch ((Tools.LFM)this.Instruction[i])
                {
                    case Tools.LFM.LEFT:
                        this.TurnL();
                        break;
                    case Tools.LFM.RIGHT:
                        TurnR();
                        break;
                    case Tools.LFM.MOVE:
                        this.Move();
                        break;
                }
            }
        }

        /// <summary>
        /// To manage of the Rover direction to turn left
        /// </summary>
        private void TurnL()
        {
            switch (this.Direction)
            {
                case Tools.Compass.North:
                    this.Direction = Tools.Compass.West;
                    break;
                case Tools.Compass.South:
                    this.Direction = Tools.Compass.East;
                    break;
                case Tools.Compass.East:
                    this.Direction = Tools.Compass.North;
                    break;
                case Tools.Compass.West:
                    this.Direction = Tools.Compass.South;
                    break;
                default: break;
            }
        }

        /// <summary>
        /// To manage of the Rover direction to turn right
        /// </summary>
        private void TurnR()
        {
            switch (this.Direction)
            {
                case Tools.Compass.North:
                    this.Direction = Tools.Compass.East;
                    break;
                case Tools.Compass.South:
                    this.Direction = Tools.Compass.West;
                    break;
                case Tools.Compass.East:
                    this.Direction = Tools.Compass.South;
                    break;
                case Tools.Compass.West:
                    this.Direction = Tools.Compass.North;
                    break;
                default: break;
            }
        }

        /// <summary>
        /// Provide to move the Rover
        /// </summary>
        private void Move()
        {
            switch (this.Direction)
            {
                case Tools.Compass.North:
                    this.Y++;
                    break;
                case Tools.Compass.South:
                    this.Y--;
                    break;
                case Tools.Compass.East:
                    this.X++;
                    break;
                case Tools.Compass.West:
                    this.X--;
                    break;
                default: break;
            }
        }
    }
    #endregion

    public class Mission
    {
        public string PlateauSize { get; set; }
        public string Coordinate { get; set; }
        public string Instruction { get; set; }
    }
}

#region emresandikci
//This application developed for hepsiburada.com by | me@emresandikci.com |
#endregion