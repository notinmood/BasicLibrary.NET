using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Mathes.StringParse
{
    /// <summary>
    /// 最简逆波兰表达式
    /// （推荐使用RPN类）
    /// </summary>
    public class SimpleRPN
    {
        public SimpleRPN()
        {
            _OptStack = new Stack<char>();
            _SuffixStack = new Stack<float>();
        }

        private Stack<char> _OptStack;
        private Stack<float> _SuffixStack;

        public float Calculate(string expression)
        {
            string lastNum = string.Empty;
            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsNumber(expression[i]) || expression[i].Equals('.'))
                {
                    lastNum += expression[i];
                }
                else
                {
                    if (lastNum != string.Empty)
                    {
                        Merger(float.Parse(lastNum));
                        lastNum = string.Empty;
                    }
                    AddOpt(expression[i]);
                }
            }
            if (lastNum != string.Empty)
            {
                Merger(float.Parse(lastNum));
            }
            while (_OptStack.Count > 0)
            {
                Merger(_OptStack.Pop());
            }

            return _SuffixStack.Pop();
        }

        private void AddOpt(char opt)
        {
            if (_OptStack.Count == 0)
            {
                _OptStack.Push(opt);
                return;
            }
            if (opt.Equals(')'))
            {
                while (!_OptStack.Peek().Equals('('))
                {
                    Merger(_OptStack.Pop());
                }
                _OptStack.Pop();
                return;
            }
            char tempOpt = _OptStack.Peek();
            if ((opt.Equals('-') || opt.Equals('+')) &&
                (tempOpt.Equals('*') || tempOpt.Equals('/')))
            {
                while (_OptStack.Count > 0)
                {
                    Merger(_OptStack.Pop());
                }
            }

            _OptStack.Push(opt);
        }

        private void Merger(float exp)
        {
            _SuffixStack.Push(exp);
        }

        private void Merger(char exp)
        {
            float num1 = _SuffixStack.Pop();
            float num2 = _SuffixStack.Pop();
            float result = 0;
            switch (exp)
            {
                case '+':
                    result = num2 + num1;
                    break;
                case '-':
                    result = num2 - num1;
                    break;
                case '*':
                    result = num2 * num1;
                    break;
                case '/':
                    result = num2 / num1;
                    break;
            }
            _SuffixStack.Push(result);
        }
    }
}