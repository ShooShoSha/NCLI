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
    /// Main entry point into the library.
    /// </summary>
    [Serializable]
    public class Options : IEnumerable<Option>
    {
        private readonly Dictionary<string, Option> shortOptions = new Dictionary<string, Option>();
        private readonly Dictionary<string, Option> longOptions = new Dictionary<string, Option>();
        private readonly Dictionary<string, OptionGroup> optionGroups = new Dictionary<string, OptionGroup>();

        private HashSet<Option> options;
        private HashSet<OptionGroup> optionGroups;

        /// <summary>
        /// Adds the specified option group.
        /// </summary>
        /// <remarks>
        /// An individual <see cref="Option"/> in an <see cref="OptionGroup"/> cannot be required when the <see cref="OptionGroup"/> is not required; either the group is required or nothing is required.
        /// </remarks>
        /// <param name="group"></param>
        /// <returns>The resulting <see cref="Options"/> instance.</returns>
        public Options AddOptionGroup(OptionGroup group)
        {
            // TODO: Handle required options
            throw new NotImplementedException();
        }

        public Options AddOption(Option option)
        {

        }

        #region IEnumerable Members
        public IEnumerator<Option> GetEnumerator()
        {
            foreach (var item in options)
            {
                yield return item;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[ Options: [ short ");
            sb.Append(shortOptions.ToString());
            sb.Append(" ] [ long ");
            sb.Append(longOptions.ToString());
            sb.Append(" ]");

            return sb.ToString();
        }
    }
}
