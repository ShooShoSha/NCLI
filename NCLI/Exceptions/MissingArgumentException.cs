using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Copyright 2015 Kevin O'Brien
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace NCLI.Exceptions
{
    /// <summary>
    /// Thrown when a required argument has not been provided.
    /// </summary>
    /// <seealso cref="ParseException"/>
    public class MissingArgumentException : ParseException
    {
        /// <summary>
        /// Construct a new <see cref="MissingArgumentException"/> with the specified detailed message.
        /// </summary>
        /// <param name="message">Detailed message of <see cref="Exception"/>.</param>
        public MissingArgumentException(string message) : base(message)
        {

        }
    }
}
