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
    /// Implementation of <see cref="ICommandLineParser"/> can <see cref="Parse"/> a <see cref="System.String[]"/> according to the <see cref="Options"/> specified and return a <see cref="CommandLine"/>.
    /// </summary>
    public interface ICommandLineParser
    {
        /// <summary>
        /// Parse the aguments according to the specified options.
        /// </summary>
        /// <param name="options">Specified options to parse against.</param>
        /// <param name="arguments">Command line arguments.</param>
        /// <returns>List of atomic option and value tokens.</returns>
        /// <exception cref="ParseException">
        /// Thrown when parsing encounters a problem.
        /// </exception>
        CommandLine Parse(Options options, string[] arguments);
        /// <summary>
        /// Parse the arguments according to the specified options and to continue parse after a non-option.
        /// </summary>
        /// <param name="options">Specified options to parse against.</param>
        /// <param name="arguments">Command line arguments.</param>
        /// <param name="stopAtNonOption">Flag to continue parsing if a non-option is encountered.</param>
        /// <returns>List of atomic option and value tokens.</returns>
        /// <exception cref="ParseException">
        /// Thrown when parsing encounters a problem.
        /// </exception>
        CommandLine Parse(Options options, string[] arguments, bool stopAtNonOption);
    }
}
