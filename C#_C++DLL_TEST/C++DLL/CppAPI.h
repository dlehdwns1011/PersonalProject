#pragma once

#include <string>

#define CppAPI __declspec(dllexport) // ���Ǹ� ����

extern "C" {
    CppAPI int Add(int a, int b);
    CppAPI int Minus(int a, int b);

    CppAPI bool IsEvenNumber(int number);

    CppAPI void WriteMessage(char* message);
}