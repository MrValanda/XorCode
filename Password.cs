using System;
using System.Linq;
using System.Text;

namespace GenPassword
{
    public class Password
    {
        private string _setSymbols, _symbols;
            private StringBuilder _password;
            private int _size;

            private bool _containsEngUpperLetter,
                _containsNumber,
                _containsSymbol,
                _containsEngLowerLetter,
                _containsRusUpperLetter,
                _containsRusLowerLetter;

            public Password(string symbols, int length, bool containsRusUpperLetter=false, bool containsRusLowerLetter=false, bool containsEngUpperLetter=false, bool containsNumber=false,
                bool containsSymbol=false, bool containsEngLowerLetter=false)
            {
                _symbols = symbols;
                _size = length;
                _containsRusUpperLetter = containsRusUpperLetter;
                _containsRusLowerLetter = containsRusLowerLetter;
                _containsEngUpperLetter = containsEngUpperLetter;
                _containsNumber = containsNumber;
                _containsSymbol = containsSymbol;
                _containsEngLowerLetter = containsEngLowerLetter;
                for (int i = 0; i < length; i++)
                {
                    _setSymbols += (char) i;
                }

                _password = new StringBuilder(_setSymbols);
            }

            public void GenerateFinalPassword()
            {
                if (!_containsNumber && !_containsEngUpperLetter && !_containsSymbol && !_containsEngLowerLetter &&
                    !_containsRusLowerLetter && !_containsRusUpperLetter)
                    return;
                int[] passwordIndex = new int[_size];
                Random rand = new Random();
                passwordIndex = passwordIndex.Select(x => -1).ToArray();
                for (int i = 0; i < _size;)
                {

                    if (_containsEngUpperLetter && i < _size)
                        CheckContainsAndAddChar(ref i, passwordIndex, rand, 'A', 'Z' + 1);
                    if (_containsEngLowerLetter && i < _size)
                        CheckContainsAndAddChar(ref i, passwordIndex, rand, 'a', 'z' + 1);

                    if (_containsRusUpperLetter && i < _size)
                        CheckContainsAndAddChar(ref i, passwordIndex, rand, 'А', 'Я' + 1);
                    
                    if (_containsRusLowerLetter && i < _size)
                        CheckContainsAndAddChar(ref i, passwordIndex, rand, 'а', 'я' + 1);

                    if (_containsSymbol && i < _size)
                        CheckContainsAndAddChar(ref i, passwordIndex, rand);

                    if (_containsNumber && i < _size)
                        CheckContainsAndAddChar(ref i, passwordIndex, rand, '0', '9' + 1);
                }
            }

            public StringBuilder GetPassword()
            {
                return _password;
            }

            private void CheckContainsAndAddChar(ref int i, int[] passwordIndex, Random rand, int left = 0,
                int right = 0)
            {
                int tempIndex = rand.Next(0, _size);
                while (passwordIndex.Contains(tempIndex)) tempIndex = rand.Next(0, _size);
                
                if (left != 0 && right != 0)
                {
                    char letter = (char) rand.Next(left, right);
                    _password[tempIndex] = letter;
                }
                else
                    _password[tempIndex] = _symbols[rand.Next(0, _symbols.Length)];

                passwordIndex[i] = tempIndex;

                i++;
            }
    }
}