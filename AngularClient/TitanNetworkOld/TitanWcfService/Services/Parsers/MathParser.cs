using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TitanWcfService.Services.Parsers
{
    public class MathParser
    {
        protected readonly Regex _digit = new Regex("\\d");
        protected readonly Regex _double = new Regex("^\\s*\\-?\\d+\\s*(\\s*,\\d*\\s*)?$");
        protected readonly Regex _allowedSymbols = new Regex("[\\d,\\s]");
        protected readonly Regex _mathReg = new Regex("\\w*(?<==math\\()[\\s0-9+/\\*\\-^,()]*(?=\\))\\w*");
        protected readonly Regex _replaceReg = new Regex("(=math\\()[\\s0-9+/\\*\\-^,()]*(\\))");
        protected readonly Regex _calculateReg = new Regex("^[\\s0-9+/\\*\\-^,()]*$");
        
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
