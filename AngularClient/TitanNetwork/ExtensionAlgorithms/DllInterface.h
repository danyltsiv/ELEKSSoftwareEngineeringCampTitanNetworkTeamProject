#pragma once
#include <Windows.h>

#define TITANDLL_API __declspec(dllexport)
typedef char* (__stdcall *Callback) (const char* id);