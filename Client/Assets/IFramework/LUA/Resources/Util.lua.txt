
Util={}
-- 深度克隆一个值
-- @param mixed object 要克隆的值
-- @return mixed

-- 下面的代码，t2 是 t1 的引用，修改 t2 的属性时，t1 的内容也会发生变化
-- local t1 = {a = 1, b = 2}
-- local t2 = t1
-- t2.b = 3    -- t1 = {a = 1, b = 3} <-- t1.b 发生变化

-- clone() 返回 t1 的副本，修改 t2 不会影响 t1
-- local t1 = {a = 1, b = 2}
-- local t2 = clone(t1)
-- t2.b = 3    -- t1 = {a = 1, b = 2} <-- t1.b 不受影响

function Util.Clone(object)
    local lookup_table = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookup_table[object] then
            return lookup_table[object]
        end
        local new_table = {}
        lookup_table[object] = new_table
        for key, value in pairs(object) do
            new_table[_copy(key)] = _copy(value)
        end
        return setmetatable(new_table, getmetatable(object))
    end
    return _copy(object)
end

function Util.CheckTable(value)
    if type(value) ~= "table" then value = {} end
    return value
end

-- 如果表格中指定 key 的值为 nil，或者输入值不是表格，返回 false，否则返回 true
-- @param table hashtable 要检查的表格
-- @param mixed key 要检查的键名
-- @return boolean
function Util.ContainsKey(hashtable, key)
    local t = type(hashtable)
    return (t == "table" or t == "userdata") and hashtable[key] ~= nil
end

function Util.Handler(obj, method)
    return function(...)
        return method(obj, ...)
    end
end
function Util.PrintFormat(fmt, ...)
    print(string.format(tostring(fmt), ...))
end
return Util

