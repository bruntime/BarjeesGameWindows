using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Barjees.Windows;
using Barjees.Common;
using Barjees.Elements;
using System.Drawing;

namespace Barjees
{
    /// <summary>
    /// Main game application class
    /// </summary>
    public sealed class GameApp
    {
        #region Properties   
        /// <summary>
        /// Flag indicates if the game is in initial state
        /// </summary>
        public bool IsInitial
        {
            get { return (State is GameInitialState); }
        }
        /// <summary>
        /// Flag indicates if the game is in running state
        /// </summary>
        public bool IsRunning
        {
            get { return (State is GameRunningState); }
        }
        /// <summary>
        /// Flag indicates if the game is in pausing state
        /// </summary>
        public bool IsPaused
        {
            get { return (State is GamePauseState); }
        }
        /// <summary>
        /// Flag indicates if the game is in game over state
        /// </summary>
        public bool IsGameOver
        {
            get { return (State is GameOverState); }
        }
        /// <summary>
        /// Current game state
        /// </summary>
        private GameState state;
        /// <summary>
        /// Get current game state
        /// </summary>
        public GameState State
        {
            get { return state; }
        }
        /// <summary>
        /// Stores the main Barjees window instance
        /// </summary>
        private BarjeesForm mainWindow = null;
        /// <summary>
        /// Returns main window instance
        /// </summary>
        public BarjeesForm MainForm
        {
            get { return mainWindow; }
        }
        /// <summary>
        /// Game objects tree
        /// </summary>
        private GameObjectTree tree;
        /// <summary>
        /// Game objects tree
        /// </summary>
        public GameObjectTree Tree
        {
            get { return tree; }
            set { tree = value; }
        }
        /// <summary>
        /// Players array
        /// </summary>
        private Player[] players;
        /// <summary>
        /// Players array
        /// </summary>
        public Player[] Players
        {
            get { return players; }
            set { players = value; }
        }
        /// <summary>
        /// Main game board
        /// </summary>
        private GameBoard board;
        /// <summary>
        /// Main game board
        /// </summary>
        public GameBoard Board
        {
            get { return board; }
            set { board = value; }
        }
        /// <summary>
        /// Current player turn
        /// </summary>
        private PlayerTurn turn;
        /// <summary>
        /// Current player turn
        /// </summary>
        public PlayerTurn Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameApp()
        {
            Turn = new PlayerTurn();
        }

        #endregion

        #region Game Loop Operations
        /// <summary>
        /// Update the game objects
        /// </summary>
        public void Update()
        {
            MainForm.UpdateForm();

            if (GameApp.Instance.IsRunning)
                Turn.Update();

        }
        /// <summary>
        /// Renders the game objects 
        /// </summary>
        public void Render(Graphics g, Rectangle bound)
        {
            if (Board.Bound != bound)
                Board.SetRect(bound);

            foreach (var v in Tree.Objects.Where(x=> x is VisualGameObject))
                ((VisualGameObject)v).Render(g);
        }
        #endregion

        #region Public State Control Operations
        /// <summary>
        /// Start the game application and show the form
        /// </summary>
        public void Start(BarjeesForm form)
        {
            mainWindow = form;

            Init();

            // Run the form
            Application.Run(mainWindow);
        }
        /// <summary>
        /// Start a new game instance
        /// </summary>
        public void StartNewGame()
        { 
            state = State.ChangeState(new GameRunningState());
            
            // GameApp.Instance.Players.First(x => x is UserPlayer).Pawns[0].Move(73);
            Logging.Logger.Log("State", null, null, null, "Start new game", state.ToString());
            // Turn = new PlayerTurn();
            //ResetGame();
            Turn.CurrentPlayer = null;
            Turn.FlipTurn();
            //Update();
        }

        /// <summary>
        /// Reset the game to default state
        /// </summary>
        private void ResetGame()
        {
            // GameApp.Instance.Tree.Objects.Clear();
            // GameApp.Instance.Init();
            GameApp.Instance.Turn.Reset();

        }
        /// <summary>
        /// Pause the current running game
        /// </summary>
        public void PauseGame()
        {
            if(IsRunning)
            {
                state = State.ChangeState(new GamePauseState());
                Update();
            }
        }
        /// <summary>
        /// Resume the game if it is paused
        /// </summary>
        public void ResumeGame()
        { 
            if(IsPaused)
            {
                state = State.ChangeState(null);
                Update();
            }
        }
        /// <summary>
        /// End current game session
        /// </summary>
        public void GameOver()
        {
            if(IsRunning)
            {
                state = State.ChangeState(new GameOverState());
                Logging.Logger.Log("State", null, null, null, "Game over", state.ToString());
                Update();
            }
        }
        /// <summary>
        /// Exit game
        /// </summary>
        public void ExitGame()
        {
            Logging.Logger.Log("Exit", null, null, null, "Exit game application", null);
            Application.Exit();
        }
        /// <summary>
        /// Display a message in the form status bar
        /// </summary>
        /// <param name="message">A string to display in the status bar</param>
        public void SetStatus(string message)
        {
            MainForm.SetStatus(message);
        }
        #endregion

        #region Private Operations
        /// <summary>
        /// Initialize the game application
        /// </summary>
        private void Init()
        {
            // Set Game State to Initial
            state = new GameInitialState();
            Tree = new GameObjectTree();            
            // Init game board
            Board = GameObject.CreateGameObject<GameBoard>(null); 
            // Init cells
            InitCells();
            InitPlayers();            
        }

        /// <summary>
        /// Create board cells
        /// </summary>
        private void InitCells()
        {
            bool isHorizontal = true;
            int gid = 1;
            Board.Cells.Clear();
            // region will change horizontal variable value every 3 cells
            int region = 1;
            for(int i = 1; i <= 96; i++)
            {
                gid = i;
                if(region > 3)
                {
                    isHorizontal = !isHorizontal;
                    region = 1;
                }
                Cell c = GameObject.CreateGameObject<Cell>(Board);
                c.GlobalID = gid;
                c.IsHorizontal = isHorizontal;
                Board.Cells.Add(gid, c);
                region++;
            }

            // Create cooking cell for north and south
            Cell northCookCell = GameObject.CreateGameObject<Cell>(Board);
            northCookCell.GlobalID = 97;
            Board.Cells.Add(97, northCookCell);
            Cell southCookCell = GameObject.CreateGameObject<Cell>(Board);
            southCookCell.GlobalID = 98;
            Board.Cells.Add(98, southCookCell);

            Board.SetBanjCells();
            Board.SetShieldCells();
            Board.UpdateCells();
        }

        /// <summary>
        /// Create game players
        /// </summary>
        private void InitPlayers()
        {
            // Init players
            players = new Player[]
            {
                GameObject.CreateGameObject<UserPlayer>(null),
                GameObject.CreateGameObject<ComputerPlayer>(null)
            };
            // Init pawns
            foreach (var p in Players)
            {
                if (p is UserPlayer)
                    p.Name = "User";
                else
                    p.Name = "Computer";

                p.CreatePawns();
            }
        }
        #endregion

        #region Static Properties
        /// <summary>
        /// Static singleton instance member
        /// </summary>
        private static GameApp instance = null;
        /// <summary>
        /// Static accessor to application singleton instance
        /// </summary>
        public static GameApp Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameApp();
                return instance;
            }
        }
        #endregion
    }
}
