#pragma once

#include <string>

#define CppAPI __declspec(dllexport) // 편의를 위함

extern "C" {
    CppAPI int Add(int a, int b);
    CppAPI int Minus(int a, int b);

    CppAPI bool IsEvenNumber(int number);

    CppAPI void WriteMessage(char* message);
}