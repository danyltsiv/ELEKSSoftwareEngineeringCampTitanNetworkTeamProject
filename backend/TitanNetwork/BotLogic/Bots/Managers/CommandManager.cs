using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TitanWcfService.Services.Bots.Managers
{
    /// <summary>
    /// Class CommandManager.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private readonly Dictionary<string, Commands.ICommander> _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager()
        {
            _manager = new Dictionary<string, Commands.ICommander>
            {
                {"help", null},
                 {"url",null},
                { "email", null},
                {"what is", null},
                {"math",null}
            };
        }

        /// <summary>
        /// Executes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>System.String.</returns>
        public string Execute(string key, string expression)
        {
            var command = _manager[key] ?? InitializeCommand(key);
            return command.Execute(expression);
        }

        /// <summary>
        /// Initializes the command.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Commands.ICommander.</returns>
        /// <exception cref="System.Exception">Command not fount</exception>
        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        protected virtual Commands.ICommander InitializeCommand(string key)
        {
            switch (key)
            {
                case "email":
                    return new Commands.Email.EmailMessager("vk.com/enef_a", "Andriy", "Diachuk");
                case "help":
                    return new Commands.Help.Helper();
                case "what is":
                    return new Commands.WhatIs.Answerer();
                case "math":
                    return new Commands.Math.Mather();
                case "url":
                    return new Commands.Url.Urler();
                default:
                    throw new Exception("Command not fount");
            }
        }
    }
}
