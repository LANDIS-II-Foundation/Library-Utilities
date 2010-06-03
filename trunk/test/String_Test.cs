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

using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class String_Test
    {
        private void PrintException(StringReader reader)
        {
            try {
                int index;
                InputValue<string> val = String.Read(reader, out index);
            }
            catch (InputValueException exc) {
                Data.Output.WriteLine(exc.Message);
                throw exc;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Read_EmptyString()
        {
            StringReader reader = new StringReader("");
            PrintException(reader);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Read_Whitespace()
        {
            StringReader reader = new StringReader("\t \n\r");
            PrintException(reader);
        }

        //---------------------------------------------------------------------

        private void CheckReadResults(string readerInitVal,
                                      string expectedReadResult,
                                      string readerValAfterRead)
        {
            StringReader reader = new StringReader(readerInitVal);
            int index;
            InputValue<string> val = String.Read(reader, out index);
            Assert.AreEqual(expectedReadResult, val.Actual);
            Assert.AreEqual(readerValAfterRead, reader.ReadToEnd());
        }

        //---------------------------------------------------------------------

        private void CheckReadResults(string readerInitVal,
                                      string expectedReadResult)
        {
            StringReader reader = new StringReader(readerInitVal);
            int index;
            InputValue<string> val = String.Read(reader, out index);
            Assert.AreEqual(expectedReadResult, val.Actual);
            Assert.AreEqual(-1, reader.Peek());
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_JustWord()
        {
            CheckReadResults("ABCs",
                             "ABCs");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_WhitespaceWord()
        {
            CheckReadResults("   \t 987",
                             "987");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_WordWhitespace()
        {
            CheckReadResults("hello\n",
                             "hello",
                             "\n");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_WhitespaceWordWhitespace()
        {
            CheckReadResults("\r \t hello\n",
                             "hello",
                             "\n");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_MultipleWords()
        {
            string[] words = new string[] { "987.01", ".'.", "x-y*z^2", @"C:\some\Path\to\a\file.ext" };
            StringReader reader = new StringReader(string.Join(" ", words));
            foreach (string word in words) {
                int index;
                InputValue<string> str = String.Read(reader, out index);
                Assert.AreEqual(word, str.Actual);
                Assert.AreEqual(index + word.Length, reader.Index);
            }
            Assert.AreEqual(-1, reader.Peek());
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Read_DoubleQuote_NoEnd()
        {
            StringReader reader = new StringReader("\"");
            PrintException(reader);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Read_DoubleQuote_TextNoEnd()
        {
            StringReader reader = new StringReader("\"Four score and ");
            PrintException(reader);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_Empty()
        {
            CheckReadResults("\"\"",
                             "");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_EmptyWhitespace()
        {
            CheckReadResults("\"\"\n ",
                             "",
                             "\n ");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_WhitespaceEmpty()
        {
            CheckReadResults(" \t  \"\"",
                             "");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_WhitespaceEmptyWhitespace()
        {
            CheckReadResults(" \t  \"\"\r ",
                            "",
                            "\r ");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_Str()
        {
            CheckReadResults("\"Hello world!\"",
                             "Hello world!");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_WhitespaceStr()
        {
            CheckReadResults(" \t \"Hello world!\"",
                             "Hello world!");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_StrEscape()
        {
            CheckReadResults(" \t \"It went \\\"Boom!\\\"\" ",
                             "It went \"Boom!\"",
                             " ");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_StrEscapeOtherQuote()
        {
            CheckReadResults(" \t \"It went \\\'Boom!\\\'\" ",
                             "It went 'Boom!'",
                             " ");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_DoubleQuotes_StrOtherQuote()
        {
            CheckReadResults(" \t \"It went 'Boom!'\" ",
                             "It went 'Boom!'",
                             " ");
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Read_SingleQuote_NoEnd()
        {
            StringReader reader = new StringReader("'");
            PrintException(reader);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Read_SingleQuote_TextNoEnd()
        {
            StringReader reader = new StringReader("'Four score and ");
            PrintException(reader);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_Empty()
        {
            CheckReadResults("''",
                             "");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_EmptyWhitespace()
        {
            CheckReadResults("''\n ",
                             "",
                             "\n ");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_WhitespaceEmpty()
        {
            CheckReadResults(" \t  ''",
                             "");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_WhitespaceEmptyWhitespace()
        {
            CheckReadResults(" \t  ''\r ",
                             "",
                             "\r ");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_Str()
        {
            CheckReadResults("'Hello world!'",
                             "Hello world!");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_WhitespaceStr()
        {
            CheckReadResults(" \t 'Hello world!'",
                             "Hello world!");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_StrEscape()
        {
            CheckReadResults(" \t 'It went \\'Boom!\\'' ",
                             "It went 'Boom!'",
                             " ");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_StrEscapeOtherQuote()
        {
            CheckReadResults(" \t 'It went \\\"Boom!\\\"' ",
                             "It went \"Boom!\"",
                             " ");
        }

        //---------------------------------------------------------------------

        [Test]
        public void Read_SingleQuote_StrOtherQuote()
        {
            CheckReadResults(" \t 'It went \"Boom!\"' ",
                             "It went \"Boom!\"",
                             " ");
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        [Test]
        public void PrependArticle_Null()
        {
            Assert.AreEqual(null, String.PrependArticle(null));
        }

        //---------------------------------------------------------------------

        [Test]
        public void PrependArticle_Empty()
        {
            Assert.AreEqual("", String.PrependArticle(""));
        }

        //---------------------------------------------------------------------

        [Test]
        public void PrependArticle_Lower()
        {
            Assert.AreEqual("an apple", String.PrependArticle("apple"));
            Assert.AreEqual("a PB&J sandwich", String.PrependArticle("PB&J sandwich"));
        }

        //---------------------------------------------------------------------

        [Test]
        public void PrependArticle_Upper()
        {
            Assert.AreEqual("A WINNER!", String.PrependArticle("WINNER!"));
            Assert.AreEqual("AN OCEAN", String.PrependArticle("OCEAN"));
        }

        //---------------------------------------------------------------------

        [Test]
        public void PrependArticle_Capitalized()
        {
            Assert.AreEqual("An Idea", String.PrependArticle("Idea"));
            Assert.AreEqual("A Grand Opening", String.PrependArticle("Grand Opening"));
        }
    }
}
