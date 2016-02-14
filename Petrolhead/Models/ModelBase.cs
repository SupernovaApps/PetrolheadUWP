using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Petrolhead.Models
{
    /// <summary>
    /// Abstract base class for all Petrolhead models
    /// </summary>
    public abstract class ModelBase : Template10.Mvvm.BindableBase
    {
        
        /// <summary>
        /// Calls the ResetID() function to generate a new GUID.
        /// </summary>
        public ModelBase()
        {
            
            CreationDate = DateTime.Today;
            this.PropertyChanged += ModelBase_PropertyChanged;
        }

        /// <summary>
        /// Updates the ModifiedDate property.
        /// </summary>
        /// <param name="sender">The sender of the PropertyChanged event</param>
        /// <param name="e">The instance of PropertyChangedEventArgs for this event</param>
        protected void ModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ModifiedDate = DateTime.Now;
        }

       
       

        private string _Name = default(string);
        /// <summary>
        /// The human-readable name of a data model.
        /// </summary>
        public string Name { get { return _Name; } set { Set(ref _Name, value); } }

        
        private DateTimeOffset _CreationDate = default(DateTimeOffset);
        /// <summary>
        /// The date a data model was created.
        /// </summary>
        public DateTimeOffset CreationDate { get { return _CreationDate; } protected set { Set(ref _CreationDate, value); } }


        private DateTimeOffset _ModifiedDate = default(DateTimeOffset);
        /// <summary>
        /// The date a data model was last modified.
        /// </summary>
        public DateTimeOffset ModifiedDate { get { return _ModifiedDate; } protected set { Set(ref _ModifiedDate, value); } }

        /// <summary>
        /// The description of a data model.
        /// </summary>
        private string _Description = default(string);
        public string Description { get { return _Description; } set { Set(ref _Description, value); } }
        /// <summary>
        /// Requests that a particular data model be updated.
        /// </summary>
       
    }
}
