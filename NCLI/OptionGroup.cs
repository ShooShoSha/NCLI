using NCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    [Serializable]
    public class OptionGroup : IEnumerable<Option>
    {
        private readonly Dictionary<string, Option> options = new Dictionary<string, Option>();
        public Dictionary<string, Option>.KeyCollection Names
        {
            get { return options.Keys; }
        }
        public Dictionary<string, Option>.ValueCollection Options
        {
            get { return options.Values; }
        }
        public Option Selected { get; private set; }
        public bool IsRequired { get; set; }

        /// <summary>
        /// Add the specified <see cref="Option"/> to this group.
        /// </summary>
        /// <param name="option">The option to add to this group.</param>
        /// <returns>The <see cref="OptionGroup"/> with the added option.</returns>
        public OptionGroup AddOption(Option option)
        {
            options.Add(option.Key, option);
            return this;
        }

        /// <summary>
        /// Set the selected option of this group.
        /// </summary>
        /// <param name="option">The selected <see cref="Option"/>.</param>
        /// <exception cref="AlreadySelectedException">If an option from this group has already been selected.</exception>
        public void SetSelected(Option option)
        {
            if ((Selected == null) || (Selected.Equals(option)))
            {
                Selected = option;
            }
            else
            {
                throw new AlreadySelectedException("An option from this group has already been selected: '" + Selected + "'");
            }
        }


        #region IEnumerable Members
        public IEnumerator<Option> GetEnumerator()
        {
            foreach (var item in options)
            {
                yield return item.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
