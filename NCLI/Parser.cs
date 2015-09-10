﻿using System;
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
    public abstract class Parser : ICommandLineParser
    {
        private CommandLine commandLine;
        private Options options;
        #region ICommandLineParser Members
        public CommandLine Parse(Options options, string[] arguments)
        {
            throw new NotImplementedException();
        }

        public CommandLine Parse(Options options, string[] arguments, bool stopAtNonOption)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
