#pragma once
#include "DllInterface.h"
#include "User.h"

extern "C" TITANDLL_API void __stdcall RegisterCallback(Callback callback);
extern "C" TITANDLL_API char* __stdcall GetWayBetweenUsers(const char* id1, const char* id2);
extern "C" TITANDLL_API char* __stdcall GetCommonFriends(const char* id1, const char* id2);