#include "stdafx.h"
#include "CppUnitTest.h"
#include "../ExtensionAlgorithms/UserAlgorithms.h"
#include <map>
#include <iostream>
#include <fstream>
#include <Windows.h>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace TitanNetworkExtensionTests
{
	TEST_CLASS(UnitTest1)
	{
	public:
		static map<string, string> Users;
		static char* __stdcall GetUserFromDB(const char* id)
		{
			return (char*)Users[id].c_str();
		}

		TEST_CLASS_INITIALIZE(InitializeLibrary)
		{
			RegisterCallback(GetUserFromDB);
			Users["0"] = "1 3 5 7 9";
			Users["1"] = "0 6 8";
			Users["2"] = "4 5";
			Users["3"] = "0 4 9";
			Users["4"] = "2 3 9";
			Users["5"] = "0 2 6";
			Users["6"] = "1 5 7 8";
			Users["7"] = "0 6";
			Users["8"] = "1 8";
			Users["9"] = "0 3 4";
		}

		TEST_METHOD(GetWayBetweenUsers_1_And_4_User)
		{
			char* result;
			result = GetWayBetweenUsers("1", "4");
			Assert::AreEqual(result, "1 0 3 4");
		}

		TEST_METHOD(GetWayBetweenUsers_9_And_9_User)
		{
			char* result;
			result = GetWayBetweenUsers("9", "9");
			Assert::AreEqual(result, "9");
		}


		TEST_METHOD(GetWayBetweenUsers_9_And_6_User)
		{
			char* result;
			result = GetWayBetweenUsers("9", "6");
			Assert::AreEqual(result, "9 0 1 6");
		}

		TEST_METHOD(GetWayBetweenUsers_null_And_2_User)
		{
			char* result;
			result = GetWayBetweenUsers(nullptr, "2");
			Assert::AreEqual(result, "");
		}
	};

	map<string, string> UnitTest1::Users;
}