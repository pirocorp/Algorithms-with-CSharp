namespace _01._State_Machines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Expression
    {
        private delegate decimal ParserDelegate(string str);

        private readonly string _script;
        private int _index;
        private Dictionary<string, decimal> _variables;

        public Expression(string script)
        {
            this._script = script;
            this._index = 0;
            this._variables = new Dictionary<string, decimal>();
        }

        public void Eval()
        {
            this._script
                .Split(Environment.NewLine)
                .Select(l => l.Trim())
                .ToList()
                .ForEach(line =>
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        return;
                    }
                    
                    this._index = 0;
                    var result = this.ParseExpression(line);
                    //Console.WriteLine(result);
                });
        }

        private decimal ParseExpression(string str)
        {
            ParserDelegate parseFunction = this.ParseAssignment;

            var result = parseFunction(str);

            if (this._index < str.Length)
            {
                throw new ArgumentException($"Unexpected token {str[this._index]} at position {this._index + 1}");
            }

            return result;
        }

        private decimal ParseParentheses(string str)
        {
            ParserDelegate parseFunction = this.ParseAssignment;

            this.SkipWhiteSpace(str);

            if (str[this._index] == '(')
            {
                this._index++;
                var result = parseFunction(str);
                this.SkipWhiteSpace(str);

                if (str[this._index] != ')')
                {
                    throw new ArgumentException($"No matching ) for ( at position {this._index + 1}");
                }

                this._index++;
                return result;
            }
            else if (str[this._index] == '[')
            {
                this._index++;
                var result = parseFunction(str);
                this.SkipWhiteSpace(str);

                if (str[this._index] != ']')
                {
                    throw new ArgumentException($"No matching ] for [ at position {this._index + 1}");
                }

                this._index++;
                return Math.Round(result);
            }
            else if (str[this._index] == '|')
            {
                this._index++;
                var result = parseFunction(str);
                this.SkipWhiteSpace(str);

                if (str[this._index] != '|')
                {
                    throw new ArgumentException($"No matching | for | at position {this._index + 1}");
                }

                this._index++;
                return Math.Abs(result);
            }
            else //functions
            {
                if (this._index + 5 < str.Length && str.Substring(this._index, 5) == "print")
                {
                    this._index += 5;
                    this.SkipWhiteSpace(str);

                    if (str[this._index] == '(')
                    {
                        ++this._index;
                        var param = parseFunction(str);
                        this.SkipWhiteSpace(str);

                        if (str[this._index] != ')')
                        {
                            throw new ArgumentException($"Compile error: No matching ) for ( at position {this._index + 1}");
                        }

                        ++this._index;
                        Console.WriteLine(param);
                        return 0;
                    }
                }
                else if (this._index + 4 < str.Length && str.Substring(this._index, 4) == "sqrt")
                {
                    this._index += 4;
                    this.SkipWhiteSpace(str);

                    if (str[this._index] == '(')
                    {
                        ++this._index;
                        var result = parseFunction(str);
                        this.SkipWhiteSpace(str);

                        if (str[this._index] != ')')
                        {
                            throw new ArgumentException($"Compile error: No matching ) for ( at position {this._index + 1}");
                        }

                        ++this._index;
                        return (decimal)Math.Sqrt((double)result);
                    }
                }
                else if (this._index + 3 < str.Length && str.Substring(this._index, 3) == "log")
                {
                    this._index += 3;
                    this.SkipWhiteSpace(str);

                    if (str[this._index] == '(')
                    {
                        ++this._index;
                        var number = parseFunction(str);
                        this.SkipWhiteSpace(str);

                        if (str[this._index] != ',')
                        {
                            if (str[this._index] != ')')
                            {
                                throw new ArgumentException($"Compile error: No matching ) for ( at position {this._index + 1}");
                            }

                            ++this._index;
                            return (decimal)Math.Log((double)number, 10);
                        }

                        ++this._index;
                        var newBase = parseFunction(str);
                        this.SkipWhiteSpace(str);

                        if (str[this._index] != ')')
                        {
                            throw new ArgumentException($"Compile error: No matching ) for ( at position {this._index + 1}");
                        }

                        ++this._index;
                        return (decimal)Math.Log((double)number, (double)newBase);
                    }
                }
            }

            var variable = this.ParseVariable(str);

            if (variable == null)
            {
                return this.ParseNumber(str);
            }

            return this._variables[variable];
        }

        private decimal ParsePower(string str)
        {
            ParserDelegate parseFunction = this.ParseParentheses;

            var result = parseFunction(str);
            this.SkipWhiteSpace(str);

            if (this._index < str.Length)
            {
                if (str[this._index] == '^')
                {
                    ++this._index;
                    result = (decimal)Math.Pow((double)result, (double) this.ParsePower(str));
                    this.SkipWhiteSpace(str);
                }
                else if (str[this._index] == '*' && this._index + 1 < str.Length && str[this._index + 1] == '*')
                {
                    this._index += 2;
                    result = (decimal)Math.Pow((double)result, (double) this.ParsePower(str));
                    this.SkipWhiteSpace(str);
                }
            }

            return result;
        }

        private decimal ParseProduct(string str)
        {
            ParserDelegate parseFunction = this.ParsePower;

            var result = parseFunction(str);
            this.SkipWhiteSpace(str);

            while (this._index < str.Length)
            {
                if (str[this._index] == '*')
                {
                    ++this._index;
                    result *= parseFunction(str);
                    this.SkipWhiteSpace(str);
                }
                else if (str[this._index] == '/')
                {
                    ++this._index;
                    result /= parseFunction(str);
                    this.SkipWhiteSpace(str);
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private decimal ParseSum(string str)
        {
            ParserDelegate parseFunction = this.ParseProduct;

            var result = parseFunction(str);
            this.SkipWhiteSpace(str);

            while (this._index < str.Length)
            {

                if (str[this._index] == '+')
                {
                    ++this._index;
                    result += parseFunction(str);
                    this.SkipWhiteSpace(str);

                }
                else if (str[this._index] == '-')
                {
                    ++this._index;
                    result -= parseFunction(str);
                    this.SkipWhiteSpace(str);
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private decimal ParseAssignment(string str)
        {
            this.SkipWhiteSpace(str);
            var startIndex = this._index;

            var variable = this.ParseVariable(str);

            if (variable == null)
            {
                return this.ParseSum(str);
            }

            this.SkipWhiteSpace(str);

            if (this._index >= str.Length || str[this._index] != '=')
            {
                this._index = startIndex;
                return this.ParseSum(str);
            }

            this._index++;
            var value = this.ParseAssignment(str);

            this._variables[variable] = value;
            return value;
        }

        private string ParseVariable(string str)
        {
            this.SkipWhiteSpace(str);
            
            if (this._index < str.Length && !IsDigit(str[this._index]) && IsVariableChar(str[this._index]))
            {
                var startIndex = this._index;
                ++this._index;

                while (this._index < str.Length && IsVariableChar(str[this._index]))
                {
                    ++this._index;
                }

                return str.Substring(startIndex, this._index - startIndex);
            }

            return null;
        }

        private decimal ParseNumber(string str)
        {
            this.SkipWhiteSpace(str);

            decimal number = 0;
            var significance = 0.1M;
            var isFloat = false;
            var isNegative = false;

            if (str[this._index] == '-')
            {
                isNegative = true;
                this._index++;
            }
            else if (str[this._index] == '+')
            {
                this._index++;
            }

            for (; this._index < str.Length; this._index++)
            {
                var ch = str[this._index];

                if (!isFloat)
                {
                    if (IsDigit(ch))
                    {
                        number *= 10;
                        number += ch - '0';
                    }
                    else if (ch == '.')
                    {
                        isFloat = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (IsDigit(ch))
                    {
                        number += (ch - '0') * significance;
                        significance /= 10;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (isNegative)
            {
                return -number;
            }

            return number;
        }

        private void SkipWhiteSpace(string str)
        {
            while (this._index < str.Length && str[this._index] == ' ')
            {
                this._index++;
            }
        }

        private static bool IsVariableChar(char c)
        {
            return (   ('0' <= c && c <= '9')
                    || ('a' <= c && c <= 'z')
                    || ('A' <= c && c <= 'Z')
                    || c == '_') ;
        }

        private static bool IsDigit(char c)
        {
            return '0' <= c && c <= '9';
        }
    }
}
