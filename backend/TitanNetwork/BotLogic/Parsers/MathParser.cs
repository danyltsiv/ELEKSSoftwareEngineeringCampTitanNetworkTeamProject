using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TitanWcfService.Services.Parsers
{
    /// <summary>
    /// Class MathParser.
    /// </summary>
    public class MathParser
    {
        /// <summary>
        /// The _digit
        /// </summary>
        protected readonly Regex _digit = new Regex("\\d");
        /// <summary>
        /// The _double
        /// </summary>
        protected readonly Regex _double = new Regex("^\\s*\\-?\\d+\\s*(\\s*,\\d*\\s*)?$");
        /// <summary>
        /// The _allowed symbols
        /// </summary>
        protected readonly Regex _allowedSymbols = new Regex("[\\d,\\s]");
        /// <summary>
        /// The _math reg
        /// </summary>
        protected readonly Regex _mathReg = new Regex("\\w*(?<==math\\()[\\s0-9+/\\*\\-^,()]*(?=\\))\\w*");
        /// <summary>
        /// The _replace reg
        /// </summary>
        protected readonly Regex _replaceReg = new Regex("(=math\\()[\\s0-9+/\\*\\-^,()]*(\\))");
        /// <summary>
        /// The _calculate reg
        /// </summary>
        protected readonly Regex _calculateReg = new Regex("^[\\s0-9+/\\*\\-^,()]*$");

        /// <summary>
        /// Parses the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        public string Parse(string text)
        {
            var matches = _mathReg.Matches(text);
            string replacement;

            foreach (var matched in matches)
            {
                if (!String.IsNullOrEmpty(matched.ToString()))
                {
                    try
                    {
                        var RPNList = ConvertToRPN(matched.ToString());
                        replacement = CalculateTheRPNExpression(RPNList);
                    }
                    catch (Exception)
                    {
                        replacement = "Incorrect expression!";
                    }
                    text = _replaceReg.Replace(text, replacement, 1);
                }
            }
            return text;
        }

        /// <summary>
        /// Calculates the RPN expression.
        /// </summary>
        /// <param name="RPNList">The RPN list.</param>
        /// <returns>System.String.</returns>
        public string CalculateTheRPNExpression(List<string> RPNList)
        {
            Stack<string> stack = new Stack<string>();
            double firstNum, secondNum, result;
            for (int i = 0; i < RPNList.Count; i++)
            {
                if (_double.IsMatch(RPNList[i]))
                    stack.Push(RPNList[i]);
                else
                {
                    firstNum = double.Parse(stack.Pop());
                    secondNum = double.Parse(stack.Pop());
                    result = Operation(char.Parse(RPNList[i]), firstNum, secondNum);
                    stack.Push(result.ToString());
                }
            }
            return stack.Pop();
        }

        /// <summary>
        /// Converts to RPN.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> ConvertToRPN(string expression)
        {
            Stack<char> stack = new Stack<char>();
            List<string> output = new List<string>();
            string tempNumber = "";

            for (int i = 0; i < expression.Length; i++)
            {
                if (CharIsPartOfNumber(expression, i))
                {
                    tempNumber = String.Concat(tempNumber, expression[i].ToString());
                    if (i == expression.Length - 1 && _double.IsMatch(tempNumber)) output.Add(tempNumber);
                    continue;
                }
                else if (!String.IsNullOrEmpty(tempNumber) && _double.IsMatch(tempNumber))
                {
                    output.Add(tempNumber);
                    tempNumber = "";
                }

                switch (expression[i])
                {
                    case '-':
                    case '+':
                    case '/':
                    case '*':
                        while (stack.Count > 0 && Priority(stack.ElementAt(0)) >= Priority(expression[i]))
                        {
                            output.Add(stack.Pop().ToString());
                        }
                        stack.Push(expression[i]);
                        break;
                    case '^':
                        while (stack.Count > 0 && Priority(stack.ElementAt(0)) > Priority(expression[i]))
                        {
                            output.Add(stack.Pop().ToString());
                        }
                        stack.Push(expression[i]);
                        break;
                    case '(': stack.Push(expression[i]); break;
                    case ')':
                        while (stack.Count > 0 && stack.ElementAt(0) != '(')
                        {
                            output.Add(stack.Pop().ToString());
                        }
                        if (stack.Count != 0)
                        {
                            stack.Pop();
                        }
                        break;
                    case ' ': break;
                }
            }

            return output.Concat(stack.Select(c => c.ToString())).ToList();
        }

        /// <summary>
        /// Priorities the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>System.Int32.</returns>
        private int Priority(char operation)
        {
            int priority = -1;
            switch (operation)
            {
                case '(': priority = 0; break;
                case ')': priority = 1; break;
                case '+':
                case '-': priority = 2; break;
                case '*':
                case '/': priority = 3; break;
                case '^': priority = 4; break;
            }
            return priority;
        }

        /// <summary>
        /// Operations the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="firstNum">The first number.</param>
        /// <param name="secondNum">The second number.</param>
        /// <returns>System.Double.</returns>
        private double Operation(char operation, double firstNum, double secondNum)
        {
            double result = 0;

            switch (operation)
            {
                case '+': result = secondNum + firstNum; break;
                case '-': result = secondNum - firstNum; break;
                case '*': result = secondNum * firstNum; break;
                case '/': result = secondNum / firstNum; break;
                case '^': result = Math.Pow(secondNum, firstNum); break;
            }
            return result;
        }

        /// <summary>
        /// Characters the is part of number.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="index">The index.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CharIsPartOfNumber(string str, int index)
        {
            int lastIndex = str.Length - 1;

            if (_allowedSymbols.IsMatch(str[index].ToString()))
            {
                return true;
            }
            else if (str[index] == '-')
            {
                if (index == 0)
                {
                    return true;
                }
                else if (index != lastIndex)
                {
                    return !_digit.IsMatch(str[index - 1].ToString())
                        && Priority(str[index - 1]) > 1 || Priority(str[index - 1]) == 0 
						|| str[index - 1] == ' ' && _digit.IsMatch(str[index + 1].ToString());
                }
            }
            return false;
        }
    }
}
