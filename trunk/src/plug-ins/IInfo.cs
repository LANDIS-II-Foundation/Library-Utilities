// Copyright 2005 University of Wisconsin 
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

namespace Edu.Wisc.Forest.Flel.Util.PlugIns
{
    /// <summary>
    /// Information about a plug-in.
    /// </summary>
    public interface IInfo
    {
        /// <summary>
        /// The name that users refer to the plug-in by.
        /// </summary>
        string Name
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The type of the plug-in's interface.
        /// </summary>
        System.Type InterfaceType
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The AssemblyQualifiedName of the class that implements the plug-in.
        /// </summary>
        string ImplementationName
        {
            get;
        }
    }
}
