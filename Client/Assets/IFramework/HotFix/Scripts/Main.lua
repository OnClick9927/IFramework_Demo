--锁住 _G
local function Lock_G()
		setmetatable(_G, {
					    -- 控制新建全局变量
					    __newindex = function(_, k)
					        error("attempt to add a new value to global,key: " .. k, 2)
					    end,

					    -- 控制访问全局变量
					    -- __index = function(_, k)
					    --     error("attempt to index a global value,key: "..k,2)
					    -- end
						})
end

-- 全局函数
-- 用于声明全局变量
local function Define(name, value) rawset(_G, name, value) end
-- 是否定义全局变量
local function IsDefine( name ) return rawget(_G, name) ~= nil end

-- C# 调用方法
    -- Using("UnityEngine.KeyCode")
    -- print(KeyCode.Space)
local function Using( classname ,variant)
	local function Get( name )
		return load("return "..name)()
	end 
	if not variant then
		local a,b,c=string.find(classname,"[^%s]+%.([^%s]+)")
		if c then 
			variant =c
		else 
			variant=classname
		end
	end

   local _target = rawget(_G, variant)
   if _target == nil then
   	  _target =Get("CS."..classname)
   	  rawset(_G,variant,_target)
   end
   return _target
end



Using("UnityEngine")
Using("IFramework")
Using("IFramework.Game")

local updateEvent
local function BindToUpdate(object,method)
	updateEvent:Subscribe(object,method)
end
local function UnBindToUpdate(object,method)
	updateEvent:UnSubscribe(object,method)
end
local disposeEvent
local function BindToDispose(object,method)
	disposeEvent:Subscribe(object,method)
end
local function UnBindToDispose(object,method)
	disposeEvent:UnSubscribe(object,method)
end


function Awake()

	Define("Log",require("Base.Log"))
	Define("Class",require ("Base.Class"))

	Lock_G()

	Define("Using",Using)
	Define("IsDefine",IsDefine)
	Define("Define",Define)
	Define("BindToUpdate",BindToUpdate)
	Define("UnBindToUpdate",UnBindToUpdate)
	Define("BindToDispose",BindToDispose)
	Define("UnBindToDispose",UnBindToDispose)
	
	
	Define("MathUtil",require("Base.MathUtil"))
	Define("TableUtil",require("Base.TableUtil"))
	Define("Convert",require("Base.Convert"))
	Define("IOUtil",require("Base.IOUtil"))
	Define("StringUtil",require("Base.StringUtil"))
	Define("Util",require("Base.Util"))
	
	Define("Delegate",require("Base.Classes.Delegate"))
	Define("ObservableValue",require("Base.Classes.ObservableValue"))
	Define("ObservableObject",require("Base.Classes.ObservableObject"))
	


	updateEvent=Delegate()
	disposeEvent=Delegate()
	
	IFramework.Framework.BindEnvUpdate(Update,Game.env)
	require("Custom.FixCsharp")
	require("Custom.GameLogic")
end





function Update()

	Log.L(0,"Update")
	updateEvent:Invoke()
end

function OnDispose()
	Log.L(0,"OnDispose")
	updateEvent:Dispose()
	disposeEvent:Invoke()
	disposeEvent:Dispose()
	
	IFramework.Framework.UnBindEnvUpdate(Update,Game.env)
end

