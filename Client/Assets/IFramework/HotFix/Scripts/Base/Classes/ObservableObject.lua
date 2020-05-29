local ObservableObject = Class("ObservableObject")


---ctor 
---@param t 字段们
function ObservableObject:ctor( t )
	self.__actions={}
	local meta = setmetatable({__index=function ( table,key,value )
			if string.find(key, "^_") == 1 then
				return getmetatable(table)[key]
			else
				return rawget(table,"_attr_"..key) or getmetatable(table)[key]
			end	
	end
	,
	__newindex=function ( table,key,value )
		--print(key)

		if type(v)=="function" or string.find(key, "^_") == 1 then
				rawset(table,key,value)
			else
				rawset(table,"_attr_"..key,value)

				getmetatable(table).Invoke(table,key)
			end
	end
	},getmetatable(self))
	setmetatable(self,meta)

	if t and type(t)=="table" then			
		for k,v in pairs(t) do 
			self[k] = v end
	end
end

---Invoke 触发事件
---@param key 字段
function ObservableObject:Invoke( key )
	if not self.__actions then
		return
	end

	for _, v in pairs(self.__actions) do
		if key == nil or v.key == key then	
			v.action()
		end
	end
end





---Subscribe 注册监听
---@param key 字段
---@param func 监听方法
function ObservableObject:Subscribe( key,func )

	if key and func then
		table.insert(self.__actions,{key= key,action = func})
		func()
	end
end
---Subscribe 移除监听
---@param key 字段
---@param func 监听方法
function ObservableObject:UnSubscribe(key,func)
	if not self.__actions then
		return
	end
	
	for k,v in pairs(self.__actions) do
		if key == v.key and func == v.action then
			table.remove(self.__actions,k)
			return
		end
	end
end

---Dispose 释放
function ObservableObject:Dispose()
	
end

return ObservableObject