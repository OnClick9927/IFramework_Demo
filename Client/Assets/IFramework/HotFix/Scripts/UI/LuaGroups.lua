local LuaGroups = Class("LuaGroups")
local VVMGroup =require("UI.VVMGroup")
function LuaGroups:ctor()
	self.onDispose=function ()
		self:OnDispose()
	end
	self.onSubscribe=function ( panel )
		self:OnSubscribe(panel)
	end
	self.onUnSubscribe=function( panel )
		self:OnUnSubscribe(panel)
	end
	self.onFindPanel=function ( name )
		return self:OnFindPanel(name)
	end
	self.onInvokeListeners=function ( arg )
		self:OnInvokeListeners(arg)
	end
	self.groups={}
	self.FindGroup=function(name)
		return rawget(self.groups,name)
	end
end
--map: {name={ViewType=77,VMType=66},}
function LuaGroups:SetMap(map)
	if map == nil then
		error("map could not be null ")
		return
	end
	self.CS_instance=IFramework.Lua.LuaGroups()
	self.CS_instance:onDispose("+",self.onDispose)
	self.CS_instance:onSubscribe("+",self.onSubscribe)
	self.CS_instance:onUnSubscribe("+",self.onUnSubscribe)
	self.CS_instance:onFindPanel("+",self.onFindPanel)
	self.CS_instance:onInvokeListeners("+",self.onInvokeListeners)

	self.map=map
	return self.CS_instance
end

-- 以下方法私有

function LuaGroups:OnDispose()

	for i, group in pairs(self.groups) do
		group.view:OnClear()
		group:Dispose()
	end

	self.CS_instance:onDispose("-",self.onDispose)
	self.CS_instance:onSubscribe("-",self.onSubscribe)
	self.CS_instance:onUnSubscribe("-",self.onUnSubscribe)
	self.CS_instance:onFindPanel("-",self.onFindPanel)
	self.CS_instance:onInvokeListeners("-",self.onInvokeListeners)
	self.groups=nil
	self.CS_instance=nil
	self.map=nil
end

function LuaGroups:OnSubscribe(panel)
	local panelType=panel:GetType()
	local vvmType 
	for i, v in pairs(self.map) do
		if v.PanelType==panelType then
			vvmType=v
			break
		end
	end
	if(vvmType==nil) then
		error("not find vvm type with Type : "..panelType)
		return
	end
	if rawget(self.groups,panel.name) ~=nil then
		error("same name with panel  "..panel.name)
		return
	end

	local message=Delegate()
	local viewModel=vvmType.VMType(message)
	local view=vvmType.ViewType(message,viewModel,panel)
	local vvmGroup=VVMGroup(panel,view,viewModel,message)
	rawset(self.groups,panel.name,vvmGroup)
	view:OnLoad()
end
function LuaGroups:OnUnSubscribe(panel)

	local group= rawget(self.groups ,panel.name)
	if group ~=nil then
		group.view:OnClear()
		group:Dispose()
	end
end


function LuaGroups:OnFindPanel(name)
	local group=self.FindGroup(name)
	if group ~= nil then
		return group.panel
	end
	return nil
end
function LuaGroups:OnInvokeListeners(arg)

	if arg.pressPanel ~=nil then
		self.FindGroup(arg.pressPanel.name).view:OnPress(arg)
	end
	if arg.popPanel ~=nil then
		self.FindGroup(arg.popPanel.name).view:OnPop(arg)
	end
	if arg.curPanel  ~=nil then
		self.FindGroup(arg.curPanel .name).view:OnTop(arg)
	end
end



return LuaGroups
