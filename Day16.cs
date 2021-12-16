using System;
using System.Numerics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day16
    {
        public BigInteger Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            string hex = "";
            while (!sr.EndOfStream)
            {
                hex = sr.ReadLine().Trim();
            }
            sr.Close();

            string binary = HexToBinary(hex);
            int numberBitsProcessed = 0;
            BigInteger retVal = 0;
            BigInteger totalVersion = 0;

            ProcessBITS(binary, ref numberBitsProcessed, ref retVal, ref totalVersion);

            return totalVersion;
        }

        public BigInteger Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            string hex = "";
            while (!sr.EndOfStream)
            {
                hex = sr.ReadLine().Trim();
            }
            sr.Close();

            string binary = HexToBinary(hex);
            int numberBitsProcessed = 0;
            BigInteger retVal = 0;
            BigInteger totalVersion = 0;

            ProcessBITS(binary, ref numberBitsProcessed, ref retVal, ref totalVersion);

            return retVal;
        }

        private void ProcessBITS(string binary,
                                 ref int numberBitsProcessed,
                                 ref BigInteger retVal,
                                 ref BigInteger totalVersion)
        {
            if (String.IsNullOrEmpty(binary)) return;

            string part = "";
            part = binary.Substring(0, 3);
            
            BigInteger version = BinaryToBigInteger(part);
            totalVersion += version;

            part = binary.Substring(3, 3);
            BigInteger packetId = BinaryToBigInteger(part);

            numberBitsProcessed = 6;
            if (packetId == 4) //Literal
            {
                string valInBinary = "";
                int index = 6;
                while (index < binary.Length)
                {
                    if (index + 5 <= binary.Length)
                    {
                        string temp = binary.Substring(index, 5);
                        valInBinary += temp.Substring(1);
                        numberBitsProcessed += 5;
                        if (temp[0] == '0')
                        {
                            index += 5;
                            break;
                        }
                    }
                    index += 5;
                }

                //Process extra zeroes{
                bool allZeroesAtEnd = true;
                int tempCount = 0;
                while (index < binary.Length)
                {
                    if (binary[index] == '1')
                    {
                        allZeroesAtEnd = false;
                        break;
                    }
                    index++;
                    tempCount++;
                }

                if (allZeroesAtEnd)
                {
                    numberBitsProcessed += tempCount;
                }
                //Process extra zeroes}

                BigInteger val = BinaryToBigInteger(valInBinary);
                retVal = val;
            }
            else //Operator
            {
                char typeId = binary[6];
                numberBitsProcessed++;
                if (typeId == '0')
                {
                    BigInteger totalLength = BinaryToBigInteger(binary.Substring(7, 15));
                    numberBitsProcessed += 15;

                    int index = 22;
                    int count = 0;
                    List<BigInteger> listRetVal = new List<BigInteger>();
                    while (count < totalLength)
                    {
                        int tempNumberBitsProcessed = 0;
                        BigInteger tempRetVal = 0;
                        ProcessBITS(binary.Substring(index),
                                    ref tempNumberBitsProcessed,
                                    ref tempRetVal,
                                    ref totalVersion);

                        numberBitsProcessed += tempNumberBitsProcessed;
                        listRetVal.Add(tempRetVal);

                        count += tempNumberBitsProcessed;
                        index += tempNumberBitsProcessed;
                    }

                    BigInteger partialVal = 0;
                    switch ((int)packetId)
                    {
                        case 0:
                            partialVal = 0;
                            foreach (BigInteger v in listRetVal)
                            {
                                partialVal += v;
                            }
                            break;
                        case 1:
                            partialVal = 1;
                            foreach (BigInteger v in listRetVal)
                            {
                                partialVal *= v;
                            }
                            break;
                        case 2:
                            partialVal = -1;
                            foreach (BigInteger v in listRetVal)
                            {
                                if (partialVal == -1) partialVal = v;
                                else partialVal = BigInteger.Min(partialVal, v);
                            }
                            break;
                        case 3:
                            partialVal = -1;
                            foreach (BigInteger v in listRetVal)
                            {
                                if (partialVal == -1) partialVal = v;
                                else partialVal = BigInteger.Max(partialVal, v);
                            }
                            break;
                        case 5:
                            partialVal = (listRetVal[0] > listRetVal[1]) ? 1 : 0;
                            break;
                        case 6:
                            partialVal = (listRetVal[0] < listRetVal[1]) ? 1 : 0;
                            break;
                        case 7:
                            partialVal = (listRetVal[0] == listRetVal[1]) ? 1 : 0;
                            break;
                    }
                    retVal = partialVal;
                }
                else
                {
                    BigInteger numberOfSubPackets = BinaryToBigInteger(binary.Substring(7, 11));
                    numberBitsProcessed += 11;

                    int index = 18;
                    int count = 0;
                    List<BigInteger> listRetVal = new List<BigInteger>();
                    while (count < numberOfSubPackets)
                    {
                        int tempNumberBitsProcessed = 0;
                        BigInteger tempRetVal = 0;
                        ProcessBITS(binary.Substring(index),
                                    ref tempNumberBitsProcessed,
                                    ref tempRetVal,
                                    ref totalVersion);

                        numberBitsProcessed += tempNumberBitsProcessed;
                        listRetVal.Add(tempRetVal);

                        count++;
                        index += tempNumberBitsProcessed;
                    }

                    BigInteger partialVal = 0;
                    switch ((int)packetId)
                    {
                        case 0:
                            partialVal = 0;
                            foreach (BigInteger v in listRetVal)
                            {
                                partialVal += v;
                            }
                            break;
                        case 1:
                            partialVal = 1;
                            foreach (BigInteger v in listRetVal)
                            {
                                partialVal *= v;
                            }
                            break;
                        case 2:
                            partialVal = -1;
                            foreach (BigInteger v in listRetVal)
                            {
                                if (partialVal == -1) partialVal = v;
                                else partialVal = BigInteger.Min(partialVal, v);
                            }
                            break;
                        case 3:
                            partialVal = -1;
                            foreach (BigInteger v in listRetVal)
                            {
                                if (partialVal == -1) partialVal = v;
                                else partialVal = BigInteger.Max(partialVal, v);
                            }
                            break;
                        case 5:
                            partialVal = (listRetVal[0] > listRetVal[1]) ? 1 : 0;
                            break;
                        case 6:
                            partialVal = (listRetVal[0] < listRetVal[1]) ? 1 : 0;
                            break;
                        case 7:
                            partialVal = (listRetVal[0] == listRetVal[1]) ? 1 : 0;
                            break;
                    }
                    retVal = partialVal;
                }
            }
        }

        private string HexToBinary(string hex)
        {
            Hashtable map = new Hashtable();
            map.Add('0', "0000");
            map.Add('1', "0001");
            map.Add('2', "0010");
            map.Add('3', "0011");
            map.Add('4', "0100");
            map.Add('5', "0101");
            map.Add('6', "0110");
            map.Add('7', "0111");
            map.Add('8', "1000");
            map.Add('9', "1001");
            map.Add('A', "1010");
            map.Add('B', "1011");
            map.Add('C', "1100");
            map.Add('D', "1101");
            map.Add('E', "1110");
            map.Add('F', "1111");

            string binary = "";
            foreach (char c in hex)
            {
                binary += (string)map[c];
            }

            return binary;
        }

        private BigInteger BinaryToBigInteger(string binary)
        {
            BigInteger n = 0;

            BigInteger power = 1;
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                n += ((int)binary[i] - '0') * power;
                power *= 2;
            }

            return n;
        }
    }
}
