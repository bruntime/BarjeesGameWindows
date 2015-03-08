using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Commands
{
    /// <summary>
    /// Represents the status of command
    /// </summary>
    public enum CommandStatus
    {
        /// <summary>
        /// Command is active but not executed yet
        /// </summary>
        Active,
        /// <summary>
        /// Command is running
        /// </summary>
        Running,
        /// <summary>
        /// Command has completed execution
        /// </summary>
        Completed
    }
    /// <summary>
    /// Base class for all action commands that player can do
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Command context
        /// </summary>
        private CommandContext context;
        /// <summary>
        /// Command context
        /// </summary>
        public CommandContext Context
        {
            get { return context; }            
        }

        /// <summary>
        /// Command name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the command status
        /// </summary>
        private CommandStatus status;
        /// <summary>
        /// Gets or sets the command status
        /// </summary>
        public CommandStatus Status
        {
            get { return status; }
            set 
            { 
                status = value;
                if(status == CommandStatus.Completed)
                    CommandExecuted(this, new EventArgs());
            }
        }
        /// <summary>
        /// Returns true if command is still active
        /// </summary>
        public bool IsActive
        {
            get { return Status == CommandStatus.Active; }
        }
        /// <summary>
        /// Returns true if command is still running
        /// </summary>
        public bool IsRunning
        {
            get { return Status == CommandStatus.Running; }
        }
        /// <summary>
        /// Returns true when command completed
        /// </summary>
        public bool IsCompleted
        {
            get { return Status == CommandStatus.Completed; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Command(CommandContext context)
        {
            this.context = context;
            CommandExecuted += OnCommandExecuted;
            Status = CommandStatus.Active;

        }

        /// <summary>
        /// Executes when command is completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnCommandExecuted(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Execute command
        /// </summary>
        public virtual void Execute()
        { }

        /// <summary>
        /// Fires when command is completed
        /// </summary>
        public event EventHandler<EventArgs> CommandExecuted;
    }
}
