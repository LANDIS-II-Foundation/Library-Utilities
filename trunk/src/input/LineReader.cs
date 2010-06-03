// Copyright 2004 University of Wisconsin 
// All rights reserved. 
// 
// The copyright holder licenses this file under the New (3-clause) BSD 
// License (the "License").  You may not use this file except in 
// compliance with the License.  A copy of the License is available at 
// 
//   http://www.opensource.org/licenses/bsd-license.php 
// 
// and is included in the NOTICE.txt file distributed with this work.
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Represents a reader of lines from a text source.
    /// </summary>
    public abstract class LineReader
    {
        private int lineNumber;
        private bool isClosed;

        private string commentLineMarker;
        private string endCommentMarker;

        /// <summary>
        /// Controls if blank lines are skipped by reader.
        /// </summary>
        public bool SkipBlankLines;

        /// <summary>
        /// Controls if comment lines are skipped by reader.
        /// </summary>
        public bool SkipCommentLines;

        /// <summary>
        /// Controls if end-of-line comments are trimmed by reader.
        /// </summary>
        public bool TrimEndComments;

        //---------------------------------------------------------------------

        /// <summary>
        /// The value of a reader's LineNumber property if the reader has no
        /// more lines left.
        /// </summary>
        public static readonly int EndOfInput = -1;

        /// <summary>
        /// Default markers for comment lines and end-of-line comments.
        /// </summary>
        public static class Default
        {
            public static readonly string CommentLineMarker = "#";
            public static readonly string EndCommentMarker  = "##";
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Name of the reader's source (for example, if the reader's source
        /// is a text file, this would be the file's name).
        /// </summary>
        public abstract string SourceName {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The line number of the line returned by the most recent call to
        /// the ReadLine method.
        /// </summary>
        public virtual int LineNumber {
            get {
                return lineNumber;
            }
        }

        //---------------------------------------------------------------------

        public string CommentLineMarker {
            get {
                return commentLineMarker;
            }
            set {
                ValidateMarker(value, "CommentLineMarker");
                commentLineMarker = value;
            }
        }

        //---------------------------------------------------------------------

        public string EndCommentMarker {
            get {
                return endCommentMarker;
            }
            set {
                ValidateMarker(value, "EndCommentMarker");
                endCommentMarker = value;
            }
        }

        //---------------------------------------------------------------------

        private void ValidateMarker(string marker,
                                    string markerName)
        {
            if (marker.Length == 0)
                throw new System.ApplicationException(
                                    markerName + " cannot be empty string.");
            if (char.IsWhiteSpace(marker, 0))
                throw new System.ApplicationException(
                                    "First character in " + markerName
                                    + " cannot be whitespace.");
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public LineReader()
        {
            this.lineNumber = 0;
            this.isClosed = false;
            this.SkipBlankLines = false;
            this.SkipCommentLines = false;
            this.TrimEndComments = false;
            this.commentLineMarker = Default.CommentLineMarker;
            this.endCommentMarker = Default.EndCommentMarker;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Get the next line from the reader's source.
        /// </summary>
        /// <returns>
        /// The next line from the source or null if there are no lines
        /// left.
        /// </returns>
        protected abstract string GetNextLine();

        //---------------------------------------------------------------------

        /// <summary>
        /// Read the next line from the reader's source.
        /// </summary>
        /// <returns>
        /// The next line or null.
        /// </returns>
        public string ReadLine()
        {
            if (isClosed)
                throw new System.InvalidOperationException();

            if (lineNumber == EndOfInput)
                return null;

            string line;
            while ((line = GetNextLine()) != null) {
                lineNumber++;

                //  Trimming must occur before testing whether to skip blank
                //  lines because trimming a EOL comment may make a line blank.
                if (TrimEndComments) {
                    int indexOfMarker = line.IndexOf(endCommentMarker);
                    if (indexOfMarker > -1)
                        line = line.Substring(0, indexOfMarker);
                }

                if (SkipBlankLines || SkipCommentLines) {
                    string trimmedLine = line.TrimStart(null);
                    if (SkipBlankLines && (trimmedLine.Length == 0))
                        continue;
                    if (SkipCommentLines &&
                            trimmedLine.StartsWith(commentLineMarker))
                        continue;
                }

                return line;
            }

            lineNumber = EndOfInput;
            return null;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Closes the LineReader and releases any system resources used by
        /// it.
        /// </summary>
        public virtual void Close()
        {
            isClosed = true;
        }
    }
}
