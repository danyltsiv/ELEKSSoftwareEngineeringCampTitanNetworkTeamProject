#include "UserAlgorithms.h"

extern "C" TITANDLL_API void __stdcall RegisterCallback(Callback callback)
{
	User::InitDB(callback);
}

extern "C" TITANDLL_API char* __stdcall GetWayBetweenUsers(const char* id1, const char* id2)
{
	string result = "";
	if (id1 == nullptr || id2 == nullptr)
	{
		return ReturnStringToSharp(result);
	}

	if (strcmp(id1, id2) == 0)
	{
		result = id1;
		return ReturnStringToSharp(result);
	}

	User user1 = User::GetExternal(id1);
	User user2 = User::GetExternal(id2);
	if (user1.friends.size() < user2.friends.size())
	{
		if (user1.HasFriend(user2.id))
		{
			result = string(id1) + " " + string(id2);
			return ReturnStringToSharp(result);
		}
	}
	else
	{
		if (user2.HasFriend(user1.id))
		{
			result = string(id1) + " " + string(id2);
			return ReturnStringToSharp(result);
		}
	}

	result += user1.id + " ";
	while (true)
	{
		if (user1.HasFriend(user2.id))
		{
			result += user2.id + " ";
			return ReturnStringToSharp(result);
		}
		for (string _friend : user1.friends)
		{
			if (user2.HasFriend(_friend))
			{
				result += _friend + " " + user2.id + " ";
				return ReturnStringToSharp(result);
			}
		}
		user1 = user1.GetRandomFriend();
		result += user1.id + " ";
	}
}


extern "C" TITANDLL_API char* __stdcall GetCommonFriends(const char* id1, const char* id2)
{
	string result = "";
	User user1 = User::GetExternal(id1);
	User user2 = User::GetExternal(id2);
	for (string _friend1 : user1.friends)
	{
		if (user2.HasFriend(_friend1))
		{
			result += _friend1 + " ";
		}
	}
	return ReturnStringToSharp(result);
}