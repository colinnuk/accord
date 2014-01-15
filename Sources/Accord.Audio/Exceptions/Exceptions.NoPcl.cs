﻿// Accord Audio Library
// The Accord.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2014
// cesarsouza at gmail.com
//
// Copyright © AForge.NET, 2005-2011
// contacts@aforgenet.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.Audio
{
    using System.Runtime.Serialization;

    public partial class AudioException
    {

        /// <summary>
        ///   Initializes a new instance of the <see cref="AudioException"/> class.
        /// </summary>
        /// 
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// 
        protected AudioException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        
    }

    public partial class InvalidSignalPropertiesException
    {

        /// <summary>
        ///   Initializes a new instance of the <see cref="InvalidSignalPropertiesException"/> class.
        /// </summary>
        /// 
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// 
        protected InvalidSignalPropertiesException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    
    }

    public partial class UnsupportedSampleFormatException
    {

        /// <summary>
        ///   Initializes a new instance of the <see cref="UnsupportedSampleFormatException"/> class.
        /// </summary>
        /// 
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// 
        protected UnsupportedSampleFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        
    }
}