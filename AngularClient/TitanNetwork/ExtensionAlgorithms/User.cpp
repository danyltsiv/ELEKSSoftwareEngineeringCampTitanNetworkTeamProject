#include "User.h"
#include <sstream>
#include <functional>

Callback User::DatabaseInterface;

User::User(const char* user_data)
{
	string temp = user_data;
	stringstream ss(temp);
	ss >> id;
	do
	{
		ss >> temp;
		friends.push_back(temp);
	} while (ss);
	friends.pop_back();
}

string User::ToString()
{
	string temp = id + " ";
	for (string var : friends)
		temp += var + " ";
	return temp;
}

User User::GetExternal(const char* id)
{
	string result = DatabaseInterface(id);
	string temp = string(id) + " " + result;
	return User(temp.c_str());
}

bool User::HasFriend(string _id)
{
	return find(friends.begin(), friends.end(), _id) != friends.end();
}

User User::GetRandomFriend()
{
	int index = GetRandomNumber(0, friends.size());
	if (index > 0)
		index--;
	return User::GetExternal(friends[index].c_str());
}

void User::InitDB(Callback callback)
{
	DatabaseInterface = callback;
}