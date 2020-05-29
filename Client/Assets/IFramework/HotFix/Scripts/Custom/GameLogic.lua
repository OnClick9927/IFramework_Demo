Log.L(0,"Start Game Logic ")

local _group=require("UI.LuaGroups")()
local _uimodule=  Game.unityModules.UI

_uimodule:SetGroups(_group:SetMap(require("Custom.UI.UIMap_MVVM")))


Using("IFramework.Tweens.TweenEx")
--TweenEx.DoMove(Game.instance.transform,UnityEngine.Vector3.zero,10)
print(Game.instance:IsNull())
Game.instance.transform:DoMove(UnityEngine.Vector3.zero,10)