using System;

namespace De.JanRoslan.WPFUtils.Controls
{



/// <summary>
    ///     ''' 
    ///     ''' </summary>
public class ChangelogEntry
    {
        public String Caption { get; set; }

        public DateTime EntryDate { get; set; }

        public String Text { get; set; }


    public ChangelogEntry()
    {
    }



    /// <summary>
        ///         ''' Creates a ChangelogEntry with the current date.
        ///         ''' </summary>
    ///         ''' <param name="caption"></param>
    ///         ''' <param name="text"></param>
    public ChangelogEntry(String caption, string text)
    {
    this.Caption = caption;
    this.Text = text;
    this.EntryDate = DateTime.Now;
    }



    /// <summary>
        ///         ''' Creates a ChangelogEntry with a set date.
        ///         ''' </summary>
    ///         ''' <param name="caption"></param>
    ///         ''' <param name="text"></param>
    public ChangelogEntry(String caption, string text, DateTime setDate)
    {
    this.Caption = caption;
    this.Text = text;
    this.EntryDate = setDate;
    }
    }
    }
    