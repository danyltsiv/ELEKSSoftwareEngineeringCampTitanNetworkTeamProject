#pragma once

#include <iostream>
#include <vector>
#include <map>
#include "DllInterface.h"
#include "Helpers.h"

extern "C"
struct User
{
private:
	static Callback DatabaseInterface;

public:
	static User GetExternal(const char* id);
	static void InitDB(Callback callback);

public:
	User(const char* user_data);

	string ToString();
	bool HasFriend(string id);
	User GetRandomFriend();

	string id;
	vector<string> friends;
};