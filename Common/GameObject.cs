using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Common
{
    /// <summary>
    /// Base game object
    /// </summary>
    public abstract class GameObject : INotifyPropertyChanged
    {
        #region Properties
        /// <summary>
        /// Object unique identifier
        /// </summary>
        private int id;
        /// <summary>
        /// Game object unique identifier
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Parent object field
        /// </summary>
        private GameObject parent;

        /// <summary>
        /// Parent object property
        /// </summary>
        public GameObject Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("Parent");
                PropertyChanged(this, e);
            }
        } 
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameObject()
        {
            PropertyChanged += OnPropertyChanged;
        } 
        #endregion

        #region Protected Operations
        /// <summary>
        /// Fires when a property has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        } 
        #endregion

        #region Public Opertations
        /// <summary>
        /// Update the game object state
        /// </summary>
        public virtual void Update()
        { } 
        #endregion

        #region Events
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

        #region Static Properties
        /// <summary>
        /// Static sequence ID generator
        /// </summary>
        private static int IDSequence = 1; 
        #endregion

        #region Static Operations
		/// <summary>
        /// Static game object factory method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T CreateGameObject<T>(GameObject parent) where T : GameObject, new()
        {
            T obj = new T();
            obj.ID = IDSequence;
            obj.Parent = parent;
            GameApp.Instance.Tree.Objects.Add(obj);
            IDSequence++;
            return obj;
        }
 
	    #endregion    
    }
}
