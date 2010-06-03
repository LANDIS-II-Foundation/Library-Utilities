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
using System.Collections.Generic;
using System.IO;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class ExpectedLine_Test
    {
        private List<ExpectedLine> ReadLines(string filename)
        {
            string path = Path.Combine(Data.Directory, filename);
            return ExpectedLine.ReadLines(path);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EmptyFile()
        {
            List<ExpectedLine> lines = ReadLines("ExpectedLine_Empty.txt");
            Assert.AreEqual(0, lines.Count);
        }

        //---------------------------------------------------------------------

        private void PrintException(string filename)
        {
            try {
                List<ExpectedLine> lines = ReadLines(filename);
            }
            catch (System.Exception e) {
                Data.Output.WriteLine(e.Message);
                throw;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(LineReaderException))]
        public void NoLineNum()
        {
            PrintException("ExpectedLine_NoLineNum.txt");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(LineReaderException))]
        public void NoColon()
        {
            PrintException("ExpectedLine_NoColon.txt");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(LineReaderException))]
        public void LineNum0()
        {
            PrintException("ExpectedLine_LineNum0.txt");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(LineReaderException))]
        public void LessThanPrev()
        {
            PrintException("ExpectedLine_LessThanPrev.txt");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(LineReaderException))]
        public void SameAsPrev()
        {
            PrintException("ExpectedLine_SameAsPrev.txt");
        }

        //---------------------------------------------------------------------

        [Test]
        public void GoodFile()
        {
            List<ExpectedLine> lines = ReadLines("ExpectedLine_GoodFile.txt");
            Assert.AreEqual(6, lines.Count);

            Assert.AreEqual(1, lines[0].Number);
            Assert.AreEqual("Foo", lines[0].Text);

            Assert.AreEqual(22, lines[1].Number);
            Assert.AreEqual("\tThe line below 3 tabs.", lines[1].Text);

            Assert.AreEqual(33, lines[2].Number);
            Assert.AreEqual("\t\t\t", lines[2].Text);

            Assert.AreEqual(444, lines[3].Number);
            Assert.AreEqual("The line below has nothing after the colon.",
                            lines[3].Text);

            Assert.AreEqual(555, lines[4].Number);
            Assert.AreEqual("", lines[4].Text);

            Assert.AreEqual(6666, lines[5].Number);
            Assert.AreEqual("The last line in this file.", lines[5].Text);
        }
    }
}
