using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendControls.FilterCommand
{
    public class FilterCommandEventArgs : EventArgs
    {
        // Fields
        private string filterExpression;

        // Methods
        public FilterCommandEventArgs()
        {
        }

        public FilterCommandEventArgs(string filterExpression)
        {
            this.filterExpression = filterExpression;
        }

        // Properties
        public string FilterExpression
        {
            get
            {
                return this.filterExpression;
            }
            set
            {
                this.filterExpression = value;
            }
        }        
    }
}
