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

namespace NCLI
{
    /// <summary>
    /// List of arguments parsed against an <see cref="Options"/> descriptor.
    /// </summary>
    /// <remarks>
    /// It allows querying for the existence of an option (<see cref="HasOption"/>) or retrieving the option (<see cref="GetOptionValue(System.String)"/>) requiring arguments.
    /// <para>
    /// Any leftover or unrecognized arguments are available for further processing.
    /// </para>
    /// </remarks>
    public class CommandLine : IEnumerable<Option>
    {

        #region IEnumerable Members
        public IEnumerator<Option> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion
    }
}
