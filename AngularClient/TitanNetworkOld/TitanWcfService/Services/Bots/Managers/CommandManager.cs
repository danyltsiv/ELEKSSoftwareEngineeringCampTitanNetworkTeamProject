using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TitanWcfService.Services.Bots.Managers
{
    public class CommandManager
    {
        private readonly Dictionary<string, Commands.ICommander> _manager;

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

        public string Execute(string key, string expression)
        {
            var command = _manager[key] ?? InitializeCommand(key);
            return command.Execute(expression);
        }

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
                    throw new Exceptions.CommandNotFoundException("Command not fount");
            }
        }
    }
}
