using System.Runtime.CompilerServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create mock database objects containing strings for testing
            IList<DbString> strings = new List<DbString>
           {
               new DbString{value = "AB1;C2*D"},
               new DbString{value = "D2C;1B*A"}
           };

            //Use the mock to use interface db objects
            Mock<IStringRepository> mockStringRepository = new Mock<IStringRepository>();
            mockStringRepository.Setup(m => m.FindAllStrings()).Returns(strings);

            IStringRepository MockStringRepository = mockStringRepository.Object;

            //Feed mocked values into reverse method and report results
            IList<DbString> testStrings = MockStringRepository.FindAllStrings();
            foreach(DbString testString in testStrings)
            {
                Console.Write(testString.value + " -> ");
                Console.Write(reverse(testString.value) + '\n');
            }
        }

        public static string reverse(string stringToReverse)
        {
            char[] str = stringToReverse.ToCharArray();

            //Create pointers
            int frontPointer = 0;
            int backPointer = str.Length -1;

            while(frontPointer < backPointer)
            {
                //Ignore any non alphanumeric characters
                if (!char.IsLetter(str[frontPointer]) && !char.IsNumber(str[frontPointer])) frontPointer++;
                else if (!char.IsLetter(str[backPointer]) && !char.IsNumber(str[backPointer])) backPointer--;
                else
                {
                    //characters at both the front and back are alphanumeric
                    //perform swap
                    char temp = str[frontPointer];
                    str[frontPointer] = str[backPointer];
                    str[backPointer] = temp;
                    frontPointer++;
                    backPointer--;
                }
            }
            return new string(str);
        }
    }
}
