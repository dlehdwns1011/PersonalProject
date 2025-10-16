#include "CppAPI.h"
#include "iostream"

CppAPI int Add(int a, int b)
{
    return a + b;
}

CppAPI int Minus(int a, int b)
{
    return a - b;
}

CppAPI bool IsEvenNumber(int number)
{
    return (number % 2 == 0) ? true : false;
}

CppAPI void WriteMessage(char* message)
{
    std::cout << message;
}
