using System;
using System.Collections.Generic;
using System.IO;
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
    /// Formats a help message showing the options and their descriptions.
    /// </summary>
    /// <remarks>
    /// Prints formatted help message to the console via <see cref="System.Console.Out"/>
    /// </remarks>
    public class HelpFormatter
    {
        #region Constants
        const int DESCRIPTION_PAD = 3;
        const int LEFT_PAD = 1;
        const int WIDTH = 74;
        const string SYNTAX_PREFIX = "usage:";
        #endregion

        /// <summary>
        /// Prints a formatted help message for the given command line syntax, its options, and default formatting values.
        /// </summary>
        /// <param name="commandLineSyntax">Syntax for this application to run at command line.</param>
        /// <param name="options">Available <see cref="Options"/> for this application.</param>
        public static void PrintHelp(string applicationName, string commandLineSyntax, Options options)
        {
            new HelpFormatter.Builder(applicationName, commandLineSyntax, options).Build().PrintHelp();
        }

        /// <summary>
        /// Prints a formatted help message.
        /// </summary>
        public void PrintHelp()
        {

        }

        /// <summary>
        /// Gets a string of the usage statement for the specified application.
        /// </summary>
        private string GetUsage()
        {
            return SyntaxPrefix + " " + ApplicationName + " " + Syntax;
        }

        private void PrintWrapped()
        {

        }

        private void PrintOptions()
        {

        }

        private HelpFormatter(Builder builder)
        {
            this.ApplicationName = builder.ApplicationName;
            this.Syntax = builder.Syntax;
            this.Options = builder.Options;

            this.SyntaxPrefix = builder.SyntaxPrefix;
            this.ShortOptionPrefix = builder.ShortOptionPrefix;
            this.LongOptionPrefix = builder.LongOptionPrefix;

            this.Header = builder.Header;
            this.Footer = builder.Footer;

            this.LeftPadding = builder.LeftPadding;
            this.DescriptionPadding = builder.DescriptionPadding;
            this.Width = builder.Width;

            this.PrintWriter = builder.PrintWriter;
            this.AutoUsage = builder.AutoUsage;
        }

        private HelpFormatter() { }

        /// <summary>
        /// Nested builder class for configuring a <see cref="HelpFormatter"/>.
        /// </summary>
        public sealed class Builder
        {
            public Builder(string applicationName, string syntax, Options options)
            {
                ApplicationName = applicationName;
                Syntax = syntax;
                Options = options;
                header();
                footer();
                printWriter();
                autoUsage();
                width();
                syntaxPrefix();
                shortOptionPrefix();
                longOptionPrefix();
                leftPadding();
                descriptionPadding();
            }

            /// <summary>
            /// Creates the configured <see cref="HelpFormatter"/>.
            /// </summary>
            /// <returns>A configured <see cref="HelpFormatter"/>.</returns>
            /// <exception cref="ArgumentException">Thrown when <see cref="HelpFormatter.ApplicationName"/>, <see cref="HelpFormatter.Syntax"/>, or <see cref="HelpFormatter.Options"/> is not defined.</exception>
            public HelpFormatter Build()
            {
                if (string.IsNullOrEmpty(ApplicationName))
                {
                    throw new ArgumentException("Application name was not defined");
                }
                if (string.IsNullOrEmpty(Syntax))
                {
                    throw new ArgumentException("Command line syntax was not defined");
                }
                if (this.Options == null)
                {
                    throw new ArgumentException("Options was not defined");
                }
                return new HelpFormatter(this);
            }

            public Builder header(string header = "")
            {
                Header = header;
                return this;
            }

            public Builder footer(string footer = "")
            {
                Footer = footer;
                return this;
            }

            public Builder printWriter(TextWriter writer = null)
            {
                PrintWriter = (writer == null) ? Console.Out : writer;
                return this;
            }

            public Builder autoUsage(bool autoUsage = false)
            {
                AutoUsage = autoUsage;
                return this;
            }

            public Builder width(int width = WIDTH)
            {
                Width = width;
                return this;
            }

            public Builder syntaxPrefix(string prefix = SYNTAX_PREFIX)
            {
                SyntaxPrefix = prefix;
                return this;
            }

            public Builder shortOptionPrefix(string prefix = Option.SHORT_OPT_PREFIX)
            {
                ShortOptionPrefix = prefix;
                return this;
            }

            public Builder longOptionPrefix(string prefix = Option.LONG_OPT_PREFIX)
            {
                LongOptionPrefix = prefix;
                return this;
            }

            public Builder leftPadding(int padding = LEFT_PAD)
            {
                LeftPadding = padding;
                return this;
            }

            public Builder descriptionPadding(int padding = DESCRIPTION_PAD)
            {
                DescriptionPadding = padding;
                return this;
            }

            public string Syntax { get; set; }

            public Options Options { get; set; }

            public string Header { get; set; }

            public string Footer { get; set; }

            public TextWriter PrintWriter { get; set; }

            public bool AutoUsage { get; set; }

            public int Width { get; set; }

            public string SyntaxPrefix { get; set; }

            public string ShortOptionPrefix { get; set; }

            public string LongOptionPrefix { get; set; }

            public int LeftPadding { get; set; }

            public int DescriptionPadding { get; set; }

            public string ApplicationName { get; set; }
        }

        public string Header { get; set; }

        public string Footer { get; set; }

        public string Syntax { get; set; }

        public Options Options { get; set; }

        public string SyntaxPrefix { get; set; }

        public int LeftPadding { get; set; }

        public int DescriptionPadding { get; set; }

        public TextWriter PrintWriter { get; set; }

        public string ShortOptionPrefix { get; set; }

        public string LongOptionPrefix { get; set; }

        public bool AutoUsage { get; set; }

        public int Width { get; set; }

        public string ApplicationName { get; set; }
    }
}
