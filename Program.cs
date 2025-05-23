namespace Brackets_Analysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Hello! Enter your sequence of parentheses");
                string? sequence = Console.ReadLine();
                if (sequence == null)
                {
                    Console.WriteLine("Sequence cannot be null");
                    continue;
                }
                bool result = CheckSequence2(sequence);
                Console.WriteLine($"Correctness of the sequence is: {result}");
                Console.WriteLine();
            }

            bool CheckSequence2(string sequence)
            {
                bool result = true;

                Dictionary<char, char> parenthesesPairs = new Dictionary<char, char>()
                {
                    { ']' , '[' },
                    { '}' , '{' },
                    { ')' , '(' },
                    { '>' , '<' },
                };

                Stack<char> stack = new Stack<char>();

                int i = 0;
                foreach (char item in sequence)
                {
                    i++;
                    if (parenthesesPairs.ContainsValue(item))
                    {
                        stack.Push(item);
                    }
                    else if (parenthesesPairs.ContainsKey(item))
                    {
                        char peek;
                        if (stack.TryPeek(out peek) && peek == parenthesesPairs[item])
                            stack.Pop();
                        else
                        {
                            Console.WriteLine($"Open parenthese: {item}, Index: {i}");
                            result = false;
                        }
                    }   
                }

                stack.Clear();
                char[] arrayOfChars = sequence.ToCharArray();
                Array.Reverse(arrayOfChars);
                sequence = new string(arrayOfChars);
                i = sequence.Length;
                List<Tuple<char, int>> unclosed = new();

                foreach (char item in sequence)
                {
                    if (parenthesesPairs.ContainsKey(item))
                    {
                        stack.Push(item);
                    }
                    else if (parenthesesPairs.ContainsValue(item))
                    {
                        char peek;
                        if (stack.TryPeek(out peek) && peek == parenthesesPairs.FirstOrDefault(pp => pp.Value == item).Key)
                            stack.Pop();
                        else
                            unclosed.Add(Tuple.Create(item, i));
                    }
                    i--;
                }

                if (unclosed.Count > 0)
                {
                    result = false;
                    var sorted = unclosed.OrderBy(item => item.Item2);
                    foreach (var item in sorted)
                    {
                        Console.WriteLine($"Unclosed parenthese: {item.Item1}, Index: {item.Item2}");
                    }
                }

                return result;
            }

            bool CheckSequence(string sequence)
            {
                Dictionary<char, char> keyValuePairs = new Dictionary<char, char>()
                {
                    { '[' , ']' },
                    { '{' , '}' },
                    { '(' , ')' },
                    { '<' , '>' },
                };

                Dictionary<KeyValuePair<char, char>, int> dictionary = new Dictionary<KeyValuePair<char, char>, int>();

                foreach (var keyValuePair in keyValuePairs)
                {
                    dictionary.Add(keyValuePair, 0);
                }

                bool result = true;

                int i = 0;
                foreach (char item in sequence)
                {
                    i++;
                    if (keyValuePairs.ContainsKey(item))
                    {
                        ++dictionary[keyValuePairs.FirstOrDefault(p => keyValuePairs.Comparer.Equals( p.Key , item))];
                    }
                    else if (keyValuePairs.ContainsValue(item))
                    {
                        if ( --dictionary[keyValuePairs.FirstOrDefault(p => keyValuePairs.Comparer.Equals( p.Value, item))] < 0 )
                        {
                            Console.WriteLine($"Open parenthese: {item}, Index: {i}");
                            dictionary[keyValuePairs.FirstOrDefault(p => keyValuePairs.Comparer.Equals(p.Value, item))] = 0;
                            result = false;
                        }
                    }
                }

                foreach (var keyValuePair in keyValuePairs)
                {
                    dictionary[keyValuePair] =  0;
                }

                char[] arrayOfChars = sequence.ToCharArray();
                Array.Reverse(arrayOfChars);
                sequence = new string(arrayOfChars);
                i = sequence.Length;
                List<Tuple<char, int>> unclosed = new();
                foreach (char item in sequence)
                {
                    i--;
                    if (keyValuePairs.ContainsKey(item))
                    {
                        if ( --dictionary[keyValuePairs.FirstOrDefault(p => keyValuePairs.Comparer.Equals(p.Key, item))] < 0)
                        {
                            unclosed.Add(Tuple.Create(item, i));
                            dictionary[keyValuePairs.FirstOrDefault(p => keyValuePairs.Comparer.Equals(p.Key, item))] = 0;
                            result = false;
                        }
                    }
                    else if (keyValuePairs.ContainsValue(item))
                    {
                        ++dictionary[keyValuePairs.FirstOrDefault(p => keyValuePairs.Comparer.Equals(p.Value, item))];
                    }
                }

                if (unclosed.Count > 0) 
                {
                    var sorted = unclosed.OrderBy(item => item.Item2);
                    foreach (var item in sorted)
                    {
                        Console.WriteLine($"Unclosed parenthese: {item.Item1}, Index: {item.Item2}");
                    }
                }

                return result;
            }
        }
    }
}
