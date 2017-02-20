#pragma once
#include <sstream>
#include <random>

using namespace std;

template <typename T>
string ToString(const T& t)
{
	stringstream ss;
	ss << t;
	return ss.str();
}

inline int IntToStr(const string& str)
{
	return atoi(str.c_str());
}

inline int GetRandomNumber(int min, int max)
{
	default_random_engine generator;
	uniform_int_distribution<int> distribution(min, max);
	return distribution(generator);
}

inline char* ReturnStringToSharp(string& result)
{
	//trim result
	result.erase(result.find_last_not_of(" \n\r\t") + 1);
	
	//magic to acces the return string in C#
	size_t len = result.length() + 1;
	char* buff = (char*)CoTaskMemAlloc(len);
	strcpy_s(buff, len, result.c_str());
	return buff;
}