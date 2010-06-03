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
using System;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class InputVar_Int_Test
    {
        InputVar<int> intVar;
        private int[] values;
        private string valuesAsStr;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            intVar = new InputVar<int>("intVar");

            values = new int[] { -4, 78900, 0, 555 };
            string[] valsAsStrs = Array.ConvertAll(values,
                                                   new Converter<int, string>(Convert.ToString));
            valuesAsStr = string.Join(" ", valsAsStrs);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntVar_JustDigits()
        {
            StringReader reader = new StringReader("1234");
            intVar.ReadValue(reader);
            Assert.AreEqual(1234, intVar.Value.Actual);
            Assert.AreEqual("1234", intVar.Value.String);
            Assert.AreEqual(0, intVar.Index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntVar_PlusDigits()
        {
            StringReader reader = new StringReader("+1,234");
            intVar.ReadValue(reader);
            Assert.AreEqual(1234, intVar.Value.Actual);
            Assert.AreEqual("+1,234", intVar.Value.String);
            Assert.AreEqual(0, intVar.Index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntVar_MinusDigits()
        {
            StringReader reader = new StringReader("-1234");
            intVar.ReadValue(reader);
            Assert.AreEqual(-1234, intVar.Value.Actual);
            Assert.AreEqual("-1234", intVar.Value.String);
            Assert.AreEqual(0, intVar.Index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntVar_LeadingWhiteSpace()
        {
            StringReader reader = new StringReader(" \t -1234");
            intVar.ReadValue(reader);
            Assert.AreEqual(-1234, intVar.Value.Actual);
            Assert.AreEqual("-1234", intVar.Value.String);
            Assert.AreEqual(3, intVar.Index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntVar_TrailingWhiteSpace()
        {
            StringReader reader = new StringReader("-1234 \n ");
            intVar.ReadValue(reader);
            Assert.AreEqual(-1234, intVar.Value.Actual);
            Assert.AreEqual("-1234", intVar.Value.String);
            Assert.AreEqual(0, intVar.Index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntVar_NumWithWhiteSpace()
        {
            StringReader reader = new StringReader(" \t -1,234 \n ");
            intVar.ReadValue(reader);
            Assert.AreEqual(-1234, intVar.Value.Actual);
            Assert.AreEqual("-1,234", intVar.Value.String);
            Assert.AreEqual(3, intVar.Index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntVar_StringOfInts()
        {
            StringReader reader = new StringReader(valuesAsStr);
            foreach (int i in values) {
                intVar.ReadValue(reader);
                Assert.AreEqual(i, intVar.Value.Actual);
            }
        }
    }
}
