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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCLI
{
    /// <summary>
    /// Describes a single command-line Option.
    /// </summary>
    [Serializable]
    public class Option : ICloneable
    {
        public const int UNINTIALIZED = -1;
        public const int UNLIMITED_VALUES = -2;

        #region Properties
        public string ShortOption { get; set; }
        public string LongOption { get; set; }
        public string ArgumentName { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public bool HasOptionalArgument { get; set; }
        public int NumberOfArguments { get; set; }
        public char ValueSeparator { get; set; }
        // private readonly Class<?> type;
        public List<string> Values { get; set; }

        public int ID
        {
            get
            {
                return ((ShortOption == null) ? LongOption : ShortOption).ElementAt(0);
            }
        }
        #endregion

        #region Constructors
        public Option(string shortOption, string description) : this(shortOption, null, false, description) { }

        public Option(string shortOption, bool hasArg, string description) : this(shortOption, null, hasArg, description) { }

        public Option(string shortOption, string longOption, bool hasArg, string description)
        {
            OptionValidator.Validate(shortOption);
            ShortOption = shortOption;
            LongOption = longOption;
            if (hasArg)
            {
                NumberOfArguments = 1;
            }
            Description = description;
        }

        private Option(Builder builder)
        {
            ShortOption = builder.ShortOption;
            LongOption = builder.LongOption;
            Description = builder.Description;
            ArgumentName = builder.ArgumentName;
            IsRequired = builder.IsRequired;
            HasOptionalArgument = builder.HasOptionalArgument;
            NumberOfArguments = builder.NumberOfArguments;
            ValueSeparator = builder.ValueSeparator;
        } 
        #endregion

        public bool HasArguments()
        {
            return NumberOfArguments > 0 || NumberOfArguments == UNLIMITED_VALUES;
        }

        public bool HasValueSeparator()
        {
            return ValueSeparator > 0;
        }

        void AddValueForProcessing(string value)
        {
            if (NumberOfArguments == UNINTIALIZED)
            {
                throw new InvalidOperationException("No arguments allowed");
            }
            ProcessValue(value);
        }

        bool AcceptsArguments()
        {
            return (HasArguments() || HasOptionalArgument) && (NumberOfArguments <= 0 || Values.Count < NumberOfArguments);
        }

        bool RequiresArgument()
        {
            if (HasOptionalArgument)
            {
                return false;
            }
            if (NumberOfArguments == UNLIMITED_VALUES)
            {
                return HasNoValues();
            }
            return AcceptsArguments();
        }

        void ClearValues()
        {
            Values.Clear();
        }

        private void ProcessValue(string value)
        {
            if (HasValueSeparator())
            {
                int index = value.IndexOf(ValueSeparator);
                while (index != -1)
                {
                    if (value.Length == NumberOfArguments - 1)
                    {
                        break;
                    }
                    Add(value.Substring(0, index));
                }
            }
        }

        private void Add(string value)
        {
            if (!AcceptsArguments())
            {
                throw new InvalidOperationException("Cannot add value, list is full");
            }
            Values.Add(value);
        }

        private bool HasNoValues()
        {
            return Values.Count == 0;
        }

        #region ICloneable Members
        public object Clone()
        {
            Option option = (Option)base.MemberwiseClone();
            option.Values = new List<string>(Values);
            return option;
        } 
        #endregion

        #region Overridden Members
        public override int GetHashCode()
        {
            int result;
            result = ShortOption != null ? ShortOption.GetHashCode() : 0;
            result = 31 * result + (string.IsNullOrEmpty(LongOption) ? LongOption.GetHashCode() : 0);
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Option option = obj as Option;
            if (ShortOption != null ? !ShortOption.Equals(option.ShortOption) : option.ShortOption != null)
            {
                return false;
            }
            if (LongOption != null ? !LongOption.Equals(option.LongOption) : option.LongOption != null)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            // [Option: <shortOption>[ <longOption>][ \[[<sepChar>|\=][<argName>|arg]\]][ :: <description>]

            StringBuilder sb = new StringBuilder("Option: " + ShortOption);
            if (!string.IsNullOrEmpty(LongOption))
            {
                sb.Append(" ").Append(LongOption);
            }
            if (HasArguments())
            {
                if (HasOptionalArgument)
                {
                    sb.Append("[");
                }
                sb.Append(ValueSeparator);
                sb.Append(ArgumentName);
                if (HasOptionalArgument)
                {
                    sb.Append("]");
                }
            }
            if (!string.IsNullOrEmpty(Description))
            {
                sb.Append(" :: " + Description);
            }
            if (!IsRequired)
            {
                sb.Insert(0, "[ ");
                sb.Append(" ]");
            }

            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// Nested builder class to create <see cref="Option"/> instances using descriptive methods and properites.
        /// </summary>
        public sealed class Builder
        {
            // Required parameters
            public string ShortOption { get; set; }
            public string LongOption { get; set; }
            public string Description { get; set; }
            public string ArgumentName { get; set; }
            public bool IsRequired { get; set; }
            public bool HasOptionalArgument { get; set; }
            public int NumberOfArguments { get; set; }
            public char ValueSeparator { get; set; }

            public Builder(string option)
            {
                OptionValidator.Validate(option);
                ShortOption = option;
                desc();
                argName();
                longOpt();
                numberOfArgs();
                optionalArg();
                required();
                valueSeparator();
                hasArg(false);
            }

            public Builder desc(string desc = "")
            {
                Description = desc;
                return this;
            }

            public Builder argName(string argName = "arg")
            {
                ArgumentName = argName;
                return this;
            }

            public Builder longOpt(string longOpt = "")
            {
                LongOption = longOpt;
                return this;
            }

            public Builder numberOfArgs(int numArgs = 0)
            {
                NumberOfArguments = numArgs;
                return this;
            }

            public Builder optionalArg(bool isOptional = true)
            {
                HasOptionalArgument = isOptional;
                return this;
            }

            public Builder required(bool required = true)
            {
                IsRequired = required;
                return this;
            }

            public Builder valueSeparator(char sep = '=')
            {
                ValueSeparator = sep;
                return this;
            }

            public Builder hasArg(bool hasArg = true)
            {
                NumberOfArguments = hasArg ? 1 : UNINTIALIZED;
                return this;
            }

            public Builder hasArgs()
            {
                NumberOfArguments = UNLIMITED_VALUES;
                return this;
            }

            public Option Build()
            {
                if (string.IsNullOrEmpty(ShortOption) && string.IsNullOrEmpty(LongOption))
                {
                    throw new ArgumentException("Either Option or LongOption must be specified");
                }
                return new Option(this);
            }
        }
    }
}
