using System;
using System.ComponentModel.DataAnnotations;

namespace De.JanRoslan.WPFUtils.Controls
{
    /// <summary>
    ///     ''' 
    ///     ''' </summary>
    public class PreInitGridProperty
    {
        // ' Properties
        public string DisplayName { get; }
        public string InternalName { get; }
        public DataType Type { get; }
        public object Extension { get; }

        public object Value { get; set; }

        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        ///         ''' <param name="displayName"></param>
        ///         ''' <param name="type"></param>
        ///         ''' <param name="extension"></param>
        public PreInitGridProperty(string displayName, string internalName, DataType type, Object extension,
            object defaultValue = null)
        {
            this.DisplayName = displayName;
            this.InternalName = internalName;
            this.Type = type;
            this.Extension = extension;
            Value = defaultValue;
        }


        public enum DataType
        {
            // Integer
            Int,

            // Floating-point number
            Dec,

            // Boolean
            Bool,

            // string
            Text,

            // List of strings
            List,

            // Button
            Button,

            // Mutli value table
            Table
        }
    }
}