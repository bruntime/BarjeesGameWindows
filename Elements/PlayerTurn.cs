using Barjees.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// Controll the player turn 
    /// </summary>
    public class PlayerTurn : INotifyPropertyChanged
    {
        /// <summary>
        /// Random number generator
        /// </summary>
        private System.Random rand = null;
        /// <summary>
        /// Gets or sets the current player in the turn
        /// </summary>
        private Player currentPlayer;
        /// <summary>
        /// Gets or sets the current player in the turn
        /// </summary>
        public Player CurrentPlayer 
        {
            get { return currentPlayer; }
            set
            {
                currentPlayer = value;
                if (GameApp.Instance.IsRunning)
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentPlayer"));
            }
        }
        /// <summary>
        /// Stack of commands
        /// </summary>
        private Stack<Command> CommandsStack;
        /// <summary>
        /// Command context of the current turn
        /// </summary>
        private CommandContext context;
        /// <summary>
        /// Command context of the current turn
        /// </summary>
        public CommandContext Context
        {
            get { return context; }
        }

        /// <summary>
        /// Requires the player to select pawn 
        /// </summary>
        public bool RequireSelectPawn { get; set; }
        /// <summary>
        /// Requires the player to select cell
        /// </summary>
        public bool RequireSelectCell { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerTurn()
        {
            CommandsStack = new Stack<Command>();
            PropertyChanged += OnPropertyChanged;
            RequireSelectPawn = false;
            RequireSelectCell = false;
        }

        /// <summary>
        /// Handle property changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CurrentPlayer")
            {
                CommandsStack.Clear();
            }
        }

        /// <summary>
        /// Updates the player turn
        /// </summary>
        private void HandleTurn()
        {
            if (CurrentPlayer == null)
                return;

            RequireSelectPawn = false;
            RequireSelectCell = false;
            UpdateLabel();
            
            GameApp.Instance.MainForm.UpdateGUI();

            // initialize commands stack and context            
            context = new CommandContext()
            {
                Player = CurrentPlayer
            };

            InvokePlayCommand();
        }

        /// <summary>
        /// Update the state of the turn
        /// </summary>
        public void Update()
        {
            if(GameApp.Instance.IsRunning)
            {
                if (CommandsStack.Count > 0)
                {
                    Command cmd = CommandsStack.Peek();
                    if (cmd.IsCompleted)
                    {
                        CommandsStack.Pop();
                        Update();
                    }
                    else 
                    {
                        cmd.Execute();
                    }
                }
                else
                {

                    FlipTurn();
                }
            }
        }

        /// <summary>
        /// Reset the turn state
        /// </summary>
        public void Reset()
        {
            foreach(var pl in GameApp.Instance.Players)
            {
                foreach (var p in pl.Pawns)
                    p.Kill();
            }
            CurrentPlayer = null;
            context = null;
            RequireSelectCell = false;
            RequireSelectPawn = false;
            CommandsStack.Clear();
        }

        /// <summary>
        /// Update the current player label on form
        /// </summary>
        private void UpdateLabel()
        {
            if (CurrentPlayer != null)
            {
                GameApp.Instance.SetStatus("Current player: " + CurrentPlayer.Name);
                Logging.Logger.Log("New Turn", CurrentPlayer.Name, null, null, "Change user turn", null);
            }
            else
            {
                GameApp.Instance.SetStatus("Current player: None");
            }
        }

        /// <summary>
        /// Flip the turn
        /// </summary>
        public void FlipTurn()
        {
            if (GameApp.Instance.IsRunning)
            {
                if(CurrentPlayer == null)
                {
                    if (rand == null)
                        rand = new Random();

                    int value = rand.Next(0, 2);
                    CurrentPlayer = GameApp.Instance.Players.First(x => x.ID == GameApp.Instance.Players[value].ID);
                    HandleTurn();
                    //System.Windows.Forms.MessageBox.Show("Next");
                }
                else
                {
                    if (CurrentPlayer.Pawns.Count(x => x.IsAchieved) == 4)
                    {
                        GameApp.Instance.GameOver();
                        
                    }
                    else
                    {
                        CurrentPlayer = GameApp.Instance.Players.First(x => x.ID != CurrentPlayer.ID);
                        HandleTurn();
                    }
                    //System.Windows.Forms.MessageBox.Show("Next");
                }
            }
            else
                CurrentPlayer = null;

            GameApp.Instance.Update();
        }

        /// <summary>
        /// Add a roll command to the stack of commands
        /// </summary>
        public void InvokeRollCommand()
        {
            RollCommand cmd = new RollCommand(Context);
            CommandsStack.Push(cmd);
            //Update();
        }

        /// <summary>
        /// Add a play command to the stack of commands
        /// </summary>
        public void InvokePlayCommand()
        {
            PlayCommand cmd = new PlayCommand(Context);
            CommandsStack.Push(cmd);
            //Update();
        }

        /// <summary>
        /// Add select pawn command to the stack
        /// </summary>
        public void InvokeSelectPawnCommand()
        {
            SelectPawnCommand cmd = new SelectPawnCommand(Context);
            CommandsStack.Push(cmd);
        }
        /// <summary>
        /// Add select cell command to the stack
        /// </summary>
        public void InvokeSelectCellCommand()
        {
            SelectCellCommand cmd = new SelectCellCommand(Context);
            CommandsStack.Push(cmd);
        }

        /// <summary>
        /// Fires when property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
