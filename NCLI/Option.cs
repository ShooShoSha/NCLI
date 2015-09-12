using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Describes a single command-line Option.
    /// </summary>
    [Serializable]
    public class Option : ICloneable
    {
        #region Constants
        /// <summary>
        /// Constant specifying the number of arguments for this <see cref="Option"/> is not specified.
        /// </summary>
        public const int UNINTIALIZED = -1;
        /// <summary>
        /// Constant specifying the number of arguments for this <see cref="Option"/> is unlimited.
        /// </summary>
        public const int UNLIMITED_VALUES = -2;
        /// <summary>
        /// Constant specifying the character delimiting the <see cref="Option"/> name from its argument.
        /// </summary>
        public const char VALUE_SEPARATOR = '=';
        /// <summary>
        /// Constant specifying the default name for the argument of this <see cref="Option"/>.
        /// </summary>
        public const string ARG_NAME = "arg";
        /// <summary>
        /// Constant specifying the prefix before the long <see cref="Option"/> name.
        /// </summary>
        public const string LONG_OPT_PREFIX = "--";
        /// <summary>
        /// Constant specifying the prefix before the short <see cref="Option"/> name.
        /// </summary>
        public const string SHORT_OPT_PREFIX = "-"; 
        #endregion

        #region Properties
        /// <summary>
        /// The abbreviated or short alias for this <see cref="Option"/>.
        /// </summary>
        public string ShortOption { get; set; }
        /// <summary>
        /// The long alias for this <see cref="Option"/>.
        /// </summary>
        public string LongOption { get; set; }
        /// <summary>
        /// Name for each argument of this <see cref="Option"/>.
        /// </summary>
        /// <remarks>
        /// Usually shown during the help message.
        /// </remarks>
        /// <seealso cref="HelpFormatter"/>
        public string ArgumentName { get; set; }
        /// <summary>
        /// Summary of what the <see cref="Option"/> does and any pertinent information for its arguments, values, ranges, and so on.
        /// </summary>
        /// <remarks>
        /// Usually shown during the help message.
        /// </remarks>
        /// <seealso cref="HelpFormatter"/>
        public string Description { get; set; }
        /// <summary>
        /// Indicates if this <see cref="Option"/> is required for its command line syntax.
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// Indicates if this <see cref="Option"/>'s arguments are optional.
        /// </summary>
        public bool HasOptionalArgument { get; set; }
        private int numberOfArguments;
        /// <summary>
        /// The number of arguments this <see cref="Option"/> accepts.
        /// </summary>
        /// <remarks>
        /// The typical values for <see cref="NumberOfArguments"/> are <see cref="UNINITIALIZED"/>, <see cref="UNLIMITED_VALUES"/>, or an integer with a value of at least zero (>= 0).
        /// </remarks>
        public int NumberOfArguments
        {
            get
            {
                return numberOfArguments;
            }
            set
            {
                if (value < UNLIMITED_VALUES && value < UNINTIALIZED)
                {
                    throw new ArgumentOutOfRangeException("NumberOfArguments is below the minimum");
                }
                numberOfArguments = value;
            }
        }
        /// <summary>
        /// The character that delimits the <see cref="Option"/> name (<see cref="ShortOption"/> or <see cref="LongOption"/>) and its arguments.
        /// </summary>
        public char ValueSeparator { get; set; }
        /// <summary>
        /// The arguments stored with this <see cref="Option"/>.
        /// </summary>
        public List<string> Values { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get
            {
                return Key[0];
            }
        }

        internal string Key
        {
            get
            {
                return (ShortOption == null) ? LongOption : ShortOption;
            }
        }
        #endregion

        #region Constructors
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
            return !Char.IsControl(ValueSeparator);
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
            return this.GetHashCode() == option.GetHashCode();
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
                Description = "";
                ArgumentName = ARG_NAME;
                LongOption = "";
                NumberOfArguments = UNINTIALIZED;
                HasOptionalArgument = false;
                IsRequired = false;
                ValueSeparator = VALUE_SEPARATOR;
            }

            public Option Build()
            {
                if (string.IsNullOrEmpty(ShortOption) && string.IsNullOrEmpty(LongOption))
                {
                    throw new ArgumentException("Either ShortOption or LongOption must be specified");
                }
                if (NumberOfArguments < UNLIMITED_VALUES && NumberOfArguments < UNINTIALIZED)
                {
                    throw new ArgumentOutOfRangeException("NumberOfArguments is below the minimum");
                }
                return new Option(this);
            }
        }
    }
}
